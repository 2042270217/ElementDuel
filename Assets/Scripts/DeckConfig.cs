using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DeckConfig", menuName = "ElementDuel/DeckConfig")]
public class DeckConfig : ScriptableObject
{
	public List<ActionCard> actionCards;

	public List<BaseCharacterCard> player1Char;
	public List<BaseCharacterCard> player2Char;
}
