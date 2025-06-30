using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;


public class ShengYvZaZuo : ActionCard
{
	//在荒性和芒性之中，切换芙宁娜的形态。
	//如果我方场上存在沙龙成员或众水的歌者，也切换其形态。


	public override void Use(PlayerSystem owner, ElementDuelGame game)
	{
		base.Use(owner, game);
		var fu = owner.charList.Find(c => c.id == new FuNingNa().id) as FuNingNa;

	}
}
