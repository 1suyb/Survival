using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIInventory : UI
{
	[SerializeField] private Transform _inventoryPanel;

	private Inventory _inventory;

	private UIInventorySlot[] _slots;
	private UIInventorySlot _selectedSlot;

	private void Awake()
	{
		_slots = _inventoryPanel.GetComponentsInChildren<UIInventorySlot>();
		foreach(UIInventorySlot slot in _slots)
		{
			slot.Init(this);
		}
	}
	public void SelectSlot(UIInventorySlot slot)
	{
		_selectedSlot = slot;
	}
	public void SwapSlot(UIInventorySlot slot)
	{
		InventoryItem tempItem = slot.Item;
		slot.SetItem(_selectedSlot.Item);
		_selectedSlot.SetItem(tempItem);
	}

	public void UpdateInventory()
	{

	}

}
