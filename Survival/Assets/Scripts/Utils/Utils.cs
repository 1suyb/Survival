using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


public class Utils : MonoBehaviour
{
	public static string GetPath<T>()
	{
		List<string> path = new List<string>();
		Type type = typeof(T);
		while (type != null && type != typeof(object))
		{
			if (type == typeof(MonoBehaviour))
				break;
			path.Add(type.Name);
			type = type.BaseType;
		}
		path.Reverse();
		return string.Join("/", path);
	}
}
