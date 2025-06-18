using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

namespace ElementDuel
{
	public class ThrowUI : IUserInterface
	{
		GameObject m_diceGroup;
		GameObject m_dicePrefab;

		public ThrowUI(ElementDuelGame edGame, GameObject dicePrefab) : base(edGame)
		{
			m_dicePrefab = dicePrefab;
			Initialize();
		}

		public override void Initialize()
		{
			m_Root = UITools.FindUIGameObject("ThrowUI");

			m_diceGroup = UnityTools.FindChildGameObject(m_Root, "DiceGroup");

			Hide();
		}

		public override void Release()
		{

		}

		public override void Update()
		{

		}

		void Clear()
		{
			foreach (Transform child in m_diceGroup.transform)
			{
				if (child != null)
				{
					GameObject.Destroy(child.gameObject);
				}
			}
		}

		public void ShowInfo(List<ElementType> dices)
		{
			Clear();

			foreach (ElementType dice in dices)
			{
				var item = GameObject.Instantiate(m_dicePrefab, m_diceGroup.transform);
				item.transform.localScale = Vector3.one * 3;
				var go = UnityTools.FindChildGameObject(item, "Image");
				go.GetComponent<ElementDiceSetup>().elementType = dice;
			}

			Show();
		}

	}
}
