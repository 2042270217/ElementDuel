using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SkillUI : IUserInterface
{
	SkillGroupView m_skillGroupView;

	public SkillUI(ElementDuelGame edGame) : base(edGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
		m_Root = UITools.FindUIGameObject("SkillUI");

		m_skillGroupView = UITools.GetUIComponet<SkillGroupView>(m_Root, "SkillGroup");
	}

	public override void Release()
	{

	}

	/// <summary>
	/// 用于重建SkillUI
	/// </summary>
	public override void Update()
	{
		m_skillGroupView.ReleaseAll();
		m_skillGroupView.Initialize(m_EDGame.CurrentPlayer.fightingCharecter.skills);
	}

	public void ClearSelection()
	{
		m_skillGroupView.SetSelectionAll(false);
	}

	public void RegisterSkillDoubleClick(Action<Skill> call)
	{
		m_skillGroupView.RegisterSkillDoubleClick(call);
	}

	public void RemoveSkillDoubleClick(Action<Skill> call)
	{
		m_skillGroupView.RemoveSkillDoubleClick(call);
	}
}
