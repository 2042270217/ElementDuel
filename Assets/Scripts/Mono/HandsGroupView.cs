using ElementDuel;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HandsGroupView : MonoBehaviour, IHoverReceiver
{
	[SerializeField] HandsView m_handsViewPrefab;
	UIPool<HandsView> m_handsPool;
	List<HandsView> m_handsViews;
	List<ActionCard> m_hands;

	RectTransform m_transform;
	[SerializeField] RectTransform m_hoverTransform;
	Vector3 m_pos;
	Vector3 m_scale;

	public event Action OnHandsGroupHoverEnter;
	public event Action OnHandsGroupHoverExit;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		m_pos = GetComponent<RectTransform>().localPosition;
		m_scale = GetComponent<RectTransform>().localScale;
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Initialize()
	{
		m_transform = GetComponent<RectTransform>();
		

		if (m_handsPool == null)
			m_handsPool = new UIPool<HandsView>(m_handsViewPrefab, transform);
		if (m_handsViews == null)
			m_handsViews = new List<HandsView>();
	}


	public void Initialize(List<ActionCard> cards)
	{
		Initialize();

		m_hands = cards;
		foreach (var card in cards)
		{
			var view = m_handsPool.Get();
			view.Initialize(card.data, false);
			view.handsGroupView = this;
			m_handsViews.Add(view);
		}
		SetupChildClick(true);
	}

	public void ReleaseAll()
	{
		m_handsPool?.ReleaseAll(m_handsViews);
		if (m_handsViews == null || m_handsViews.Count == 0) return;
		m_handsViews?.Clear();
		m_hands = null;
	}

	public void OnHoverEnter()
	{
		OnHandsGroupHoverEnter?.Invoke();
		m_transform.localPosition = m_hoverTransform.localPosition;
		m_transform.localScale = m_hoverTransform.localScale;
		GetComponent<HorizontalLayoutGroup>().spacing = 17.0f;
		SetSelectionAll(false);
	}

	public void OnHoverExit()
	{
		OnHandsGroupHoverExit?.Invoke();
		m_transform.localPosition = m_pos;
		m_transform.localScale = m_scale;
		GetComponent<HorizontalLayoutGroup>().spacing = -32.0f;
		SetSelectionAll(false);
	}

	void SetupChildClick(bool enableClick)
	{
		foreach (var view in m_handsViews)
		{
			view.SetupClick(enableClick);
		}
	}

	public void SetSelectionAll(bool isSelected)
	{
		if (m_handsViews == null || m_handsViews.Count == 0) return;
		foreach (var view in m_handsViews)
		{
			view.SetSelection(isSelected);
		}
	}

	public ActionCard GetHands(HandsView view)
	{
		int index = m_handsViews.IndexOf(view);
		return m_hands[index];
	}

	public void RegisterDoubleClick(Action<ActionCard> call)
	{
		foreach (var hands in m_handsViews)
		{
			hands.doubleClicked += call;
		}
	}

	public void RemoveDoubleClick(Action<ActionCard> call)
	{
		foreach (var hands in m_handsViews)
		{
			hands.doubleClicked -= call;
		}
	}

}
