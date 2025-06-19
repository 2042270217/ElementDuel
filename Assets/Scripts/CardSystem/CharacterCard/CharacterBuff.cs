using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardSystem.CharacterCard
{
    [CreateAssetMenu(fileName = "NewBuff", menuName = "Character/Buff", order = 0)]
    public class CharacterBuffData : ScriptableObject
    {
        public static readonly int InactiveContinuousRounds = -1;

        [Header("基础信息")] [SerializeField] private string buffName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private bool isPermanent;
        [SerializeField] private bool isOnField;
        [SerializeField] private bool isAlwaysAvailable;
        [SerializeField] protected int continuousRounds = InactiveContinuousRounds;
        [SerializeField] private int availableCount = 1;


        public string BuffName => buffName;
        public Sprite Icon => icon;
        public string Description => description;

        public bool IsOnField => isOnField;
        public bool IsAlwaysAvailable => isAlwaysAvailable;
        public int ContinuousRounds => continuousRounds;
        public int AvailableCount => availableCount;


        public bool IsPermanent => isPermanent;

        public bool IsSame(CharacterBuff other)
        {
            if (other.GetType() != GetType()) return false;
            return buffName == other.BuffName;
        }
    }

    public abstract class CharacterBuff
    {
        protected static readonly int InactiveContinuousRounds = -1;

        private CharacterBuffData _buffData;


        protected int RemainingRounds;
        protected int BuffCount;


        public string BuffName => _buffData.BuffName;
        public Sprite Icon => _buffData.Icon;
        public string Description => _buffData.Description;

        public bool IsOnField => _buffData.IsOnField;
        public bool IsAlwaysAvailable => _buffData.IsAlwaysAvailable;
        public int ContinuousRounds => _buffData.ContinuousRounds;
        public int AvailableCount => _buffData.AvailableCount;

        public int CurrentBuffCount => BuffCount;

        public int CurrentRemainingRounds => RemainingRounds;

        public bool IsPermanent => _buffData.IsPermanent;

        public bool IsActive => RemainingRounds != InactiveContinuousRounds;

        public bool IsSame(CharacterBuff other)
        {
            if (other.GetType() != GetType()) return false;
            return _buffData.BuffName == other._buffData.BuffName;
        }

        public virtual void Initlize(CharacterBuffData buffData)
        {
            _buffData = buffData;
            RemainingRounds = ContinuousRounds;
            BuffCount = 0;
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
            if (IsPermanent)
            {
                return;
            }

            if (CurrentRemainingRounds == InactiveContinuousRounds)
            {
                return;
            }

            RemainingRounds--;
            if (CurrentRemainingRounds <= 0)
            {
                RemoveBuff(target);
            }
        }
    }
}