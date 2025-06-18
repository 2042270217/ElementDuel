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
		bool isFirst;

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

			var diceList = m_controller.EDGame.ThrowDice();
			m_controller.EDGame.ShowThrowUI(diceList);
		}

		public override void GamePhaseUpdate()
		{
			base.GamePhaseUpdate();
			if(Input.GetKeyUp(KeyCode.R))
			{
				EDebug.Log("throw again");
				var diceList = m_controller.EDGame.ThrowDice();
				m_controller.EDGame.ShowThrowUI(diceList);
			}
		}
	}
}
