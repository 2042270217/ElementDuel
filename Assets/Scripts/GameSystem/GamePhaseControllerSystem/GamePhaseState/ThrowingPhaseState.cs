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
		public static bool m_isCurrentPlayerFinished;
		public static bool m_isOppsitePlayerFinished;

		public ThrowingPhaseState(GamePhaseControllerSystem controller, bool isCurrentPlayerFinished = false, bool isOppsitePlayerFinished = false) : base(controller)
		{
			m_gamePhase = GamePhaseEnum.ThrowingPhase;
			m_isOppsitePlayerFinished = isOppsitePlayerFinished;
			m_isCurrentPlayerFinished = isCurrentPlayerFinished;
		}

		public override void GamePhaseEnd()
		{
			base.GamePhaseEnd();
		}

		public override void GamePhaseStart()
		{
			base.GamePhaseStart();
			var game = m_controller.EDGame;
			game.SetBattleUIActive(false);
			game.SetCurrentHandsUIActive(false);
			game.SetDiceUIActive(false);
			game.SetDeckActive(false);


			game.UpdateInfoUI(m_controller.EDGame.CurrentPlayer.name + "投掷阶段");

			var diceList = m_controller.EDGame.ThrowDice(true);
			m_controller.EDGame.UpdateThrowUI(diceList);
		}

		public override void GamePhaseUpdate()
		{
			base.GamePhaseUpdate();

		}

	}
}
