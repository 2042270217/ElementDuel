using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DuEZhenFu", menuName = "ElementDuel/Buff/DuEZhenFu")]
public class DuEZhenFu : BaseBuff
{
	//出战状态
	//我方角色使用技能后：如果该角色生命值未满，则治疗该角色2点。
	//可用次数：3


	public override void Initialize(PlayerSystem ownerPlayer, ElementDuelGame game)
	{
		m_ownerPlayer = ownerPlayer;
		m_game = game;
		count = data.countMax;
		m_ownerPlayer.AfterUseSkill.AddListener(HealCharacter);
	}

	public override void OnDuplicateAdd()
	{
		count = data.countMax;
	}

	public override void Release()
	{
		m_ownerPlayer.AfterUseSkill.RemoveListener(HealCharacter);
	}

	public override void TransferTo(BaseCharacterCard target)
	{

	}

	void HealCharacter(BaseCharacterCard character)
	{
		if (character.hpLost > 0 && count > 0)
		{
			count--;
			character.GetHeal(2);
			if (count == 0)
			{
				m_ownerPlayer.RemoveFightingBuff(this);
			}
		}
	}
}
