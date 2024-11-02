using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class UIInventory : UI
{
	private IInventoryPresenter _presenter;
	[SerializeField] private Transform _inventoryPanel;
	[SerializeField] private UIInventoryItemInfo _itemInfoUI;
	private UIInventorySlot[] _slots;
	private UIInventorySlot _selectedSlot;

	public bool IsAtNull(int index)
	{
		return _presenter.IsAtNull(index);
	}
	private void Awake()
	{
		_slots = _inventoryPanel.GetComponentsInChildren<UIInventorySlot>();
		for (int i = 0; i < _slots.Length; i++)
		{
			_slots[i].Init(this, i);
		}
	}
	public void Init(IInventoryPresenter presenter)
	{
		_presenter = presenter;
	}
	public void SelectSlot(UIInventorySlot slot)
	{
		_selectedSlot = slot;
	}
	public void SwapSlot(UIInventorySlot slot)
	{
		if (_selectedSlot != null)
		{
			_presenter.Swap(_selectedSlot.Index, slot.Index);
			_selectedSlot = null;
		}
	}
	public void UpdateInventoryUI(in Sprite[] sprites, in string[] texts)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			_slots[i].UpdateUI(texts[i], sprites[i]);
		}
	}

	public void OpenItemInfo(int index)
	{
		_itemInfoUI.OpenUI(_presenter.OpenItemInfo(index));
	}
	public void CloseItemInfo()
	{
		_itemInfoUI.CloseUI();
	}
}
