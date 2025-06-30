using ElementDuel;
using System;
using System.Collections.Generic;

public abstract class PlayerAction
{
	public bool isCombatAction;
	public ElementDuelGame game;
	public PlayerAction(bool isCombatAction, ElementDuelGame game)
	{
		this.isCombatAction = isCombatAction;
		this.game = game;
	}

	public abstract bool Execute();
}

public class SkillAction : PlayerAction
{
	Skill m_skill;
	public SkillAction(bool isCombatAction, ElementDuelGame game, Skill skill) : base(isCombatAction, game)
	{
		m_skill = skill;
	}

	public override bool Execute()
	{
		if (DiceSystem.TryToCostDice(m_skill.skillData.cdts, game.CurrentPlayer.diceList))
		{
			m_skill.Use(game.OppsitePlayer.fightingCharecter);
			return true;
		}
		else
		{
			game.UpdateInfoUI("骰子不足");
			return false;
		}
	}
}

public class CardAction : PlayerAction
{
	ActionCard m_card;

	public CardAction(bool isCombatAction, ElementDuelGame game, ActionCard card) : base(isCombatAction, game)
	{
		m_card = card;
	}

	public override bool Execute()
	{
		if (DiceSystem.TryToCostDice(m_card.data.cdts, game.CurrentPlayer.diceList))
		{
			m_card.Use(game.CurrentPlayer, game);
			game.CurrentPlayer.RemoveHands(m_card);
			game.UpdateCurrentHandsUI();
			game.UpdateDiceUI();
			return true;
		}
		else
		{
			game.UpdateInfoUI("骰子不足");
			return false;
		}
	}
}

public class SwitchCharacterAction : PlayerAction
{
	BaseCharacterCard m_character;

	public SwitchCharacterAction(bool isCombatAction, ElementDuelGame game, BaseCharacterCard character) : base(isCombatAction, game)
	{
		m_character = character;
	}

	public override bool Execute()
	{
		List<SkillCostCondition> cdts = new List<SkillCostCondition>();
		cdts.Add(SkillCostCondition.GetAnyCost(1));
		if (DiceSystem.TryToCostDice(cdts, game.CurrentPlayer.diceList))
		{
			game.CurrentPlayer.SetFightingCharacter(m_character);
			var currentUI = game.GetCharacterUI(game.CurrentPlayer);
			currentUI.SetFightingCharacter(m_character);
			game.UpdateDiceUI();
			return true;
		}
		else
		{
			game.UpdateInfoUI("骰子不足");
			return false;
		}
	}
}
