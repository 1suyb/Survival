using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	private ClassPoolSystem<InventoryItem> pool = new ClassPoolSystem<InventoryItem>(minSize:GameConfig.INVENTORYSIZE);
	private InventoryItem[] _inventoryItems = new InventoryItem[GameConfig.INVENTORYSIZE];
	public InventoryItem[] InventoryItems => _inventoryItems;

	public int Size => GameConfig.INVENTORYSIZE;
	public bool IsFull => IndexOfEmptySlot() == -1;


	public bool HasThisItem(InventoryItem item)
	{
		return HasThisItem(item.Data.ID);
	}
	public bool HasThisItem(ItemData targetItem)
	{
		return HasThisItem(targetItem.ID);
	}
	public bool HasThisItem(int targetItemID)
	{
		foreach (InventoryItem item in _inventoryItems)
		{
			if(item != null)
			{
				if (targetItemID == item.Data.ID)
				{
					return true;
				}
			}
		}
		return false;
	}

	public int IndexOf(InventoryItem item)
	{
		return IndexOf(item.Data.ID);
	}
	public int IndexOf(ItemData targetItem)
	{
		return IndexOf(targetItem.ID);
	}
	public int IndexOf(int id)
	{
		for (int i = 0; i < _inventoryItems.Length; i++)
		{
			if (!IsNull(i))
			{
				if (id == _inventoryItems[i].Data.ID)
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
				if (id == _inventoryItems[i].Data.ID)
				{
					indices.Add(i);
				}
			}
		}
		return indices;
	}

	public InventoryItem At(int i)
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

	public void AddItem(ItemData itemData,int count)
	{
		if (HasThisItem(itemData))
		{
			if (itemData.ISStackable)
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
			SpawnManager.Instance.SpawnItem(itemData.ID, PlayerManager.Instance.Player.transform.position ,count);
		}
		else
		{
			AddNewItem(itemData, count);
		}
	}

	private int DistributeItemQuantity(ItemData itemData, int count)
	{
		List<int> indices = Indices(itemData.ID);
		for (int i = 0; i < indices.Count; i++)
		{
			if (!At(indices[i]).IsFull)
			{
				if (At(indices[i]).RemainCapacity > count)
				{
					AddItemQuantity(indices[i], count);
					return 0;
				}
				else
				{
					count -= At(indices[i]).RemainCapacity;
					AddItemQuantity(indices[i], At(indices[i]).RemainCapacity);
				}
			}
		}
		return count;
	}

	private void AddItemQuantity(int index, int count)
	{
		_inventoryItems[index].AddCount(count);
	}
	private void AddNewItem(ItemData data,int count)
	{
		int index = IndexOfEmptySlot();
		if (IsNull(index))
		{
			_inventoryItems[index] = pool.TakeFromPool();
			_inventoryItems[index].Init(data,count);
			if (count > data.MaxQuantity)
			{
				count -= data.MaxQuantity;
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
			if (count <= _inventoryItems[index].RemainCapacity)
			{
				_inventoryItems[index].AddCount(count);
			}
			else
			{
				_inventoryItems[index].AddCount(_inventoryItems[index].RemainCapacity);
				AddItem(_inventoryItems[index].Data, count - (data.MaxQuantity-1));
			}
			
		}
	}
	public void DropItem(int index, int count)
	{
		InventoryItem item = _inventoryItems[index];
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
		InventoryItem tempItem = _inventoryItems[i];
		_inventoryItems[i] = _inventoryItems[j];
		_inventoryItems[j] = tempItem;
	}

}
