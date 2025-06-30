using CardSystem.CharacterCard;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SummonCardView : MonoBehaviour
{
	GameObject m_main;
	Image m_icon;

	GameObject m_timerBlock;
	TMP_Text m_time;

	EffectView m_effectView;

	void Initialize()
	{
		m_main = UnityTools.FindChildGameObject(gameObject, "Main");
		m_icon = UITools.GetUIComponet<Image>(m_main, "icon");

		m_timerBlock = UnityTools.FindChildGameObject(gameObject, "TimerBlock");
		m_time = UITools.GetUIComponet<TMP_Text>(m_timerBlock, "time");

		m_effectView = UITools.GetUIComponet<EffectView>(gameObject,"EffectView");
	}

	public void Initialize(Summon summon)
	{
		Initialize();

		m_effectView.Initialize(summon.data.effectType, summon.data.elementType, summon.data.value);
		SetupSummonSprite(summon.data.sprite);
		SetupTimer(summon.count);
	}

	public void SetupSummonSprite(Sprite sprite)
	{
		m_icon.sprite = sprite;
	}

	public void SetupTimer(int time)
	{
		m_time.text = time.ToString();
	}


}
