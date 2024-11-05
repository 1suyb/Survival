using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObjectTest : MonoBehaviour
{
	private IInteractable interactable;
	[SerializeField] private ResourceObject resouceObject;

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
			SpawnManager.Instance.SpawnItem(102, this.transform.position);
		}
	}


}
