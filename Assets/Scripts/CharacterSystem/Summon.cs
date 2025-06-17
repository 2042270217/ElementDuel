using UnityEngine;

namespace CharacterSystem
{
    [CreateAssetMenu(fileName = "NewSummon", menuName = "Character/Summon", order = 0)]
    public class Summon : ScriptableObject
    {
        [Header("基础信息")] [SerializeField] private string summonName;
        [SerializeField] private Sprite icon;
        [SerializeField] private string description;

        [Header("属性")] [SerializeField] private int maxRounds;
        [SerializeField] private int attackDamage;
        [SerializeField] private CharacterBuff defenseBuff;
        [SerializeField] private int healAmount;
    }
}