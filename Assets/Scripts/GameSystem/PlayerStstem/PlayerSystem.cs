using ElementDuel;
using ElementDuel.GamePhase;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSystem : IGameSystem
{
	CharacterField m_charField;
	AssitField m_assitField;
	SummonField m_summonField;
	DiceField m_diceField;
	HandsField m_handsField;

	/// <summary>
	/// 可重投次数
	/// </summary>
	public int m_rethrowCount;
	/// <summary>
	/// 已重投次数
	/// </summary>
	public int m_countRethrowed;
	List<ElementType> m_specialElement;

	string m_name;
	bool m_isTurnOver;

	public string name => m_name;

	public bool isTurnOver
	{
		get
		{
			return m_isTurnOver;
		}
	}

	public int diceCount
	{
		get
		{
			return m_diceField.diceList.Count;
		}
	}

	public List<ElementType> diceList
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

	public List<ActionCard> handsList
	{
		get
		{
			return m_handsField.actionCards;
		}
		set
		{
			m_handsField.actionCards = value;
		}
	}

	public List<BaseCharacterCard> charList
	{
		get
		{
			return m_charField.chars;
		}
	}

	public List<Summon> summonList => m_summonField.summonList;

	public List<Assist> assistList => m_assitField.m_assistList;

	public BaseCharacterCard fightingCharecter => m_charField.fightingCharacter;

	public List<ElementType> specialElement { get { return m_specialElement; } }


	public UnityEvent<BaseCharacterCard> AfterUseSkill = new();
	public UnityEvent<BaseCharacterCard> BeforeUseSkill = new();


	public PlayerSystem(ElementDuelGame edGame, string name, List<BaseCharacterCard> chars) : base(edGame)
	{
		Initialize();
		m_name = name;
		foreach (var character in chars)
		{
			var c = ScriptableObject.Instantiate(character);
			c.Initialize(m_EDGame, this);
			m_charField.chars.Add(c);
			if (!m_specialElement.Contains(character.charData.elementType))
				m_specialElement.Add(character.charData.elementType);
		}
	}

	public override void Initialize()
	{
		m_charField = new CharacterField(this);
		m_assitField = new AssitField(this);
		m_diceField = new DiceField(this);
		m_summonField = new SummonField(this);
		m_handsField = new HandsField(this);
		m_specialElement = new List<ElementType>();
		m_rethrowCount = 1;
		m_countRethrowed = 0;
		m_isTurnOver = false;
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

	public void SetFightingCharacter(BaseCharacterCard character)
	{
		if (fightingCharecter != null)
		{
			foreach (var buff in fightingCharecter.fightingBuff)
			{
				buff.TransferTo(character);
				character.fightingBuff.Add(buff);
			}
			fightingCharecter.fightingBuff.Clear();
			m_EDGame.GetCharacterUI(this).UpdateCharacter(character);
			m_EDGame.GetCharacterUI(this).UpdateCharacter(fightingCharecter);
		}

		m_charField.SetFightingCharacter(character);
		m_EDGame.GetCharacterUI(this).SetFightingCharacter(character);
	}

	public void ForceChangeToNext()
	{
		var live = m_charField.chars.Where(c => !c.isDead).ToList();
		if (live.Count == 0)
		{
			//所有角色阵亡
			m_EDGame.SetPhaseState(GamePhaseEnum.GameEnd);
			return;
		}
		int next = (live.IndexOf(fightingCharecter) + 1) % live.Count;
		SetFightingCharacter(live[next]);
	}

	public void SetTurnOver(bool isOver)
	{
		m_isTurnOver = isOver;
	}

	public void AddAssist(Assist assist)
	{
		if (assistList.Count == 4)
		{
			assistList[0].Release();
			assistList.RemoveAt(0);
			assistList.Add(assist);
		}
		else
		{
			assistList.Add(assist);
			assist.Initialize(this, m_EDGame);
		}
	}

	public void AddDices(List<ElementType> dicesAdded)
	{
		if (diceCount + dicesAdded.Count <= 16)
		{
			diceList.AddRange(dicesAdded);
		}
		else
		{
			int addCount = diceCount + dicesAdded.Count - 16;
			for (int i = 0; i < addCount; i++)
			{
				diceList.Add(dicesAdded[i]);
			}
		}
		DiceSystem.SortDiceList(diceList, specialElement);
	}

	public void AddDice(ElementType ele)
	{
		if (diceCount < 16)
		{
			diceList.Add(ele);
			DiceSystem.SortDiceList(diceList, specialElement);
		}
	}

	public void AddHands(List<ActionCard> actionCards)
	{
		if (handsList.Count + actionCards.Count < 10)
		{
			handsList.AddRange(actionCards);
		}
		else
		{
			int addCount = handsList.Count + actionCards.Count - 10;
			for (int i = 0; i < addCount; i++)
			{
				handsList.Add(actionCards[i]);
			}
		}
	}

	public void RemoveHands(ActionCard actionCard)
	{
		handsList.Remove(actionCard);
	}

	public void AddSummon(Summon summon)
	{
		if (summonList.Count == 4)
		{
			summonList[0].Release();
			summonList.RemoveAt(0);
			summonList.Add(summon);
		}
		else
		{
			summonList.Add(summon);
			summon.Initialize(this, fightingCharecter, m_EDGame);
		}
		m_EDGame.GetSummonUI(this).Update();
	}

	public void AddFightingBuff(BaseBuff buff)
	{
		fightingCharecter.AddFightingBuff(buff);
	}

	public void RemoveFightingBuff(BaseBuff buff)
	{
		fightingCharecter.RemoveFightingBuff(buff);
	}

	public BaseCharacterCard GetTheMostInjured()
	{
		BaseCharacterCard c = charList[0];
		foreach (var character in charList)
		{
			if (character.hpLost > c.hpLost)
				c = character;
		}
		return c;
	}
}

