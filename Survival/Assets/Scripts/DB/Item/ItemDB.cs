using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class ItemDB : Singleton<ItemDB>
{
	private ItemSheet _itemSheet;
	private Dictionary<int,ItemData> _items;

	public void Awake()
	{
		_items = new Dictionary<int,ItemData>();
		_itemSheet = Resources.Load<ItemSheet>("DataSO/ItemSheet");
		for(int i = 0; i < _itemSheet.ItemData.Count; i++)
		{
			_items.Add(_itemSheet.ItemData[i].ID, _itemSheet.ItemData[i]);
		}
	}
	public ItemData Get(int id)
	{
		if (_items.TryGetValue(id, out var item))
		{
			return item;
		}
		return null;
	}
	public IEnumerator AllData()
	{
		return _itemSheet.ItemData.GetEnumerator();
	}

}
