using ElementDuel;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class DiceSystem : IGameSystem
{
	// 默认属性优先顺序
	private static readonly ElementType[] DefaultElementOrder =
	{
		ElementType.Fire,
		ElementType.Water,
		ElementType.Wind,
		ElementType.Thunder,
		ElementType.Grass,
		ElementType.Ice,
		ElementType.Rock
	};

	public DiceSystem(ElementDuelGame edGame) : base(edGame)
	{
		Initialize();
	}

	public override void Initialize()
	{
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}

	public static void AddDice(int count, List<ElementType> diceList, List<ElementType> specialElements)
	{
		for (int i = 0; i < count; i++)
		{
			var e = GetRandomElement();
			diceList.Add(e);
		}

		SortDiceList(diceList, specialElements);
	}

	public static List<ElementType> GenerateAndSortDice(int count, List<ElementType> specialElements)
	{
		var diceList = new List<ElementType>();
		for (int i = 0; i < count; i++)
		{
			var e = GetRandomElement();
			diceList.Add(e);
		}

		SortDiceList(diceList, specialElements);

		return diceList;
	}

	static ElementType GetRandomElement()
	{
		int random = Random.Range(0, 8);
		if (random == 7)
		{
			return ElementType.All;
		}
		else
		{
			random = 1 << random;
			return (ElementType)random;
		}
	}

	public static void SortDiceList(List<ElementType> diceList, List<ElementType> specialElements)
	{
		// ✅ 预统计每种骰子的出现次数
		var freq = diceList.GroupBy(e => e).ToDictionary(g => g.Key, g => g.Count());

		diceList.Sort((a, b) =>
		{
			int GetTier(ElementType e)
			{
				if (e == ElementType.All) return 0;
				if (specialElements.Contains(e)) return 1;
				return 2;
			}

			int tierA = GetTier(a);
			int tierB = GetTier(b);
			if (tierA != tierB) return tierA.CompareTo(tierB);

			// ✅ 使用预统计的频次，确保稳定
			int countA = freq[a];
			int countB = freq[b];
			if (countA != countB) return countB.CompareTo(countA);

			// ✅ 默认顺序（火 > 水 > 风 > 雷 > 草 > 冰 > 岩）
			int indexA = Array.IndexOf(DefaultElementOrder, a);
			int indexB = Array.IndexOf(DefaultElementOrder, b);
			return indexA.CompareTo(indexB);
		});
	}
}