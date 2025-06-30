using UnityEngine;

[CreateAssetMenu(fileName = "AssistData", menuName = "ElementDuel/AssistData")]
public class AssistData : ScriptableObject
{
	public Sprite sprite;
	//可用次数
	public int countMax;
	public bool useCounter;
}
