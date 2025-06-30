using System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElementDuel.GamePhase
{
	public class EndPhaseState : GamePhaseState
	{
		float duration = 3.0f;

		public EndPhaseState(GamePhaseControllerSystem controller) : base(controller)
		{
			m_gamePhase = GamePhaseEnum.EndPhase;
		}

		public override void GamePhaseEnd()
		{
			base.GamePhaseEnd();
		}

		public override void GamePhaseStart()
		{
			base.GamePhaseStart();
			var game = m_controller.EDGame;
			game.UpdateInfoUI("结束阶段");

			//结束阶段执行事件
			m_controller.ExecuteOnEndPhase();

			var currentCards = game.DrawCrads(2);
			var oppsiteCards = game.DrawCrads(2);
			game.CurrentPlayer.AddHands(currentCards);
			game.OppsitePlayer.AddHands(oppsiteCards);
		}

		public override void GamePhaseUpdate()
		{
			base.GamePhaseUpdate();
			if (duration > 0)
			{
				duration -= Time.deltaTime;
			}
			else
			{
				m_controller.EDGame.SetPhaseState(GamePhaseEnum.ThrowingPhase);
			}
		}
	}
}
