using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildInventoryTest : MonoBehaviour
{
    [SerializeField] private BuildInventoryController presenter;
    [SerializeField] private UIBuildInventory inventoryUI;

    private BuildInventory inventory;
    void Start()
    {
        inventory = new BuildInventory();
        presenter.Init(inventory);
        presenter.OpenUI();
	}

    public void AddItemInventoryTest()
    {
        BuildItemData data = new BuildItemData();
     
		presenter.AddItem(data, 5);
    }

	public void AddItemInventoryTest2()
	{
        BuildItemData data = new BuildItemData();
	

		presenter.AddItem(data, 1);
	}
	public void AddItemInventoryTest3()
	{
		
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
                Debug.Log($"{i} : {inventory.At(i).Data.Id} / {inventory.At(i).Count}");
			}
        }
    }

}