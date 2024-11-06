using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildInventory
{
	private ClassPoolSystem<BuildInventoryItem> pool = new ClassPoolSystem<BuildInventoryItem>(minSize:GameConfig.BUILDINVENTORYSIZE); // 이것도... 빌드인벤토리에선 필요없어용...
	private BuildInventoryItem[] _inventoryItems = new BuildInventoryItem[GameConfig.BUILDINVENTORYSIZE];
	public BuildInventoryItem[] InventoryItems => _inventoryItems;

	public int Size => GameConfig.BUILDINVENTORYSIZE;
	public bool IsFull => IndexOfEmptySlot() == -1;


	public BuildInventory()
	{
		BuildItemDB.Instance.ItemDB();
		var enumerator = BuildItemDB.Instance.DbEnumerator();

        if (enumerator != null)
        {
			int index = 0;
			foreach(var item in enumerator.Values)
			{
				_inventoryItems[index++] = new BuildInventoryItem(item, 1);
			}
        }

    }

	public bool HasThisItem(BuildInventoryItem item)
	{
		return HasThisItem(item.Data.Id);
	}
	public bool HasThisItem(BuildItemData targetItem)
	{
		return HasThisItem(targetItem.Id);
	}
	public bool HasThisItem(int targetItemID)
	{
		foreach (BuildInventoryItem item in _inventoryItems)
		{
			if(item != null)
			{
				if (targetItemID == item.Data.Id)
				{
					return true;
				}
			}
		}
		return false;
	}

	public int IndexOf(BuildInventoryItem item)
	{
		return IndexOf(item.Data.Id);
	}
	public int IndexOf(BuildItemData targetItem)
	{
		return IndexOf(targetItem.Id);
	}
	public int IndexOf(int id)
	{
		for (int i = 0; i < _inventoryItems.Length; i++)
		{
			if (!IsNull(i))
			{
				if (id == _inventoryItems[i].Data.Id)
				{
					return i;
				}
			}

		}
		return -1;
	}

	public List<int> Indices(int id)
	{
		List<int> indices = new List<int>();
		for (int i = 0; i< _inventoryItems.Length; i++)
		{
			if (!IsNull(i))
			{
				if (id == _inventoryItems[i].Data.Id)
				{
					indices.Add(i);
				}
			}
		}
		return indices;
	}

	public BuildInventoryItem At(int i)
	{
		return _inventoryItems[i];
	}
	public int IndexOfEmptySlot()
	{
		for(int i = 0; i<_inventoryItems.Length; i++)
		{
            if (IsNull(i))
            {
				return i;
            }
        }
		return -1;
	}

	private bool IsNull(int i) { return _inventoryItems[i] == null; }



}
