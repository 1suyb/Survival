using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using System.Collections;

public class MonsterController : CharacterController
{

    [Tooltip("테스트 타겟입니다.")] // Player.Instance 사용 시 null 오류 
    public GameObject _TestTarget;

    [Header("Move")]
    [SerializeField] private float _minWanderDistance; // 최소 거리
    [SerializeField] private float _maxWanderDistance; // 최대 거리 
    [SerializeField] private float _rotationSpeed = 3f; // 회전 속도

    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private NavMeshPath _path;

    [Header("Run")]
    [SerializeField] private float _fieldOfView = 120f; // 시야 각도 
    [SerializeField] private int _attackDistance = 20; // 공격 감지 거리값 

    [Header("Attack")]
    [SerializeField] private Coroutine _attackCoroutine;

    public MonsterAI _monsterAI;
    private Monster _monster;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _monsterAI = GetComponent<MonsterAI>();
        _monster = GetComponent<Monster>();
        _path = new NavMeshPath();

        // 몬스터 data 값 받아오기 

    } // 초기화 
    public override void Move()  // 목적지에 도달하면 
    {
        Vector3 newLocation = GetWanderLocation();
        _agent.SetDestination(newLocation);
        _agent.isStopped = false;
    }
    public bool HasReachedDestination() // 도착했는지 확인
    {
        return _agent.remainingDistance < 0.1f;
    }
    Vector3 GetWanderLocation() // 새로운 위치 생성 
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(_minWanderDistance, _maxWanderDistance)),
                               out hit, _maxWanderDistance, NavMesh.AllAreas);

        return hit.position;
    }
    public void Run() // 타겟 추적 
    {
        if (_monsterAI.PlayerDistance > _attackDistance || !IsPlayerInFieldOfView())
        {
            _agent.isStopped = false;

            //if (agent.CalculatePath(PlayerManager.Instance.Player.transform.position, path))
            //{
            //    agent.SetDestination(PlayerManager.Instance.Player.transform.position);
            //}

            if (_agent.CalculatePath(_TestTarget.transform.position, _path))
            {
                _agent.SetDestination(_TestTarget.transform.position);
            }
        }

        else
        {
            _agent.isStopped = true;
        }
    }
    bool IsPlayerInFieldOfView() // 시야가 있는지 
    {
        Vector3 directionToPlayer = _TestTarget.transform.position - transform.position;
        float angle = Vector3.Angle(transform.forward, directionToPlayer);
        return angle < _fieldOfView * 0.5f;
    }
    public override void Attack()
    {
        if (_attackCoroutine != null) StopCoroutine(_attackCoroutine);
        _attackCoroutine = StartCoroutine(AttackRoutine());
    }
    private IEnumerator AttackRoutine()
    {
        var playerController = PlayerManager.Instance.Player.GetComponent<PlayerController>();
        if (playerController != null)
        {
            playerController.TakeDamage(); // 인수 필요: AttackDamage 
            yield return new WaitForSeconds(_monster.AttackSpeed); // ()안에 공격 스피드 넣기
        }
    }
    public void StopAttack()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
            _attackCoroutine = null;
        }
    }
    public void Return()
    {
        // 원래 자리로 돌아가다
    }
    public override void Die()
    {
        // 오브젝트 파괴
    }
    public override void TakeDamage()
    {
        // 체력 - 데미지 
    }
    public override void Look()
    {
        // 타겟 방향으로 회전 
    }
}

