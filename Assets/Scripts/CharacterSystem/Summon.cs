using System;
using System.Collections.Generic;
using UnityEngine;

namespace CharacterSystem
{
    [CreateAssetMenu(fileName = "NewSummon", menuName = "Character/Summon", order = 0)]
    public class Summon : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string summonName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private SummonEffectType summonEffectType;
        [SerializeField] private int maxRounds;
        [SerializeField] private int attackDamage;
        [SerializeField] private CharacterBuff defenseBuff;
        [SerializeField] private int healAmount;

        public string SummonName => summonName;

        public Sprite Icon => icon;

        public string Description => description;

        public SummonEffectType SummonEffectType => summonEffectType;

        public int MaxRounds => maxRounds;

        public int AttackDamage => attackDamage;

        public CharacterBuff DefenseBuff => defenseBuff;

        public int HealAmount => healAmount;

        public virtual void BeforeApplyEffect(List<Character> target)
        {
        }

        public virtual void AfterApplyEffect(List<Character> target)
        {
        }

        public virtual void ApplyEffect(List<Character> target, List<Summon> summonsOnFiled = null)
        {
            BeforeApplyEffect(target);

            if (summonEffectType.HasFlag(SummonEffectType.Attack))
            {
                foreach (var character in target)
                {
                    character.TakeDamage(attackDamage);
                }
            }

            if (summonEffectType.HasFlag(SummonEffectType.Heal))
            {
                foreach (var character in target)
                {
                    character.Heal(healAmount);
                }
            }

            if (summonEffectType.HasFlag(SummonEffectType.Defense))
            {
                foreach (var character in target)
                {
                    character.AddBuff(defenseBuff);
                }
            }

            if (summonEffectType.HasFlag(SummonEffectType.EnergyRestore))
            {
                foreach (var character in target)
                {
                    character.RestoreEnergy(healAmount);
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