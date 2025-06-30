using ElementDuel.GamePhase;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UnityEditor.Progress;

namespace ElementDuel
{
	public class StartUI : IUserInterface
	{
		GameObject m_handsGroup;
		GameObject m_handsViewPrefab;
		Button m_ackBtn;
		TMP_Text m_text;

		List<ActionCard> m_actions;
		List<HandsView> m_handsViews;

		public StartUI(ElementDuelGame edGame, GameObject handsViewPrefab) : base(edGame)
		{
			m_handsViewPrefab = handsViewPrefab;
			Initialize();
		}

		public override void Initialize()
		{
			m_Root = UITools.FindUIGameObject("StartUI");

			m_handsGroup = UnityTools.FindChildGameObject(m_Root, "HandsGroup");
			m_ackBtn = UITools.GetUIComponet<Button>(m_Root, "AckBtn");
			m_text = UITools.GetUIComponet<TMP_Text>(m_Root, "text");

			Hide();
		}

		public override void Release()
		{

		}

		public override void Update()
		{

		}

		public void ShowInfo(List<ActionCard> cards, bool canBeSelected = true)
		{
			m_actions = cards;

			Clear();
			GenerateCards(canBeSelected);
			Show();
		}

		void Clear()
		{
			if (m_handsGroup.transform.childCount == 0) return;

			foreach (Transform child in m_handsGroup.transform)
			{
				GameObject.Destroy(child.gameObject);
			}
		}

		void GenerateCards(bool canBeSelected)
		{
			List<HandsView> views = new List<HandsView>();
			foreach (var card in m_actions)
			{
				var go = GameObject.Instantiate(m_handsViewPrefab, m_handsGroup.transform);
				HandsView view = go.GetComponent<HandsView>();
				views.Add(view);
				view.Initialize(card.data, canBeSelected);
			}
			m_handsViews = views;
		}

		public List<ActionCard> GetSelectedCard()
		{
			List<ActionCard> selectedCards = new List<ActionCard>();
			for (int i = 0; i < m_handsViews.Count; i++)
			{
				if (m_handsViews[i].isSelected)
				{
					selectedCards.Add(m_actions[i]);
				}
			}
			return selectedCards;
		}

		public void AddBtnListener(UnityAction call)
		{
			m_ackBtn.onClick.AddListener(call);
		}

		public void UpdateText(string info)
		{
			m_text.text = info;
		}
	}
}
