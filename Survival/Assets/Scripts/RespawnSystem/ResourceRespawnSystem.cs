using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ResourceRespawnSystem : MonoBehaviour
{
	private ResourceObject[] _resources;

	private void Awake()
	{
		_resources = gameObject.GetComponentsInChildren<ResourceObject>();
	}
	public void Spawn()
	{
		foreach (ResourceObject resource in _resources)
		{
			resource.gameObject.SetActive(true);
		}
	}
}
