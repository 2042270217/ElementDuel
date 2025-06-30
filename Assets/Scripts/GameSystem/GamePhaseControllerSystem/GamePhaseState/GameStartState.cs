using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElementDuel.GamePhase
{
	public class GameStartState : GamePhaseState
	{
		enum StartState
		{
			DrawCurrentPlayer,
			ReplaceCurrentPlayer,
			DrawOppsitePlayer,
			ReplaceOppsitePlayer,
			CurrentPlayerSelectChar,
			OppsitePlayerSelectChar,
			Finish
		}

		StartState m_currentState;

		List<ActionCard> m_cards;

		public GameStartState(GamePhaseControllerSystem controller) : base(controller)
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
			m_controller.EDGame.SetDeckActive(false);
			m_currentState = StartState.DrawCurrentPlayer;
			m_controller.EDGame.AddStartUIBtnListener(OnClickBtn);
			SwitchState();
		}

		public override void GamePhaseUpdate()
		{
			base.GamePhaseUpdate();
		}

		void SwitchState()
		{
			switch (m_currentState)
			{
				case StartState.DrawCurrentPlayer:
					DrawCurrent();
					break;
				case StartState.ReplaceCurrentPlayer:
					ReplaceCurrent();
					break;
				case StartState.DrawOppsitePlayer:
					DrawOppsite();
					break;
				case StartState.ReplaceOppsitePlayer:
					ReplaceOppsite();
					break;
				case StartState.CurrentPlayerSelectChar:
					CurrentSelect();
					break;
				case StartState.OppsitePlayerSelectChar:
					OppsiteSelect();
					break;
				case StartState.Finish:
					Finish();
					break;
			}

		}


		void OnClickBtn()
		{
			switch (m_currentState)
			{
				case StartState.DrawCurrentPlayer:
					m_currentState = StartState.ReplaceCurrentPlayer;
					break;
				case StartState.ReplaceCurrentPlayer:
					m_currentState = StartState.DrawOppsitePlayer;
					break;
				case StartState.DrawOppsitePlayer:
					m_currentState = StartState.ReplaceOppsitePlayer;
					break;
				case StartState.ReplaceOppsitePlayer:
					m_currentState = StartState.CurrentPlayerSelectChar;
					break;
			}
			SwitchState();
		}

		void DrawCurrent()
		{
			m_cards = m_controller.EDGame.DrawCrads(5);
			m_controller.EDGame.UpdateStartUI(m_cards, true);
			m_controller.EDGame.UpdateStartUIText(m_controller.EDGame.CurrentPlayer.name + "请选择想要替换的手牌");
		}

		void ReplaceCurrent()
		{
			var selectedCards = m_controller.EDGame.GetStartUISelectedCards();

			if (selectedCards.Count == 0)
			{
				m_controller.EDGame.CurrentPlayer.AddHands(m_cards);
				m_currentState = StartState.DrawOppsitePlayer;
				SwitchState();

				return;
			}

			foreach (var card in selectedCards)
			{
				m_cards.Remove(card);
			}
			var addCards = m_controller.EDGame.DrawCrads(selectedCards.Count);
			foreach (var card in addCards)
			{
				m_cards.Add(card);
			}

			m_controller.EDGame.UpdateStartUI(m_cards, false);

			m_controller.EDGame.CurrentPlayer.AddHands(m_cards);

			m_controller.EDGame.UpdateStartUIText(m_controller.EDGame.CurrentPlayer.name + "卡牌结果");

		}

		void DrawOppsite()
		{
			m_cards = m_controller.EDGame.DrawCrads(5);
			m_controller.EDGame.UpdateStartUI(m_cards, true);
			m_controller.EDGame.UpdateStartUIText(m_controller.EDGame.OppsitePlayer.name + "请选择想要替换的手牌");
		}

		void ReplaceOppsite()
		{
			var selectedCards = m_controller.EDGame.GetStartUISelectedCards();

			if (selectedCards.Count == 0)
			{
				m_controller.EDGame.OppsitePlayer.AddHands(m_cards);
				m_currentState = StartState.CurrentPlayerSelectChar;
				SwitchState();

				return;
			}

			foreach (var card in selectedCards)
			{
				m_cards.Remove(card);
			}
			var addCards = m_controller.EDGame.DrawCrads(selectedCards.Count);
			foreach (var card in addCards)
			{
				m_cards.Add(card);
			}

			m_controller.EDGame.UpdateStartUI(m_cards, false);

			m_controller.EDGame.OppsitePlayer.AddHands(m_cards);

			m_controller.EDGame.UpdateStartUIText(m_controller.EDGame.OppsitePlayer.name + "卡牌结果");
		}

		void CurrentSelect()
		{
			var game = m_controller.EDGame;
			game.HideStartUI();
			game.UpdateInfoUI(game.CurrentPlayer.name + "选择出战角色");
			var charUI = game.GetCharacterUI(game.CurrentPlayer);
			charUI.Show();
			charUI.RegisterCharDoubleClick(OnCharacterDoubleClick);
		}

		void OppsiteSelect()
		{
			var game = m_controller.EDGame;
			game.UpdateInfoUI(game.CurrentPlayer.name + "选择出战角色");
			var charUI = game.GetCharacterUI(game.CurrentPlayer);
			charUI.Show();
			charUI.RegisterCharDoubleClick(OnCharacterDoubleClick);
		}

		void Finish()
		{
			m_controller.SetPhaseState(new ThrowingPhaseState(m_controller));
		}

		void OnCharacterDoubleClick(BaseCharacterCard character)
		{
			var game = m_controller.EDGame;

			game.CurrentPlayer.SetFightingCharacter(character);
			var charUI = game.GetCharacterUI(game.CurrentPlayer);
			charUI.SetFightingCharacter(character);
			charUI.Hide();
			charUI.RemoveCharDoubleClick(OnCharacterDoubleClick);
			switch (m_currentState)
			{
				case StartState.CurrentPlayerSelectChar:
					m_currentState = StartState.OppsitePlayerSelectChar;
					break;
				case StartState.OppsitePlayerSelectChar:
					m_currentState = StartState.Finish;
					break;
			}
			game.ExchangeCurrent(false);
			SwitchState();
		}
	}
}