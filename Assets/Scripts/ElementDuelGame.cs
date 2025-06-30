using ElementDuel.GamePhase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

namespace ElementDuel
{
	public class ElementDuelGame
	{
		InputSystem m_inputSystem;
		GamePhaseControllerSystem m_phaseSystem;
		PlayerSystem m_player1;
		PlayerSystem m_player2;
		PlayerSystem m_currentPlayer;
		DiceSystem m_diceSystem;
		DeckSystem m_deckSystem;
		ActionSystem m_actionSystem;


		ThrowUI m_throwUI;
		InfoUI m_infoUI;
		BattleUI m_battleUI;
		StartUI m_startUI;
		DiceUI m_diceUI;
		CurrentHandsUI m_currentHandsUI;
		CharacterUI m_p1CharUI;
		CharacterUI m_p2CharUI;
		SkillUI m_skillUI;
		AssistUI m_p1AssistUI;
		AssistUI m_p2AssistUI;
		SummonUI m_p1SummonUI;
		SummonUI m_p2SummonUI;

		PlayerSystem m_firstMovePlayer;

		public PlayerSystem FirstMovePlayer
		{
			get { return m_firstMovePlayer; }
			set { m_firstMovePlayer = value; }
		}

		public PlayerSystem CurrentPlayer
		{
			get { return m_currentPlayer; }
		}

		public PlayerSystem OppsitePlayer
		{
			get { return m_currentPlayer == m_player1 ? m_player2 : m_player1; }
		}

		public void Initialize()
		{
			//System初始化
			m_inputSystem = new InputSystem(this);
			m_phaseSystem = new GamePhaseControllerSystem(this);
			m_diceSystem = new DiceSystem(this);
			m_player1 = new PlayerSystem(this, "玩家一", GameSetup.Instance.AssetData.Config.player1Char);
			m_player2 = new PlayerSystem(this, "玩家二", GameSetup.Instance.AssetData.Config.player2Char);
			m_deckSystem = new DeckSystem(this, GameSetup.Instance.AssetData.Config);
			m_actionSystem = new ActionSystem(this);

			//游戏设置
			m_currentPlayer = m_player1;

			//UI初始化
			m_throwUI = new ThrowUI(this, GameSetup.Instance.AssetData.DicePrefab);
			m_infoUI = new InfoUI(this);
			m_battleUI = new BattleUI(this);
			m_startUI = new StartUI(this, GameSetup.Instance.AssetData.HandsViewPrefab);
			m_diceUI = new DiceUI(this);
			m_currentHandsUI = new CurrentHandsUI(this);
			m_p1CharUI = new CharacterUI(this, true);
			m_p2CharUI = new CharacterUI(this, false);
			m_skillUI = new SkillUI(this);
			m_p1AssistUI = new AssistUI(this, true);
			m_p2AssistUI = new AssistUI(this, false);
			m_p1SummonUI = new SummonUI(this, true);
			m_p2SummonUI = new SummonUI(this, false);



			m_phaseSystem.SetPhaseState(new GameStartState(m_phaseSystem));

		}

		public void Update()
		{

			m_inputSystem.Update();

			m_phaseSystem.Update();


			m_infoUI.Update();

#if UNITY_EDITOR

			if (Input.GetKeyDown(KeyCode.Alpha1))
			{
				CurrentPlayer.AddDice(ElementType.All);
				m_diceUI.Update();
			}
			else if (Input.GetKeyDown(KeyCode.Alpha2))
			{
				CurrentPlayer.fightingCharecter.ReceiveDamage(1, ElementType.None);
			}

#endif
		}

		public void Release()
		{

		}

		#region DiceSystem

		public List<ElementType> ThrowDice(bool isCurrentPlayer)
		{
			return DiceSystem.GenerateAndSortDice(8, isCurrentPlayer ? CurrentPlayer.specialElement : OppsitePlayer.specialElement);
		}

		public void ReThrowDice(List<ElementType> diceList, List<ElementType> removeDiceList, bool isCurrentPlayer)
		{
			foreach (ElementType e in removeDiceList)
			{
				diceList.Remove(e);
			}

			List<ElementType> charElement = isCurrentPlayer ? CurrentPlayer.specialElement : OppsitePlayer.specialElement;
			DiceSystem.AddDice(removeDiceList.Count, diceList, charElement);
		}

