using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostDiceView : MonoBehaviour
{
	SkillCostCondition m_cdt;

	Image m_image;
	TMP_Text m_count;
	ElementColorData m_color = GameSetup.Instance.AssetData.ElementColor;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		m_image = UITools.GetUIComponet<Image>(gameObject, "Image");
		m_count = UITools.GetUIComponet<TMP_Text>(gameObject, "count");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void Setup(SkillCostCondition cdt)
	{
		m_cdt = cdt;
		switch (cdt.type)
		{
			case ElementCostType.Any:
				m_image.sprite = m_color.AnyCostIcon;
				break;
			case ElementCostType.Same:
				m_image.sprite = m_color.SameCostIcon;
				break;
			case ElementCostType.Specific:
				m_image.sprite = SwitchElementIcon(cdt.element);
				break;
			case ElementCostType.Energy:
				break;
		}
		m_count.text = cdt.count.ToString();
	}

	Sprite SwitchElementIcon(ElementType type)
	{
		switch (type)
		{
			case ElementType.Fire:
				return m_color.FireIcon;
			case ElementType.Water:
				return m_color.WaterIcon;
			case ElementType.Wind:
				return m_color.WindIcon;
			case ElementType.Thunder:
				return m_color.ThunderIcon;
			case ElementType.Grass:
				return m_color.GrassIcon;
			case ElementType.Ice:
				return m_color.IceIcon;
			case ElementType.Rock:
				return m_color.RockIcon;
			default:
				return null;
		}

	}
}