using System;
using System.Collections.Generic;
using CardSystem.CharacterCard;
using UnityEngine;

namespace CardSystem.ActionCard
{
    [CreateAssetMenu(fileName = "NewAction", menuName = "Card/Action", order = 0)]
    public class ActionData : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string actionName;
        [SerializeField] private Sprite actionImage;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private ActionType actionType;
        [SerializeField] private int identityCost;
        [SerializeField] private int distinctCost;
        [SerializeField] private int energyCost;
        [SerializeField] private int attackDamage;
        [SerializeField] private int healAmount;
        [SerializeField] private int energyRestoreAmount;


        public string ActionName => actionName;

        public Sprite ActionImage => actionImage;

        public string Description => description;

        public ActionType ActionType => actionType;

        public int IdentityCost => identityCost;

        public int DistinctCost => distinctCost;

        public int EnergyCost => energyCost;

        public int AttackDamage => attackDamage;


        public int HealAmount => healAmount;

        public int EnergyRestoreAmount => energyRestoreAmount;


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


    public class Action
    {
        private ActionData _actionData;

        private List<CharacterBuff> _buffs;
        private List<Summon> _summonUnits;


        public string ActionName => _actionData.ActionName;

        public Sprite ActionImage => _actionData.ActionImage;

        public string Description => _actionData.Description;

        public ActionType ActionType => _actionData.ActionType;

        public int IdentityCost => _actionData.IdentityCost;

        public int DistinctCost => _actionData.DistinctCost;

        public int EnergyCost => _actionData.EnergyCost;

        public int AttackDamage => _actionData.AttackDamage;

        public List<CharacterBuff> Buffs => _buffs;

        public int HealAmount => _actionData.HealAmount;

        public int EnergyRestoreAmount => _actionData.EnergyRestoreAmount;

        public List<Summon> SummonUnits => _summonUnits;
        
        public virtual void Initialize(ActionData actionData)
        {
            _actionData = actionData;
            _buffs = new List<CharacterBuff>();
            _summonUnits = new List<Summon>();
        }


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