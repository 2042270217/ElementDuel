using UnityEngine;
using System.Collections.Generic;
using ElementDuel;
using TMPro;

public class DiceUI : IUserInterface
{
	DiceGroupView m_diceGroupView;
	GameObject m_counter;
	TMP_Text m_diceCount;

	public DiceUI(ElementDuelGame edGame) : base(edGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
		m_Root = UITools.FindUIGameObject("DiceUI");

		m_diceGroupView = UITools.GetUIComponet<DiceGroupView>(m_Root, "DiceGroup");
		m_counter = UnityTools.FindChildGameObject(m_Root, "Counter");
		m_diceCount = UITools.GetUIComponet<TMP_Text>(m_counter, "count");
		Hide();
	}

	public override void Release()
	{

	}

	/// <summary>
	/// ÷ÿΩ®DiceList
	/// </summary>
	public override void Update()
	{
		m_diceGroupView.ReleaseAll();
		m_diceGroupView.Initialize(m_EDGame.CurrentPlayer.diceList);
		SetDiceCountUI();
	}

	void SetDiceCountUI()
	{
		m_diceCount.text = m_EDGame.CurrentPlayer.diceCount.ToString();
	}
}
