using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Monster : CharacterData
{
    private Vector3 _spawnPosition;

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

    public GameObject DropPrefab;

    private void Awake() // 데이터 불러오기 
    {
        MonsterData monsterData = MonsterDB.Instance.Get(Id);

        if (monsterData != null)
        {
            Name = monsterData.Name;
            Health = monsterData.Health;
            Speed = monsterData.Speed;
            AttackPower = monsterData.AttackPower;
            AttackSpeed = monsterData.AttackSpeed;
            DropPrefab = monsterData.DropPrefab;
        }
        else
        {
            Debug.LogWarning($"Monster with ID {Id} not found in MonsterDB."); // 확인용
        }
    }

    private void OnEnable()
    {
        _spawnPosition = this.transform.position;
    }
}
