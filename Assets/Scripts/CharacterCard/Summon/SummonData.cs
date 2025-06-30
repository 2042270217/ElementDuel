using UnityEngine;

[CreateAssetMenu(fileName = "SummonData", menuName = "ElementDuel/SummonData")]
public class SummonData : ScriptableObject
{
	public Sprite sprite;
	public int countMax;
	public EffectType effectType = EffectType.Damage;
	public ElementType elementType = ElementType.Water;
	/// <summary>
	/// ���ƻ����˺�����ֵ
	/// </summary>
	public int value;
	[TextArea(5, 10)] public string decription;
}
