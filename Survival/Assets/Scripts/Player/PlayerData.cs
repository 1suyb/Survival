using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : CharacterData
{
    private float _jumpPower = 8.0f;
    private float _attackDistance = 3.0f;

    public int Heath()
    {
        return _health;
    }

    public void ChangeMaxHeath(int heath)
    {
        _health = heath;
    }

    public int AttackPower()
    {
        return _attackpower;
    }

    public void ChangeAttackPower(int attackpower)
    {
        _attackpower = attackpower;
    }

    public float JumpPower()
    {
        return _jumpPower;
    }

    public void ChangeJumpPower(float jumpPower)
    {
        _jumpPower = jumpPower;
    }

    public float AttackDistance()
    {
        return _attackDistance;
    }

    public void ChangeAttackDistance(float attackDistance)
    {
        _attackDistance = attackDistance;
    }

    public float Speed()
    {
        return _speed;
    }

    public void ChangeSpeed(float speed)
    {
        _speed = speed;
    }

    public float Attackspeed()
    {
        return _attackspeed;
    }

    public void ChangeAttackspeed(int attackspeed)
    {
        _attackspeed = attackspeed;
    }

    public int Damage()
    {
        return _damage;
    }

    public void ChangeDamage(int damage)
    {
        _damage = damage;
    }
}
