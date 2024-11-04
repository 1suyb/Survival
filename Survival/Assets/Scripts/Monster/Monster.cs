using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Monster : CharacterData
{
    public int Id;
    public string Name;

    public int Health
    {
        get { return _health; }
        set { _health = Mathf.Max(0, value); } 
    }

    public float Speed
    {
        get { return _speed; }
        set { _speed = Mathf.Max(0, value); } 
    }

    public int AttackPower
    {
        get { return _attackpower; }
        set { _attackpower = Mathf.Max(0, value); } 
    }

    public float AttackSpeed
    {
        get { return _attackspeed; }
        set { _attackspeed = Mathf.Max(0, value); } 
    }

    public int Damage
    {
        get { return _damage; }
        set { _damage = Mathf.Max(0, value); } 
    }
}
