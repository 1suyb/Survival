using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;

public interface IInventoryPresenter
{
	public void OpenUI();
	public void AddItem(ItemData item, int count);
	public void DropItem(InventoryItem item);
	public void Swap(int i, int j);
	public bool IsAtNull(int index);

	public ItemInfo OpenItemInfo(int index);
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
		_inventoryUI.Init(this);
		UpdateInventoryUI();
	}

	public void OpenUI()
	{
		_inventoryUI = UIManager.Instance.OpenUI<UIInventory>();
		_inventoryUI.Init(this);
		UpdateInventoryUI();
	}

	public void AddItem(ItemData item, int count)
	{
		_inventory.AddItem(item, count);

		string[] counts = new string[_inventory.Size];
		UpdateInventoryUI();
	}

	public void DropItem(InventoryItem item)
	{
		_inventory.DropItem();
		UpdateInventoryUI();
	}

	public void Swap(int i, int j)
	{
		_inventory.SwapItem(i, j);
		UpdateInventoryUI();
	}

	public void UpdateInventoryUI()
	{
		_inventoryUI.UpdateInventoryUI(GetItemSprites(),GetItemCountText());
	}

	private Sprite[] GetItemSprites()
	{
		Sprite[] sprites = new Sprite[_inventory.Size];

		for(int i = 0; i < _inventory.Size; i++)
		{
			if (_inventory.At(i) != null)
			{
				sprites[i] = Resources.Load<Sprite>(_inventory.At(i).Data.SpritePath);
			}
			else
			{
				sprites[i] = null;
			}
		}

		return sprites;
	}
	private string[] GetItemCountText()
	{
		string[] countText = new string[_inventory.Size];

		for (int i = 0; i < _inventory.Size; i++)
		{
			if (_inventory.At(i) != null)
			{
				countText[i] = _inventory.At(i).Count == 1 ? "" : _inventory.At(i).Count.ToString();
			}
			else
			{
				countText[i] = "";
			}
		}

		return countText;
	}
	public bool IsAtNull(int index)
	{
		return _inventory.At(index) == null;
	}

	public ItemInfo OpenItemInfo(int index)
	{
		ItemInfo info = new ItemInfo();
		info.IsActive = _inventory.At(index) != null;
		if(!info.IsActive)
		{
			return info;
		}
		ItemData data = _inventory.At(index).Data;
		info.Name = data.Name;
		info.Description = data.Description;
		info.Type = data.Type;
		info.ItemCountText = _inventory.At(index).Count == 1 ? "" : _inventory.At(index).Count.ToString();
		info.Sprite = Resources.Load<Sprite>(data.SpritePath);
		return info;
	}
}
