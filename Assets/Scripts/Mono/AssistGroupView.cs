using System;
using System.Collections.Generic;
using UnityEngine;

public class AssistGroupView : MonoBehaviour
{
	[SerializeField] AssistCardView assistCardPrefab;
	UIPool<AssistCardView> m_viewPool;
	List<AssistCardView> m_views;

	void Initialize()
	{
		if (m_viewPool == null)
			m_viewPool = new UIPool<AssistCardView>(assistCardPrefab, transform);
		if (m_views == null)
			m_views = new List<AssistCardView>();
	}

	public void Initialize(List<Assist> assists)
	{
		Initialize();

		int index = 0;
		foreach (Assist assist in assists)
		{
			var view = m_viewPool.Get();
			view.Initialize(assist);
			view.transform.SetSiblingIndex(index++);
			m_views.Add(view);
		}
	}

	public void ReleaseAll()
	{
		m_viewPool?.ReleaseAll(m_views);
		m_views?.Clear();
	}
}
