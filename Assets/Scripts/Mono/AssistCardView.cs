using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AssistCardView : MonoBehaviour
{
	GameObject m_main;
	Image m_icon;

	GameObject m_counterBlock;
	TMP_Text m_count;

	void Initialize()
	{
		m_main = UnityTools.FindChildGameObject(gameObject, "Main");
		m_icon = UITools.GetUIComponet<Image>(m_main, "icon");

		if (m_counterBlock == null)
			m_counterBlock = UnityTools.FindChildGameObject(gameObject, "CounterBlock");
		m_count = UITools.GetUIComponet<TMP_Text>(m_counterBlock, "count");
	}

	public void Initialize(Assist assist)
	{
		Initialize();

		SetupAssistSpirte(assist.data.sprite);
		if (assist.data.useCounter)
		{
			m_counterBlock.SetActive(true);
			SetupCount(assist.count);
		}
		else
		{
			m_counterBlock.SetActive(false);
		}
	}

	public void SetupAssistSpirte(Sprite sprite)
	{
		m_icon.sprite = sprite;
	}


	/// <summary>
	/// ÉèÖÃµþ²ã²ãÊý
	/// </summary>
	/// <param name="count"></param>
	public void SetupCount(int count)
	{
		m_count.text = count.ToString();
	}

}
