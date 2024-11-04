using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum AIState
{
    Idle,
    Wandering,
    Moving,
    Attacking,
    Returning,
    Death
}

public class MonsterAI : MonoBehaviour
{
    private float playerDistance; 
    public float PlayerDistance 
    {
        get { return playerDistance; } 
    }

    private float detectDistance = 20;
    public float _minWanderWaitTime;
    public float _maxWanderWaitTime;

    private AIState aiState;
    private MonsterController monsterController;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        monsterController = GetComponent<MonsterController>();
    } // 초기화 
    private void Start()
    {
        SetState(AIState.Wandering);
    } // Wandering 상태 시작 
    private void Update()
    {
        // 전체 다 코루틴으로 돌리는 게 효율적 (프레임 낭비)
        playerDistance = Vector3.Distance(transform.position, monsterController._TestTarget.transform.position);

        if (aiState != AIState.Attacking && playerDistance < 3)
        {
            SetState(AIState.Attacking);
        }
        else if (aiState == AIState.Attacking && playerDistance >= 3)
        {
            SetState(AIState.Wandering);
        }
        else if (aiState == AIState.Wandering && monsterController.HasReachedDestination())
        {
            SetState(AIState.Idle);
        }
        else if (playerDistance < detectDistance && aiState == AIState.Wandering)
        {
            SetState(AIState.Moving);
        }
        else if (playerDistance >= detectDistance && aiState == AIState.Moving)
        {
            SetState(AIState.Wandering);
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
                animator.SetBool("isMoving", false);
                break;
            case AIState.Wandering:
                monsterController.Wander();
                animator.SetBool("isRunning", false);
                animator.SetBool("isMoving", true);
                break;
            case AIState.Moving:
                animator.SetBool("isRunning", true);
                monsterController.Move();
                break;
            case AIState.Attacking:
                if (!animator.GetCurrentAnimatorStateInfo(0).IsName("Attack")) // 끝까지 재생시키기 
                {
                    animator.SetTrigger("Attack");
                }
                monsterController.Attack();
                animator.SetBool("isRunning", false);
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
        SetState(AIState.Wandering);
    }
    }


