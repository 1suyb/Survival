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

	private void Update()
	{

		Debug.DrawRay(Camera.main.transform.position,Camera.main.transform.forward*10,Color.white,1f);
		if (Physics.Raycast(Camera.main.ScreenPointToRay(mousePosition),out RaycastHit hit)) 
		{
			if(hit.collider != null)
			{
				if(hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable ib))
				{
					if(interactable!= ib)
					{
						interactable = ib;
						ib.ShowPrompt();
					}
				}
			}
			
		}
	}
}
