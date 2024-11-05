using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public interface IInteractable
{
	public void ShowPrompt();
	public void Interact();
	public void ClosePrompt();
}


public class ItemObject : MonoBehaviour, ILoadable, IInteractable
{
	private ItemData _data;
	private int _count;

	public ItemData Data => _data;
	public int Count => _count;

	public void Init(ItemData data, int count)
	{
		_data = data;
		_count = count;
	}
	public void Load(int id)
	{
		_data = ItemDB.Instance.Get(id);
		_count = 1;
		Debug.Log(_data.PrefabPath);
		ResourceManager.Instantiate(_data.PrefabPath,this.transform);
	}

	public void Interact()
	{

	}
	public void ShowPrompt()
	{
		// 플레이어의 인벤토리로 아이템을 추가하기
		UIInfoDisplay infoUI = UIManager.Instance.OpenUI<UIInfoDisplay>("ItemPrompt");
		infoUI.Init(_data.Name, "인터렉션 키를 눌러 줍기");
	}
	public void ClosePrompt()
	{
		UIManager.Instance.CloseUI<UIInfoDisplay>("ItemPrompt");
	}



}
