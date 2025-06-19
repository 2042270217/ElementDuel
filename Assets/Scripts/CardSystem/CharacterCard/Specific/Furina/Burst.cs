using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.CharacterCard.Specific.Furina
{
    public class Burst : CharacterAbility
    {
        public override void BeforeApplyEffect(List<Character> target)
        {
        }

        public override void AfterApplyEffect(List<Character> target)
        {
        }

        public override void ApplyEffect(List<Character> target, List<CharacterCard.Summon> summonsOnFiled = null)
        {
            BeforeApplyEffect(target);

            /*
             * TODO: Apply the burst effect, such as dealing damage to the target character with Furina's burst attack damage
             * and add a buff to the target character that increases their attack damage
             */

            AfterApplyEffect(target);
        }
    }
}