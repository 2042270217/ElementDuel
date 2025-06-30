using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "QiQiBurst", menuName = "ElementDuel/Skill/QiQi/Burst")]
public class QiQiBurst : BaseSkill<QiQi>
{
	//造成3点冰元素伤害，生成度厄真符
	//出战状态
	//我方角色使用技能后：如果该角色生命值未满，则治疗该角色2点。
	//可用次数：3

	public DuEZhenFu buff;

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//技能释放前
		m_owner.InvokeBeforeUseBurst();
		//技能释放
		target.ReceiveDamage(3, ElementType.Ice);
		m_owner.RemoveEnergy(3);
		//技能释放后
		m_owner.InvokeAfterUseBurst();
		ownerPlayer.AddFightingBuff(Instantiate(buff));
	}
}
