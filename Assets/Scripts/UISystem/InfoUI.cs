
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ElementDuel
{
	public class InfoUI : IUserInterface
	{
		TMP_Text m_text;

		float m_timer = 0;
		float m_showTime = 1.0f;

		public InfoUI(ElementDuelGame edGame) : base(edGame)
		{
			Initialize();
		}

		public override void Initialize()
		{
			m_Root = UITools.FindUIGameObject("InfoUI");

			m_text = UITools.GetUIComponet<TMP_Text>(m_Root, "text");

			Hide();
		}

		public override void Release()
		{

		}

		public override void Update()
		{
			if (IsVisible())
			{
				if (m_timer < m_showTime)
				{
					m_timer += Time.deltaTime;
				}
				else
				{
					m_timer = 0;
					Hide();
				}
			}

		}

		public void ShowInfo(string info)
		{
			m_text.text = info;
			m_timer = 0;
			Show();
		}
	}
}
