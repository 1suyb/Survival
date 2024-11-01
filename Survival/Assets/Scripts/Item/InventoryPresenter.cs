using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPresenter : MonoBehaviour
{
	private Inventory _inventory;
	private UIInventory _inventoryUI;

	public void Init(Inventory inventory)
	{
		_inventory = inventory;
	}

	public void TestUI(UIInventory inventoryUI)
	{
		_inventoryUI = inventoryUI;
	}

	public void OpenInventoryUI()
	{
		_inventoryUI = UIManager.Instance.OpenUI<UIInventory>();
	}

	public void AddItem(ItemObject item)
	{
		_inventory.AddItem(item.Data, item.Count);
		_inventoryUI.UpdateInventoryUI(_inventory.InventoryItems);
	}

	public void DropItem(InventoryItem item)
	{
		_inventory.DropItem();
		_inventoryUI.UpdateInventoryUI(_inventory.InventoryItems);
	}

	public void Swap(int i, int j)
	{
		_inventory.SwapItem(i, j);
		_inventoryUI.UpdateInventoryUI(_inventory.InventoryItems);
	}

}