		#endregion

		#region PhaseControlSystem

		public void SetPhaseState(GamePhaseEnum gamePhase)
		{
			switch (gamePhase)
			{
				case GamePhaseEnum.ThrowingPhase:
					m_phaseSystem.SetPhaseState(new ThrowingPhaseState(m_phaseSystem));
					break;
				case GamePhaseEnum.ActionPhase:
					m_phaseSystem.SetPhaseState(new ActionPhaseState(m_phaseSystem));
					break;
				case GamePhaseEnum.EndPhase:
					m_phaseSystem.SetPhaseState(new EndPhaseState(m_phaseSystem));
					break;
				case GamePhaseEnum.GameStart:
					m_phaseSystem.SetPhaseState(new GameStartState(m_phaseSystem));
					break;
				case GamePhaseEnum.GameEnd:
					m_phaseSystem.SetPhaseState(new GameEndSate(m_phaseSystem));
					break;
			}

		}

		public void RegisterEndPhase(Action call)
		{
			m_phaseSystem.OnEndPhase += call;
		}

		public void RemoveEndPhase(Action call)
		{
			m_phaseSystem.OnEndPhase -= call;
		}

		public void RegisterOnActionPhaseBeginning(Action call)
		{
			m_phaseSystem.OnActionPhaseBeginning += call;
		}

		public void RemoveOnActionPhaseBeginning(Action call)
		{
			m_phaseSystem.OnActionPhaseBeginning -= call;
		}

		#endregion

		#region DeckSystem

		public List<ActionCard> DrawCrads(int n)
		{
			return m_deckSystem.DrawCards(n);
		}

		public void InsertCards(List<ActionCard> cards)
		{
			m_deckSystem.InsertCardsRandomly(cards);
		}

		#endregion

		#region PlayerSystem



		#endregion

		#region ActionSystem

		public void HandleAction(PlayerAction action)
		{
			m_actionSystem.HandleAction(action);
		}

		#endregion




		#region ThrowUI

		public void UpdateThrowUI(List<ElementType> diceList)
		{
			m_throwUI.ShowInfo(diceList);
		}

		#endregion

		#region InfoUI

		public void UpdateInfoUI(string info)
		{
			m_infoUI.ShowInfo(info);
		}

		#endregion

		#region CharacterUI

		public CharacterUI GetCharacterUI(PlayerSystem player)
		{
			return player == m_player1 ? m_p1CharUI : m_p2CharUI;
		}

		public void SetCharacterUIActive(bool active)
		{
			if (active)
			{
				m_p1CharUI.Show();
				m_p2CharUI.Show();
			}
			else
			{
				m_p1CharUI.Hide();
				m_p2CharUI.Hide();
			}
		}

		#endregion

		#region DiceUI

		public void SetDiceUIActive(bool active)
		{
			if (active)
				m_diceUI.Show();
			else
				m_diceUI.Hide();
		}

		/// <summary>
		/// 用于重建DiceUI
		/// </summary>
		public void UpdateDiceUI()
		{
			m_diceUI.Update();
		}

		#endregion

		#region CurrentHandsUI

		public void SetCurrentHandsUIActive(bool active)
		{
			if (active)
				m_currentHandsUI.Show();
			else
				m_currentHandsUI.Hide();
		}

		/// <summary>
		/// 用于重建CurrentHandsUI
		/// </summary>
		public void UpdateCurrentHandsUI()
		{
			m_currentHandsUI.Update();
		}

		public void RegisterHandsDoubleClick(Action<ActionCard> call)
		{
			m_currentHandsUI.RegisterDoubleClick(call);
		}

		public void RemoveHandsDoubleClick(Action<ActionCard> call)
		{
			m_currentHandsUI.RemoveDoubleClick(call);
		}

		#endregion

		#region Deck

		public void SetDeckActive(bool active)
		{
			if (active)
				ShowAllDeck();
			else
				HideAllDeck();
		}

