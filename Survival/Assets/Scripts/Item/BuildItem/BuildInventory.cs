using System;
using System.Collections.Generic;
using UnityEngine;

public class BuildInventory
{
	private ClassPoolSystem<BuildInventoryItem> pool = new ClassPoolSystem<BuildInventoryItem>(minSize:GameConfig.INVENTORYSIZE);
	private BuildInventoryItem[] _inventoryItems = new BuildInventoryItem[GameConfig.BUILDINVENTORYSIZE];
	public BuildInventoryItem[] InventoryItems => _inventoryItems;

	public int Size => GameConfig.INVENTORYSIZE;
	public bool IsFull => IndexOfEmptySlot() == -1;


	public BuildInventory()
	{
        var enumerator = BuildItemDB.Instance.DbEnumerator() as IEnumerator<KeyValuePair<int, BuildItemData>>;

        if (enumerator != null)
        {
            int index = 0;
            while (enumerator.MoveNext() && index < _inventoryItems.Length)
            {
                var currentItem = enumerator.Current;
				_inventoryItems[index] = new BuildInventoryItem(currentItem.Value, 1);
                index++;
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

	public void AddItem(BuildItemData itemData,int count)
	{
		if (HasThisItem(itemData))
		{
			if (itemData.MaxStackAmount > 0)
			{
				count = DistributeItemQuantity(itemData, count);
				if(count == 0)
				{
					return;
				}
			}
		}

		if(IsFull)
		{
			Debug.Log("주울 수 없음!");
		}
		else
		{
			AddNewItem(itemData, count);
		}
	}

	private int DistributeItemQuantity(BuildItemData itemData, int count)
	{
		List<int> indices = Indices(itemData.Id);
		for (int i = 0; i < indices.Count; i++)
		{
			
			
		}
		return count;
	}

	private void AddItemQuantity(int index, int count)
	{
		_inventoryItems[index].AddCount(count);
	}
	private void AddNewItem(BuildItemData data,int count)
	{
		int index = IndexOfEmptySlot();
		if (IsNull(index))
		{
			_inventoryItems[index] = pool.TakeFromPool();
			_inventoryItems[index].Init(data,count);
			if (count > data.MaxStackAmount)
			{
				count -= data.MaxStackAmount;
			}
			count = 0;
		}
		else
		{
			_inventoryItems[index].SetData(data);
			count--;
		}
		
		if(count > 0)
		{
			
			
		}
	}
	public void DropItem(int index, int count)
	{
		BuildInventoryItem item = _inventoryItems[index];
		if (item.Count < count)
		{
			Debug.Log("잘못된 개수 요청");
			return;
		}
		item.SubtractCount(count);
		if(item.Count == 0)
		{
			pool.Relase(item);
		}
		_inventoryItems[index]=null;
		
	}

	public void SwapItem(int i, int j)
	{
		BuildInventoryItem tempItem = _inventoryItems[i];
		_inventoryItems[i] = _inventoryItems[j];
		_inventoryItems[j] = tempItem;
	}

}
