using ElementDuel;
using UnityEngine;

public abstract class Assist : ScriptableObject
{
	public AssistData data;

	//ʣ����ô���
	[HideInInspector]public int count;

	protected PlayerSystem m_owner;
	protected ElementDuelGame m_game;

	/// <summary>
	/// ʵ��֧Ԯ��Ĺ��ܣ�һ��Ϊ��game�����¼�
	/// </summary>
	public abstract void Initialize(PlayerSystem owner, ElementDuelGame game);

	/// <summary>
	/// ʵ��֧Ԯ���Ƴ�ʱ���еĲ�����һ��Ϊ�����game���Ĺ����¼�
	/// </summary>
	public abstract void Release();
}
