using System;
using System.Collections.Generic;
using CardSystem.CharacterCard;
using UnityEngine;

namespace CardSystem.ActionCard
{
    [CreateAssetMenu(fileName = "NewAction", menuName = "Card/Action", order = 0)]
    public class Action : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string actionName;
        [SerializeField] private Sprite actionImage;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private ActionType actionType;
        [SerializeField] private int identityCost;
        [SerializeField] private int distinctCost;
        [SerializeField] private int energyCost;
        [SerializeField] private int attackDamage;
        [SerializeField] private CharacterBuff[] buffs;
        [SerializeField] private int healAmount;
        [SerializeField] private int energyRestoreAmount;
        [SerializeField] private List<Summon> summonUnits;


        public string ActionName => actionName;

        public Sprite ActionImage => actionImage;

        public string Description => description;

        public ActionType ActionType => actionType;

        public int IdentityCost => identityCost;

        public int DistinctCost => distinctCost;

        public int EnergyCost => energyCost;

        public int AttackDamage => attackDamage;

        public CharacterBuff[] Buffs => buffs;

        public int HealAmount => healAmount;

        public int EnergyRestoreAmount => energyRestoreAmount;

        public List<Summon> SummonUnits => summonUnits;


        public virtual void BeforeApplyEffect(Character target)
        {
            // TODO: Override this method to implement pre-effect logic
        }

        public virtual void AfterApplyEffect(Character target)
        {
            // TODO: Override this method to implement post-effect logic
        }

        public virtual void ApplyEffect(Character target, Character[] charactersOnField = null)
        {
            BeforeApplyEffect(target);

            // TODO: Apply attack damage if the action type includes attack

            AfterApplyEffect(target);
        }
    }


    [Flags]
    public enum ActionType
    {
        None = 0,
        Attack = 1 << 0,
        Heal = 1 << 1,
        Buff = 1 << 2,
        Debuff = 1 << 3,
        Summon = 1 << 4,
        EnergyRestore = 1 << 5,
    }
}