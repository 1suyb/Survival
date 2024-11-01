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
		_pool.Release(this.GetComponent<T>());
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
	public Stack<T> Pool
	{
		get
		{
			return _pool;
		}
	}
	public PoolingSystem(int minSize, int maxSize, int id = 0, Transform poolManagerTransform = null)
	{
		_pool = new Stack<T>();
		_poolManagerTransform = poolManagerTransform;
		MAXSIZE = maxSize;
		MINSIZE = minSize;
		_id = id;
		for(int i = 0; i < MINSIZE; i++)
		{
			_poolSize++;
			CreatePooledItem();
		}
	}

	private T CreatePooledItem()
	{
		GameObject go = ResourceManager.Instantiate<T>(_poolManagerTransform);
		Poolable<T> poolable = go.AddUniqueComponent<Poolable<T>>();
		poolable.Init(this, _id);
		go.SetActive(false);
		T item = go.GetComponent<T>();
		Push(item);
		return item;
	}

	public T TakeFromPool()
	{
		if(_pool.Count == 0)
		{
			CreatePooledItem();
		}
		T item = _pool.Pop();
		item.gameObject.SetActive(true);
		return item;
	}

	public void Release(T item)
	{
		if (_poolSize == MAXSIZE)
		{
			GameObject.Destroy(item.gameObject);
			return;
		}
		Push(item);
	}

	private void Push(T item)
	{
		_poolSize++;
		_pool.Push(item);
	}
}
