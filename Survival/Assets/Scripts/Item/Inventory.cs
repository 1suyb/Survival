using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
	private InventoryItem[] _inventoryItem = new InventoryItem[GameConfig.INVENTORYSIZE];

	public int InventorySize => GameConfig.INVENTORYSIZE;
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
		foreach (InventoryItem item in _inventoryItem)
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
		for (int i = 0; i < _inventoryItem.Length; i++)
		{
			if (!IsNull(i))
			{
				if (id == _inventoryItem[i].Data.ID)
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
		for (int i = 0; i< _inventoryItem.Length; i++)
		{
			if (!IsNull(i))
			{
				if (id == _inventoryItem[i].Data.ID)
				{
					indices.Add(i);
				}
			}
		}
		return indices;
	}

	public InventoryItem At(int i)
	{
		return _inventoryItem[i];
	}
	public int IndexOfEmptySlot()
	{
		for(int i = 0; i<_inventoryItem.Length; i++)
		{
            if (IsNull(i))
            {
				return i;
            }
        }
		return -1;
	}

	private bool IsNull(int i) { return _inventoryItem[i] == null; }

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
			Debug.Log("주울 수 없음!");
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
		_inventoryItem[index].AddCount(count);
	}
	private void AddNewItem(ItemData data,int count)
	{
		int index = IndexOfEmptySlot();
		if (IsNull(index))
		{
			_inventoryItem[index] = new InventoryItem(data, count);
			if (count > data.MaxQuantity)
			{
				count -= data.MaxQuantity;
			}
			count = 0;
		}
		else
		{
			_inventoryItem[index].SetData(data);
			count--;
		}
		
		if(count > 0)
		{
			if (count <= _inventoryItem[index].RemainCapacity)
			{
				_inventoryItem[index].AddCount(count);
			}
			else
			{
				_inventoryItem[index].AddCount(_inventoryItem[index].RemainCapacity);
				AddItem(_inventoryItem[index].Data, count - (data.MaxQuantity-1));
			}
			
		}
	}
	public void DropItem()
	{
		Debug.Log("아이템 버리기");
	}

}
public class InventoryItem
{
	private ItemData _data;
	private int _count;
	public ItemData Data => _data;
	public int Count => _count;
	public int RemainCapacity => _data.MaxQuantity - Count;
	public bool IsFull => RemainCapacity == 0;

	public InventoryItem(ItemData data, int count)
	{
		_data = data;
		_count = count<data.MaxQuantity ? count : data.MaxQuantity;
	}

	public void SetData(ItemData data)
	{
		_data = data;
		_count = 1;
	}
	public void AddCount(int count)
	{
		if (!_data.ISStackable)
		{
			Debug.LogError("쌓을 수 없는 아이템!");
			return;
		}
		if((Count + count) > _data.MaxQuantity)
		{
			Debug.LogError("용량초과!");
			return;
		}
		_count += count;
	}
	public void SubtractCount(int count)
	{
		if ((Count - count) < 0)
		{
			Debug.LogError("개수 부족!");
		}
		_count -= count;
	}


	public void Use()
	{

	}
}