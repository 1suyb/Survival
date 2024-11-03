
using System;
using UnityEngine;


public interface IUIUpdater<T>
{
	public event Action<T> OnDataUpdateEvent;
}

public class InventoryController : MonoBehaviour, IUIUpdater<ItemInfoArray>
{
	private Inventory _inventory;
	public event Action<ItemInfoArray> OnDataUpdateEvent;

	public void Init(Inventory inventory)
	{
		_inventory = inventory;
	}

	public void TestUI(UIInventory inventoryUI)
	{
		inventoryUI.Init(this);
		UpdateInventoryUI();
		inventoryUI.OnUseEvent += UseItem;
		inventoryUI.OnDropEvent += DropItem;
		inventoryUI.OnSwapEvent += Swap;
	}

	public void OpenUI()
	{
		UIInventory inventoryUI = UIManager.Instance.OpenUI<UIInventory>();
		inventoryUI.Init(this);

		inventoryUI.OnUseEvent += UseItem;
		inventoryUI.OnDropEvent += DropItem;
		inventoryUI.OnSwapEvent += Swap;

		UpdateInventoryUI();
	}

	public void AddItem(ItemData item, int count)
	{
		_inventory.AddItem(item, count);
		UpdateInventoryUI();
	}

	public void DropItem(int index)
	{
		_inventory.DropItem(index,_inventory.At(index).Count);
		UpdateInventoryUI();
	}

	public void UseItem(int index)
	{
		InventoryItem item = _inventory.At(index);
		item.Use();
		UpdateInventoryUI();
	}

	public void Swap(int i, int j)
	{
		_inventory.SwapItem(i, j);
		UpdateInventoryUI();
	}


	public ItemInfoArray MakeItemInfoArray()
	{
		ItemInfo[] itemInfos = new ItemInfo[_inventory.Size];
		for(int i = 0; i < _inventory.Size; i++)
		{
			InventoryItem item = _inventory.At(i);
			itemInfos[i] = new ItemInfo();
			if (item == null)
			{
				itemInfos[i].IsNullItem = true;
			}
			else
			{
				itemInfos[i].IsNullItem = false;
				itemInfos[i].Name = item.Data.Name;
				itemInfos[i].Description = item.Data.Description;
				itemInfos[i].Type = item.Data.Type;
				itemInfos[i].IsEquiped = item.IsEquiped;
				itemInfos[i].Sprite = Resources.Load<Sprite>(item.Data.SpritePath);
				itemInfos[i].ItemCount = item.Count;
			}
		}
		ItemInfoArray itemInfoArray = new ItemInfoArray();
		itemInfoArray.Items = itemInfos;
		return itemInfoArray;
	}



	public void UpdateInventoryUI()
	{
		OnDataUpdateEvent?.Invoke(MakeItemInfoArray());
	}
}
