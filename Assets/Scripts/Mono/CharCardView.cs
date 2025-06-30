using ElementDuel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CharCardView : MonoBehaviour, IClickReceiver
{
	[HideInInspector] public CharGroupView charGroupView;
	public Color deadColor;
	public Color frozenColor;

	GameObject m_main;
	Image m_charImage;
	Image m_isSelectedIcon;

	GameObject m_healthBlock;
	TMP_Text m_healthPoint;

	GameObject m_energyBlock;
	[SerializeField] EnergyView m_energyPrefab;
	List<EnergyView> m_energyViews;

	[SerializeField] BuffView m_buffPrefab;
	GameObject m_commonBuffBlock;
	GameObject m_commonBuffViewPort;
	UIPool<BuffView> m_commonBuffPool;
	List<BuffView> m_commonBuffViews;

	GameObject m_fightingBuffBlock;
	GameObject m_fightingBuffViewPort;
	UIPool<BuffView> m_fightingBuffPool;
	List<BuffView> m_fightingBuffViews;

	GameObject m_equipmentBlock;
	Image m_weapon;
	Image m_relic;

	GameObject m_element;
	[SerializeField] ElementView m_elementPrefab;
	UIPool<ElementView> m_elementPool;
	List<ElementView> m_elementViews;

	[SerializeField] bool m_isSelected = false;
	bool m_enableClick = true;

	public event Action<BaseCharacterCard> doubleClicked;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{


	}

	// Update is called once per frame
	void Update()
	{

	}

	void Initialize()
	{
		m_main = UnityTools.FindChildGameObject(gameObject, "Main");
		m_charImage = UITools.GetUIComponet<Image>(m_main, "char");
		m_isSelectedIcon = UITools.GetUIComponet<Image>(m_main, "isSelected");

		m_healthBlock = UnityTools.FindChildGameObject(gameObject, "HealthBlock");
		m_healthPoint = UITools.GetUIComponet<TMP_Text>(m_healthBlock, "point");

		m_energyBlock = UnityTools.FindChildGameObject(gameObject, "EnergyBlock");
		m_energyViews = new List<EnergyView>();

		m_commonBuffBlock = UnityTools.FindChildGameObject(gameObject, "CommonBuffBlock");
		m_commonBuffViewPort = UnityTools.FindChildGameObject(m_commonBuffBlock, "ViewPort");

		m_fightingBuffBlock = UnityTools.FindChildGameObject(gameObject, "FightingBuffBlock");
		m_fightingBuffViewPort = UnityTools.FindChildGameObject(m_fightingBuffBlock, "ViewPort");

		if (m_commonBuffPool == null)
			m_commonBuffPool = new UIPool<BuffView>(m_buffPrefab, m_commonBuffViewPort.transform);
		if (m_fightingBuffPool == null)
			m_fightingBuffPool = new UIPool<BuffView>(m_buffPrefab, m_fightingBuffViewPort.transform);
		if (m_commonBuffViews == null)
			m_commonBuffViews = new List<BuffView>();
		if (m_fightingBuffViews == null)
			m_fightingBuffViews = new List<BuffView>();

		m_equipmentBlock = UnityTools.FindChildGameObject(gameObject, "EquipmentBlock");
		m_weapon = UITools.GetUIComponet<Image>(m_equipmentBlock, "weapon");
		m_relic = UITools.GetUIComponet<Image>(m_equipmentBlock, "relic");

		m_element = UnityTools.FindChildGameObject(gameObject, "Element");
		if (m_elementPool == null)
			m_elementPool = new UIPool<ElementView>(m_elementPrefab, m_element.transform);
		if (m_elementViews == null)
			m_elementViews = new List<ElementView>();

	}

	/// <summary>
	/// 待实现
	/// </summary>
	public void Initialize(BaseCharacterCard character)
	{
		//获取相关组件
		Initialize();

		//设置角色卡面
		UpdateCharSprite(character);

		//设置角色充能
		InitializeEnergy(character.charData.MaxEnergy);

		//设置角色生命值
		UpdateHealthPoint(character.attrib.currentHp);

		//设置角色Buff
		InitializeBuff();

		//设置角色装备
		SetupRelicActive(character.attrib.reliced);
		SetupWeaponActive(character.attrib.weaponed);
	}

	public void UpdateUI(BaseCharacterCard character)
	{
		UpdateCharSprite(character);
		UpdateEnergy(character.attrib.currentEnergy);
		UpdateHealthPoint(character.attrib.currentHp);
		UpdateAttachedElement(character.attrib.attachedElement);
		SetupRelicActive(character.attrib.reliced);
		SetupWeaponActive(character.attrib.weaponed);
		//更新buff
		UpdateBuff(character.fightingBuff, character.commonBuff);
	}

	void ReleaseAllBuff()
	{
		m_fightingBuffPool?.ReleaseAll(m_fightingBuffViews);
		m_fightingBuffViews?.Clear();

		m_commonBuffPool?.ReleaseAll(m_commonBuffViews);
		m_commonBuffViews?.Clear();
	}

	void UpdateFightingBuff(List<BaseBuff> fightingBuff)
	{
		int index = 0;
		foreach (BaseBuff b in fightingBuff)
		{
			var buffView = m_fightingBuffPool.Get();
			buffView.Initialize(b);
			buffView.transform.SetSiblingIndex(index++);
			m_fightingBuffViews.Add(buffView);
		}
	}

	void UpdateCommonBuff(List<BaseBuff> commonBuff)
	{
		int index = 0;
		foreach (BaseBuff b in commonBuff)
		{
			var buffView = m_commonBuffPool.Get();
			buffView.Initialize(b);
			buffView.transform.SetSiblingIndex(index++);
			m_commonBuffViews.Add(buffView);
		}
	}

	public void UpdateBuff(List<BaseBuff> fightingBuff, List<BaseBuff> commonBuff)
	{
		ReleaseAllBuff();

		UpdateCommonBuff(commonBuff);
		UpdateFightingBuff(fightingBuff);
	}

	void ClearChild(GameObject root)
	{
		if (root.transform.childCount == 0) return;
		foreach (Transform child in root.transform)
		{
			Destroy(child.gameObject);
		}
	}

	public void UpdateAttachedElement(List<ElementType> elements)
	{
		m_elementPool?.ReleaseAll(m_elementViews);
		m_elementViews?.Clear();

		if (elements == null || elements.Count == 0)
		{
			return;
		}
		int index = 0;
		foreach (ElementType element in elements)
		{
			var view = m_elementPool.Get();
			view.Initialize(element);
			view.transform.SetSiblingIndex(index++);
			m_elementViews.Add(view);
		}
	}

	public void UpdateCharSprite(BaseCharacterCard c)
	{
		m_charImage.sprite = c.charData.sprite;
		if (c.attrib.currentHp <= 0)
		{
			m_charImage.color = deadColor;
			return;
		}
		if (c.attrib.isFrozen)
		{
			m_charImage.color = frozenColor;
			return;
		}
		m_charImage.color = Color.white;

	}

	public void UpdateHealthPoint(int point)
	{
		m_healthPoint.text = point.ToString();
	}

	public void InitializeEnergy(int maxEnergy)
	{
		ClearChild(m_energyBlock);

		for (int i = 0; i < maxEnergy; i++)
		{
			var energyView = Instantiate(m_energyPrefab, m_energyBlock.transform);
			energyView.Initialize(false);
			m_energyViews.Add(energyView);
		}
	}

	public void UpdateEnergy(int count)
	{
		int energyCount = count;
		foreach (var view in m_energyViews)
		{
			if (count > 0)
			{
				view.SetActive(true);
				count--;
			}
			else
				view.SetActive(false);
		}
	}



	void InitializeBuff()
	{
		ClearChild(m_commonBuffViewPort);
		ClearChild(m_fightingBuffViewPort);
	}


	public void SetupWeaponActive(bool active)
	{
		m_weapon.gameObject.SetActive(active);
	}

	public void SetupRelicActive(bool active)
	{
		m_relic.gameObject.SetActive(active);
	}

	public void OnClick()
	{
		if (!m_enableClick) return;
		if (m_isSelected)
		{
			doubleClicked?.Invoke(charGroupView.GetCharCard(this));
			m_isSelected = false;
			UpdateCharacterSelection();
		}
		else
		{
			charGroupView.SetSelectionAll(false);
			m_isSelected = !m_isSelected;
			UpdateCharacterSelection();
		}

	}

	public void UpdateCharacterSelection()
	{
		Color c = m_isSelectedIcon.color;
		c.a = m_isSelected ? 1 : 0;
		m_isSelectedIcon.color = c;
	}

	public void SetupClick(bool enableClick)
	{
		m_enableClick = enableClick;
		m_charImage.raycastTarget = enableClick;
		SetSelection(false);
	}

	public void SetSelection(bool isSelected)
	{
		m_isSelected = isSelected;
		UpdateCharacterSelection();
	}
}
