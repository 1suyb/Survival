using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceRespawnSystem : MonoBehaviour
{
	private List<GameObject> _resources;

	private void Awake()
	{
		// 리소스 오브젝트 전부를 _resources에 저장
	}
	public void Spawn()
	{
		// 리소스 오브젝트 전부를 돌면서 setactive true로 변경
	}
}
