using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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

    [Header("Idle")]
    [SerializeField] private float _minWanderWaitTime = 10f;
    [SerializeField] private float _maxWanderWaitTime = 10f;

    [Header("Run")]
    [SerializeField] private float _detectDistance = 20f;

    [Header("Attack")]
    [SerializeField] private float _attackDistance = 5f;
    [SerializeField] private float _returnDistance = 400f;


    [SerializeField] private AIState aiState;
    [SerializeField] private MonsterController _monsterController;
    [SerializeField] private Animator _animator;

    private Monster _monster;

    private Coroutine _idleCoroutine;
	private WaitForSeconds _wait = new WaitForSeconds(0.5f);

	private void Awake()
    {
        _monsterController = GetComponent<MonsterController>();
        _monster = GetComponent<Monster>();
    }  
    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        SetState(AIState.Move);
        StartCoroutine(AIUpdate());
    }  

    private void IdleAnimation()
    {
		_animator.SetBool("isRunning", false);
		_animator.SetBool("isMoving", false);
		_animator.SetBool("isAttack", false);
	}

	private void MoveAnimation()
	{
		_animator.SetBool("isRunning", false);
		_animator.SetBool("isMoving", true);
		_animator.SetBool("isAttack", false);
	}
	private void RunAnimation()
	{
		_animator.SetBool("isRunning", false);
		_animator.SetBool("isMoving", true);
		_animator.SetBool("isAttack", false);
	}

    private void AttackAnimation()
    {
		_animator.SetBool("isRunning", false);
		_animator.SetBool("isMoving", false);
		_animator.SetBool("isAttack", true);
	}

	private IEnumerator AIUpdate()
    {
        while (true)
        {
			yield return _wait;
			_playerDistance = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
			_playerDistance = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);
			if (_monsterController.IsDie())
			{
				Debug.Log("A");
				SetState(AIState.Death);
			}
			else if ((Vector3.Distance(transform.position, _monster.SavedPosition()) > _returnDistance))
			{
				Debug.Log("B");
				SetState(AIState.Return);
			}
			else if (_playerDistance < _attackDistance)
			{
				Debug.Log("C");
				SetState(AIState.Attack);
			}
			else if (_playerDistance < _detectDistance)
			{
				Debug.Log("D");
				SetState(AIState.Run);
			}
			else if (_monsterController.HasReachedDestination())
			{
				Debug.Log("E");
				SetState(AIState.Idle);
			}
			else if (aiState != AIState.Move && _playerDistance > _detectDistance)
			{
				Debug.Log("F");
				SetState(AIState.Move);
			}
		}

	}

    private void SetState(AIState newState)
    {

			ExitState(aiState);
			aiState = newState;
			EnterState(aiState);

    }
    private void EnterState(AIState state) 
    {
        switch (state)
        {
            case AIState.Idle:
                Debug.Log("대기!");
				_idleCoroutine = StartCoroutine(IdleRoutine());
                _animator.SetBool("isMoving", false);
                break;
            case AIState.Move:
                Debug.Log("순찰!");
                _monsterController.Move();
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isMoving", true);
                _animator.SetBool("isAttack", false);
                break;
            case AIState.Run:
                Debug.Log("쫓아가자!");
                _animator.SetBool("isRunning", true);
                _animator.SetBool("isMoving", false);
                _animator.SetBool("isAttack", false);
                _monsterController.Run();
                if (_idleCoroutine != null)
                {
                    StopCoroutine(_idleCoroutine);
                    _idleCoroutine = null;
                }
                break; 
            case AIState.Attack: 
                Debug.Log("때리자!");
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isMoving", false);
				_animator.SetBool("isAttack", true);
				_monsterController.Attack();
				if (_idleCoroutine != null)
				{
					StopCoroutine(_idleCoroutine);
					_idleCoroutine = null;
				}
				break;
            case AIState.Return:
                Debug.Log("너무 멀리왔다!");
                _monsterController.Return();
                _animator.SetBool("isMoving", true);
                _animator.SetBool("isRunning", false);
                _animator.SetBool("isAttack", false);
                break;
            case AIState.Death:
                this.gameObject.SetActive(false);
                //Debug.Log("죽었다!");
                //_animator.SetBool("Die", true);
                break;
        }
    } // 상태에 따른 동작과 애니메이션 기능
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
        _idleCoroutine = null;
    } // 랜덤 시간 동안 대기 상태 
}


