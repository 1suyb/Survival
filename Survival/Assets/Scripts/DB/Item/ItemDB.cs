using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDB : Singleton<ItemDB>
{
	private ItemSheet _itemSheet;
	private Dictionary<int,ItemData> _items;

	public ItemDB()
	{
		_itemSheet = Resources.Load<ItemSheet>("DataSO/ItemSheet");
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
