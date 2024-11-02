using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : Slot, IDragHandler, IPointerDownHandler, IEndDragHandler, IDropHandler, IBeginDragHandler
{
	private UIInventory _inventoryUI;
	private int _index;
	public int Index => _index;


	public void Init(UIInventory inventoryUI, int index)
	{
		_inventoryUI = inventoryUI;
		_index = index;
	}

	public void OnEndDrag(PointerEventData eventData)
	{

		_icon.transform.SetParent(this.transform, false);
		_icon.transform.position = this.transform.position;

	}
	public void OnDrag(PointerEventData eventData)
	{
		
		if (!_inventoryUI.IsAtNull(_index))
		{
			_icon.transform.position = eventData.position;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{

		_inventoryUI.SwapSlot(this);

	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (!_inventoryUI.IsAtNull(_index))
		{
			_inventoryUI.SelectSlot(this);
		}
	}


	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!_inventoryUI.IsAtNull(_index))
		{
			_icon.transform.SetParent(_inventoryUI.transform, false);
			_icon.transform.position = eventData.position;
		}
	}
}
