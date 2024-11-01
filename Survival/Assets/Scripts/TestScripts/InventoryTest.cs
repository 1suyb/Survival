using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private InventoryPresenter presenter;
    [SerializeField] private UIInventory inventoryUI;

    private Inventory inventory;
    void Start()
    {
        inventory = new Inventory();
        presenter.Init(inventory);
        presenter.TestUI(inventoryUI);
		inventoryUI.SetPresenter(presenter);
	}

    public void AddItemInventoryTest()
    {
        ItemData data = new ItemData();
        data.ID = 101;
        data.Name = "name";
        data.Description = "description";
        data.ISStackable= true;
        data.MaxQuantity = 30;
        data.SpritePath = "Sprites/1";

        ItemObject itemObject = new ItemObject();
        itemObject.Init(data, 5);

		presenter.AddItem(itemObject);
    }

	public void AddItemInventoryTest2()
	{
		ItemData data = new ItemData();
		data.ID = 102;
		data.Name = "name";
		data.Description = "description";
		data.ISStackable = false;
		data.MaxQuantity = 0;
		data.SpritePath = "Sprites/2";


		ItemObject itemObject = new ItemObject();
		itemObject.Init(data, 0);

		presenter.AddItem(itemObject);
	}

    public void ShowInventoryList()
    {
        for(int i = 0; i<GameConfig.INVENTORYSIZE; i++)
        {
            if (inventory.At(i) == null)
            {
                Debug.Log($"{i} : null");
            }
            else
            {
                Debug.Log($"{i} : {inventory.At(i).Data.ID} / {inventory.At(i).Count}");
			}
        }
    }

}
