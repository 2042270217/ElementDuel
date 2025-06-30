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
			var game = m_controller.EDGame;
			game.SetDiceUIActive(false);
			game.SetBattleUIActive(false);
			game.RemoveChangeBtnListener(OnChangeBtnClick);

			var currentUI = game.GetCharacterUI(game.CurrentPlayer);
			currentUI.RemoveCharDoubleClick(OnCharacterDoubleClick);
			var oppsiteUI = game.GetCharacterUI(game.OppsitePlayer);
			oppsiteUI.RemoveCharDoubleClick(OnCharacterDoubleClick);

			game.RemoveHandsDoubleClick(OnHandsDoubleClick);
		}

		public override void GamePhaseStart()
		{
			base.GamePhaseStart();
			var game = m_controller.EDGame;
			//行动阶段开始时事件
			m_controller.ExecuteOnActionPhaseBeginning();

			game.SetDiceUIActive(true);
			game.SetDeckActive(true);
			game.SetSkillUIActive(true);
			game.SetCurrentHandsUIActive(true);
			game.SetBattleUIActive(true);
			game.AddChangeBtnListener(OnChangeBtnClick);

			game.UpdateSkillUI();
			game.UpdateCurrentHandsUI();
			game.UpdateDiceUI();
			game.UpdateInfoUI(game.CurrentPlayer.name + "行动");

			var currentUI = game.GetCharacterUI(game.CurrentPlayer);
			currentUI.SetCharacterClick(true);
			currentUI.RegisterCharDoubleClick(OnCharacterDoubleClick);
			var oppsiteUI = game.GetCharacterUI(game.OppsitePlayer);
			oppsiteUI.SetCharacterClick(false);
			oppsiteUI.RegisterCharDoubleClick(OnCharacterDoubleClick);

			game.RegisterHandsDoubleClick(OnHandsDoubleClick);
			game.RegisterSkillDoubleClick(OnSkillDoubleClick);

		}

		public override void GamePhaseUpdate()
		{
			base.GamePhaseUpdate();
		}


		void OnCharacterDoubleClick(BaseCharacterCard character)
		{
			var game = m_controller.EDGame;
			var action = new SwitchCharacterAction(true, game, character);
			game.HandleAction(action);
		}

		void OnChangeBtnClick()
		{
			var game = m_controller.EDGame;
			game.CurrentPlayer.SetTurnOver(true);
			if (game.OppsitePlayer.isTurnOver)
			{
				game.SetCurrentPlayer(game.FirstMovePlayer);
				game.CurrentPlayer.SetTurnOver(false);
				game.OppsitePlayer.SetTurnOver(false);
				game.SetPhaseState(GamePhaseEnum.EndPhase);
			}
			else
			{
				game.FirstMovePlayer = game.CurrentPlayer;
				game.ExchangeCurrent();
			}
		}

		void OnHandsDoubleClick(ActionCard hands)
		{
			var game = m_controller.EDGame;
			var action = new CardAction(false, game, hands);
			game.HandleAction(action);
		}

		void OnSkillDoubleClick(Skill skill)
		{
			var game = m_controller.EDGame;
			bool isBurst = false;

			foreach (var cdt in skill.skillData.cdts)
			{
				if (cdt.type == ElementCostType.Energy)
				{
					isBurst = true;
				}
			}

			if (isBurst)
			{
				if (game.CurrentPlayer.fightingCharecter.canUseBurst)
				{
					var action = new SkillAction(true, game, skill);
					game.HandleAction(action);
				}
				else
				{
					game.UpdateInfoUI("能量不足");
				}
			}
			else
			{
				var action = new SkillAction(true, game, skill);
				game.HandleAction(action);
			}
		}
	}
}
