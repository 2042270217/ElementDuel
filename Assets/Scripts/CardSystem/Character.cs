using System;
using System.Collections.Generic;
using UnityEngine;


namespace CardSystem
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Card/Character", order = 0)]
    public class Character : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string characterName;
        [SerializeField] private Sprite characterImage;
        [SerializeField] private Country country;
        [SerializeField] private Element element;
        [SerializeField] private Weapon weaponType;

        [Header("属性")] [SerializeField] private int maxHp = 10;
        [SerializeField] private int maxEnergy;


        [Header("技能信息")] [SerializeField] private CharacterAbility[] abilities;

        private int _currentHp = 10;
        private int _currentEnergy;

        private List<CharacterBuff> _buffs;

        public string CharacterName => characterName;

        public Sprite CharacterImage => characterImage;

        public int MaxHp => maxHp;

        public int CurrentHp => _currentHp;

        public int MaxEnergy => maxEnergy;

        public int CurrentEnergy => _currentEnergy;


        public CharacterAbility[] Abilities => abilities;


        public bool CanUseEnergy(int amount = 0)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Energy usage amount cannot be negative.");
            }

            return _currentEnergy >= amount;
        }


        public void TakeDamage(int damage)
        {
            if (damage < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(damage), "Damage cannot be negative.");
            }

            _currentHp = Mathf.Max(_currentHp - damage, 0);
        }


        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Heal amount cannot be negative.");
            }

            _currentHp = Mathf.Min(_currentHp + amount, maxHp);
        }


        public void RestoreEnergy(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Energy restoration amount cannot be negative.");
            }

            _currentEnergy = Mathf.Min(_currentEnergy + amount, maxEnergy);
        }


        public void UseEnergy(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Energy usage amount cannot be negative.");
            }

            _currentEnergy = Mathf.Max(_currentEnergy - amount, 0);
        }


        public void AddBuff(CharacterBuff buff)
        {
            _buffs.Add(buff);
        }
    }

    public enum Element
    {
        Pyro, // 火
        Hydro, // 水
        Anemo, // 风
        Electro, // 雷
        Dendro, // 草
        Cryo, // 冰
        Geo, // 岩
    }

    public enum Weapon
    {
        Bows,
        Catalysts,
        Claymores,
        Polearms,
        Swords
    }

    public enum Country
    {
        Mondstadt,
        Liyue,
        Inazuma,
        Sumeru,
        Fontaine,
        Natlan,
        Snezhnaya
    }
}