using NUnit.Framework;
using System.Collections.Generic;

public class DiceField : IPlayerField
{
	public List<ElementType> diceList;

	public DiceField(PlayerSystem player) : base(player)
	{
		diceList = new List<ElementType>();
	}

	public override void Initialize()
	{

	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}
}