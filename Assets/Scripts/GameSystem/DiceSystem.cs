using ElementDuel;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

			// 同一梯队：数量降序
			int countA = diceList.Count(x => x == a);
			int countB = diceList.Count(x => x == b);
			if (countA != countB) return countB.CompareTo(countA);

			// 同一梯队 + 数量相同：默认属性顺序
			int indexA = System.Array.IndexOf(DefaultElementOrder, a);
			int indexB = System.Array.IndexOf(DefaultElementOrder, b);
			return indexA.CompareTo(indexB);
		});
	}
}