using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "BaBaLaBurst", menuName = "ElementDuel/Skill/BaBaLa/Burst")]
public class BaBaLaBurst : BaseSkill<BaBaLa>
{
	//治疗我方所有角色4点

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//技能释放前
		m_owner.InvokeBeforeUseBurst();
		//技能释放
		ownerPlayer.charList.ForEach(c => c.GetHeal(4));
		//技能释放后
		m_owner.InvokeAfterUseBurst();
	}
}
