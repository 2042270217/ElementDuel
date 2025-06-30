using UnityEngine;

[CreateAssetMenu(fileName = "AssistData", menuName = "ElementDuel/AssistData")]
public class AssistData : ScriptableObject
{
	public readonly Sprite sprite;
	//���ô���
	public readonly int countMax;
	public readonly bool useCounter;
}
