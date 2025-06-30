using ElementDuel;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

public abstract class BaseCharacterCard : ScriptableObject
{
	public CharacterCardData charData;
	public List<Skill> skills = new();
	[HideInInspector] public virtual int id => IdRegistry.GetId(GetType());
	[HideInInspector] public CharacterAttribute attrib;
	[HideInInspector] public List<BaseBuff> fightingBuff = new List<BaseBuff>();
	[HideInInspector] public List<BaseBuff> commonBuff = new List<BaseBuff>();


	[HideInInspector]
	public UnityEvent AfterUseNormalAttack = new UnityEvent();
	[HideInInspector]
	public UnityEvent BeforeUseNormalAttack = new UnityEvent();

	[HideInInspector]
	public UnityEvent AfterUseElementSkill = new UnityEvent();
	[HideInInspector]
	public UnityEvent BeforeUseElementSkill = new UnityEvent();

	[HideInInspector]
	public UnityEvent AfterUseBurst = new UnityEvent();
	[HideInInspector]
	public UnityEvent BeforeUseBurst = new UnityEvent();

	[HideInInspector]
	public UnityEvent AfterTakenFireDamage = new UnityEvent();
	[HideInInspector]
	public UnityEvent AfterTakenPhysicalDamage = new UnityEvent();

	public ElementDuelGame game;
	public PlayerSystem ownerPlayer;


	public int hpLost => charData.MaxHp - attrib.currentHp;
	public bool canUseBurst => attrib.currentEnergy >= charData.MaxEnergy;
	public bool isDead => attrib.currentHp <= 0;
	public bool canAct => !attrib.isFrozen && !isDead;

	//ÊÂ¼þ»ã×Ü
	void InvokeAfterUseSkill()
	{
		ownerPlayer.AfterUseSkill?.Invoke(this);
	}

	void InvokeBeforeUseSkill()
	{
		ownerPlayer.BeforeUseSkill?.Invoke(this);
	}

	public void InvokeAfterUseElementSkill()
	{
		AfterUseElementSkill?.Invoke();
		InvokeAfterUseSkill();
	}

	public void InvokeBeforeUseElementSkill()
	{
		BeforeUseElementSkill?.Invoke();
		InvokeBeforeUseSkill();
	}

	public void InvokeAfterUseBurst()
	{
		AfterUseBurst?.Invoke();
		InvokeAfterUseSkill();
	}

	public void InvokeBeforeUseBurst()
	{
		BeforeUseBurst?.Invoke();
		InvokeBeforeUseSkill();
	}

	public void InvokeAfterUseNormalAttack()
	{
		AfterUseNormalAttack?.Invoke();
		InvokeAfterUseSkill();
	}

	public void InvokeBeforeUseNormalAttack()
	{
		BeforeUseNormalAttack?.Invoke();
		InvokeBeforeUseSkill();
	}

	void InvokeAfterReceiveDamage(ElementType ele)
	{
		switch (ele)
		{
			case ElementType.None:
				AfterTakenPhysicalDamage?.Invoke();
				break;
			case ElementType.Fire:
				AfterTakenFireDamage?.Invoke();
				break;
			case ElementType.Water:
				break;
			case ElementType.Wind:
				break;
			case ElementType.Thunder:
				break;
			case ElementType.Grass:
				break;
			case ElementType.Ice:
				break;
			case ElementType.Rock:
				break;
			case ElementType.All:
				break;
		}
	}

	public void Initialize(ElementDuelGame game, PlayerSystem player)
	{
		attrib = new CharacterAttribute();
		attrib.currentEnergy = 0;
		attrib.currentHp = charData.MaxHp;

		skills = skills.Select(skill => Instantiate(skill)).ToList();
		foreach (Skill skill in skills)
		{
			skill.Initialize(this);
		}
		this.game = game;
		ownerPlayer = player;
	}

	int GetDamageTakenBonus(ElementType ele)
	{
		int count = 0;
		switch (ele)
		{
			case ElementType.None:
				count += attrib.physicalDamageTakenBonus;
				break;
			case ElementType.Fire:
				count += attrib.fireDamageTakenBonus;
				break;
			case ElementType.Water:
				count += attrib.waterDamageTakenBonus;
				break;
			case ElementType.Wind:
				count += attrib.windDamageTakenBonus;
				break;
			case ElementType.Thunder:
				count += attrib.thunderDamageTakenBonus;
				break;
			case ElementType.Grass:
				count += attrib.grassDamageTakenBonus;
				break;
			case ElementType.Ice:
				count += attrib.iceDamageTakenBonus;
				break;
			case ElementType.Rock:
				count += attrib.rockDamageTakenBonus;
				break;
		}
		count += attrib.damageTakenBonus;
		return count;
	}

	public void ReceiveDamage(int count, ElementType ele)
	{
		int baseDamage = count;
		if (ele != ElementType.None)
		{
			if (attrib.attachedElement.Count > 0)
			{
				List<ElementReaction> reactions = new List<ElementReaction>();
				for (int i = attrib.attachedElement.Count - 1; i >= 0; i--)
				{
					var attached = attrib.attachedElement[i];
					var r = ElementReactionRegistry.GetReaction(ele, attached);
					if (r != null)
					{
						reactions.Add(r);
						attrib.attachedElement.RemoveAt(i);
					}
				}

				foreach (var reaction in reactions)
				{
					baseDamage += reaction.bonusDamage;
					reaction.effect?.Invoke(this, game);
				}
			}
			else
			{
				attrib.attachedElement.Add(ele);
			}
		}
		baseDamage += GetDamageTakenBonus(ele);
		attrib.currentHp = Mathf.Max(0, attrib.currentHp - baseDamage);
		InvokeAfterReceiveDamage(ele);
		if (attrib.currentHp == 0)
		{
			ownerPlayer.ForceChangeToNext();
			attrib.attachedElement.Clear();
		}
		game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
	}

