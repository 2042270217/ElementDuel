using UnityEngine;

public enum BuffType
{
	Common,
	Fighting
}

public enum BuffCountType
{
	Count,
	Persistent
}

[CreateAssetMenu(fileName = "BuffData", menuName = "ElementDuel/BuffData")]
public class BuffData : ScriptableObject
{
	public Sprite sprite;
	public int countMax;
	public bool useCounter = true;
	[TextArea(5, 10)] public string description;
	
}
