using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIBuildInventorySlot : Slot,
	IBeginDragHandler, IDragHandler, IEndDragHandler,
	IPointerDownHandler, IPointerUpHandler,
	IPointerEnterHandler, IPointerExitHandler
{

	[SerializeField] private Image _slotImage;

	private UIBuildInventory _inventoryUI;

	public int Index { get; private set; }

	private BuildItemInfo _item;
	public BuildItemInfo ItemInfo => _item;

	public void Init(UIBuildInventory inventoryUI, int index)
	{
		_inventoryUI = inventoryUI;
		Index = index;
	}

	public void UpdateUI(BuildItemInfo item)
	{
		_item = item;
		UpdateUI(item.ItemCount<=1?"":item.ItemCount.ToString(), item.Sprite);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		_inventoryUI.OpenItemInfo(_item);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		_inventoryUI.SelectSlot(this);
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (!_item.IsNullItem)
		{
			_icon.transform.SetParent(_inventoryUI.transform, false);
			_icon.transform.position = eventData.position;
		}
	}

	public void OnDrag(PointerEventData eventData)
	{

		if (!_item.IsNullItem)
		{
			_icon.transform.position = eventData.position;
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{
		_icon.transform.SetParent(this.transform, false);
		_icon.transform.position = this.transform.position;
	}



	public void OnPointerExit(PointerEventData eventData)
	{
		_inventoryUI.CloseItemInfo();
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		_inventoryUI.OpenItemButtons(Index);
	}

}
