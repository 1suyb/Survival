using System.Collections.Generic;
using UnityEngine;

public interface ILoadable
{
	public void Load(int id);
}

public class PoolingSystem
{
	private GameObject _targetObject;
	private Transform _poolManagerTransform;
	private Stack<GameObject> _pool;
	private readonly int MAXSIZE;
	private readonly int MINSIZE;
	private int _poolSize;
	private int _id;
	public Stack<GameObject> Pool
	{
		get
		{
			return _pool;
		}
	}
	public PoolingSystem(GameObject _targetObject, int minSize=0, int maxSize = 100, int id = 0, Transform poolManagerTransform = null)
	{
		_pool = new Stack<GameObject>();
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

	private GameObject CreatePooledItem()
	{
		GameObject go = ResourceManager.Instantiate(_targetObject,_poolManagerTransform);
		Poolable poolable = go.AddUniqueComponent<Poolable>();
		poolable.Init(this, _id);
		go.SetActive(false);
		Push(go);
		return go;
	}

	public GameObject TakeFromPool()
	{
		if(_pool.Count == 0)
		{
			CreatePooledItem();
		}
		GameObject item = _pool.Pop();
		item.SetActive(true);
		return item;
	}

	public void Release(GameObject item)
	{
		if (_poolSize == MAXSIZE)
		{
			GameObject.Destroy(item.gameObject);
			return;
		}
		Push(item);
	}

	private void Push(GameObject item)
	{
		_poolSize++;
		_pool.Push(item);
	}
}
