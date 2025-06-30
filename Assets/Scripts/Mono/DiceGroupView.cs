using System;
using System.Collections.Generic;
using UnityEngine;

public class DiceGroupView : MonoBehaviour
{
	[SerializeField] DiceView m_dicePrefab;
	UIPool<DiceView> m_diceViewPool;
	List<DiceView> m_diceViews;

	void Initialize()
	{
		if (m_diceViewPool == null)
			m_diceViewPool = new UIPool<DiceView>(m_dicePrefab, transform);
		if (m_diceViews == null)
			m_diceViews = new List<DiceView>();
	}

	public void Initialize(List<ElementType> diceList)
	{
		Initialize();

		int index = 0;
		foreach (ElementType dice in diceList)
		{
			var view = m_diceViewPool.Get();
			view.Initialize(false, dice);
			view.transform.SetSiblingIndex(index++);
			m_diceViews.Add(view);
		}
	}

	public void ReleaseAll()
	{
		m_diceViewPool?.ReleaseAll(m_diceViews);
		m_diceViews?.Clear();
	}
}
