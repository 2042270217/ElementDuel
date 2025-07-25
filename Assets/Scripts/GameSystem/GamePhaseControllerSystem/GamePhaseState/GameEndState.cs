﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElementDuel.GamePhase
{
	public class GameEndSate : GamePhaseState
	{
		public GameEndSate(GamePhaseControllerSystem controller) : base(controller)
		{
			m_gamePhase = GamePhaseEnum.GameStart;
		}

		public override void GamePhaseEnd()
		{
			base.GamePhaseEnd();
		}

		public override void GamePhaseStart()
		{
			base.GamePhaseStart();
		}

		public override void GamePhaseUpdate()
		{
			base.GamePhaseUpdate();
		}
	}
}
