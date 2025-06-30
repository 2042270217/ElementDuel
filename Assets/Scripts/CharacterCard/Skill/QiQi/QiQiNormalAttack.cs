using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "QiQiNormalAttack", menuName = "ElementDuel/Skill/QiQi/NormalAttack")]
public class QiQiNormalAttack : BaseSkill<QiQi>
{
	//造成两点物理伤害

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//技能释放前
		m_owner.InvokeBeforeUseNormalAttack();
		//技能释放
		target.ReceiveDamage(2, ElementType.None);
		m_owner.AddEnergy(1);

		//技能释放后
		m_owner.InvokeAfterUseNormalAttack();
	}
}
