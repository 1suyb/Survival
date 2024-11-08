﻿using System.Collections;
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
    public int Dropitem;
    // 아이템 

    private GameObject _dropPrefab;
    public GameObject DropPrefab
    {
        get
        {
            if (_dropPrefab == null)
            {
                _dropPrefab = Resources.Load(PrefabPath) as GameObject;
            }

            return _dropPrefab;
        }
    }
}