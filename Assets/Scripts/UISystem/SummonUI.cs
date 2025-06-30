using ElementDuel;
using System;
using System.Collections.Generic;

public class SummonUI : IUserInterface
{
	bool m_isDown;

	SummonGroupView m_summonGroupView;

	public SummonUI(ElementDuelGame edGame,bool isDown) : base(edGame)
	{
		m_isDown = isDown;
		Initialize();
	}

	public override void Initialize()
	{
		string parentName;
		if (m_isDown)
			parentName = "Player1Group";
		else
			parentName = "Player2Group";

		m_Root = UITools.FindUIGameObject(parentName);
		m_Root = UnityTools.FindChildGameObject(m_Root, "SummonUI");

		m_summonGroupView = UITools.GetUIComponet<SummonGroupView>(m_Root, "SummonGroup");
	}

	public override void Release()
	{

	}

	public override void Update()
	{
		m_summonGroupView.ReleaseAll();
		m_summonGroupView.Initialize(m_EDGame.GetPlayer(m_isDown).summonList);
	}
}