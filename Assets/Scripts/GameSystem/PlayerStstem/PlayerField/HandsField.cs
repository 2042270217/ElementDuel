using System.Collections.Generic;

public class HandsField : IPlayerField
{
	public List<ActionCard> actionCards;

	public HandsField(PlayerSystem player) : base(player)
	{
		Initialize();
	}

	public override void Initialize()
	{
		actionCards = new List<ActionCard>();
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}
}