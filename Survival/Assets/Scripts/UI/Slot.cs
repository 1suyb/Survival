using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Slot : UI
{
	[SerializeField] protected TMP_Text _text;
	[SerializeField] protected Image _icon;

	public virtual void UpdateUI(string text, Sprite image)
	{
		_text.text = text;

		if (image == null)
		{
			_icon.enabled = false;
		}
		else
		{
			_icon.enabled = true;
			_icon.sprite = image;
		}

	}
}
