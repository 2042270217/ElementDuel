using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElementDuel.GamePhase
{
	public class ActionPhaseState : GamePhaseState
	{
		public ActionPhaseState(GamePhaseControllerSystem controller) : base(controller)
		{
			m_gamePhase = GamePhaseEnum.ActionPhase;
		}

		public override void GamePhaseEnd()
		{
			base.GamePhaseEnd();
		}

		public override void GamePhaseStart()
		{
			base.GamePhaseStart();
			m_controller.EDGame.UpdateInfoUI("行动阶段");
			m_controller.EDGame.UpdateDiceListUI();
		}

		public override void GamePhaseUpdate()
		{
			base.GamePhaseUpdate();
		}
	}
}
