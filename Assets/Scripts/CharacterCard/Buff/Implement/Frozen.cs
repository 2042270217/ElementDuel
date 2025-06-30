using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Frozen", menuName = "ElementDuel/Buff/Frozen")]
public class Frozen : BaseBuff
{
	//角色无法使用技能（状态持续到回合结束为止）；角色受到火元素伤害或物理伤害时伤害值+2，并移除此状态。

	public override void Initialize(BaseCharacterCard owner, ElementDuelGame game)
	{
		m_owner = owner;
		m_game = game;
		count = data.countMax;

		m_owner.attrib.isFrozen = true;
		m_owner.attrib.physicalDamageTakenBonus += 2;
		m_owner.attrib.fireDamageTakenBonus += 2;
		m_owner.AfterTakenFireDamage.AddListener(Thraw);
		m_owner.AfterTakenPhysicalDamage.AddListener(Thraw);
		m_game.RegisterEndPhase(Thraw);
	}

	public override bool OnDuplicateAdd()
	{
		return false;
	}

	public override void Release()
	{
		m_owner.attrib.isFrozen = false;
		m_owner.attrib.physicalDamageTakenBonus -= 2;
		m_owner.attrib.fireDamageTakenBonus -= 2;
		m_owner.AfterTakenFireDamage.RemoveListener(Thraw);
		m_owner.AfterTakenPhysicalDamage.RemoveListener(Thraw);
		m_game.RemoveEndPhase(Thraw);
	}

	public override void TransferTo(BaseCharacterCard target)
	{

	}

	void Thraw()
	{
		m_owner.RemoveCommonBuff(this);
	}
}