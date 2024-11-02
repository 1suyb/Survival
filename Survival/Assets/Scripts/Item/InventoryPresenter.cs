using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface IInventoryPresenter
{
	public void OpenUI();
	public void AddItem(ItemData item, int count);
	public void DropItem(InventoryItem item);
	public void Swap(int i, int j);
}

public class InventoryPresenter : MonoBehaviour, IInventoryPresenter
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

	public void OpenUI()
	{
		_inventoryUI = UIManager.Instance.OpenUI<UIInventory>();
	}

	public void AddItem(ItemData item, int count)
	{
		_inventory.AddItem(item, count);
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
