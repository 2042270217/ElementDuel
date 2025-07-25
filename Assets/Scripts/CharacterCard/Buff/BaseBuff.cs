﻿using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBuff : ScriptableObject
{
	public BuffData data;
	//当前剩余计数
	[HideInInspector] public int count;
	[HideInInspector] public virtual int id => IdRegistry.GetId(GetType());

	protected BaseCharacterCard m_owner;
	protected ElementDuelGame m_game;

	/// <summary>
	/// Buff初始化做的操作
	/// </summary>
	/// <param name="ownerPlaer"></param>
	/// <param name="game"></param>
	public abstract void Initialize(BaseCharacterCard owner, ElementDuelGame game);
	/// <summary>
	/// 当Buff转移时的操作
	/// </summary>
	/// <param name="target"></param>
	public abstract void TransferTo(BaseCharacterCard target);
	/// <summary>
	/// Buff重复添加时做的操作，return true为新增，return false为更新原有的
	/// </summary>
	public abstract bool OnDuplicateAdd();
	/// <summary>
	/// Buff移除时做的操作
	/// </summary>
	public abstract void Release();
}