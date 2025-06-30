using ElementDuel;
using UnityEngine;
using System.Collections.Generic;
using System;

public class CurrentHandsUI : IUserInterface
{
	HandsGroupView m_handsGroupView;

	public CurrentHandsUI(ElementDuelGame edGame) : base(edGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
		m_Root = UITools.FindUIGameObject("CurrentHandsUI");

		m_handsGroupView = UITools.GetUIComponet<HandsGroupView>(m_Root, "CurrentHandsGroup");
		m_handsGroupView.OnHandsGroupHoverEnter += OnHoverEnter;
		m_handsGroupView.OnHandsGroupHoverExit += OnHoverExit;
	}

	public override void Release()
	{

	}

	/// <summary>
	/// ÷ÿΩ® ÷≈∆UI
	/// </summary>
	public override void Update()
	{
		m_handsGroupView.ReleaseAll();
		m_handsGroupView.Initialize(m_EDGame.CurrentPlayer.handsList);
	}

	public void ClearSelection()
	{
		m_handsGroupView.SetSelectionAll(false);
	}

	public void RegisterDoubleClick(Action<ActionCard> call)
	{
		m_handsGroupView.RegisterDoubleClick(call);
	}

	public void RemoveDoubleClick(Action<ActionCard> call)
	{
		m_handsGroupView.RemoveDoubleClick(call);
	}

	public void OnHoverEnter()
	{
		m_EDGame.SetSkillUIActive(false);
	}

	public void OnHoverExit()
	{
		m_EDGame.SetSkillUIActive(true);
	}
}
