using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	Sword,      //���ֽ�
	Catalyst,   //����
	Bow,        //��
	Claymore,   //˫�ֽ�
	Pole        //��ǹ
}

[CreateAssetMenu(fileName = "CardData", menuName = "ElementDuel/CharacterCardData")]
public class CharacterCardData : ScriptableObject
{
	public Sprite sprite;
	public int MaxEnergy;
	public int MaxHp = 10;
	public WeaponType weaponType;
	public ElementType elementType = ElementType.Fire;
}
