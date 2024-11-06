using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T s_instance;

	public static T Instance
	{
		get
		{
			if(s_instance == null)
			{
				s_instance = GameObject.FindObjectOfType<T>();
				if(s_instance == null)
				{
					GameObject go = GameObject.Find("Manager");
					if(go == null)
					{
						go = new GameObject("Manager");
						DontDestroyOnLoad(go);
					}
					s_instance = go.AddUniqueComponent<T>();
				}
			}
			return s_instance;
		}
	}
	private void Awake()
	{
		if(s_instance == null)
		{
			s_instance = this as T;
			DontDestroyOnLoad (this.gameObject);
		}
		else
		{
			Destroy(this.gameObject);
		}
	}
}
