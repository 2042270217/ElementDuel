using UnityEngine;
using System.Collections.Generic;
using ElementDuel;

[CreateAssetMenu(fileName = "GeShengZhiHuan", menuName = "ElementDuel/Summon/GeShengZhiHuan")]
public class GeShengZhiHuan : Summon
{
	//结束阶段：治疗所有我方角色1点，然后对我方出战角色附着水元素。
	//可用次数：2

	public override void Initialize(PlayerSystem ownerPlayer, BaseCharacterCard owner, ElementDuelGame game)
	{
		m_game = game;
		m_ownerPlayer = ownerPlayer;
		m_owner = owner;
		count = data.countMax;

		m_game.RegisterEndPhase(HealAll);
	}

	public override void Release()
	{
		m_game.RemoveEndPhase(HealAll);
	}


	public override bool OnDuplicateAdd()
	{
		count = data.countMax;
		return false;
	}

	void HealAll()
	{
		m_ownerPlayer.charList.ForEach(c => c.GetHeal(data.value));
		m_ownerPlayer.fightingCharecter.AttachElement(ElementType.Water);
	}
}
