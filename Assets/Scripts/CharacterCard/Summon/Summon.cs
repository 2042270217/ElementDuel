using ElementDuel;
using UnityEngine;

public abstract class Summon : ScriptableObject
{
	public SummonData data;

	//Summon��ʣ��غ�
	[HideInInspector] public int count;
	[HideInInspector] public virtual int id => IdRegistry.GetId(GetType());

	protected PlayerSystem m_ownerPlayer;
	protected BaseCharacterCard m_owner;
	protected ElementDuelGame m_game;

	/// <summary>
	/// ʵ���ٻ���Ĺ��ܣ�һ��Ϊ��game�����¼�
	/// </summary>
	public abstract void Initialize(PlayerSystem ownerPlayer, BaseCharacterCard owner, ElementDuelGame game);

	/// <summary>
	/// �ٻ����ظ����ʱ���Ĳ�����return trueΪ������return falseΪ����ԭ�е�
	/// </summary>
	public abstract bool OnDuplicateAdd();

	/// <summary>
	/// ʵ���ٻ����Ƴ�ʱ���еĲ�����һ��Ϊ�����game���Ĺ����¼�
	/// </summary>
	public abstract void Release();
}
