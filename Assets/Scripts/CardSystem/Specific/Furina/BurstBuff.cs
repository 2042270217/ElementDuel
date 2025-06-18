using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.Specific.Furina
{
    public class BurstBuff : CharacterBuff
    {
        [SerializeField] private BurstEffect effect;

        public override void InitBuff(List<Character> target)
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
            continuousRounds--;
            if (continuousRounds <= 0)
            {
                RemoveBuff(target);
            }
        }
    }
}