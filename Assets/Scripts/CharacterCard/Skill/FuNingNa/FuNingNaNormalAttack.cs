using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "FuNingNaNormalAttack", menuName = "ElementDuel/Skill/FuNingNa/NormalAttack")]
public class FuNingNaNormalAttack : BaseSkill<FuNingNa>
{
	//造成2点物理伤害。
	//每回合1次：如果手牌中没有圣俗杂座，则生成手牌圣俗杂座。
	//圣俗杂座：
	//在荒性和芒性之中，切换芙宁娜的形态。
	//如果我方场上存在沙龙成员或众水的歌者，也切换其形态。



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
