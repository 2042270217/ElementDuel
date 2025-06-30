using ElementDuel;
using System;

public class ElementReaction
{
	public string name;
	public int bonusDamage;
	public Action<BaseCharacterCard, ElementDuelGame> effect;
}
