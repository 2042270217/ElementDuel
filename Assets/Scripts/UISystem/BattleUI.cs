using ElementDuel;
using TMPro;
using UnityEngine;

public class BattleUI : IUserInterface
{
	GameObject m_diceGroup;
	GameObject m_diceGroupCounter;
	GameObject m_dice;
	GameObject m_dicePrefab;

	TMP_Text m_diceCount;

	public BattleUI(ElementDuelGame edGame, GameObject dicePrefab) : base(edGame)
	{
		Initialize();
		m_dicePrefab = dicePrefab;
	}

	public override void Initialize()
	{
		m_Root = UITools.FindUIGameObject("BattleUI");
		m_diceGroup = UnityTools.FindChildGameObject(m_Root, "DiceGroup");
		m_diceGroupCounter = UnityTools.FindChildGameObject(m_diceGroup, "Counter");
		m_dice = UnityTools.FindChildGameObject(m_diceGroup, "Dice");

		m_diceCount = UITools.GetUIComponet<TMP_Text>(m_diceGroupCounter, "count");

	}

	public override void Release()
	{

	}

	public override void Update()
	{
		UpdateDiceList();
	}

	public void UpdateDiceList()
	{
		m_diceCount.text = m_EDGame.CurrentPlayer.DiceCount.ToString();
		var diceList = m_EDGame.CurrentPlayer.DiceList;
		foreach (ElementType dice in diceList)
		{

			var item = GameObject.Instantiate(m_dicePrefab, m_dice.transform);
			var diceView = item.GetComponent<DiceView>();
			diceView.CanBeClicked = false;
			diceView.SetElement(dice);
		}
	}
}
