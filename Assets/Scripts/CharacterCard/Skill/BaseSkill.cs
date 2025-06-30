using ElementDuel;
using UnityEngine;

public class BaseSkill<TCharacter> : Skill where TCharacter : BaseCharacterCard
{
	[HideInInspector] public TCharacter m_owner;

	protected BaseCharacterCard m_target;

	public override void Initialize(BaseCharacterCard character)
	{
		m_owner = (TCharacter)character;
	}

	public override void Use(BaseCharacterCard target)
	{
		m_target = target;

		Debug.Log(this.GetType().Name);
	}
}
