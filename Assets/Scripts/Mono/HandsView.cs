using ElementDuel;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class HandsView : MonoBehaviour, IClickReceiver
{
	[HideInInspector] public HandsGroupView handsGroupView;
	[SerializeField] GameObject m_costDicePrefab;
	[SerializeField] ActionCardData m_cardData;
	[SerializeField] bool m_isSelected = false;
	bool m_enableClick = true;

	GameObject m_main;
	Image m_icon;
	Image m_isSelectedIcon;

	GameObject m_costGroup;

	public event Action<ActionCard> doubleClicked;

	public List<SkillCostCondition> cdts => m_cardData.cdts;
	public ActionCardTag cardTag => m_cardData.tag;
	public ActionCardType cardType => m_cardData.type;
	public bool isSelected => m_isSelected;

	private void Awake()
	{
		Initialize();
	}
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnValidate()
	{
		// 1. 排除运行时
		if (EditorApplication.isPlayingOrWillChangePlaymode) return;

		// 2. 排除Prefab模板（Prefab资产本体）
		if (PrefabUtility.IsPartOfPrefabAsset(gameObject)) return;

		// 3. 排除在Prefab编辑模式下的对象（是个临时场景）
		if (PrefabUtility.IsPartOfPrefabInstance(gameObject) &&
			PrefabStageUtility.GetCurrentPrefabStage() != null) return;

		EditorApplication.delayCall += () =>
		{
			Initialize();
			if (m_cardData == null || m_costDicePrefab == null) return;
			SetupSpirte(m_cardData.sprite);
			foreach (Transform child in m_costGroup.transform)
			{
				DestroyImmediate(child.gameObject);
			}
			SetupCostGroup(m_cardData.cdts);
		};

	}

	void Initialize()
	{
		if (m_main == null)
			m_main = UnityTools.FindChildGameObject(gameObject, "Main");
		m_icon = UITools.GetUIComponet<Image>(m_main, "icon");
		m_isSelectedIcon = UITools.GetUIComponet<Image>(m_main, "isSelected");

		if (m_costGroup == null)
			m_costGroup = UnityTools.FindChildGameObject(gameObject, "CostGroup");
	}

	public void Initialize(ActionCardData cardData, bool canBeClick)
	{
		Initialize();

		SetupClick(canBeClick);
		m_cardData = cardData;
		SetupSpirte(cardData.sprite);
		SetupCostGroup(cardData.cdts);
	}

	void SetupSpirte(Sprite sprite)
	{
		m_icon.sprite = sprite;
	}

	void SetupCostGroup(List<SkillCostCondition> cdts)
	{
		ClearChild(m_costGroup);
		foreach (SkillCostCondition cdt in cdts)
		{
			var go = Instantiate(m_costDicePrefab, m_costGroup.transform);
			go.GetComponent<CostDiceView>().Initialize(cdt);
		}
	}

	void ClearChild(GameObject go)
	{
		if (go.transform.childCount == 0) return;

		foreach (Transform child in go.transform)
		{

			Destroy(child.gameObject);

		}
	}

	public void OnClick()
	{
		if (handsGroupView == null)
		{
			m_isSelected = !m_isSelected;
			UpdateHandsSelection();
		}
		else
		{
			if (!m_enableClick) return;
			if (m_isSelected)
			{
				doubleClicked?.Invoke(handsGroupView.GetHands(this));
				m_isSelected = false;
				UpdateHandsSelection();
			}
			else
			{
				handsGroupView.SetSelectionAll(false);
				m_isSelected = !m_isSelected;
				UpdateHandsSelection();
			}
		}
	}

	public void UpdateHandsSelection()
	{
		Color c = m_isSelectedIcon.color;
		c.a = m_isSelected ? 1 : 0;
		m_isSelectedIcon.color = c;
	}

	public void SetupClick(bool enableClick)
	{
		m_enableClick = enableClick;
		m_icon.raycastTarget = enableClick;
		SetSelection(false);
	}

	public void SetSelection(bool isSelected)
	{
		m_isSelected = isSelected;
		UpdateHandsSelection();
	}

	public void Release()
	{
		handsGroupView = null;
	}
}
