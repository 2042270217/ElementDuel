using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CardSystem
{
    [CreateAssetMenu(fileName = "NewCharacterAbility", menuName = "Character/Ability", order = 0)]
    public class CharacterAbility : ScriptableObject
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

            if (abilityEffectType.HasFlag(AbilityEffectType.Attack))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].TakeDamage(attackDamage);
            }

            if (abilityEffectType.HasFlag(AbilityEffectType.Defense))
            {
                // target.AddDefense(defenseValue);
            }

            if (abilityEffectType.HasFlag(AbilityEffectType.Buff))
            {
            }

            if (abilityEffectType.HasFlag(AbilityEffectType.Heal))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].Heal(healAmount);
            }

            if (abilityEffectType.HasFlag(AbilityEffectType.Energy))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].RestoreEnergy(energyRestoreAmount);
            }

            if (abilityEffectType.HasFlag(AbilityEffectType.Summon))
            {
                if (summonsOnFiled != null)
                {
                    if (summonsOnFiled.Count < 4)
                    {
                        summonsOnFiled.Add(summonUnit);
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