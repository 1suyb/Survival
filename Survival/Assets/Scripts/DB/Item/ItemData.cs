using System;

public enum ItemUseType
{
	Consumable = 1,
	Resource = 2,
	Weapon

}

[Serializable]
public class ItemData
{
	public int ID;
	public string Name;
	public string Description;
	public ItemUseType Type;

	public int Health;
	public int Stamina;
	public int Hunger;
	public int Water;
	public float BodyTemperture;

	public int AttackPower;
	public float MoveSpeed;
	public float JumpForce;

	public bool ISStackable;
	public int MaxQuantity;

	public string SpritePath;
	public string PrefabPath;
}
