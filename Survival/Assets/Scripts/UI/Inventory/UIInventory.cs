using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIInventory : UI
{
	private InventoryPresenter _presenter;
	[SerializeField] private Transform _inventoryPanel;
	private UIInventorySlot[] _slots;
	private UIInventorySlot _selectedSlot;

	private void Awake()
	{
		_slots = _inventoryPanel.GetComponentsInChildren<UIInventorySlot>();
		for (int i = 0; i < _slots.Length; i++)
		{
			_slots[i].Init(this, i);
		}
	}

	public void SetPresenter(InventoryPresenter presenter)
	{
		_presenter = presenter;
	}

	public void SelectSlot(UIInventorySlot slot)
	{
		_selectedSlot = slot;
	}
	public void SwapSlot(UIInventorySlot slot)
	{
		_presenter.Swap(_selectedSlot.Index, slot.Index);
	}

	public void UpdateInventoryUI(in InventoryItem[] items)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			_slots[i].SetItem(items[i]);
		}
	}
}
