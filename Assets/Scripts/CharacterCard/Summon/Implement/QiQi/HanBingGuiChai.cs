using UnityEngine;
using System.Collections.Generic;
using ElementDuel;

[CreateAssetMenu(fileName = "HanBingGuiChai", menuName = "ElementDuel/Summon/HanBingGuiChai")]
public class HanBingGuiChai : Summon
{
	//�����׶Σ����1�㿨��Ԫ���˺���
	//���ٻ����ڳ�ʱ������ʹ�á���ͨ���������������������ҷ���ɫ1�㣻
	//ÿ�غ�1�Σ��������ҷ���ս��ɫ1�㡣
	//���ô�����3

	int healCount;

	public override void Initialize(PlayerSystem ownerPlayer, BaseCharacterCard owner, ElementDuelGame game)
	{
		m_game = game;
		m_ownerPlayer = ownerPlayer;
		m_owner = owner;
		count = data.countMax;
		healCount = 1;

		m_game.RegisterEndPhase(DealDamage);
		m_owner.AfterUseNormalAttack.AddListener(AfterNormalAttack);
		m_game.RegisterEndPhase(ReplyHealCount);
	}

	public override void Release()
	{
		m_game.RemoveEndPhase(DealDamage);
		m_owner.AfterUseNormalAttack.RemoveListener(AfterNormalAttack);
		m_game.RemoveEndPhase(ReplyHealCount);
	}

	void AfterNormalAttack()
	{
		m_ownerPlayer.GetTheMostInjured().GetHeal(1);
		if (healCount > 0)
		{
			m_ownerPlayer.fightingCharecter.GetHeal(1);
			healCount--;
		}
	}

	void ReplyHealCount()
	{
		healCount++;
	}

	void DealDamage()
	{
		PlayerSystem target = m_ownerPlayer == m_game.CurrentPlayer ? m_game.OppsitePlayer : m_game.CurrentPlayer;
		target.fightingCharecter.ReceiveDamage(data.value, ElementType.Ice);
		count--;
		if (count == 0)
		{
			m_ownerPlayer.RemoveSummon(this);
		}
	}

	public override bool OnDuplicateAdd()
	{
		count = data.countMax;
		healCount = 1;
		return false;
	}
}
