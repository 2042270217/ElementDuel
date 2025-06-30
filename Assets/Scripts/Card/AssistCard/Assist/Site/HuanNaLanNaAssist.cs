using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "HuanNaLanNaAssist", menuName = "ElementDuel/Assist/HuanNaLanNaAssist")]
public class HuanNaLanNaAssist : Assist
{
	//结束阶段：收集最多2个未使用的元素骰。
	//行动阶段开始时：拿回此牌所收集的元素骰。

	List<ElementType> m_diceCollected = new List<ElementType>();

	public override void Initialize(PlayerSystem owner, ElementDuelGame game)
	{
		m_game = game;
		m_owner = owner;
		m_game.RegisterEndPhase(CollectDices);
		m_game.RegisterOnActionPhaseBeginning(GetDiceCollected);
	}

	public override void Release()
	{
		m_diceCollected.Clear();
		m_game.RemoveEndPhase(CollectDices);
		m_game.RemoveOnActionPhaseBeginning(GetDiceCollected);
	}

	void CollectDices()
	{
		if (m_owner.diceCount <= 2)
		{
			m_diceCollected = new List<ElementType>(m_owner.diceList);
		}
		else
		{
			m_diceCollected = new List<ElementType>();
			foreach (ElementType type in m_owner.diceList)
			{
				if (m_diceCollected.Count == 2) return;
				//优先收集非万能骰
				if (type != ElementType.All)
				{
					m_diceCollected.Add(type);
				}
			}
			//未收集满则从头补充
			for (int i = 0; i < m_owner.diceList.Count && m_diceCollected.Count < 2; i++)
			{
				m_diceCollected.Add(m_owner.diceList[i]);
			}
		}
	}

	void GetDiceCollected()
	{
		m_owner.AddDices(m_diceCollected);
		m_diceCollected.Clear();
	}
}
