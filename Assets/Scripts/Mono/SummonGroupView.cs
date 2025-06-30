using System;
using System.Collections.Generic;
using UnityEngine;

public class SummonGroupView : MonoBehaviour
{
	[SerializeField] SummonCardView summonCardPrefab;
	UIPool<SummonCardView> m_viewPool;
	List<SummonCardView> m_views;

	void Initialize()
	{
		if (m_viewPool == null)
			m_viewPool = new UIPool<SummonCardView>(summonCardPrefab, transform);
		if (m_views == null)
			m_views = new List<SummonCardView>();
	}

	public void Initialize(List<Summon> summons)
	{
		Initialize();

		int index = 0;
		foreach (var summon in summons)
		{
			var view = m_viewPool.Get();
			view.Initialize(summon);
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
