using System.Collections.Generic;
using UnityEngine;

public enum WeaponType
{
	Sword,      //单手剑
	Catalyst,   //法器
	Bow,        //弓
	Claymore,   //双手剑
	Pole        //长枪
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
