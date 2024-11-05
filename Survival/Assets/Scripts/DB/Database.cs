using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IItemDB<T>
{
    void ItemDB();
    T Get(int id);

}

public class Database : Singleton<Database>
{
    private Dictionary<Type, object> strategies = new Dictionary<Type, object>();

    public void SetItemStrategy<T>(IItemDB<T> strategy)
    {
        strategies[typeof(T)] = strategy;
        strategy.ItemDB();
    }

    public T GetItem<T>(int id)
    {
        if (strategies.TryGetValue(typeof(T), out object strategy))
        {
            return ((IItemDB<T>)strategy).Get(id);
        }
        return default;
    }


}
