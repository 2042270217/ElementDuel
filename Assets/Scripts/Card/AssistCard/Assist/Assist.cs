using ElementDuel;
using UnityEngine;

public abstract class Assist : ScriptableObject
{
	public AssistData data;

	//剩余可用次数
	[HideInInspector]public int count;

	protected PlayerSystem m_owner;
	protected ElementDuelGame m_game;

	/// <summary>
	/// 实现支援物的功能，一般为向game订阅事件
	/// </summary>
	public abstract void Initialize(PlayerSystem owner, ElementDuelGame game);

	/// <summary>
	/// 实现支援物移除时进行的操作，一般为解除向game订阅过的事件
	/// </summary>
	public abstract void Release();
}
