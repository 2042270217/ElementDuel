using ElementDuel;
using System.Collections.Generic;
using UnityEngine;

public class DeckSystem : IGameSystem
{
	List<ActionCard> m_actionCards;

	public DeckSystem(ElementDuelGame edGame, DeckConfig config) : base(edGame)
	{
		m_actionCards = new List<ActionCard>(config.actionCards);
		Initialize();
	}

	public override void Initialize()
	{
		ShuffleDeck();
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}

	/// <summary>
	/// 打乱顺序的函数
	/// </summary>
	public void ShuffleDeck()
	{
		System.Random rng = new System.Random();
		int n = m_actionCards.Count;

		// Fisher-Yates shuffle
		while (n > 1)
		{
			n--;
			int k = rng.Next(n + 1); // 随机选取一个位置
			ActionCard value = m_actionCards[k];
			m_actionCards[k] = m_actionCards[n];
			m_actionCards[n] = value;
		}

	}

	/// <summary>
	/// 顺序取出N张卡牌
	/// </summary>
	/// <param name="n"></param>
	/// <returns></returns>
	public List<ActionCard> DrawCards(int n)
	{
		// 如果n大于剩余卡牌数，则返回所有卡牌
		if (n > m_actionCards.Count)
		{
			n = m_actionCards.Count;
		}

		// 取出前n张卡牌
		List<ActionCard> drawnCards = m_actionCards.GetRange(0, n);

		// 移除已取出的卡牌
		m_actionCards.RemoveRange(0, n);

		// 创建副本并返回
		List<ActionCard> drawnCardCopies = new List<ActionCard>();
		foreach (var card in drawnCards)
		{
			drawnCardCopies.Add(ScriptableObject.Instantiate(card));
		}

		return drawnCardCopies;
	}

	/// <summary>
	/// 抽取一张牌
	/// </summary>
	/// <returns></returns>
	public ActionCard DrawCard()
	{
		ActionCard card = ScriptableObject.Instantiate(m_actionCards[0]);
		m_actionCards.RemoveAt(0);
		return card;
	}

	/// <summary>
	/// 随机插入 N 张牌
	/// </summary>
	/// <param name="cardsToInsert"></param>
	public void InsertCardsRandomly(List<ActionCard> cardsToInsert)
	{
		System.Random rng = new System.Random();
		int n = cardsToInsert.Count;

		// 插入N张卡
		for (int i = 0; i < n; i++)
		{
			int randomIndex = rng.Next(m_actionCards.Count + 1);
			m_actionCards.Insert(randomIndex, cardsToInsert[i]);
		}

	}
}