	public void AttachElement(ElementType ele)
	{
		if (ele != ElementType.None)
		{
			if (attrib.attachedElement.Count > 0)
			{
				List<ElementReaction> reactions = new List<ElementReaction>();
				for (int i = attrib.attachedElement.Count - 1; i >= 0; i--)
				{
					var attached = attrib.attachedElement[i];
					var r = ElementReactionRegistry.GetReaction(ele, attached);
					if (r != null)
					{
						reactions.Add(r);
						attrib.attachedElement.RemoveAt(i);
					}
				}

				foreach (var reaction in reactions)
				{
					reaction.effect?.Invoke(this, game);
				}
			}
			else
			{
				attrib.attachedElement.Add(ele);
			}
		}
	}

	public void GetHeal(int count)
	{
		if (!isDead)
		{
			attrib.currentHp += count;
			attrib.currentHp = Mathf.Min(attrib.currentHp, charData.MaxHp);
			game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
		}
	}

	public void AddEnergy(int count)
	{
		attrib.currentEnergy += count;
		attrib.currentEnergy = Mathf.Min(attrib.currentEnergy, charData.MaxEnergy);
		game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
	}

	public void RemoveEnergy(int count)
	{
		attrib.currentEnergy -= count;
		attrib.currentEnergy = Mathf.Min(attrib.currentEnergy, 0);
		game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
	}

	public void AddFightingBuff(BaseBuff buff)
	{
		if (fightingBuff.Exists(b => b.id == buff.id))
		{
			var find = fightingBuff.Find(b => b.id == buff.id);
			if (!find.OnDuplicateAdd())
			{
				game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
				return;
			}
		}

		fightingBuff.Add(buff);
		buff.Initialize(this, game);

		game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
	}

	public void RemoveFightingBuff(BaseBuff buff)
	{
		var find = fightingBuff.Find(b => b.id == buff.id);
		fightingBuff.Remove(buff);
		buff.Release();
		game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
	}

	public void AddCommonBuff(BaseBuff buff)
	{
		if (commonBuff.Exists(b => b.id == buff.id))
		{
			var find = commonBuff.Find(b => b.id == buff.id);
			if (!find.OnDuplicateAdd())
			{
				game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
				return;
			}
		}

		commonBuff.Add(buff);
		buff.Initialize(this, game);

		game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
	}

	public void RemoveCommonBuff(BaseBuff buff)
	{
		var find = commonBuff.Find(b => b.id == buff.id);
		commonBuff.Remove(buff);
		buff.Release();
		game.GetCharacterUI(ownerPlayer).UpdateCharacter(this);
	}
#if UNITY_EDITOR

	[SerializeField]
	[HideInInspector]
	protected bool m_isInitialized = false;

	protected void CreateFolderIfNotExists(string parent, string child)
	{
		string full = $"{parent}/{child}";
		if (!AssetDatabase.IsValidFolder(full))
		{
			AssetDatabase.CreateFolder(parent, child);
		}
	}

	protected void GetFolderPath(out string basePath, out string skillPath, out string skillDataPath)
	{
		string path = AssetDatabase.GetAssetPath(this);
		basePath = Path.GetDirectoryName(path).Replace("\\", "/");
		skillPath = $"{basePath}/Skill";
		skillDataPath = $"{skillPath}/Data";
	}

	protected void InitializeFolder(out string basePath, out string skillPath, out string skillDataPath)
	{
		GetFolderPath(out basePath, out skillPath, out skillDataPath);
		CreateFolderIfNotExists(basePath, "Skill");
		CreateFolderIfNotExists(skillPath, "Data");
	}

	protected void Refresh()
	{
		EditorUtility.SetDirty(this);
		AssetDatabase.SaveAssets();
		AssetDatabase.Refresh();
	}

	protected void CreatCharacterDataAsset(string path)
	{
		string name = this.GetType().Name;
		var characterData = ScriptableObject.CreateInstance<CharacterCardData>();
		characterData.name = $"{name}Data";
		AssetDatabase.CreateAsset(characterData, $"{path}/{characterData.name}.asset");
		charData = characterData;
	}

	protected void CreateSkillAsset<TSkill, TCharacter>(string skillPath, string skillDataPath) where TCharacter : BaseCharacterCard where TSkill : BaseSkill<TCharacter>
	{
		var skillData = CreateInstance<SkillData>();
		var skill = CreateInstance<TSkill>();
		string name = skill.GetType().Name;
		skill.name = name;
		skillData.name = $"{name}Data";
		skill.skillData = skillData;
		skill.m_owner = (TCharacter)this;

		AssetDatabase.CreateAsset(skill, $"{skillPath}/{skill.name}.asset");
		AssetDatabase.CreateAsset(skillData, $"{skillDataPath}/{skillData.name}.asset");
		skills.Add(skill);
	}



#endif
}
