using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public static class UnityTools
{
	public static GameObject FindGameObject(string name)
	{
		var temp = GameObject.Find(name);
		if (temp == null)
		{
			EDebug.LogError("场景中找不到名为[" + name + "]的对象");
			return null;
		}
		return temp;
	}

	public static GameObject FindChildGameObject(GameObject parent, string childName)
	{
		if (parent == null)
		{
			EDebug.LogError("parent is null.");
			return null;
		}
		Transform temp = null;
		if (parent.name == childName)
		{
			temp = parent.transform;
		}
		else
		{
			Transform[] children = parent.transform.GetComponentsInChildren<Transform>();
			foreach (Transform child in children)
			{
				if (child.name == childName)
				{
					if (temp == null)
					{
						temp = child;
					}
					else
					{
						EDebug.LogError(parent.name + "下有多个名为" + childName + "的组件");
					}
				}
			}
		}
		if (temp == null)
		{
			EDebug.LogError(parent.name + "下未找到为" + childName + "的组件");
			return null;
		}
		return temp.gameObject;

	}

}

