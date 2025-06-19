using System;
using System.Collections.Generic;
using UnityEngine;


namespace CardSystem.CharacterCard
{
    [CreateAssetMenu(fileName = "NewCharacter", menuName = "Card/Character", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string characterName;
        [SerializeField] private Sprite characterImage;
        [SerializeField] private Country country;
        [SerializeField] private Element element;
        [SerializeField] private Weapon weaponType;

        [Header("属性")] [SerializeField] private int maxHp = 10;
        [SerializeField] private int maxEnergy;


        [Header("技能信息")] [SerializeField] private CharacterAbility[] abilities;


        public string CharacterName => characterName;

        public Sprite CharacterImage => characterImage;

        public int MaxHp => maxHp;


        public int MaxEnergy => maxEnergy;


        public CharacterAbility[] Abilities => abilities;
    }

    public class Character
    {
        private CharacterData _characterData;

        private int _currentHp = 10;
        private int _currentEnergy;

        private List<CharacterBuff> _buffs;

        public string CharacterName => _characterData.CharacterName;

        public Sprite CharacterImage => _characterData.CharacterImage;

        public int MaxHp => _characterData.MaxHp;

        public int CurrentHp => _currentHp;

        public int MaxEnergy => _characterData.MaxEnergy;

        public int CurrentEnergy => _currentEnergy;


        public CharacterAbility[] Abilities => _characterData.Abilities;


        public bool CanUseEnergy(int amount = 0)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Energy usage amount cannot be negative.");
            }

            return _currentEnergy >= amount;
        }

        public virtual void Initialize(CharacterData characterData)
        {
            _characterData = characterData ??
                             throw new ArgumentNullException(nameof(characterData), "Character data cannot be null.");
            _currentHp = MaxHp;
            _currentEnergy = 0;
            _buffs = new List<CharacterBuff>();
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

            _currentHp = Mathf.Min(_currentHp + amount, MaxHp);
        }


        public void RestoreEnergy(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Energy restoration amount cannot be negative.");
            }

            _currentEnergy = Mathf.Min(_currentEnergy + amount, MaxEnergy);
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