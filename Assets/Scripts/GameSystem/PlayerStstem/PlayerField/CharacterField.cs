
using System;
using System.Collections.Generic;

public class CharacterField : IPlayerField
{
	public List<BaseCharacterCard> chars;

	BaseCharacterCard m_fightingCharacter;

	public BaseCharacterCard fightingCharacter => m_fightingCharacter;

	public CharacterField(PlayerSystem player) : base(player)
	{
		Initialize();
	}

	public override void Initialize()
	{
		chars = new List<BaseCharacterCard>();
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}

	public void SetFightingCharacter(BaseCharacterCard character)
	{
		m_fightingCharacter = character;
	}
}

