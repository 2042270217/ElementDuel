using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillData", menuName = "ElementDuel/SkillData")]
public class SkillData : ScriptableObject
{
	public Sprite icon;
	public List<SkillCostCondition> cdts;
	[TextArea(5, 10)] public string description;
}
