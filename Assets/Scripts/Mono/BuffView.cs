using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffView : MonoBehaviour
{
	TMP_Text m_count;
	Image m_icon;

	/// <summary>
	/// ÉèÖÃBuffµþ²ãµÄ²ãÊý
	/// </summary>
	/// <param name="count"></param>
	public void SetupBuffCount(int count)
	{
		m_count.text = count.ToString();
	}

	public void SetupBuffSprite(Sprite sprite)
	{
		m_icon.sprite = sprite;
	}

	void Initialize()
	{
		if (m_count == null)
			m_count = UITools.GetUIComponet<TMP_Text>(gameObject, "count");
		if (m_icon == null)
			m_icon = UITools.GetUIComponet<Image>(gameObject, "icon");
	}

	public void Initialize(BaseBuff buff)
	{
		Initialize();
		if (!buff.data.useCounter)
		{
			m_count.gameObject.SetActive(false);
		}
		SetupBuffCount(buff.count);
		SetupBuffSprite(buff.data.sprite);
	}
}
