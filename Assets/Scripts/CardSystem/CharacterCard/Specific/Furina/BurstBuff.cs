using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.CharacterCard.Specific.Furina
{
    public class BurstBuff : CharacterBuff
    {
        private BurstEffect effect;

        public override void Initlize(CharacterBuffData buffData)
        {
        }

        public override void ApplyBuff(List<Character> target)
        {
        }

        public override void UpdateBuff(List<Character> target)
        {
            effect.ApplyEffect(target[0]);
        }

        public override void UpdateRounds(List<Character> target)
        {
            RemainingRounds--;
            if (CurrentRemainingRounds <= 0)
            {
                RemoveBuff(target);
            }
        }
    }
}