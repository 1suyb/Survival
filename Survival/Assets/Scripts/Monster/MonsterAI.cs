using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum AIState
{
    Idle,
    Track,
    Attack,
    Return,
    Death
}

public class MonsterAI : MonoBehaviour
{
    private Monster _monster;
    private void Awake()
    {
        _monster = GetComponent<Monster>();
    }

    [Header("AI")]
    private float _range;

    [Header("Idel")]

    [Header("Tracking")] 

    [Header("Attack")] 
    private float _attackInterval; 

    [Header("Return")]
    private Vector3 _wayPoint;

    [Header("Death")]
    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
