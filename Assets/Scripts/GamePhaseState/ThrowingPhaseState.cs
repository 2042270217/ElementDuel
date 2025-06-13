using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElementDuel.GamePhase
{
	public class ThrowingPhaseState : GamePhaseState
	{
		public ThrowingPhaseState(GamePhaseControllerSystem controller) : base(controller)
		{
			m_gamePhase = GamePhaseEnum.ThrowingPhase;
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
