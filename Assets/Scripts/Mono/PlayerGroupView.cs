using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGroupView : MonoBehaviour
{
	CharGroupView m_charGroup;


	private void Start()
	{

	}

	private void Update()
	{

	}

	void Initialize()
	{
		if (m_charGroup == null)
			m_charGroup = UITools.GetUIComponet<CharGroupView>(gameObject, "CharGroup");
	}

	public void InitializeCharacter(List<BaseCharacterCard> chars)
	{
		m_charGroup.Initialize(chars);
	}

	public void Initialize(PlayerSystem player)
	{
		Initialize();

		InitializeCharacter(player.charList);

	}

	public void SetCharacterClick(bool enableClick)
	{
		m_charGroup.SetClickAll(enableClick);
	}

	public void RegisterCharDoubleClick(Action<BaseCharacterCard> call)
	{
		m_charGroup.RegisterDoubleClick(call);
	}
}