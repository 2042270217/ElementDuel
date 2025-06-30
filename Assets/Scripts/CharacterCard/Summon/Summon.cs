using ElementDuel;
using UnityEngine;

public abstract class Summon : ScriptableObject
{
	public SummonData data;

	//Summon的剩余回合
	[HideInInspector] public int count;
	[HideInInspector] public virtual int id => IdRegistry.GetId(GetType());

	protected PlayerSystem m_ownerPlayer;
	protected BaseCharacterCard m_owner;
	protected ElementDuelGame m_game;

	/// <summary>
	/// 实现召唤物的功能，一般为向game订阅事件
	/// </summary>
	public abstract void Initialize(PlayerSystem ownerPlayer, BaseCharacterCard owner, ElementDuelGame game);

	/// <summary>
	/// 召唤物重复添加时做的操作，return true为新增，return false为更新原有的
	/// </summary>
	public abstract bool OnDuplicateAdd();

	/// <summary>
	/// 实现召唤物移除时进行的操作，一般为解除向game订阅过的事件
	/// </summary>
	public abstract void Release();
}
