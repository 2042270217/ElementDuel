using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ElementDuel.GamePhase;

namespace ElementDuel
{
	public class GamePhaseControllerSystem : IGameSystem
	{
		GamePhaseState m_phaseState;

		public event Action OnEndPhase;
		public event Action OnActionPhaseBeginning;

		public ElementDuelGame EDGame
		{
			get
			{
				return m_EDGame;
			}
		}

		public GamePhaseControllerSystem(ElementDuelGame edGame) : base(edGame)
		{
			Initialize();
		}

		public void SetPhaseState(GamePhaseState phaseState)
		{
			if (m_phaseState == null)
			{
				m_phaseState = phaseState;
				phaseState.GamePhaseStart();
			}
			else
			{
				if (phaseState.CurrentGamePhase == m_phaseState.CurrentGamePhase)
				{
					return;
				}
				m_phaseState.GamePhaseEnd();
				m_phaseState = phaseState;
				phaseState.GamePhaseStart();
			}

		}

		public void PhaseStateUpdate()
		{
			if (m_phaseState != null)
			{
				m_phaseState.GamePhaseUpdate();
			}
		}

		public override void Initialize()
		{

		}

		public override void Update()
		{
			PhaseStateUpdate();
		}

		public override void Release()
		{

		}

		public void ExecuteOnEndPhase()
		{
			OnEndPhase?.Invoke();
		}

		public void ExecuteOnActionPhaseBeginning()
		{
			OnActionPhaseBeginning?.Invoke();
		}
	}
}
