using UnityEngine;

namespace ElementDuel
{
	public abstract class IUserInterface
	{
		protected ElementDuelGame m_EDGame;
		protected GameObject m_Root;
		private bool m_active;

		public IUserInterface(ElementDuelGame edGame)
		{
			m_EDGame = edGame;
		}

		public bool IsVisible()
		{
			return m_active;
		}

		public virtual void Show()
		{
			m_Root.SetActive(true);
			m_active = true;
		}

		public virtual void Hide()
		{
			m_Root.SetActive(false);
			m_active = false;
		}

		public abstract void Initialize();

		public abstract void Update();

		public abstract void Release();
	}
}


