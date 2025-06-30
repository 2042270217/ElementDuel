using ElementDuel;
using UnityEngine;

public abstract class Summon : ScriptableObject
{
	public SummonData data;

	//Summon��ʣ��غ�
	[HideInInspector] public int count;

	protected PlayerSystem m_ownerPlayer;
	protected BaseCharacterCard m_owner;
	protected ElementDuelGame m_game;

	/// <summary>
	/// ʵ���ٻ���Ĺ��ܣ�һ��Ϊ��game�����¼�
	/// </summary>
	public abstract void Initialize(PlayerSystem ownerPlayer, BaseCharacterCard owner, ElementDuelGame game);

	/// <summary>
	/// ʵ���ٻ����Ƴ�ʱ���еĲ�����һ��Ϊ�����game���Ĺ����¼�
	/// </summary>
	public abstract void Release();
}
