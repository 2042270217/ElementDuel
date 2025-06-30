using ElementDuel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour, IClickReceiver
{
	public SkillGroupView skillGroupView;
	GameObject m_main;
	GameObject m_costBlock;
	Image m_icon;
	Image m_isSelectedIcon;


	[SerializeField] CostDiceView m_costDicePrefab;
	UIPool<CostDiceView> m_costDicePool;
	List<CostDiceView> m_costDiceViews;

	bool m_enableClick;
	bool m_isSelected;

	public event Action<Skill> doubleClicked;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Initialize()
	{
		m_main = UnityTools.FindChildGameObject(gameObject, "Main");
		m_costBlock = UnityTools.FindChildGameObject(gameObject, "CostBlock");
		m_isSelectedIcon = UITools.GetUIComponet<Image>(m_main, "isSelected");
		m_icon = UITools.GetUIComponet<Image>(m_main, "icon");

		if (m_costDicePool == null)
			m_costDicePool = new UIPool<CostDiceView>(m_costDicePrefab, m_costBlock.transform);
		if (m_costDiceViews == null)
			m_costDiceViews = new List<CostDiceView>();
		SetupClick(true);
	}

	void SetSprite(Sprite sprite)
	{
		Debug.Log(m_icon);
		m_icon.sprite = sprite;
	}

	public void Initialize(SkillData data)
	{
		Initialize();
		SetSprite(data.icon);
		GenerateCostDice(data.cdts);
	}

	void GenerateCostDice(List<SkillCostCondition> cdts)
	{
		foreach (SkillCostCondition cdt in cdts)
		{
			var view = m_costDicePool.Get();
			view.Initialize(cdt);
			view.transform.localScale = new Vector3(1.8f, 1.8f, 1.8f);
			m_costDiceViews.Add(view);
		}
	}

	public void ReleaseAll()
	{
		m_costDicePool?.ReleaseAll(m_costDiceViews);
		m_costDiceViews?.Clear();
	}

	public void OnClick()
	{
		if (!m_enableClick) return;
		if (m_isSelected)
		{
			doubleClicked?.Invoke(skillGroupView.GetSkill(this));
			m_isSelected = false;
			UpdateSkillSelection();
		}
		else
		{
			skillGroupView.SetSelectionAll(false);
			m_isSelected = !m_isSelected;
			UpdateSkillSelection();
		}
	}

	public void UpdateSkillSelection()
	{
		Color c = m_isSelectedIcon.color;
		c.a = m_isSelected ? 1 : 0;
		m_isSelectedIcon.color = c;
	}

	public void SetSelection(bool isSelected)
	{
		m_isSelected = isSelected;
		UpdateSkillSelection();
	}

	public void SetupClick(bool enableClick)
	{
		m_enableClick = enableClick;
		m_icon.raycastTarget = enableClick;
		SetSelection(false);
	}
}
