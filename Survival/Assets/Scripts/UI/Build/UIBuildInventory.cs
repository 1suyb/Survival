
using System;
using System.Net.Http.Headers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIBuildInventory : UI
{
	[Header("InventorySlots")]
	[SerializeField] private Transform _inventoryPanel;
	private UIBuildInventorySlot[] _slots;
	private UIBuildInventorySlot _selectedSlot;
	public bool IsSlotDragging { get; set; }


	[Header("ItemInfo")]
	[SerializeField] private UIBuildInventoryItemInfo _itemInfoUI;

	[Header("ItemUseButton")]
	[SerializeField] private Transform _clickedButtonsParentTransform;
	[SerializeField] private Button _useButton;
	[SerializeField] private TMP_Text _useButtonLabel;

	public Button UseButton => _useButton;
	public event Action<int> OnUseEvent;


	private void Awake()
	{
		_slots = _inventoryPanel.GetComponentsInChildren<UIBuildInventorySlot>();
		for (int i = 0; i < _slots.Length; i++)
		{
			_slots[i].Init(this, i);
		}

		_useButton.onClick.RemoveAllListeners();

		_useButton.onClick.AddListener(() =>
		{
			OnUseEvent(_selectedSlot.Index);
			CloseItemButtons();
		});


	}
	public void Init(IUIUpdater<BuildItemInfoArray> inventoryController)
	{
		inventoryController.OnDataUpdateEvent += UpdateInventoryUI;
	}

	public void UpdateInventoryUI(BuildItemInfoArray itemInfo)
	{
		for (int i = 0; i < _slots.Length; i++)
		{
			_slots[i].UpdateUI(itemInfo.Items[i]);
		}
	}

	public void SelectSlot(UIBuildInventorySlot slot)
	{
		_selectedSlot = slot;
		CloseItemButtons();
	}


	public void OpenItemInfo(BuildItemInfo itemInfo)
	{
		_itemInfoUI.OpenUI(itemInfo);
	}
	public void CloseItemInfo()
	{
		_itemInfoUI.CloseUI();
	}

	public void OpenItemButtons(int index)
	{
		BuildItemInfo itemInfo = _selectedSlot.ItemInfo;
		if(itemInfo.IsNullItem) return;

		if(_selectedSlot.Index == index)
		{
			
		   _useButtonLabel.text = "설치하기";
					
		} 
		_clickedButtonsParentTransform.position = _selectedSlot.transform.position+ new Vector3(-50,-50,0);
		_clickedButtonsParentTransform.gameObject.SetActive(true);
	}
	public void CloseItemButtons()
	{
		_clickedButtonsParentTransform.gameObject.SetActive(false);
	}
}
