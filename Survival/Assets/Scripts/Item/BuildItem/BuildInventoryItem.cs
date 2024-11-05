using UnityEngine;

public class BuildInventoryItem
{
	private BuildItemData _data;
	private int _count;
	public bool IsEquiped { get; private set; }

	public BuildItemData Data => _data;
	public int Count => _count;



	public BuildInventoryItem() { }
	public BuildInventoryItem(BuildItemData data, int count)
	{
		_data = data;
	
	}
	public void Init(BuildItemData data, int count)
	{
		_data = data;

	}

	public void SetData(BuildItemData data)
	{
		_data = data;
		_count = 1;
	}
	public void AddCount(int count)
	{
		
	}
	public void SubtractCount(int count)
	{
		if ((Count - count) < 0)
		{
			Debug.LogError("개수 부족!");
		}
		_count -= count;
	}


	public void Use()
	{
		
	}
}