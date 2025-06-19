using UnityEngine;

namespace CardSystem.CharacterCard
{
    [CreateAssetMenu(fileName = "NewBuffEffect", menuName = "Character/BuffEffect", order = 0)]
    public class BufferEffect : ScriptableObject
    {
        [SerializeField] private string effectName;

        [SerializeField] private string description;


        public string EffectName => effectName;
        public string Description => description;


        public virtual void ApplyEffect(Character target)
        {
            // Implement the logic to apply the buff effect to the target character
            // For example, increase the target's attack or defense based on the power
        }
        
        public virtual void UpdateEffect(Character target)
        {
            // Implement the logic to update the buff effect on the target character
            // For example, decrease the remaining rounds or update the effect based on certain conditions
        }
    }
}