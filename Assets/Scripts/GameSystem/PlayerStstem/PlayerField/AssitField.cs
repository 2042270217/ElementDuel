using System.Collections.Generic;

public class AssitField: IPlayerField
{
	public List<Assist> m_assistList;

	public AssitField(PlayerSystem player) : base(player)
	{
		Initialize();
	}

	public override void Initialize()
	{
		m_assistList = new List<Assist>();
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}
}

