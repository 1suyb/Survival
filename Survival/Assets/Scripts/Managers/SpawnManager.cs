using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
	private Transform _poolingObjectTransform;
	private Dictionary<string,Dictionary<int,PoolingSystem>> _pools;
	private void Awake()
	{
		_pools=new Dictionary<string, Dictionary<int, PoolingSystem>>();
	}

	private PoolingSystem FindPool(string path, int id = 0)
	{
		if (_pools.ContainsKey(path))
		{
			if (_pools[path].ContainsKey(id))
			{
				return _pools[path][id];
			}
		}
		return null;
	}

	private PoolingSystem CreatePool(string path, int id = 0 , int maxSize = 10)
	{
		GameObject targetObject = ResourceManager.Load(path);
		_pools.Add(path, new Dictionary<int, PoolingSystem>());
		_pools[path].Add(id, new PoolingSystem(targetObject, maxSize: maxSize, id: id));
		return _pools[path][id];
	}

	public GameObject Spawn(string path, int id, Vector3 position, bool isPooling = true)
	{
		GameObject go;
		if (isPooling)
		{
			PoolingSystem pool = FindPool(path, id);
			if(pool == null)
			{
				pool = CreatePool(path, id);
			}
			go = pool.TakeFromPool();
		}
		else
		{
			go = ResourceManager.Instantiate(path);
			if (go.TryGetComponent<ILoadable>(out ILoadable loadableObject))
			{
				loadableObject.Load(id);
			}
		}
		go.transform.position = position;
		return go;
	}

	public GameObject SpawnItem(int id,Vector3 position, int count = 1)
	{
		GameObject go =  Spawn("ItemObject", id, position);
		go.GetComponent<ItemObject>().SetCount(count);
		return go;
	}
}
