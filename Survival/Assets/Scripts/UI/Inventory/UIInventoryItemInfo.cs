using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventoryItemInfo : MonoBehaviour
{
	[SerializeField] private TMP_Text _itemName;
	[SerializeField] private TMP_Text _itemType;
	[SerializeField] private TMP_Text _itemDescription;
	[SerializeField] private Slot _icon;

	public void OpenUI(in ItemInfo info)
	{
		if(info.IsActive)
		{
			_icon.UpdateUI(info.ItemCountText, info.Sprite);
			_itemName.text = info.Name;
			_itemType.text = info.Type;
			_itemDescription.text = info.Description;
		}
		this.gameObject.SetActive(info.IsActive);
	}
	public void CloseUI()
	{
		this.gameObject.SetActive(false);
	}
 }

public struct ItemInfo
{
	public string Name;
	public string Type;
	public string Description;
	public Sprite Sprite;
	public string ItemCountText;
	public bool IsActive;
}