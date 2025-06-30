using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CostDiceView : MonoBehaviour
{
	[SerializeField] SkillCostCondition skillCdt;
	public ElementColorData elementData;

	public SkillCostCondition cdt => skillCdt;

	Image m_image;
	TMP_Text m_count;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
	}

	private void OnValidate()
	{
		Initialize();
		Setup();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Initialize()
	{
		if (m_image == null)
			m_image = UITools.GetUIComponet<Image>(gameObject, "Image");

		if (m_count == null)
			m_count = UITools.GetUIComponet<TMP_Text>(gameObject, "count");
	}

	public void Initialize(SkillCostCondition cdt)
	{
		Initialize();
		skillCdt = cdt;
		Setup();
	}

	void Setup()
	{
		switch (skillCdt.type)
		{
			case ElementCostType.Any:
				m_image.sprite = elementData.AnyCostIcon;
				break;
			case ElementCostType.Same:
				m_image.sprite = elementData.SameCostIcon;
				break;
			case ElementCostType.Specific:
				m_image.sprite = SwitchElementIcon(skillCdt.element);
				break;
			case ElementCostType.Energy:
				m_image.sprite = elementData.EnergyCostIcon;
				break;
		}
		m_count.text = skillCdt.count.ToString();
	}

	Sprite SwitchElementIcon(ElementType type)
	{
		switch (type)
		{
			case ElementType.Fire:
				return elementData.FireIcon;
			case ElementType.Water:
				return elementData.WaterIcon;
			case ElementType.Wind:
				return elementData.WindIcon;
			case ElementType.Thunder:
				return elementData.ThunderIcon;
			case ElementType.Grass:
				return elementData.GrassIcon;
			case ElementType.Ice:
				return elementData.IceIcon;
			case ElementType.Rock:
				return elementData.RockIcon;
			default:
				return null;
		}

	}
}