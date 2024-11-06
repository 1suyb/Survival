using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceDB : Singleton<ResourceDB>
{
	private ResourceSheet _resouceSheet;
	private Dictionary<int, ResourceData> _resouceDatas;
	private void Awake()
	{
		_resouceSheet = Resources.Load<ResourceSheet>("DataSO/ResourceSheet");
		_resouceDatas = new Dictionary<int, ResourceData>();

		for (int i = 0; i < _resouceSheet.ResourceData.Count; i++)
		{
			_resouceDatas.Add(_resouceSheet.ResourceData[i].ID, _resouceSheet.ResourceData[i]);
		}
	}

	public ResourceData Get(int id)
	{
		if (_resouceDatas.TryGetValue(id, out var resouceData))
		{
			return resouceData;
		}
		return null;
	}
	public IEnumerator AllData()
	{
		return _resouceSheet.ResourceData.GetEnumerator();
	}

}
