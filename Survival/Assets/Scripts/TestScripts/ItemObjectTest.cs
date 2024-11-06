using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObjectTest : MonoBehaviour
{
	private IInteractable _interactable;
	[SerializeField] private ResourceObject _resouceObject;
	private int _itemid;

	private void Start()
	{
		SpawnManager.Instance.SpawnItem(102, this.transform.position);
	}

	void OnGUI()
	{
		// Make a background box
		GUI.Box(new Rect(10, 10, 300, 100), "Loader Menu");

		// Make the first button. If it is pressed, Application.Loadlevel (1) will be executed
		if (GUI.Button(new Rect(20, 40, 150, 50), "Spawn"))
		{
			SpawnManager.Instance.SpawnItem(_itemid, this.transform.position);
		}

		_itemid = int.Parse(GUI.TextField(new Rect(20, 110, 150, 50), _itemid.ToString()));
	}


}
