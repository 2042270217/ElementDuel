using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "QiQiSkill", menuName = "ElementDuel/Skill/QiQi/Skill")]
public class QiQiSkill : BaseSkill<QiQi>
{
	//�ٻ��������
	//�����׶Σ����1�㿨��Ԫ���˺���
	//���ٻ����ڳ�ʱ������ʹ�á���ͨ���������������������ҷ���ɫ1�㣻
	//ÿ�غ�1�Σ��������ҷ���ս��ɫ1�㡣
	//���ô�����3

	public HanBingGuiChai summon;

	public override void Use(BaseCharacterCard target)
	{
		base.Use(target);
		PlayerSystem ownerPlayer = m_owner.ownerPlayer;
		ElementDuelGame game = m_owner.game;
		//�����ͷ�ǰ
		m_owner.InvokeBeforeUseElementSkill();
		//�����ͷ�
		m_owner.ownerPlayer.AddSummon(Instantiate(summon));
		m_owner.AddEnergy(1);

		//�����ͷź�
		m_owner.InvokeAfterUseElementSkill();
	}
}
