using UnityEngine;
using System.Collections.Generic;

using ElementDuel;
using System;

public class CharacterUI : IUserInterface
{
	CharGroupView m_charGroupView;
	bool m_isDown;

	public CharacterUI(ElementDuelGame edGame, bool isDown) : base(edGame)
	{
		m_isDown = isDown;
		Initialize();
	}

	public override void Initialize()
	{
		string parentName;
		if (m_isDown)
			parentName = "Player1Group";
		else
			parentName = "Player2Group";
		m_Root = UITools.FindUIGameObject(parentName);
		m_Root = UnityTools.FindChildGameObject(m_Root, "CharacterUI");

		m_charGroupView = UITools.GetUIComponet<CharGroupView>(m_Root, "CharGroup");
		m_charGroupView.Initialize(m_EDGame.GetPlayer(m_isDown).charList);
	}

	public override void Release()
	{

	}

	public override void Update()
	{

	}

	public void SetCharacterClick(bool enableClick)
	{
		m_charGroupView.SetClickAll(enableClick);
	}

	public void RegisterCharDoubleClick(Action<BaseCharacterCard> call)
	{
		m_charGroupView.RegisterDoubleClick(call);
	}

	public void RemoveCharDoubleClick(Action<BaseCharacterCard> call)
	{
		m_charGroupView.RemoveDoubleClick(call);
	}

	public void SetFightingCharacter(BaseCharacterCard character)
	{
		m_charGroupView.SetFightingCharacter(character, m_isDown);
	}

	public void ClearSelection()
	{
		m_charGroupView.SetSelectionAll(false);
	}

	public void UpdateCharacter(BaseCharacterCard character)
	{
		m_charGroupView.UpdateCharacter(character);
	}
}
