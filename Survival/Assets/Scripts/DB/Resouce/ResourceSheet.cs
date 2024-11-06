using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DataSO")]
public class ResourceSheet : ScriptableObject
{
	public List<ResourceData> ResourceData; // Replace 'EntityType' to an actual type that is serializable.

}
