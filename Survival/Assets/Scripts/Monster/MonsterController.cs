using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using System.IO;
using UnityEngine.AI;

public class MonsterController : CharacterController
{
    public List<Monster> monsters = new List<Monster>();
    public MonsterAI monsterAI;

    [Header("Wandering")]
    public float minWanderDistance;
    public float maxWanderDistance;
    public float minWanderWaitTime;
    public float maxWanderWaitTime;

    public Transform player;
    public float rotationSpeed = 5f;

    private NavMeshAgent agent;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        monsterAI = GetComponent<MonsterAI>();

        //LoadMonsterData("Assets/MonsterData.csv");

        //if (monsters.Count > 0)
        //{
        //    monsterAI.SetMonsterData(monsters[0]);
        //}
    }

    //void LoadMonsterData(string filePath)
    //{
    //    var lines = File.ReadAllLines(filePath);

    //    for (int i = 1; i < lines.Length; i++)
    //    {
    //        string[] values = lines[i].Split(',');

    //        Monster monster = new Monster
    //        {
    //            Id = int.Parse(values[0]),
    //            Name = values[1],
    //            Health = int.Parse(values[2]),
    //            Speed = float.Parse(values[3]),
    //            AttackPower = int.Parse(values[4]),
    //            AttackSpeed = float.Parse(values[5]),
    //        };

    //        monsters.Add(monster);
    //    }
    //}

    public void Wander()
    {
        //if (aiState == AIState.Wandering && agent.remainingDistance < 0.1f)
        //{
        //    SetState(AIState.Idle);
        if (agent.remainingDistance < 0.1f)
        {
            Invoke("WanderToNewLocation", Random.Range(minWanderWaitTime, maxWanderWaitTime));
        }
        //}

        //if (playerDistance < detectDistance)
        //{
        //    SetState(AIState.Attacking);
        //}
    }
    void WanderToNewLocation()
    {
        //if (aiState != AIState.Idle)
        //{
        //    return;
        //}
        //SetState(AIState.Wandering);
        agent.SetDestination(GetWanderLocation());
    }

    Vector3 GetWanderLocation()
    {
        NavMeshHit hit;
        NavMesh.SamplePosition(transform.position + (Random.onUnitSphere * Random.Range(minWanderDistance, maxWanderDistance)),
                               out hit, maxWanderDistance, NavMesh.AllAreas);

        return hit.position;
    }

    public override void Look()
    {
        // 타겟 방향으로 회전 
    }

    public override void Move() // 
    {
        // 타겟에게 이동
    }

    public override void Attack()
    {
        // 공격력 참조 
        // 랜덤으로 공격, 치명타 발동
    }

    public override void Die()
    {
        // 오브젝트 파괴
        // 아이템 드롭 함수 실행 
    }

    public override void TakeDamage()
    {

    }
}

