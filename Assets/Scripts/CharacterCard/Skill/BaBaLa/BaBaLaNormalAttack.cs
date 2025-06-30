using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "BaBaLaNormalAttack", menuName = "ElementDuel/Skill/BaBaLa/NormalAttack")]
public class BaBaLaNormalAttack : BaseSkill<BaBaLa>
{
	//造成1点水元素伤害
	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//技能释放前
		m_owner.InvokeBeforeUseNormalAttack();
		//技能释放
		target.ReceiveDamage(1, ElementType.Water);
		m_owner.AddEnergy(1);

		//技能释放后
		m_owner.InvokeAfterUseNormalAttack();
	}
}
