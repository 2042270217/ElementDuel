using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace ElementDuel
{
	public class ThrowUI : IUserInterface
	{
		GameObject m_diceGroup;
		GameObject m_dicePrefab;
		Button m_ackBtn;

		List<ElementType> m_diceList;

		public ThrowUI(ElementDuelGame edGame, GameObject dicePrefab) : base(edGame)
		{
			m_dicePrefab = dicePrefab;
			Initialize();
		}

		public override void Initialize()
		{
			m_Root = UITools.FindUIGameObject("ThrowUI");

			m_diceGroup = UnityTools.FindChildGameObject(m_Root, "DiceGroup");
			m_ackBtn = UITools.GetUIComponet<Button>(m_Root, "AckBtn");

			m_ackBtn.onClick.AddListener(ClickAckBtn);
			Hide();
		}

		private void ClickAckBtn()
		{
			bool shouldReThrow = false;
			List<ElementType> removeElements = new List<ElementType>();
			foreach (Transform child in m_diceGroup.transform)
			{
				if (child.GetComponent<DiceView>().isSelected)
				{
					shouldReThrow = true;
					ElementType e = UnityTools.FindChildGameObject(child.gameObject, "Image").GetComponent<ElementDiceSetup>().elementType;
					removeElements.Add(e);
				}

			}

			if (!shouldReThrow)
			{
				Hide();
				return;
			}
			else
			{
				m_EDGame.ReThrowDice(m_diceList, removeElements);

				Clear();
				foreach (ElementType dice in m_diceList)
				{

					var item = GameObject.Instantiate(m_dicePrefab, m_diceGroup.transform);
					item.transform.localScale = Vector3.one * 3;
					item.GetComponent<DiceView>().CanBeClicked = true;
					//根据元素设置骰子样式
					var go = UnityTools.FindChildGameObject(item, "Image");
					go.GetComponent<ElementDiceSetup>().elementType = dice;
				}
			}
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

			m_diceList = dices;
			foreach (ElementType dice in dices)
			{

				var item = GameObject.Instantiate(m_dicePrefab, m_diceGroup.transform);
				item.transform.localScale = Vector3.one * 3;
				item.GetComponent<DiceView>().CanBeClicked = true;
				//根据元素设置骰子样式
				var go = UnityTools.FindChildGameObject(item, "Image");
				go.GetComponent<ElementDiceSetup>().elementType = dice;
			}

			Show();
		}

	}
}
