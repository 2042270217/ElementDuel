using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElementDuel
{
	public abstract class IGameSystem
	{
		protected ElementDuelGame m_EDGame;
		public IGameSystem(ElementDuelGame edGame)
		{
			m_EDGame = edGame;
		}

		public abstract void Initialize();

		public abstract void Update();

		public abstract void Release();
	}
}
