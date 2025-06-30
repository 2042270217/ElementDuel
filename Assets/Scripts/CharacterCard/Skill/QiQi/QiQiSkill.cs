using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "QiQiSkill", menuName = "ElementDuel/Skill/QiQi/Skill")]
public class QiQiSkill : BaseSkill<QiQi>
{
	//召唤寒病鬼差
	//结束阶段：造成1点卡冰元素伤害。
	//此召唤物在场时，七七使用「普通攻击」后：治疗受伤最多的我方角色1点；
	//每回合1次：再治疗我方出战角色1点。
	//可用次数：3

	public HanBingGuiChai summon;

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//技能释放前
		m_owner.InvokeBeforeUseElementSkill();
		//技能释放
		m_owner.ownerPlayer.AddSummon(Instantiate(summon));
		m_owner.AddEnergy(1);

		//技能释放后
		m_owner.InvokeAfterUseElementSkill();
	}
}
