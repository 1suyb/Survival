using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIInventorySlot : UI, IDragHandler, IPointerDownHandler, IEndDragHandler, IDropHandler, IBeginDragHandler
{
	[SerializeField] private TMP_Text _itemCount;
	[SerializeField] private Image _itemIcon;
	private UIInventory _inventoryUI;
	public InventoryItem Item { get; private set; }
	private int _index;
	public int Index => _index;


	public void Init(UIInventory inventoryUI, int index)
	{
		_inventoryUI = inventoryUI;
		_index = index;
		UpdateSlot();
	}

	public void SetItem(InventoryItem item)
	{
		Item = item;
		UpdateSlot();
	}

	public void UpdateSlot()
	{
		if(Item == null)
		{
			_itemCount.text = "";
			_itemIcon.enabled = false;
		}
		else
		{
			_itemIcon.enabled = true;
			_itemCount.text = Item.Count == 1 ? "" : Item.Count.ToString();
			_itemIcon.sprite = Resources.Load<Sprite>(Item.Data.SpritePath);
		}
	}

	public void OnEndDrag(PointerEventData eventData)
	{

		_itemIcon.transform.SetParent(this.transform, false);
		_itemIcon.transform.position = this.transform.position;

	}
	public void OnDrag(PointerEventData eventData)
	{
		if (Item != null)
		{
			_itemIcon.transform.position = eventData.position;
		}
	}

	public void OnDrop(PointerEventData eventData)
	{
		_inventoryUI.SwapSlot(this);
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if(Item != null)
		{
			_inventoryUI.SelectSlot(this);
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (Item != null)
		{
			_itemIcon.transform.SetParent(this.transform.parent, false);
		}
	}
}
