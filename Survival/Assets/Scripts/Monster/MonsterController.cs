using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using System.Collections;
using System;
using Random = UnityEngine.Random;


public class MonsterController : CharacterController, IDamagable
{
    [Header("Move")]
    [SerializeField] private float _minWanderDistance = 10f; // 최소 거리
    [SerializeField] private float _maxWanderDistance = 10f; // 최대 거리 
    [SerializeField] private float _rotationSpeed = 3f; // 회전 속도

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshPath _path;

    [Header("Run")]
    [SerializeField] private float _fieldOfView = 120f; // 시야 각도 
    [SerializeField] private int _attackDistance = 20; // 공격 감지 거리값 

    [Header("Attack")]
    [SerializeField] private Coroutine _attackCoroutine;
    [SerializeField] private bool _isDamageTaken = false;

    [SerializeField] private bool _isDie = false;

    public MonsterAI _monsterAI;
    private Monster _monster;
    private Animator _animator;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _monsterAI = GetComponent<MonsterAI>();
        _monster = GetComponent<Monster>();
		_animator = GetComponentInChildren<Animator>();  
		_path = new NavMeshPath();
    } // 초기화 
    public override void Move()  // 목적지에 도달하면 
    {
        Vector3 newLocation = GetWanderLocation();
        _agent.SetDestination(newLocation);
        _agent.isStopped = false;
    }
    public bool HasReachedDestination() // 도착했는지 확인
    {
        return _agent.remainingDistance < 0.2f;
    }
    Vector3 GetWanderLocation() // 새로운 위치 생성 
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (UnityEngine.Random.onUnitSphere * UnityEngine.Random.Range(_minWanderDistance, _maxWanderDistance)),
                               out hit, _maxWanderDistance, NavMesh.AllAreas);

        return hit.position;
    }
    public void Run() // 타겟 추적 
    {
        // 플레이어와의 거리값이 20보다 작고 시야각에 보일 때
        if (_monsterAI.PlayerDistance < _attackDistance || !IsPlayerInFieldOfView())
        {
            _agent.isStopped = false;

            // 플레이어 위치로 이동 
            if (_agent.CalculatePath(PlayerManager.Instance.Player.transform.position, _path))
            {
                _agent.SetDestination(PlayerManager.Instance.Player.transform.position);
            }
        }

        else
        {
            _agent.isStopped = true;
        }
    }
    bool IsPlayerInFieldOfView() // 시야가 있는지 
    {
        Vector3 directionToPlayer = PlayerManager.Instance.Player.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < _fieldOfView * 0.5f;
    }
    public override void Attack()
    {
        if (_attackCoroutine == null)
        {
            _attackCoroutine = StartCoroutine(AttackRoutine());
        }
    } // 공격
    private IEnumerator AttackRoutine() // 공격 시작 
    {
        var playerCondition = PlayerManager.Instance.Player.GetComponent<IDamagable>();

        if (playerCondition != null)
            playerCondition.TakeDamage(_monster.AttackPower);
        yield return new WaitForSeconds(_monster.AttackSpeed);

        _attackCoroutine = null;
    }
    public void Return()
    {
        Vector3 returnposition = _monster.SavedPosition();
        _agent.SetDestination(returnposition);
    } // 돌아가기 
    public override void Die()
    {
        int randomValue = Random.Range(1, 6);
        SpawnManager.Instance.SpawnItem(_monster.Dropitem, this.transform.position, randomValue); // 아이템을 드롭
        gameObject.SetActive(false);
    } // 사망  
    public bool IsDie()
    {
        if (_monster.Health <= 0)
        {
            _isDie = true;
        }
        return _isDie;
    }
    public override void Look() // 미사용
    {

    }
    public void ApplyPlayerDamage() // 데미지 적용 
    {
        int playerAttackPower = PlayerManager.Instance.Player.GetComponent<PlayerData>().AttackPower();
        TakeDamage(playerAttackPower);
    }
    public void TakeDamage(int damage) // 피격 시 // 플레이어가 몬스터를 공격하면 호출됩니다 
    {
        _isDamageTaken = true;
        _monster.Health -= damage;
        _animator.SetTrigger("Damage");


		if (IsDie())
        {
            Die();
        }
    }

    public bool IsDamageTaken() // 피격 상태 체크
    {
        return _isDamageTaken;
    }
}

