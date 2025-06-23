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


		ThrowUI m_throwUI;
		InfoUI m_infoUI;
		BattleUI m_battleUI;

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
			m_player1 = new PlayerSystem(this, "玩家一");
			m_player2 = new PlayerSystem(this, "玩家二");

			//UI初始化
			m_throwUI = new ThrowUI(this, GameSetup.Instance.AssetData.DicePrefab);
			m_infoUI = new InfoUI(this);
			m_battleUI = new BattleUI(this, GameSetup.Instance.AssetData.DicePrefab);

			//游戏设置
			m_currentPlayer = m_player1;
			m_player1.m_specialElement.Add(ElementType.Water);
			m_player1.m_specialElement.Add(ElementType.Wind);

			m_player2.m_specialElement.Add(ElementType.Ice);
			m_player2.m_specialElement.Add(ElementType.Wind);


			m_phaseSystem.SetPhaseState(new ThrowingPhaseState(m_phaseSystem));

		}

		public void Update()
		{

			m_inputSystem.Update();

			m_phaseSystem.Update();

			m_infoUI.Update();
		}

		public void Release()
		{

		}


		public void UpdateThrowUI(List<ElementType> diceList)
		{
			m_throwUI.ShowInfo(diceList);
		}

		public List<ElementType> ThrowDice(bool isCurrentPlayer)
		{
			return DiceSystem.GenerateAndSortDice(8, isCurrentPlayer ? CurrentPlayer.m_specialElement : OppsitePlayer.m_specialElement);
		}

		public void ReThrowDice(List<ElementType> diceList, List<ElementType> removeDiceList, bool isCurrentPlayer)
		{
			foreach (ElementType e in removeDiceList)
			{
				diceList.Remove(e);
			}

			List<ElementType> charElement = isCurrentPlayer ? CurrentPlayer.m_specialElement : OppsitePlayer.m_specialElement;
			DiceSystem.AddDice(removeDiceList.Count, diceList, charElement);
		}

		public void UpdateInfoUI(string info)
		{
			m_infoUI.ShowInfo(info);
		}

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
			}

		}

		public void UpdateDiceListUI()
		{
			m_battleUI.UpdateDiceList();
		}

	}
}
