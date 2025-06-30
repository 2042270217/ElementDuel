using System.Collections.Generic;

public class SummonField : IPlayerField
{
	public List<Summon> summonList;
	public SummonField(PlayerSystem player) : base(player)
	{
		Initialize();
	}

	public override void Initialize()
	{
		summonList = new List<Summon>();
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}
}

