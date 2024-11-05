using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResouceDB : Singleton<ResouceDB>
{
	private ResourceSheet _resouceSheet;
	private Dictionary<int, ResouceData> _resouceDatas;
	private void Awake()
	{
		_resouceSheet = Resources.Load<ResourceSheet>("DataSO/ResouceSheet");
		_resouceDatas = new Dictionary<int, ResouceData>();

		for (int i = 0; i < _resouceSheet.ResourceData.Count; i++)
		{
			_resouceDatas.Add(_resouceSheet.ResourceData[i].ID, _resouceSheet.ResourceData[i]);
		}
	}

	public ResouceData Get(int id)
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
