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
    private AIState aiState;
    private float playerDistance;

    private MonsterController monsterController;
    private Animator animator;

    private Monster monsterData;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        monsterController = GetComponent<MonsterController>();
    }

    private void Start()
    {
        SetState(AIState.Wandering);
    }
    private void Update()
    {
        playerDistance = Vector3.Distance(transform.position, PlayerManager.Instance.Player.transform.position);

        animator.SetBool("isMoving", aiState != AIState.Idle);

        switch (aiState)
        {
            case AIState.Wandering:
                monsterController.Wander();
                //animator.SetBool("isMoving", true);
                break;
            case AIState.Moving:
                monsterController.Move();
                break;
                //case AIState.Attacking:
                //    monsterController.Attack();
                //    break;
                //case AIState.Returning:
                //    monsterController.();
                //    break;
                //case AIState.Death:
                //    monsterController.Die();
                //    break;
        }
    }

    private void SetState(AIState state)
    {
        aiState = state;
    }
}