using ElementDuel;
using System;
using System.Collections.Generic;

public class ActionSystem : IGameSystem
{
	public ActionSystem(ElementDuelGame edGame) : base(edGame)
	{
		Initialize();
	}

	public override void Initialize()
	{

	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}

	public void HandleAction(PlayerAction action)
	{
		bool success = action.Execute();
		if (success && action.isCombatAction)
		{
			m_EDGame.ExchangeCurrent();
		}
	}
}
