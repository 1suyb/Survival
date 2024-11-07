using System;
using UnityEngine;

public enum ResourceGetType
{
	Gatherable,
	Mineable
}

[Serializable]
public class ResourceData
{
	public int ID;
	public string Name;
	public string Description;
	public int DropItemID;
	public ResourceGetType ResourceGetType;

	public int Durability;
	public int DropCount;

	public string PrefabPath;
}
