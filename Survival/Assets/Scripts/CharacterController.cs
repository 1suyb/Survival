using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _speed;

    [SerializeField] private int _attackpower;
    [SerializeField] private float _attackspeed;
    [SerializeField] private int _damage;

    public abstract void Move();
    public abstract void Look();
    public abstract void Attack();
    public abstract void TakeDamage();
    public abstract void Die();
}
