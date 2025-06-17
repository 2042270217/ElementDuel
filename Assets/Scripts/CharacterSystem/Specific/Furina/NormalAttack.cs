using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem.Specific.Furina
{
    [CreateAssetMenu(fileName = "NewNormalAttack", menuName = "Character/Specific/Furina/NormalAttack", order = 0)]
    public class NormalAttack : Specific.NormalAttack
    {
        public override void BeforeApplyEffect(List<Character> target)
        {
        }

        public override void AfterApplyEffect(List<Character> target)
        {
            // TODO: Generate a specific card for Furina's normal attack (if the card is not already generated)
        }

        public override void ApplyEffect(List<Character> target, List<Summon> summonsOnFiled = null)
        {
            BeforeApplyEffect(target);
            // TODO: Attack the target character with Furina's normal attack damage
            AfterApplyEffect(target);
        }
    }
}