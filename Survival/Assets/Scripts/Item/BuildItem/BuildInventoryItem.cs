using UnityEngine;

public class BuildInventoryItem
{
	private ItemData _data;
	private int _count;
	public bool IsEquiped { get; private set; }

	public ItemData Data => _data;
	public int Count => _count;
	public int RemainCapacity => _data.MaxQuantity - Count;
	public bool IsFull => RemainCapacity == 0;

	public BuildInventoryItem() { }
	public BuildInventoryItem(ItemData data, int count)
	{
		_data = data;
		_count = count<data.MaxQuantity ? count : data.MaxQuantity;
	}
	public void Init(ItemData data, int count)
	{
		_data = data;
		_count = count < data.MaxQuantity ? count : data.MaxQuantity;
	}

	public void SetData(ItemData data)
	{
		_data = data;
		_count = 1;
	}
	public void AddCount(int count)
	{
		if (!_data.ISStackable)
		{
			Debug.LogError("쌓을 수 없는 아이템!");
			return;
		}
		if((Count + count) > _data.MaxQuantity)
		{
			Debug.LogError("용량초과!");
			return;
		}
		_count += count;
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