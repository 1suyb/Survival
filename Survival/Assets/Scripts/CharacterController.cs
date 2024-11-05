using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterData : MonoBehaviour
{
    [SerializeField] protected int _health;
    [SerializeField] protected float _speed;
    [SerializeField] protected int _attackpower;
    [SerializeField] protected float _attackspeed;
    [SerializeField] protected int _damage;
}

public abstract class CharacterController : MonoBehaviour
{ 
    public abstract void Move();
    public abstract void Look();
    public abstract void Attack();
    public abstract void TakeDamage(int Damage);
    public abstract void Die();
}

