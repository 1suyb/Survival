using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO;
using UnityEngine.AI;
using System.Collections;

public class MonsterController : CharacterController
{
    public MonsterAI monsterAI;

    public int AttackPower;
    public float AttackSpeed;

    [Tooltip("Test")] // Player.Instance null 오류 
    public GameObject _TestTarget;

    [Header("Wander")]
    public float _minWanderDistance;
    public float _maxWanderDistance;
    public float _rotationSpeed = 5f;

    private NavMeshAgent agent;
    private NavMeshPath path;

    public float _fieldOfView = 120f;

    public float _attackDistance;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        monsterAI = GetComponent<MonsterAI>();
        path = new NavMeshPath();

        // 몬스터 data 값 받아오기 

    } // 초기화 

    public void Wander() // 목적지에 도달하면 
    {
        Vector3 newLocation = GetWanderLocation();
        agent.SetDestination(newLocation);
        agent.isStopped = false;
    }
    public bool HasReachedDestination() // 도착했는지 확인
    {
        return agent.remainingDistance < 0.1f;
    }

    Vector3 GetWanderLocation() // 새로운 위치 생성 
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(_minWanderDistance, _maxWanderDistance)),
                               out hit, _maxWanderDistance, NavMesh.AllAreas);

        return hit.position;
    }

    public override void Move() // 타겟 추적 
    {
        if (monsterAI.PlayerDistance > _attackDistance || !IsPlayerInFieldOfView())
        {
            agent.isStopped = false;
            /*   NavMeshPath path = new NavMeshPath();*/ // AWAKE 에서 한번 생성하고 더 이상 X 

            //if (agent.CalculatePath(PlayerManager.Instance.Player.transform.position, path))
            //{
            //    agent.SetDestination(PlayerManager.Instance.Player.transform.position);
            //}

            if (agent.CalculatePath(_TestTarget.transform.position, path))
            {
                agent.SetDestination(_TestTarget.transform.position);
            }
        }

        else
        {
            agent.isStopped = true;
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
        // 공격 코루틴을 시작하거나 재시작
        //if (attackCoroutine != null) StopCoroutine(attackCoroutine);
        //attackCoroutine = StartCoroutine(AttackRoutine());
    }

    private IEnumerator AttackRoutine()
    {
        //while (target != null)
        //{
        //    target.GetComponent<PlayerController>()?.TakeDamage(AttackPower); // 피해를 입는다 

            // 공격 후 대기
            yield return new WaitForSeconds(AttackSpeed);
        //}
    }

    //public void StopAttack() 공격 중지 
    //{
    //    if (attackCoroutine != null)
    //    {
    //        StopCoroutine(attackCoroutine);
    //        attackCoroutine = null;
    //    }
    //}

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

