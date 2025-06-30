using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseRelic
{
	protected BaseCharacterCard m_owner;
	protected ElementDuelGame m_game;

	/// <summary>
	/// 初始化做的操作
	/// </summary>
	/// <param name="ownerPlaer"></param>
	/// <param name="game"></param>
	public abstract void Initialize(BaseCharacterCard owner, ElementDuelGame game);
	/// <summary>
	/// Buff移除时做的操作
	/// </summary>
	public abstract void Release();
}

