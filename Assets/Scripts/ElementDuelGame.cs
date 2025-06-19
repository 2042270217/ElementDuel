using ElementDuel.GamePhase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElementDuel
{
	public class ElementDuelGame
	{
		InputSystem m_inputSystem;
		GamePhaseControllerSystem m_phaseSystem;
		PlayerSystem m_currentPlayer;
		PlayerSystem m_enemyPlayer;
		DiceSystem m_diceSystem;

		ThrowUI m_throwUI;
		InfoUI m_infoUI;

		public void Initialize()
		{
			m_inputSystem = new InputSystem(this);
			m_phaseSystem = new GamePhaseControllerSystem(this);
			m_diceSystem = new DiceSystem(this);

			m_throwUI = new ThrowUI(this, GameSetup.Instance.AssetData.DicePrefab);
			m_infoUI = new InfoUI(this);

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


		public void ShowThrowUI(List<ElementType> diceList)
		{
			m_throwUI.ShowInfo(diceList);
		}

		public List<ElementType> ThrowDice()
		{
			List<ElementType> charElement = new List<ElementType>();
			charElement.Add(ElementType.Water);
			charElement.Add(ElementType.Ice);
			return DiceSystem.GenerateAndSortDice(8, charElement);
		}

		public void ReThrowDice(List<ElementType> diceList, List<ElementType> removeDiceList)
		{
			foreach (ElementType e in removeDiceList)
			{
				diceList.Remove(e);
				EDebug.Log(e.ToString());
			}

			List<ElementType> charElement = new List<ElementType>();
			charElement.Add(ElementType.Water);
			charElement.Add(ElementType.Ice);
			DiceSystem.AddDice(removeDiceList.Count, diceList, charElement);
		}

		public void ShowInfo(string info)
		{
			m_infoUI.ShowInfo(info);
		}
	}
}
