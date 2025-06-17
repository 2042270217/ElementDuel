using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CharacterSystem
{
    [CreateAssetMenu(fileName = "NewCharacterAbility", menuName = "Character/Ability", order = 0)]
    public class CharacterAbility : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string abilityName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private AbilityEffectType abilityEffectType;
        [SerializeField] private AbilityType abilityType;
        [SerializeField] private int cost;
        [SerializeField] private int attackDamage;
        [SerializeField] private CharacterBuff defenseBuff;
        [SerializeField] private int healAmount;
        [SerializeField] private int energyRestoreAmount;

        [FormerlySerializedAs("summonUnits")] [SerializeField]
        private Summon summonUnit;

        public string AbilityName => abilityName;

        public Sprite Icon => icon;

        public string Description => description;

        public AbilityEffectType AbilityEffectType => abilityEffectType;

        public AbilityType AbilityType => abilityType;

        public int Cost => cost;

        public int AttackDamage => attackDamage;

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

            if (AbilityTypeCompare.IsAttack(abilityEffectType))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].TakeDamage(attackDamage);
            }

            if (AbilityTypeCompare.IsDefense(abilityEffectType))
            {
                // target.AddDefense(defenseValue);
            }

            if (AbilityTypeCompare.IsBuff(abilityEffectType))
            {
            }

            if (AbilityTypeCompare.IsHeal(abilityEffectType))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].Heal(healAmount);
            }

            if (AbilityTypeCompare.IsEnergy(abilityEffectType))
            {
                if (target.Count != 1)
                {
                    throw new ArgumentException("Target must contain exactly one character.");
                }

                target[0].RestoreEnergy(energyRestoreAmount);
            }

            if (AbilityTypeCompare.IsSummon(abilityEffectType))
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
        Buff = 1 << 2, // Renamed from Heal to Buff for clarity
        Heal = 1 << 3,
        Energy = 1 << 4,
        Summon = 1 << 5,
    }

    public static class AbilityTypeCompare
    {
        public static bool IsNone(AbilityEffectType abilityEffectType)
        {
            return abilityEffectType == AbilityEffectType.None;
        }

        public static bool IsAttack(AbilityEffectType abilityEffectType)
        {
            return (abilityEffectType & AbilityEffectType.Attack) != AbilityEffectType.None;
        }

        public static bool IsDefense(AbilityEffectType abilityEffectType)
        {
            return (abilityEffectType & AbilityEffectType.Defense) != AbilityEffectType.None;
        }

        public static bool IsBuff(AbilityEffectType abilityEffectType)
        {
            return (abilityEffectType & AbilityEffectType.Buff) != AbilityEffectType.None;
        }

        public static bool IsHeal(AbilityEffectType abilityEffectType)
        {
            return (abilityEffectType & AbilityEffectType.Heal) != AbilityEffectType.None;
        }

        public static bool IsEnergy(AbilityEffectType abilityEffectType)
        {
            return (abilityEffectType & AbilityEffectType.Energy) != AbilityEffectType.None;
        }

        public static bool IsSummon(AbilityEffectType abilityEffectType)
        {
            return (abilityEffectType & AbilityEffectType.Summon) != AbilityEffectType.None;
        }
    }

    public enum AbilityType
    {
        None,
        NormalAttack,
        Skill,
        Burst,
    }
}