using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "QiQiNormalAttack", menuName = "ElementDuel/Skill/QiQi/NormalAttack")]
public class QiQiNormalAttack : BaseSkill<QiQi>
{
	//������������˺�

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//�����ͷ�ǰ
		m_owner.InvokeBeforeUseNormalAttack();
		//�����ͷ�
		target.ReceiveDamage(2, ElementType.None);
		m_owner.AddEnergy(1);

		//�����ͷź�
		m_owner.InvokeAfterUseNormalAttack();
	}
}