		void ShowAllDeck()
		{
			m_p1CharUI.Show();
			m_p2CharUI.Show();
		}

		void HideAllDeck()
		{
			m_p1CharUI.Hide();
			m_p2CharUI.Hide();
		}

		public void ClearSelection()
		{
			m_p1CharUI.ClearSelection();
			m_p2CharUI.ClearSelection();
			m_currentHandsUI.ClearSelection();
			m_skillUI.ClearSelection();
		}

		#endregion

		#region StartUI

		public void UpdateStartUI(List<ActionCard> cards, bool canBeSelected)
		{
			m_startUI.ShowInfo(cards, canBeSelected);
		}

		public List<ActionCard> GetStartUISelectedCards()
		{
			return m_startUI.GetSelectedCard();
		}

		public void AddStartUIBtnListener(UnityAction call)
		{
			m_startUI.AddBtnListener(call);
		}

		public void UpdateStartUIText(string info)
		{
			m_startUI.UpdateText(info);
		}

		public void HideStartUI()
		{
			m_startUI.Hide();
		}

		#endregion

		#region SkillUI

		public void SetSkillUIActive(bool active)
		{
			if (active)
				m_skillUI.Show();
			else
				m_skillUI.Hide();
		}

		public void UpdateSkillUI()
		{
			m_skillUI.Update();
		}

		public void RegisterSkillDoubleClick(Action<Skill> call)
		{
			m_skillUI.RegisterSkillDoubleClick(call);
		}

		public void RemoveSkillDoubleClick(Action<Skill> call)
		{
			m_skillUI.RemoveSkillDoubleClick(call);
		}

		#endregion

		#region BattleUI

		public void SetBattleUIActive(bool active)
		{
			if (active)
				m_battleUI.Show();
			else
				m_battleUI.Hide();
		}

		public void AddChangeBtnListener(UnityAction call)
		{
			m_battleUI.AddChangeBtnListener(call);
		}

		public void RemoveChangeBtnListener(UnityAction call)
		{
			m_battleUI.RemoveChangeBtnListener(call);
		}

		#endregion

		#region AssistUI

		public void SetAssistUIActive(bool active)
		{
			if (active)
			{
				m_p1AssistUI.Show();
				m_p2AssistUI.Show();
			}
			else
			{
				m_p1AssistUI.Hide();
				m_p2AssistUI.Hide();
			}
		}

		public void UpdateAssistUI()
		{
			m_p1AssistUI.Update();
			m_p2AssistUI.Update();
		}

		public void UpdateAssistUI(bool isCurrent)
		{
			GetAssistUI(isCurrent ? CurrentPlayer : OppsitePlayer).Update();
		}

		public AssistUI GetAssistUI(PlayerSystem player)
		{
			return player == m_player1 ? m_p1AssistUI : m_p2AssistUI;
		}

		#endregion

		#region SummonUI

		public void SetSummonUIActive(bool active)
		{
			if (active)
			{
				m_p1SummonUI.Show();
				m_p2SummonUI.Show();
			}
			else
			{
				m_p1SummonUI.Hide();
				m_p2SummonUI.Hide();
			}

		}

		public SummonUI GetSummonUI(PlayerSystem player)
		{
			return player == m_player1 ? m_p1SummonUI : m_p2SummonUI;
		}

		#endregion

		public PlayerSystem GetPlayer(bool isDown)
		{
			return isDown ? m_player1 : m_player2;
		}

		public void ExchangeCurrent(bool isInActionPhase = true)
		{
			if (!OppsitePlayer.isTurnOver)
			{
				m_currentPlayer = OppsitePlayer;
			}

			if (isInActionPhase)
			{
				UpdateSkillUI();
				UpdateCurrentHandsUI();
				UpdateDiceUI();
				UpdateInfoUI(CurrentPlayer.name + "行动");
				var oppsiteUI = GetCharacterUI(OppsitePlayer);
				var currentUI = GetCharacterUI(CurrentPlayer);
				oppsiteUI.SetCharacterClick(false);
				currentUI.SetCharacterClick(true);
			}
		}

		public void SetCurrentPlayer(PlayerSystem player)
		{
			m_currentPlayer = player;
		}

	}
}
