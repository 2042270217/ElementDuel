using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardSystem.CharacterCard
{
    [CreateAssetMenu(fileName = "NewCharacterAbility", menuName = "Character/Ability", order = 0)]
    public class CharacterAbilityData : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string abilityName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private AbilityEffectType abilityEffectType;
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private int totalCost;
        [SerializeField] private int specificElementCost;
        [SerializeField] private int energyCost;
        [SerializeField] private int attackDamage;
        [SerializeField] private CharacterBuff[] buffs;
        [SerializeField] private int healAmount;
        [SerializeField] private int energyRestoreAmount;

        [SerializeField] private Summon summonUnit;

        public string AbilityName => abilityName;

        public Sprite Icon => icon;

        public string Description => description;

        public AbilityEffectType AbilityEffectType => abilityEffectType;

        public AbilityType AbilityType => abilityType;

        public int TotalCost => totalCost;

        public int SpecificElementCost => specificElementCost;

        public int EnergyCost => energyCost;

        public int AttackDamage => attackDamage;

        public CharacterBuff[] Buffs => buffs;

        public int HealAmount => healAmount;

        public int EnergyRestoreAmount => energyRestoreAmount;

        public Summon SummonUnit => summonUnit;
    }

    public class CharacterAbility
    {
        private CharacterAbilityData _abilityData;

        public string AbilityName => _abilityData.AbilityName;

        public Sprite Icon => _abilityData.Icon;

        public string Description => _abilityData.Description;

        public AbilityEffectType AbilityEffectType => _abilityData.AbilityEffectType;

        public AbilityType AbilityType => _abilityData.AbilityType;

        public int TotalCost => _abilityData.TotalCost;

        public int SpecificElementCost => _abilityData.SpecificElementCost;

        public int EnergyCost => _abilityData.EnergyCost;

        public int AttackDamage => _abilityData.AttackDamage;

        public CharacterBuff[] Buffs => _abilityData.Buffs;

        public int HealAmount => _abilityData.HealAmount;

        public int EnergyRestoreAmount => _abilityData.EnergyRestoreAmount;

        public Summon SummonUnit => _abilityData.SummonUnit;

        public virtual void Initialize(CharacterAbilityData abilityData)
        {
            _abilityData = abilityData ??
                           throw new ArgumentNullException(nameof(abilityData), "Ability data cannot be null.");
        }

        public virtual void BeforeApplyEffect(List<Character> target)
        {
        }

        public virtual void AfterApplyEffect(List<Character> target)
        {
        }

        public virtual void
            ApplyEffect(List<Character> target,
                List<Summon> summonsOnFiled = null)
        {
            BeforeApplyEffect(target);

            if (AbilityEffectType.HasFlag(AbilityEffectType.Attack))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].TakeDamage(AttackDamage);
            }

            if (AbilityEffectType.HasFlag(AbilityEffectType.Defense))
            {
                // target.AddDefense(defenseValue);
            }

            if (AbilityEffectType.HasFlag(AbilityEffectType.Buff))
            {
            }

            if (AbilityEffectType.HasFlag(AbilityEffectType.Heal))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].Heal(HealAmount);
            }

            if (AbilityEffectType.HasFlag(AbilityEffectType.Energy))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].RestoreEnergy(EnergyRestoreAmount);
            }

            if (AbilityEffectType.HasFlag(AbilityEffectType.Summon))
            {
                if (summonsOnFiled != null)
                {
                    if (summonsOnFiled.Count < 4)
                    {
                        summonsOnFiled.Add(SummonUnit);
                    }
                    else
                    {
                        // TODO: Handle the case where the field is full (try refresh existing units or ignore the summon)
                    }
                }
                else
                {
                    EDebug.Log("Summon units cannot be applied without a target field.",
                        LogLevel.All); // TODO: Replace with appropriate logging level
                }
            }

            AfterApplyEffect(target);
        }
    }


    [Flags]
    public enum AbilityEffectType
    {
        None = 0,
        Attack = 1 << 0,
        Defense = 1 << 1,
        Buff = 1 << 2,
        Heal = 1 << 3,
        Energy = 1 << 4,
        Summon = 1 << 5,
    }


    public enum AbilityType
    {
        None,
        NormalAttack,
        Skill,
        Burst,
    }
}