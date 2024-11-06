using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class Monster : CharacterData, ILoadable
{
    private Vector3 _spawnPosition;
    private MonsterData _data;
    public int Id { get; private set; }
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

    private void OnEnable()
    {
        Debug.Log("생성됨!");
        _spawnPosition = this.transform.position;
    }

    public Vector3 SavedPosition()
    { return _spawnPosition; }

    public void Load(int id)
    {
        Id = id;
        _data = MonsterDB.Instance.Get(id);

        if (_data != null)
        {
            type = _data.type;  
            Name = _data.Name;
            Health = _data.Health;
            Speed = _data.Speed;
            AttackPower = _data.AttackPower;
            AttackSpeed = _data.AttackSpeed;
            DropPrefab = _data.DropPrefab;

            if (_data.PrefabPath != null)
            {
                GameObject loadedPrefab = Resources.Load<GameObject>(_data.PrefabPath);
                if (loadedPrefab != null)
                {
                    GameObject instantiatedMonster = Instantiate(loadedPrefab, _spawnPosition, Quaternion.identity);
                    instantiatedMonster.transform.SetParent(this.transform.parent);
                }
            }
        }
    }
}
