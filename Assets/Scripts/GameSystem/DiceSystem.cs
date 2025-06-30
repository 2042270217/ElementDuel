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
		//预统计每种骰子的出现次数
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

			//使用预统计的频次，确保稳定
			int countA = freq[a];
			int countB = freq[b];
			if (countA != countB) return countB.CompareTo(countA);

			//默认顺序（火 > 水 > 风 > 雷 > 草 > 冰 > 岩）
			int indexA = Array.IndexOf(DefaultElementOrder, a);
			int indexB = Array.IndexOf(DefaultElementOrder, b);
			return indexA.CompareTo(indexB);
		});
	}

	public static bool TryToCostDice(List<SkillCostCondition> cdts, List<ElementType> playerDices, List<ElementType> specialElements)
	{
		HashSet<int> used = new();        // 标记哪些骰子被使用了
		List<int> indicesToRemove = new();

		foreach (var cdt in cdts)
		{
			switch (cdt.type)
			{
				case ElementCostType.Specific:
					{
						int need = cdt.count;
						ElementType target = cdt.element;

						// 优先从后往前用指定元素
						for (int i = playerDices.Count - 1; i >= 0 && need > 0; i--)
						{
							if (playerDices[i] == target && !used.Contains(i))
							{
								used.Add(i);
								indicesToRemove.Add(i);
								need--;
							}
						}

						// 然后从前往后补万能骰
						for (int i = 0; i < playerDices.Count && need > 0; i++)
						{
							if (playerDices[i] == ElementType.All && !used.Contains(i))
							{
								used.Add(i);
								indicesToRemove.Add(i);
								need--;
							}
						}

						if (need > 0)
							return false;

						break;
					}

				case ElementCostType.Same:
					{
						int need = cdt.count;

						// 所有未使用的非万能元素分组（按元素）
						var grouped = playerDices
							.Select((e, idx) => new { e, idx })
							.Where(x => !used.Contains(x.idx) && x.e != ElementType.All)
							.GroupBy(x => x.e);

						List<int> bestPlan = null;
						int minAllUsed = int.MaxValue;

						foreach (var group in grouped)
						{
							var normal = group.OrderByDescending(x => x.idx).ToList(); // 后往前
							int normalCount = normal.Count;
							int needAll = Math.Max(0, need - normalCount);

							// 查找万能骰（从前往后）
							var all = playerDices
								.Select((e, idx) => new { e, idx })
								.Where(x => x.e == ElementType.All && !used.Contains(x.idx))
								.OrderBy(x => x.idx)
								.ToList();

							if (normalCount + all.Count >= need)
							{
								var plan = new List<int>();
								plan.AddRange(normal.Take(need).Select(x => x.idx));
								plan.AddRange(all.Take(needAll).Select(x => x.idx));

								if (needAll < minAllUsed)
								{
									bestPlan = plan;
									minAllUsed = needAll;
								}
							}
						}

						if (bestPlan == null)
							return false;

						foreach (var i in bestPlan)
						{
							if (!used.Contains(i))
							{
								used.Add(i);
								indicesToRemove.Add(i);
							}
						}

						break;
					}

				case ElementCostType.Any:
					{
						int need = cdt.count;

						// 任意骰子，从后往前选（保留靠前的好骰子）
						for (int i = playerDices.Count - 1; i >= 0 && need > 0; i--)
						{
							if (!used.Contains(i))
							{
								used.Add(i);
								indicesToRemove.Add(i);
								need--;
							}
						}

						if (need > 0)
							return false;

						break;
					}
			}
		}

		// 最终从后往前移除
		indicesToRemove.Sort((a, b) => b.CompareTo(a));
		foreach (int i in indicesToRemove)
			playerDices.RemoveAt(i);

		return true;
	}

	public static bool TryToCostDice(List<SkillCostCondition> cdts, List<ElementType> playerDices)
	{
		HashSet<int> used = new();        // 标记哪些骰子被使用了

		foreach (var cdt in cdts)
		{
			switch (cdt.type)
			{
				case ElementCostType.Specific:
					{
						int need = cdt.count;
						ElementType target = cdt.element;

						// 从后往前找目标元素
						for (int i = playerDices.Count - 1; i >= 0 && need > 0; i--)
						{
							if (playerDices[i] == target && !used.Contains(i))
							{
								used.Add(i);
								need--;
							}
						}

						// 从前往后找万能骰
						for (int i = 0; i < playerDices.Count && need > 0; i++)
						{
							if (playerDices[i] == ElementType.All && !used.Contains(i))
							{
								used.Add(i);
								need--;
							}
						}

						if (need > 0)
							return false;

						break;
					}

				case ElementCostType.Same:
					{
						int need = cdt.count;

						// 所有未使用的非万能元素分组
						var grouped = playerDices
							.Select((e, idx) => new { e, idx })
							.Where(x => !used.Contains(x.idx) && x.e != ElementType.All)
							.GroupBy(x => x.e);

						List<int> bestPlan = null;
						int minAllUsed = int.MaxValue;

						foreach (var group in grouped)
						{
							var normal = group.OrderByDescending(x => x.idx).ToList(); // 从后往前选
							int normalCount = normal.Count;
							int needAll = Math.Max(0, need - normalCount);

							var all = playerDices
								.Select((e, idx) => new { e, idx })
								.Where(x => x.e == ElementType.All && !used.Contains(x.idx))
								.OrderBy(x => x.idx)
								.ToList();

							if (normalCount + all.Count >= need)
							{
								var plan = new List<int>();
								plan.AddRange(normal.Take(need).Select(x => x.idx));
								plan.AddRange(all.Take(needAll).Select(x => x.idx));

								if (needAll < minAllUsed)
								{
									bestPlan = plan;
									minAllUsed = needAll;
								}
							}
						}

						if (bestPlan == null)
							return false;

						foreach (var i in bestPlan)
							used.Add(i);

						break;
					}

				case ElementCostType.Any:
					{
						int need = cdt.count;

						for (int i = playerDices.Count - 1; i >= 0 && need > 0; i--)
						{
							if (!used.Contains(i))
							{
								used.Add(i);
								need--;
							}
						}

						if (need > 0)
							return false;

						break;
					}
			}
		}

		// 构建一个新列表，保留未被使用的骰子，保持顺序
		var newList = playerDices
			.Select((e, i) => new { e, i })
			.Where(x => !used.Contains(x.i))
			.Select(x => x.e)
			.ToList();

		playerDices.Clear();
		playerDices.AddRange(newList);

		return true;
	}


}