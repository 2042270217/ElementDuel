using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Cost", menuName = "ElementDuel/SkillCost")]
public class SkillCostData : ScriptableObject
{
	public List<SkillCostCondition> conditions = new List<SkillCostCondition>();

	//2同2任意
	public static SkillCostData GetSampleCostData1()
	{
		var skillCostData = new SkillCostData();
		SkillCostCondition c0 = SkillCostCondition.GetSameCost(2);
		skillCostData.conditions.Add(c0);
		SkillCostCondition c1 = SkillCostCondition.GetAnyCost(2);
		skillCostData.conditions.Add(c1);
		return skillCostData;
	}

	//2火1任意
	public static SkillCostData GetSampleCostData2()
	{
		var skillCostData = new SkillCostData();
		SkillCostCondition c0 = SkillCostCondition.GetElementCost(2,ElementType.Fire);
		skillCostData.conditions.Add(c0);
		SkillCostCondition c1 = SkillCostCondition.GetAnyCost(1);
		skillCostData.conditions.Add(c1);
		return skillCostData;
	}
}