using ElementDuel;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class DiceView : MonoBehaviour, IClickReceiver
{
	[SerializeField] ElementColorData m_colorData;
	[SerializeField] ElementType m_elementType = ElementType.Fire;

	public bool canBeClicked = false;
	public bool isSelected = false;

	Image m_selectImage;
	Image m_image;

	public ElementType elementType { get { return m_elementType; } }

	void Awake()
	{
		Initialize();
	}

	private void OnValidate()
	{
		Initialize();
		Setup();
		UpdateDiceSelection();
	}

	void Update()
	{
	}

	void Initialize()
	{
		if (m_selectImage == null)
			m_selectImage = UITools.GetUIComponet<Image>(gameObject, "isSelected");
		if (m_image == null)
			m_image = UITools.GetUIComponet<Image>(gameObject, "Image");

	}

	public void OnClick()
	{
		if (canBeClicked)
		{
			isSelected = !isSelected;
			UpdateDiceSelection();
		}
	}

	void UpdateDiceSelection()
	{
		Color color = m_selectImage.color;
		color.a = isSelected ? 1f : 0f;
		m_selectImage.color = color;
	}

	public void Setup(ElementType e)
	{
		m_elementType = e;
		Setup();
	}

	void Setup()
	{
		//if (m_image == null) return;
		switch (m_elementType)
		{
			case ElementType.Fire:
				m_image.sprite = m_colorData.FireCostIcon;
				break;
			case ElementType.Water:
				m_image.sprite = m_colorData.WaterCostIcon;
				break;
			case ElementType.Wind:
				m_image.sprite = m_colorData.WindCostIcon;
				break;
			case ElementType.Thunder:
				m_image.sprite = m_colorData.ThunderCostIcon;
				break;
			case ElementType.Grass:
				m_image.sprite = m_colorData.GrassCostIcon;
				break;
			case ElementType.Ice:
				m_image.sprite = m_colorData.IceCostIcon;
				break;
			case ElementType.Rock:
				m_image.sprite = m_colorData.RockCostIcon;
				break;
			case ElementType.All:
				m_image.sprite = m_colorData.AllDiceIcon;
				break;
			default:
				break;

		}
	}

	public void Initialize(bool enableClick, ElementType ele)
	{
		//获取子物体以及组件
		Initialize();
		//设置是否能点击
		canBeClicked = enableClick;
		//设置icon
		Setup(ele);
	}
}
