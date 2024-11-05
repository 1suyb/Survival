using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : Singleton<SpawnManager>
{
	PoolingSystem _poolingSystem;

	public GameObject Spawn(string path, int id, Vector3 position)
	{
		GameObject go =ResourceManager.Instantiate(path);
		go.transform.position = position;
		if (go.TryGetComponent<ILoadable>(out ILoadable loadableObject))
		{
			loadableObject.Load(id);
		}
		return go;
	}

	public GameObject SpawnItem(int id,Vector3 position, int count = 1)
	{
		GameObject go =  Spawn("ItemObject", id,position);
		go.GetComponent<ItemObject>().SetCount(count);
		return go;
	}
}
