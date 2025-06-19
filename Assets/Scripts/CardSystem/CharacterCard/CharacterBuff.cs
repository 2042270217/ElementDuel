using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardSystem.CharacterCard
{
    [CreateAssetMenu(fileName = "NewBuff", menuName = "Character/Buff", order = 0)]
    public abstract class CharacterBuff : ScriptableObject
    {
        protected static readonly int InactiveContinuousRounds = -1;

        [Header("基础信息")] [SerializeField] private string buffName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private bool isPermanent;
        [SerializeField] private bool isOnField;
        [SerializeField] private bool isAlwaysAvailable;
        [SerializeField] protected int continuousRounds = InactiveContinuousRounds;
        [SerializeField] private int availableCount = 1;


        protected int BuffCount;


        public string BuffName => buffName;
        public Sprite Icon => icon;
        public string Description => description;

        public bool IsOnField => isOnField;
        public bool IsAlwaysAvailable => isAlwaysAvailable;
        public int ContinuousRounds => continuousRounds;
        public int AvailableCount => availableCount;

        public int CurrentBuffCount => BuffCount;

        public bool IsPermanent => isPermanent;
        public bool IsActive => continuousRounds != InactiveContinuousRounds;

        public bool IsSame(CharacterBuff other)
        {
            if (other.GetType() != GetType()) return false;
            return buffName == other.buffName;
        }

        public virtual void InitBuff(List<Character> target)
        {
        }

        public virtual void ApplyBuff(List<Character> target)
        {
        }

        public virtual void RemoveBuff(List<Character> target)
        {
            // TODO: Implement the logic to remove the buff from the target character
        }

        public virtual void UpdateBuff(List<Character> target)
        {
        }

        public virtual void UpdateRounds(List<Character> target)
        {
            if (isPermanent)
            {
                return;
            }

            if (continuousRounds == InactiveContinuousRounds)
            {
                return;
            }

            continuousRounds--;
            if (continuousRounds <= 0)
            {
                RemoveBuff(target);
            }
        }
    }
}