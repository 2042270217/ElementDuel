using System;
using System.Collections.Generic;
using UnityEngine;

namespace CardSystem.CharacterCard
{
    [CreateAssetMenu(fileName = "NewSummon", menuName = "Character/Summon", order = 0)]
    public class SummonData : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string summonName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private SummonEffectType summonEffectType;
        [SerializeField] private int maxRounds;
        [SerializeField] private int rounds;
        [SerializeField] private int attackDamage;
        [SerializeField] private int healAmount;


        public string SummonName => summonName;

        public Sprite Icon => icon;

        public string Description => description;

        public SummonEffectType SummonEffectType => summonEffectType;

        public int MaxRounds => maxRounds;

        public int Rounds => rounds;


        public int AttackDamage => attackDamage;


        public int HealAmount => healAmount;
    }

    public class Summon
    {
        private SummonData _summonData;

        protected int RemainingRounds;


        public string SummonName => _summonData.SummonName;

        public Sprite Icon => _summonData.Icon;

        public string Description => _summonData.Description;

        public SummonEffectType SummonEffectType => _summonData.SummonEffectType;

        public int MaxRounds => _summonData.MaxRounds;

        public int Rounds => _summonData.Rounds;

        public int CurrentRemainingRounds => RemainingRounds;

        public int AttackDamage => _summonData.AttackDamage;


        public int HealAmount => _summonData.HealAmount;

        public virtual void Init(SummonData summonData)
        {
            _summonData = summonData;
            RemainingRounds = _summonData.Rounds;
        }

        public virtual void BeforeApplyEffect(List<Character> target)
        {
        }

        public virtual void AfterApplyEffect(List<Character> target)
        {
        }

        public virtual void ApplyEffect(List<Character> target, List<Summon> summonsOnFiled = null)
        {
            BeforeApplyEffect(target);

            if (SummonEffectType.HasFlag(SummonEffectType.Attack))
            {
                foreach (var character in target)
                {
                    character.TakeDamage(AttackDamage);
                }
            }

            if (SummonEffectType.HasFlag(SummonEffectType.Heal))
            {
                foreach (var character in target)
                {
                    character.Heal(HealAmount);
                }
            }

            if (SummonEffectType.HasFlag(SummonEffectType.Defense))
            {
                foreach (var character in target)
                {
                }
            }

            if (SummonEffectType.HasFlag(SummonEffectType.EnergyRestore))
            {
                foreach (var character in target)
                {
                    character.RestoreEnergy(HealAmount);
                }
            }

            AfterApplyEffect(target);
        }
    }


    [Flags]
    public enum SummonEffectType
    {
        None = 0,
        Attack = 1 << 0,
        Heal = 1 << 1,
        Defense = 1 << 2,
        EnergyRestore = 1 << 3,
    }
}