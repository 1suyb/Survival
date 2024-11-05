
using System;
using UnityEngine;


public class BuildInventoryController : MonoBehaviour, IUIUpdater<BuildItemInfoArray>
{
	private BuildInventory _buildInventory;
	public event Action<BuildItemInfoArray> OnDataUpdateEvent;

	public void Init(BuildInventory inventory)
	{
        _buildInventory = inventory;
	}

	public void TestUI(UIBuildInventory inventoryUI)
	{
		inventoryUI.Init(this);
		UpdateInventoryUI();
		inventoryUI.OnUseEvent += UseItem;
		inventoryUI.OnDropEvent += DropItem;
		inventoryUI.OnSwapEvent += Swap;
	}

	public void OpenUI()
	{
		UIBuildInventory inventoryUI = UIManager.Instance.OpenUI<UIBuildInventory>();
		inventoryUI.Init(this);

		inventoryUI.OnUseEvent += UseItem;
		inventoryUI.OnDropEvent += DropItem;
		inventoryUI.OnSwapEvent += Swap;

		UpdateInventoryUI();
	}

	public void AddItem(BuildItemData item, int count)
	{
        _buildInventory.AddItem(item, count);
		UpdateInventoryUI();
	}

	public void DropItem(int index)
	{
        _buildInventory.DropItem(index, _buildInventory.At(index).Count);
		UpdateInventoryUI();
	}

	public void UseItem(int index)
	{
		BuildInventoryItem item = _buildInventory.At(index);
		item.Use();
		UpdateInventoryUI();
	}

	public void Swap(int i, int j)
	{
        _buildInventory.SwapItem(i, j);
		UpdateInventoryUI();
	}


	public BuildItemInfoArray MakeItemInfoArray()
	{
		BuildItemInfo[] itemInfos = new BuildItemInfo[_buildInventory.Size];
		for(int i = 0; i < _buildInventory.Size; i++)
		{
			BuildInventoryItem item = _buildInventory.At(i);
			itemInfos[i] = new BuildItemInfo();
			if (item == null)
			{
				itemInfos[i].IsNullItem = true;
			}
			else
			{
				itemInfos[i].IsNullItem = false;
				itemInfos[i].Name = item.Data.Name;
				itemInfos[i].Description = item.Data.Description;
				itemInfos[i].Type = item.Data.Type.ToString();
				itemInfos[i].Sprite = Resources.Load<Sprite>(item.Data.SpritePath);
				itemInfos[i].ItemCount = item.Count;
			}
		}
		BuildItemInfoArray itemInfoArray = new BuildItemInfoArray();
		itemInfoArray.Items = itemInfos;
		return itemInfoArray;
	}



	public void UpdateInventoryUI()
	{
		OnDataUpdateEvent?.Invoke(MakeItemInfoArray());
	}
}
