using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;

public class QianYanLaoGuRelic : BaseRelic
{
	int getDiceCount = 1;


	public override void Initialize(BaseCharacterCard owner, ElementDuelGame game)
	{
		m_game = game;
		m_owner = owner;
	}

	public override void Release()
	{
		
	}
}
