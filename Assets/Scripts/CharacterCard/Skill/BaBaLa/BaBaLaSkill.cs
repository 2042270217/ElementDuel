using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "BaBaLaSkill", menuName = "ElementDuel/Skill/BaBaLa/Skill")]
public class BaBaLaSkill : BaseSkill<BaBaLa>
{
	//造成1点水元素伤害，召唤歌声之环
	//结束阶段：治疗所有我方角色1点，然后对我方出战角色附着水元素。
	//可用次数：2

	public GeShengZhiHuan summon;

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//技能释放前
		m_owner.InvokeBeforeUseElementSkill();
		//技能释放
		target.ReceiveDamage(1, ElementType.Water);
		m_owner.ownerPlayer.AddSummon(Instantiate(summon));
		m_owner.AddEnergy(1);

		//技能释放后
		m_owner.InvokeAfterUseElementSkill();
	}
}
