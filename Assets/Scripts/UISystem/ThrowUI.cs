using ElementDuel.GamePhase;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
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
				DiceView diceView = child.GetComponent<DiceView>();
				if (diceView.isSelected)
				{
					ElementType e = diceView.elementType;
					removeElements.Add(e);
				}
			}

			if (removeElements.Count > 0) shouldReThrow = true;

			if (!ThrowingPhaseState.m_isCurrentPlayerFinished)
			{
				//currentPlayer正在投掷
				if (shouldReThrow && m_EDGame.CurrentPlayer.CanRethrow())
				{
					//重投流程
					m_EDGame.ReThrowDice(m_diceList, removeElements, true);
					m_EDGame.CurrentPlayer.m_countRethrowed++;
					Clear();
					GenerateChildDice(m_EDGame.CurrentPlayer.CanRethrow());
				}
				else
				{
					//currentPlayer结束投掷
					ThrowingPhaseState.m_isCurrentPlayerFinished = true;
					m_EDGame.CurrentPlayer.SetDices(m_diceList);
					Hide();
					m_EDGame.UpdateInfoUI(m_EDGame.OppsitePlayer.name + "投掷阶段");
					var diceList = m_EDGame.ThrowDice(false);
					ShowInfo(diceList);
				}
			}
			else if (ThrowingPhaseState.m_isCurrentPlayerFinished && !ThrowingPhaseState.m_isOppsitePlayerFinished)
			{
				//oppsitePlayer正在投掷
				if (shouldReThrow && m_EDGame.OppsitePlayer.CanRethrow())
				{
					//重投流程
					m_EDGame.ReThrowDice(m_diceList, removeElements, false);
					m_EDGame.OppsitePlayer.m_countRethrowed++;
					Clear();
					GenerateChildDice(m_EDGame.OppsitePlayer.CanRethrow());
				}
				else
				{
					//oppsitePlayer结束投掷
					ThrowingPhaseState.m_isOppsitePlayerFinished = true;
					m_EDGame.OppsitePlayer.SetDices(m_diceList);
					Hide();
					m_EDGame.SetPhaseState(GamePhaseEnum.ActionPhase);
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
			Show();

			m_diceList = dices;
			GenerateChildDice();

		}

		void GenerateChildDice(bool canBeClicked = true)
		{
			foreach (ElementType dice in m_diceList)
			{

				var item = GameObject.Instantiate(m_dicePrefab, m_diceGroup.transform);
				item.transform.localScale = Vector3.one * 3;
				DiceView diceView = item.GetComponent<DiceView>();
				diceView.Initialize(canBeClicked, dice);
			}
		}

	}
}
