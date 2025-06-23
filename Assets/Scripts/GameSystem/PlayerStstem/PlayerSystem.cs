using ElementDuel;
using System.Collections.Generic;

public class PlayerSystem : IGameSystem
{
	public CharacterField m_charField;
	public AssitField m_assitField;
	public SummonField m_summonField;
	public DiceField m_diceField;

	/// <summary>
	/// 可重投次数
	/// </summary>
	public int m_rethrowCount;
	/// <summary>
	/// 已重投次数
	/// </summary>
	public int m_countRethrowed;
	public List<ElementType> m_specialElement;

	public string m_name;

	public int DiceCount
	{
		get
		{
			return m_diceField.diceList.Count;
		}
	}

	public List<ElementType> DiceList
	{
		get
		{
			return m_diceField.diceList;
		}
		set
		{
			m_diceField.diceList = value;
		}
	}

	public PlayerSystem(ElementDuelGame edGame, string name) : base(edGame)
	{
		m_charField = new CharacterField(this);
		m_assitField = new AssitField(this);
		m_diceField = new DiceField(this);
		m_summonField = new SummonField(this);
		m_specialElement = new List<ElementType>();
		m_rethrowCount = 1;
		m_countRethrowed = 0;
		m_name = name;
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

	public bool CanRethrow()
	{
		return m_countRethrowed < m_rethrowCount;
	}

	public void SetDices(List<ElementType> diceList)
	{
		m_diceField.diceList = diceList;
	}
}

