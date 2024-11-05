using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemObjectTest : MonoBehaviour
{
	private Vector3 mousePosition;
	private IInteractable interactable;

	private void Start()
	{
		SpawnManager.Instance.SpawnItem(102, this.transform.position);
		mousePosition = new Vector3(Screen.width / 2, Screen.height / 2);
	}

}
