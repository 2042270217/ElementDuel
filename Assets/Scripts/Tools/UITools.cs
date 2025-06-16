using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;

public static class UITools
{
	static GameObject m_CanvasObj;

	public static GameObject FindUIGameObject(string UIName)
	{
		if (m_CanvasObj == null)
		{
			m_CanvasObj = UnityTools.FindGameObject("Canvas");
		}
		if (m_CanvasObj == null)
		{
			EDebug.LogError("Canvas不存在");
			return null;
		}
		return UnityTools.FindChildGameObject(m_CanvasObj, UIName);
	}

	public static T GetUIComponet<T>(GameObject parent, string name) where T : Component
	{
		GameObject comp = UnityTools.FindChildGameObject(parent, name);
		if (comp == null)
		{
			return null;
		}
		T temp = comp.GetComponent<T>();
		if (temp == null)
		{
			EDebug.LogError(name + "下未找到" + typeof(T));
			return null;
		}

		return temp;
	}
}
