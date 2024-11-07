using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIInfoDisplay : UI
{
	[SerializeField] private TMP_Text _title;
	[SerializeField] private TMP_Text _description;
	[SerializeField] private TMP_Text[] _extraTexts;
	public void Init(string title, string descriptoin)
	{
		_title.text = title;
		_description.text = descriptoin;
	}
	public void Init(string title, string descriptoin, string[] extras)
	{
		Init(title, descriptoin);
		if (extras != null)
		{
			for (int i = 0; i < extras.Length; i++)
			{
				_extraTexts[i].text = extras[i];
			}
		}
	}


}
