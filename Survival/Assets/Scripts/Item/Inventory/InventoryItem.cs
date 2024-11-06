using System.Collections;
using System.Data.Common;
using UnityEngine;

public class InventoryItem
{
	private ItemData _data;
	private int _count;
	public bool IsEquiped { get; private set; }

	public ItemData Data => _data;
	public int Count => _count;
	public int RemainCapacity => _data.MaxQuantity - Count;
	public bool IsFull => RemainCapacity == 0;

	public InventoryItem() { }
	public InventoryItem(ItemData data, int count)
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
		if (_data.MaxQuantity==1)
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
		PlayerData playerdata = PlayerManager.Instance.Player.data;
		if (Data.Type == ItemUseType.Weapon)
		{
			IsEquiped = !IsEquiped;

			if (IsEquiped)
			{
				playerdata.ChangeAttackPower(playerdata.AttackPower() + Data.AttackPower);
			}
			else
			{
				playerdata.ChangeAttackPower(playerdata.AttackPower() - Data.AttackPower);
			}
		}
		if(Data.Type == ItemUseType.Consumable)
		{

			if (Data.Health > 0)
				PlayerManager.Instance.Player.condition.Heal(Data.Health);
			if(Data.Water > 0)
				PlayerManager.Instance.Player.condition.Drink(Data.Water);
			if(Data.Hunger > 0)
				PlayerManager.Instance.Player.condition.Eat(Data.Hunger);
			if (Data.AttackPower > 0)
				playerdata.ChangeAttackPower(playerdata.AttackPower() + Data.AttackPower);
			if(Data.MoveSpeed > 0) 
				playerdata.ChangeSpeed(playerdata.Speed() + Data.MoveSpeed);
			if(Data.JumpForce > 0) 
				playerdata.ChangeJumpPower(playerdata.JumpPower() + Data.JumpForce);
			if (Data.Duration > 0)
				playerdata.StartCoroutine(EndItemEffect(_data));
		}
	}
	public IEnumerator EndItemEffect(ItemData data)
	{
		yield return new WaitForSeconds(Data.Duration);
		PlayerData playerdata = PlayerManager.Instance.Player.data;


		if (Data.AttackPower > 0)
			playerdata.ChangeAttackPower(playerdata.AttackPower() - Data.AttackPower);
		if (Data.MoveSpeed > 0)
			playerdata.ChangeSpeed(playerdata.Speed() - Data.MoveSpeed);
		if (Data.JumpForce > 0)
			playerdata.ChangeJumpPower(playerdata.JumpPower() - Data.JumpForce);

	}
}