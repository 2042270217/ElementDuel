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


	public override void Initialize(BaseCharacterCard owner, ElementDuelGame game)
	{
		m_owner = owner;
		m_game = game;
		count = data.countMax;
		m_owner.ownerPlayer.AfterUseSkill.AddListener(HealCharacter);
	}

	public override bool OnDuplicateAdd()
	{
		count = data.countMax;
		return false;
	}

	public override void Release()
	{
		m_owner.ownerPlayer.AfterUseSkill.RemoveListener(HealCharacter);
	}

	public override void TransferTo(BaseCharacterCard target)
	{
		m_owner = target;
	}

	void HealCharacter(BaseCharacterCard character)
	{
		if (character.hpLost > 0 && count > 0)
		{
			count--;
			character.GetHeal(2);
			if (count == 0)
			{
				m_owner.RemoveFightingBuff(this);
			}
		}
	}
}
