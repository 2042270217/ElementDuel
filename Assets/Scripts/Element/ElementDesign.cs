using System.Collections.Generic;
using UnityEngine;

public enum ElementType
{
	None = 0,
	Fire = 1 << 0,
	Water = 1 << 1,
	Wind = 1 << 2,
	Thunder = 1 << 3,
	Grass = 1 << 4,
	Ice = 1 << 5,
	Rock = 1 << 6,
	All = int.MinValue
}

public enum ElementCostType
{
	Any,        //����Ԫ����
	Same,       //��ͬԪ����
	Specific,   //ָ��Ԫ����
	Energy		//��ɫ����
}

[System.Serializable]
public class SkillCostCondition
{
	public ElementCostType type;
	public int count;
	/// <summary>
	/// typeΪElementCostType.Specificʱʹ��
	/// </summary>
	public ElementType element = ElementType.None;

	public static SkillCostCondition GetSameCost(int count)
	{
		var output = new SkillCostCondition();
		output.type = ElementCostType.Same;
		output.count = count;
		return output;
	}

	public static SkillCostCondition GetAnyCost(int count)
	{
		var output = new SkillCostCondition();
		output.type = ElementCostType.Any;
		output.count = count;
		return output;
	}

	public static SkillCostCondition GetElementCost(int count, ElementType elementType)
	{
		var output = new SkillCostCondition();
		output.type = ElementCostType.Specific;
		output.count = count;
		output.element = elementType;
		return output;
	}
}




