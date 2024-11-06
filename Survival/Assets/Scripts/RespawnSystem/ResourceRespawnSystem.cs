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
	private void Start()
	{
		DayNightCycle.Instance.OnChangeDayEvent += Spawn;
	}
	public void Spawn()
	{
		foreach (ResourceObject resource in _resources)
		{
			if(!resource.gameObject.activeSelf)
				resource.gameObject.SetActive(true);
		}
	}
}
