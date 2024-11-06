using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MonsterData
{
    public int Id;
    public int type;
    public string Name;
    public int Health;
    public float Speed;
    public int AttackPower;
    public float AttackSpeed;
    public string PrefabPath;
    // 아이템 

    private GameObject _dropPrefab;
    public GameObject DropPrefab
    {
        get
        {
            if (_dropPrefab == null)
            {
                _dropPrefab = Resources.Load(PrefabPath) as GameObject;
                Debug.Log("프리펩을 찾았다!: " + PrefabPath); // 찾았습니다. 
            }

            return _dropPrefab;
        }
    }
}