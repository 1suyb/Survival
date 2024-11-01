using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryTest : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    void Start()
    {
        
    }

    public void AddItemInventoryTest()
    {
        ItemData data = new ItemData();
        data.ID = 101;
        data.Name = "name";
        data.Description = "description";
        data.ISStackable= true;
        data.MaxQuantity = 30;

        inventory.AddItem(data, 5);
    }
	public void AddItemInventoryTest2()
	{
		ItemData data = new ItemData();
		data.ID = 102;
		data.Name = "name";
		data.Description = "description";
		data.ISStackable = false;
		data.MaxQuantity = 0;

		inventory.AddItem(data, 0);
	}

}
