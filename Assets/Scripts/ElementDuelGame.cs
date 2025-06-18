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
		GamePhaseControllerSystem m_phaseSystem;
		PlayerSystem m_currentPlayer;
		PlayerSystem m_enemyPlayer;
		DiceSystem m_diceSystem;

		ThrowUI m_throwUI;


		public void Initialize()
		{
			m_phaseSystem = new GamePhaseControllerSystem(this);
			m_diceSystem = new DiceSystem(this);

			m_throwUI = new ThrowUI(this, GameSetup.Instance.AssetData.DicePrefab);

			m_phaseSystem.SetPhaseState(new ThrowingPhaseState(m_phaseSystem));
		}

		public void Update()
		{
			// Test Code
			//
			if (Input.GetKeyUp(KeyCode.A))
			{
				m_phaseSystem.SetPhaseState(new ThrowingPhaseState(m_phaseSystem));
			}
			if (Input.GetKeyUp(KeyCode.S))
			{
				m_phaseSystem.SetPhaseState(new ActionPhaseState(m_phaseSystem));
			}
			if (Input.GetKeyUp(KeyCode.D))
			{
				m_phaseSystem.SetPhaseState(new EndPhaseState(m_phaseSystem));
			}
			//
			////



			m_phaseSystem.Update();
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
	}
}
