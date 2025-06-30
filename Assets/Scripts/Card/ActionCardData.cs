using ElementDuel;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "ActionCardData", menuName = "ElementDuel/ActionCardData"), System.Serializable]
public class ActionCardData : ScriptableObject
{
	[HideInInspector] public int id;
	public Sprite sprite;
	public List<SkillCostCondition> cdts;
	public ActionCardType type;
	public ActionCardTag tag;
	[TextArea(5, 10)] public string description;

}

public enum ActionCardType
{
	Equipment,
	Event,
	Assist
}

public enum ActionCardTag
{
	Weapon,
	Relic,
	Food,
	Partner,
	Site,
	Other
}
