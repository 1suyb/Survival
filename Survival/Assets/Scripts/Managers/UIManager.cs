using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Resources;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
	private Dictionary<string, GameObject> _cache = new Dictionary<string, GameObject>();

	public T OpenUI<T>()
	{
		string path = Utils.GetPath<T>();

		if (IsUIExist<T>())
		{
			return _cache[path].GetComponent<T>();
		}
		return CreateUI<T>();
	}
	public T CreateUI<T>(Transform parent = null)
	{
		string path = Utils.GetPath<T>();
		if (IsUIExist<T>())
		{
			RemoveUI<T>();
		}
		GameObject go = ResourceManager.Instantiate(path);
		AddUI<T>(go);
		return go.GetComponent<T>();
	}
	public void CloseUI<T>() where T : UI
	{
		string path = Utils.GetPath<T>();
		if (IsUIExist<T>())
		{
			_cache[path].GetComponent<T>().Close();
		}
	}
	public void DestroyUI<T>()
	{
		string path = Utils.GetPath<T>();
		if (IsUIExist<T>())
		{
			Destroy(_cache[path]);
		}
	}
	public void AddUI<T>(GameObject go)
	{
		_cache.Add(Utils.GetPath<T>(), go);
	}
	public void RemoveUI<T>()
	{
		_cache.Remove(Utils.GetPath<T>());
	}
	public bool IsUIExist<T>()
	{
		return _cache.TryGetValue(Utils.GetPath<T>(), out GameObject uiGameObject);
	}
}
