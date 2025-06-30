using TMPro;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class EffectView : MonoBehaviour
{
	public EffectType effectType = EffectType.Damage;
	public ElementType element = ElementType.Fire;
	public ElementColorData elementData;
	public EffectAssetData assetData;

	Image m_icon;
	TMP_Text m_count;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	private void OnValidate()
	{
		m_icon = UITools.GetUIComponet<Image>(gameObject, "icon");
		m_count = UITools.GetUIComponet<TMP_Text>(gameObject, "count");
		SetupSprite();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Initialize()
	{
		m_icon = UITools.GetUIComponet<Image>(gameObject, "icon");
		m_count = UITools.GetUIComponet<TMP_Text>(gameObject, "count");
	}

	public void SetupSprite(EffectType eff, ElementType ele)
	{
		effectType = eff;
		element = ele;
		SetupSprite();
	}

	public void SetupSprite()
	{
		switch (effectType)
		{
			case EffectType.Damage:
				SetDamageSprite();
				break;
			case EffectType.Heal:
				SetHealSprite();
				break;
		}
	}

	void SetHealSprite()
	{
		m_icon.sprite = assetData.HealEffectSprite;
	}

	void SetDamageSprite()
	{
		switch (element)
		{
			case ElementType.Fire:
				m_icon.sprite = elementData.FireIcon;
				break;
			case ElementType.Water:
				m_icon.sprite = elementData.WaterIcon;
				break;
			case ElementType.Wind:
				m_icon.sprite = elementData.WindIcon;
				break;
			case ElementType.Thunder:
				m_icon.sprite = elementData.ThunderIcon;
				break;
			case ElementType.Grass:
				m_icon.sprite = elementData.GrassIcon;
				break;
			case ElementType.Ice:
				m_icon.sprite = elementData.IceIcon;
				break;
			case ElementType.Rock:
				m_icon.sprite = elementData.RockIcon;
				break;
			default:
				m_icon = null;
				break;

		}
	}

	public void SetupCount(int count)
	{
		m_count.text = count.ToString();
	}

	public void Initialize(EffectType eff, ElementType ele, int count)
	{
		Initialize();

		SetupSprite(eff, ele);
		SetupCount(count);
	}
}