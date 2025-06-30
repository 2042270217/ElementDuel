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
}
