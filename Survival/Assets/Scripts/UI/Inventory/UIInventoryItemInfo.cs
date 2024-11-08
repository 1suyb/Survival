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
		if(!info.IsNullItem)
		{
			_icon.UpdateUI(info.ItemCount == 1 ? "" : info.ItemCount.ToString(), info.Sprite);
			_itemName.text = info.Name;
			_itemType.text = info.Type;
			_itemDescription.text = info.Description;
		}
		this.gameObject.SetActive(!info.IsNullItem);
	}
	public void CloseUI()
	{
		this.gameObject.SetActive(false);
	}
 }

public struct ItemInfo
{
	public bool IsNullItem;

	public string Name;
	public string Description;
	public string Type;
	public bool IsEquiped;

	public Sprite Sprite;
	public int ItemCount;
}
public struct ItemInfoArray
{
	public ItemInfo[] Items;
}