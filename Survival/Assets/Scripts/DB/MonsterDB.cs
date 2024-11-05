using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class MonsterDB : Singleton<MonsterDB>
{
    private MonsterSheet _monsterSheet;
    private Dictionary<int, MonsterData> _monsters;

    public void Awake()
    {
        _monsters = new Dictionary<int, MonsterData>();
        _monsterSheet = Resources.Load<MonsterSheet>("DataSO/MonsterSheet");
        for (int i = 0; i < _monsterSheet.MonsterData.Count; i++)
        {
            _monsters.Add(_monsterSheet.MonsterData[i].Id, _monsterSheet.MonsterData[i]);
        }
    }
    public MonsterData Get(int id)
    {
        if (_monsters.TryGetValue(id, out var monster))
        {
            return monster;
        }
        return null;
    }
    public IEnumerator AllData()
    {
        return _monsterSheet.MonsterData.GetEnumerator();
    }
}
