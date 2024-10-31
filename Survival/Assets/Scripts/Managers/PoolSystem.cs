using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ILoadable
{
	public void Load(int id);
}

public class Poolable<T> : MonoBehaviour where T : MonoBehaviour
{
	private PoolingSystem<T> _pool;
	public void Init(PoolingSystem<T> pool, int id = 0)
	{
		_pool = pool;
		if(gameObject.TryGetComponent<ILoadable>(out ILoadable lodable))
		{
			lodable.Load(id);
		}
	}
	private void OnDisable()
	{
		_pool.Release(this);
	}
}


public class PoolingSystem<T> where T : MonoBehaviour
{
	private Transform _poolManagerTransform;
	private Stack<T> _pool;
	private readonly int MAXSIZE;
	private readonly int MINSIZE;
	private int _poolSize;
	private int _id;

	public PoolingSystem(int minSize, int maxSize, int id = 0)
	{
		MAXSIZE = maxSize;
		MINSIZE = minSize;
		_id = id;
		for(int i = 0; i < MINSIZE; i++)
		{
			_poolSize++;
			CreatePooledItem();
		}
	}

	public Stack<T> Pool
	{
		get
		{
			return _pool;
		}
	}

	private T CreatePooledItem()
	{
		GameObject go = ResourceManager.Instantiate<T>(_poolManagerTransform);
		Poolable<T> poolable = go.AddComponent<Poolable<T>>();
		poolable.Init(this, _id);
		go.SetActive(false);
		T item = go.GetComponent<T>();
		return item;
	}

	public T TakeFromPool()
	{
		T item = _pool.Pop();
		item.gameObject.SetActive(true);
		return item;
	}

	public void Release(Poolable<T> item)
	{
		_pool.Push(item.GetComponent<T>());
	}

}
