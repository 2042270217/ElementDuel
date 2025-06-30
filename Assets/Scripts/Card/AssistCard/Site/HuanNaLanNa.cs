using ElementDuel;
using UnityEngine;

[CreateAssetMenu(fileName = "HuanNaLanNa", menuName = "ElementDuel/Card/AssistCard/Site/HuanNaLanNa")]
public class HuanNaLanNa : ActionCard
{
	//结束阶段：收集最多2个未使用的元素骰。
	//行动阶段开始时：拿回此牌所收集的元素骰。

	public HuanNaLanNaAssist assist;

	public override void Use(PlayerSystem owner, ElementDuelGame game)
	{
		base.Use(owner, game);
		owner.AddAssist(Instantiate(assist));
		game.UpdateAssistUI(true);
	}
}
