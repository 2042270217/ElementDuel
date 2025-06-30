using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillGroupView : MonoBehaviour
{
	[SerializeField] SkillView m_skillPrefab;
	UIPool<SkillView> m_skillPool;
	List<SkillView> m_skillViews;
	List<Skill> m_skillList;

	void Initialize()
	{
		if (m_skillPool == null)
			m_skillPool = new UIPool<SkillView>(m_skillPrefab, transform);
		if (m_skillViews == null)
			m_skillViews = new List<SkillView>();
	}

	public void Initialize(List<Skill> skillList)
	{
		Initialize();
		m_skillList = skillList;
		foreach (Skill skill in skillList)
		{
			var view = m_skillPool.Get();
			view.Initialize(skill.skillData);
			view.skillGroupView = this;
			m_skillViews.Add(view);
		}
	}

	public void ReleaseAll()
	{
		m_skillPool?.ReleaseAll(m_skillViews);
		if (m_skillViews == null || m_skillViews.Count == 0) return;
		foreach (var view in m_skillViews)
		{
			view.ReleaseAll();
		}
		m_skillViews?.Clear();
		m_skillList = null;
	}

	public Skill GetSkill(SkillView skillView)
	{
		int index = m_skillViews.IndexOf(skillView);
		return m_skillList[index];
	}

	public void SetSelectionAll(bool active)
	{
		if (m_skillViews == null || m_skillViews.Count == 0) return;
		foreach (var view in m_skillViews)
		{
			view.SetSelection(active);
		}
	}

	public void RegisterSkillDoubleClick(Action<Skill> call)
	{
		foreach (var view in m_skillViews)
		{
			view.doubleClicked += call;
		}
	}

	public void RemoveSkillDoubleClick(Action<Skill> call)
	{
		foreach (var view in m_skillViews)
		{
			view.doubleClicked -= call;
		}
	}
}
