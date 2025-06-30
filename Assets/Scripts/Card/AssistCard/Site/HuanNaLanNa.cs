using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "HuanNaLanNa", menuName = "ElementDuel/Card/AssistCard/Site/HuanNaLanNa")]
public class HuanNaLanNa : ActionCard
{
	//�����׶Σ��ռ����2��δʹ�õ�Ԫ������
	//�ж��׶ο�ʼʱ���ûش������ռ���Ԫ������

	public HuanNaLanNaAssist assist;

	public override void Use(PlayerSystem owner, ElementDuelGame game)
	{
		base.Use(owner, game);
		owner.AddAssist(Instantiate(assist));
		game.UpdateAssistUI(true);
	}
}
