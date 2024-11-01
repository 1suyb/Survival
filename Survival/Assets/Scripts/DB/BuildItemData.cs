using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExcelAsset(AssetPath = "Resources/DataSO")]
public class BuildItemData : ScriptableObject
{
	public List<BuildItemData_BuildItem> BuildItemList; 
}
