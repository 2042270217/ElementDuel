using System;
using System.Collections.Generic;
using UnityEngine;

public class CharGroupView : MonoBehaviour
{
	[SerializeField] CharCardView m_charPrefab;

	List<Transform> m_roots;

	List<CharCardView> m_charViews;
	List<BaseCharacterCard> m_chars;

	void Initialize()
	{
		m_charViews = new List<CharCardView>();
		m_roots = new List<Transform>();
		for (int i = 0; i < 3; i++)
		{
			m_roots.Add(transform.GetChild(i));
		}
	}

	public void Initialize(List<BaseCharacterCard> chars)
	{
		Initialize();

		m_chars = chars;
		for (int i = 0; i < chars.Count; i++)
		{
			var charView = Instantiate(m_charPrefab, m_roots[i]);
			charView.Initialize(chars[i]);
			charView.charGroupView = this;
			m_charViews.Add(charView);
		}
	}

	public void SetSelectionAll(bool active)
	{
		foreach (var charView in m_charViews)
		{
			charView.SetSelection(active);
		}
	}

	public void SetClickAll(bool enableClick)
	{
		foreach (var charView in m_charViews)
		{
			charView.SetupClick(enableClick);
		}
	}

	public BaseCharacterCard GetCharCard(CharCardView view)
	{
		int index = m_charViews.IndexOf(view);
		return m_chars[index];
	}

	public void RegisterDoubleClick(Action<BaseCharacterCard> call)
	{
		foreach (var charView in m_charViews)
		{
			charView.doubleClicked += call;
		}
	}

	public void RemoveDoubleClick(Action<BaseCharacterCard> call)
	{
		foreach (var charView in m_charViews)
		{
			charView.doubleClicked -= call;
		}
	}

	public void SetFightingCharacter(BaseCharacterCard character, bool isDown)
	{
		for (int i = 0; i < m_chars.Count; i++)
		{
			if (character == m_chars[i])
			{
				SetFightingState(true, i, isDown);
			}
			else
			{
				SetFightingState(false, i, isDown);
			}
		}
	}

	void SetFightingState(bool isFighting, int index, bool isDown)
	{
		var t = m_roots[index].GetComponent<RectTransform>();
		Vector2 pos = t.anchoredPosition;
		pos.y = isFighting ? (isDown ? 30.0f : -30.0f) : 0.0f;
		t.anchoredPosition = pos;
	}

	public void UpdateCharacter(BaseCharacterCard character)
	{
		m_charViews[m_chars.IndexOf(character)].UpdateUI(character);
	}


}
