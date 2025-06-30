using System.Collections.Generic;
using System;
using UnityEngine;

public static class ElementReactionRegistry
{
	private static Dictionary<ElementReactionKey, ElementReaction> m_reactions;

	static ElementReactionRegistry()
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
			[new ElementReactionKey(ElementType.Water,ElementType.Ice)]=new ElementReaction
			{
				name="冻结",
				bonusDamage = 1,
				effect=(target, game) =>
				{
					Debug.Log("冻结");
				}
			}

		};
	}

	public static ElementReaction GetReaction(ElementType a, ElementType b)
	{
		m_reactions.TryGetValue(new ElementReactionKey(a, b), out var reaction);
		return reaction;
	}
}