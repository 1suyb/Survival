using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class ItemObject : MonoBehaviour
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
}
