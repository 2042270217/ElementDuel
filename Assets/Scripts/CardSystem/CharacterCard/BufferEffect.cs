using UnityEngine;

namespace CardSystem.CharacterCard
{
    [CreateAssetMenu(fileName = "NewBuffEffect", menuName = "Character/BuffEffect", order = 0)]
    public class BufferEffectData : ScriptableObject
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

    public class BufferEffect
    {
        private BufferEffectData _bufferEffectData;
        public string EffectName => _bufferEffectData.EffectName;
        public string Description => _bufferEffectData.Description;


        public virtual void Init(BufferEffectData bufferEffectData)
        {
            _bufferEffectData = bufferEffectData;
        }


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