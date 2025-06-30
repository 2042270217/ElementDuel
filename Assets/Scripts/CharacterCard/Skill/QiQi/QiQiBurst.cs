using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "QiQiBurst", menuName = "ElementDuel/Skill/QiQi/Burst")]
public class QiQiBurst : BaseSkill<QiQi>
{
	//���3���Ԫ���˺������ɶȶ����
	//��ս״̬
	//�ҷ���ɫʹ�ü��ܺ�����ý�ɫ����ֵδ���������Ƹý�ɫ2�㡣
	//���ô�����3

	public DuEZhenFu buff;

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//�����ͷ�ǰ
		m_owner.InvokeBeforeUseBurst();
		//�����ͷ�
		target.ReceiveDamage(3, ElementType.Ice);
		m_owner.RemoveEnergy(3);
		//�����ͷź�
		m_owner.InvokeAfterUseBurst();
		ownerPlayer.AddFightingBuff(Instantiate(buff));
	}
}
