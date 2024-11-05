using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Monster : CharacterData
{

    // 여기서 몬스터DB 값 저장하기 

    private Vector3 _spawnPosition;

    private void OnEnable()
    {
        _spawnPosition = this.transform.position;
    }

    public int Id;
    public int type;
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
