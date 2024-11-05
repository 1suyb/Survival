using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum AIState
{
    Idle,
    Move,
    Run,
    Attack,
    Return,
    Death
}

public class MonsterAI : MonoBehaviour
{
    [Header("AI")]
    private float _playerDistance; 
    public float PlayerDistance 
    {
        get { return _playerDistance; } 
    } // 값만 가져와서 사용

    [SerializeField] private float _detectDistance = 20;

    [Header("Idle")]
    [SerializeField] private float _minWanderWaitTime;
    [SerializeField] private float _maxWanderWaitTime;

    [SerializeField] private AIState aiState;
    [SerializeField] private MonsterController _monsterController;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _monsterController = GetComponent<MonsterController>();
    } // 초기화 
    private void Start()
    {
        SetState(AIState.Move);
    } // Wandering 상태 시작 
    private void Update()
    {
        // 전체 다 코루틴으로 돌리는 게 효율적 (프레임 낭비)
        _playerDistance = Vector3.Distance(transform.position, _monsterController._TestTarget.transform.position);

        if (aiState != AIState.Attack && _playerDistance < 5)
        {
            SetState(AIState.Attack);
        }
        else if (aiState == AIState.Attack && _playerDistance >= 5)
        {
            SetState(AIState.Move);
        }
        else if (aiState == AIState.Move && _monsterController.HasReachedDestination())
        {
            SetState(AIState.Idle);
        }
        else if (_playerDistance < _detectDistance && aiState == AIState.Move)
        {
            SetState(AIState.Run);
        }
        else if (_playerDistance >= _detectDistance && aiState == AIState.Run)
        {
            SetState(AIState.Move);
        }
    } // 특정 조건에 따른 상태 전환
    private void SetState(AIState newState)
    {
        ExitState(aiState);
        aiState = newState;
        EnterState(aiState);
    } 
    private void EnterState(AIState state) // 상태에 필요한 기능
    {
        switch (state)
        {
            case AIState.Idle:
                StartCoroutine(IdleRoutine());
                _animator.SetBool("isMoving", false);
                break;
            case AIState.Move:
                _monsterController.Move();
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isMoving", true);
                break;
            case AIState.Run:
                _animator.SetBool("isRunning", true);
                _monsterController.Run();
                break;
            case AIState.Attack:
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // 끝까지 재생시키기 
                {
                    _animator.SetTrigger("Attack");
                }
                _monsterController.Attack();
                _animator.SetBool("isRunning", false);
                break;
        }
    }
    private void ExitState(AIState state)
    {
        switch (state)
        {
            case AIState.Idle:
                StopCoroutine(IdleRoutine());
                break;
        }
    }
    private IEnumerator IdleRoutine()
    {
        yield return new WaitForSeconds(Random.Range(_minWanderWaitTime, _maxWanderWaitTime));
        SetState(AIState.Move);
    }
    }


