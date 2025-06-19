using UnityEngine;

namespace CardSystem.CharacterCard.Specific.Furina
{
    public class BurstEffect : BufferEffect
    {
        private int _count;
        public override void ApplyEffect(Character target)
        {
        }

        public override void UpdateEffect(Character target)
        {
            ++_count; // TODO: Implement the logic to increase the HP variation based on current round HP variation
        }
    }
}