using ElementDuel;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BattleUI : IUserInterface
{
	Button m_changeButton;

	public BattleUI(ElementDuelGame edGame) : base(edGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
		m_Root = UITools.FindUIGameObject("BattleUI");

		m_changeButton = UITools.GetUIComponet<Button>(m_Root, "ChangeBtn");

		Hide();
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}

	public void AddChangeBtnListener(UnityAction call)
	{
		m_changeButton.onClick.AddListener(call);
	}

	public void RemoveChangeBtnListener(UnityAction call)
	{
		m_changeButton.onClick.RemoveListener(call);
	}
}
