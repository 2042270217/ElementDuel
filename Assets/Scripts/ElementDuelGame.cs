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






		public void Initialize()
		{
			m_phaseSystem = new GamePhaseControllerSystem(this);
			m_phaseSystem.Initialize();
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
	}
}
