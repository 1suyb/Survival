
using System;
using UnityEngine;


public class BuildInventoryController : MonoBehaviour, IUIUpdater<BuildItemInfoArray>
{
	private BuildInventory _buildInventory;
	public event Action<BuildItemInfoArray> OnDataUpdateEvent;
    private bool _isInventoryUIOpened = false;

    public void Awake()
    {
        _buildInventory = new BuildInventory();
        PlayerManager.Instance.BuildInventory = this;

    }

    public void Start()
    {
        PlayerManager.Instance.Player.GetComponent<PlayerController>().buildinventory = OpenUI;

    }

    public void Init(BuildInventory inventory)
	{
        _buildInventory = inventory;
	}

	public void TestUI(UIBuildInventory inventoryUI)
	{
		inventoryUI.Init(this);
		UpdateInventoryUI();
		inventoryUI.OnUseEvent += UseItem;

	}

	public void OpenUI()
	{
        if (!_isInventoryUIOpened)
        {
            _isInventoryUIOpened = true;
            UIBuildInventory inventoryUI = UIManager.Instance.OpenUI<UIBuildInventory>();
            inventoryUI.Init(this);

            inventoryUI.OnUseEvent += UseItem;
            
            UpdateInventoryUI();
        }
        else
        {
            _isInventoryUIOpened = false;
            UIManager.Instance.CloseUI<UIBuildInventory>();
            OnDataUpdateEvent = null;
        }
    }


	public void UseItem(int index)
	{
		BuildInventoryItem item = _buildInventory.At(index);
		item.Use();
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
