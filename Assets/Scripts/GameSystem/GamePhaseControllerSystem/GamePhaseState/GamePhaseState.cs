using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace ElementDuel.GamePhase
{
	public class GamePhaseState
	{
		protected GamePhaseEnum m_gamePhase;
		public GamePhaseEnum CurrentGamePhase
		{
			get
			{
				return m_gamePhase;
			}
		}
		protected GamePhaseControllerSystem m_controller;
		public GamePhaseState(GamePhaseControllerSystem controller)
		{
			m_controller = controller;
		}

		public virtual void GamePhaseStart()
		{
			EDebug.Log(m_gamePhase + ": Start", LogLevel.GamePhaseState);
		}

		public virtual void GamePhaseUpdate()
		{
			EDebug.Log(m_gamePhase + ": Update", LogLevel.GamePhaseState);
		}

		public virtual void GamePhaseEnd()
		{
			EDebug.Log(m_gamePhase + ": End", LogLevel.GamePhaseState);
		}
	}
}
