using UnityEngine;

public class ResourceObject : MonoBehaviour, IInteractable, IDamagable, ILoadable
{
	[SerializeField] private int _id;
	private ResourceData _data;
	private int _durability;

	private void OnEnable()
	{
		if(_data != null)
		{
			_durability = _data.Durability;
		}
		else
		{
			Load(_id);
		}
	}
	public void ClosePrompt()
	{
		UIManager.Instance.CloseUI<UIInfoDisplay>("ResourcePrompt");
	}

	public void Interact()
	{
		if(_data.ResourceGetType == ResourceGetType.Gatherable)
		{
			PlayerManager.Instance.Inventory.AddItem(ItemDB.Instance.Get(_data.DropItemID),_data.DropCount);
			if (_data.ID != 102)
			{
				this.gameObject.SetActive(false);
			}
		}
		ClosePrompt();
	}

	public void ShowPrompt()
	{
		UIInfoDisplay promptUI = UIManager.Instance.OpenUI<UIInfoDisplay>("ResourcePrompt");
		if(_data.ResourceGetType == ResourceGetType .Gatherable)
		{
			promptUI.Init(_data.Name, "인터렉션 키를 눌러 채집");
		}
		else
		{
			promptUI.Init(_data.Name, "때려서 채집");
		}
	}

	public void TakeDamage(int damage)
	{
		_durability -= 1;
		if (_durability == 0)
		{
			DropItem();
			ClosePrompt();
			this.gameObject.SetActive(false);
		}
	}

	private void DropItem()
	{
		SpawnManager.Instance.SpawnItem(_data.DropItemID, this.transform.position+transform.up*1f,_data.DropCount);
	}

	public void Load(int id)
	{
		_data = ResourceDB.Instance.Get(id);
		_durability = _data.Durability;
	}
}
