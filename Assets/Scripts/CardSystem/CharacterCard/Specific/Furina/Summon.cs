using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.CharacterCard.Specific.Furina
{
    [CreateAssetMenu(fileName = "NewFurinaSummon", menuName = "Character/Specific/Furina/Summon", order = 0)]
    public class Summon : CharacterCard.Summon
    {
        public override void BeforeApplyEffect(List<Character> target)
        {
        }

        public override void AfterApplyEffect(List<Character> target)
        {
            // TODO: Another attack and attack our side character in some cases
        }

        public override void ApplyEffect(List<Character> target, List<CharacterCard.Summon> summonsOnFiled = null)
        {
            BeforeApplyEffect(target);

            // TODO: Apply the summon effect, such as attacking the target character with Furina's summon attack damage

            AfterApplyEffect(target);
        }
    }

    [CreateAssetMenu(fileName = "NewOusiaSummon", menuName = "Character/Specific/Furina/OusiaSummon", order = 1)]
    public class SummonOusia : CharacterCard.Summon
    {

        public override void BeforeApplyEffect(List<Character> target)
        {
        }

        public override void AfterApplyEffect(List<Character> target)
        {
            if (true)
            {
                // TODO: Implement the logic to heal the target character with Ousia's summon heal amount
            }
        }

        public override void ApplyEffect(List<Character> target, List<CharacterCard.Summon> summonsOnFiled = null)
        {
            BeforeApplyEffect(target);

            // TODO: Apply the Ousia summon effect, such as healing the target character with Ousia's summon heal amount

            AfterApplyEffect(target);
        }
    }
}