using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DataSO")]
public class ItemSheet : ScriptableObject
{
	public List<ItemData> ItemData; // Replace 'EntityType' to an actual type that is serializable.
}
