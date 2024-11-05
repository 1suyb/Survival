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
    } 

    [SerializeField] private float _detectDistance = 20;

    [Header("Idle")]
    [SerializeField] private float _minWanderWaitTime = 10;
    [SerializeField] private float _maxWanderWaitTime = 10;

    [SerializeField] private AIState aiState;
    [SerializeField] private MonsterController _monsterController;
    [SerializeField] private Animator _animator;

    [SerializeField] private int _attackDistance = 3;
    [SerializeField] private int _returnDistance = 40;

    private Monster _monster;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _monsterController = GetComponent<MonsterController>();
        _monster = GetComponent<Monster>();
    }  
    private void Start()
    {
        SetState(AIState.Move);
    }  
    private void Update() // 상태 전환 
    {
        // 매 프레임마다 거리 체크 
        _playerDistance = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);

        // 리스폰 구역에서 너무 벗어나면 
        //if ((Vector3.Distance(transform.position, _monster.SavedPosition()) > _returnDistance))
        //{
        //    SetState(AIState.Return);
        //}

        // 공격 거리 안이라면 공격
        if (_playerDistance < _attackDistance)
        {
            SetState(AIState.Attack);
        }

        // 공격 거리 밖이고 추적 거리 안이라면 추적
        else if (_playerDistance > _attackDistance && _playerDistance < _detectDistance)
        {
            SetState(AIState.Run);
        }

        // 목적지에 도착했고 Move 상태라면 대기 
        else if (aiState == AIState.Move && _monsterController.HasReachedDestination())
        {
            SetState(AIState.Idle);
        }

        // 추적 거리 밖이라면 순찰 
        else if (_playerDistance > _detectDistance)
        {
            SetState(AIState.Move);
        }

        //// 목적지에 도착했고 Move 상태라면 대기 
        //else if (aiState == AIState.Move && _monsterController.HasReachedDestination())
        //{
        //    SetState(AIState.Idle);
        //}

        //// 그 외 Move 상태로
        //else if (_playerDistance >= _detectDistance)
        //{
        //    SetState(AIState.Move);
        //}
    } 
    private void SetState(AIState newState)
    {
        ExitState(aiState);
        aiState = newState;
        EnterState(aiState);
    }
    private void EnterState(AIState state) // ���¿� �ʿ��� ���
    {
        switch (state)
        {
            case AIState.Idle:
                Debug.Log("대기!");
                StartCoroutine(IdleRoutine());
                _animator.SetBool("isMoving", false);
                break;
            case AIState.Move:
                Debug.Log("순찰!");
                _monsterController.Move();
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isMoving", true);
                break;
            case AIState.Run:
                Debug.Log("쫓아가자!");
                _animator.SetBool("isRunning", true);
                _monsterController.Run();
                break;
            case AIState.Attack:
                Debug.Log("때리자!");
                if (!_animator.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
                {
                    _animator.SetTrigger("Attack");
                }
                _monsterController.Attack();
                _animator.SetBool("isRunning", false);
                break;
            case AIState.Return:
                Debug.Log("너무 멀리왔다!");
                _monsterController.Attack();
                _animator.SetBool("isMoving", true);
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


