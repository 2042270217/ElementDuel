using System.Collections.Generic;
using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementReactionRegistry", menuName = "ElementDuel/ElementReactionRegistry")]
public class ElementReactionRegistry : ScriptableObject
{
	public Frozen frozenBuff;

	private static Dictionary<ElementReactionKey, ElementReaction> m_reactions;

	// 单例实例
	private static ElementReactionRegistry m_instance;

	public static ElementReactionRegistry Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = Resources.Load<ElementReactionRegistry>("ElementReactionRegistry");
				if (m_instance == null)
				{
					Debug.LogError("ElementReactionRegistry not found in Resources folder!");
				}
				else if (m_reactions == null)
				{
					Initialize();
				}
			}
			return m_instance;
		}
	}

	// 初始化静态注册表
	private static void Initialize()
	{
		m_reactions = new Dictionary<ElementReactionKey, ElementReaction>
		{
			[new ElementReactionKey(ElementType.Fire, ElementType.Ice)] = new ElementReaction
			{
				name = "融化",
				bonusDamage = 2
			},
			[new ElementReactionKey(ElementType.Fire, ElementType.Water)] = new ElementReaction
			{
				name = "蒸发",
				bonusDamage = 2
			},
			[new ElementReactionKey(ElementType.Fire, ElementType.Thunder)] = new ElementReaction
			{
				name = "超载",
				bonusDamage = 2,
				effect = (target, game) =>
				{
					Debug.Log("超载");
				}
			},
			[new ElementReactionKey(ElementType.Thunder, ElementType.Ice)] = new ElementReaction
			{
				name = "超导",
				bonusDamage = 1,
				effect = (target, game) =>
				{
					Debug.Log("超导");
				}
			},
			[new ElementReactionKey(ElementType.Water, ElementType.Ice)] = new ElementReaction
			{
				name = "冻结",
				bonusDamage = 1,
				effect = (target, game) =>
				{
					var frozen = Instantiate(Instance.frozenBuff);
					target.AddCommonBuff(frozen);
				}
			}
		};
	}

	public static ElementReaction GetReaction(ElementType a, ElementType b)
	{
		if (m_reactions == null)
			Initialize();

		m_reactions.TryGetValue(new ElementReactionKey(a, b), out var reaction);
		return reaction;
	}
}