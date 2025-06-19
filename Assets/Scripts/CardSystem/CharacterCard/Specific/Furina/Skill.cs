using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.CharacterCard.Specific.Furina
{
    [CreateAssetMenu(fileName = "NewSkill", menuName = "Character/Specific/Furina/Skill", order = 0)]
    public class Skill : CharacterAbility
    {
        public override void BeforeApplyEffect(List<Character> target)
        {
        }
    }
}