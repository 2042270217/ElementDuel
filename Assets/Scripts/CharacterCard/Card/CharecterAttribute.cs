using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttribute
{
	public int currentEnergy;
	public int currentHp;
	public List<ElementType> attachedElement = new List<ElementType>();
	public bool reliced = false;
	public bool weaponed = false;
	public int damageTakenBonus = 0;
	public int fireDamageTakenBonus = 0;
	public int waterDamageTakenBonus = 0;
	public int windDamageTakenBonus = 0;
	public int thunderDamageTakenBonus = 0;
	public int grassDamageTakenBonus = 0;
	public int iceDamageTakenBonus = 0;
	public int rockDamageTakenBonus = 0;
	public int physicalDamageTakenBonus = 0;
	public bool isFrozen = false;
}
