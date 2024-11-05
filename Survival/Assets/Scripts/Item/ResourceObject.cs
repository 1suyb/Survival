using UnityEngine;

public class ResourceObject : MonoBehaviour, IInteractable
{
	private ResouceData _data;
	private int _durability;
	public void ClosePrompt()
	{
		UIManager.Instance.CloseUI<UIInfoDisplay>("ResourcePrompt");
	}

	public void Interact()
	{
		if(_data.ResourceGetType == ResourceGetType.Gatherable)
		{
			PlayerManager.Instance.Inventory.AddItem(ItemDB.Instance.Get(_data.DropItemID),_data.DropCount);
			this.gameObject.SetActive(false);
		}
	}

	public void ShowPrompt()
	{
		UIInfoDisplay promptUI = UIManager.Instance.OpenUI<UIInfoDisplay>("ResourcePrompt");
		promptUI.Init(_data.Name, "인터렉션 키를 눌러 채집");
	}

	public void TakePhysicalDamage(int damage)
	{
		_durability -= 1;
		if (_durability == 0)
		{
			DropItem();
			this.gameObject.SetActive(false);
		}
	}
	private void DropItem()
	{
		SpawnManager.Instance.SpawnItem(_data.DropItemID, this.transform.position,_data.DropCount);
	}
}
