using ElementDuel;
using UnityEngine;

public abstract class Skill : ScriptableObject
{
	public SkillData skillData;

	public abstract void Initialize(BaseCharacterCard character);
	public abstract void Use(BaseCharacterCard target);
}
