using UnityEngine;
using UnityEngine.UI;

public class ElementView : MonoBehaviour
{
	public ElementColorData colorData;
	public ElementType elementType = ElementType.Fire;

	Image m_icon;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		
	}

	private void OnValidate()
	{
		m_icon = UITools.GetUIComponet<Image>(gameObject, "icon");
		Setup();
	}

	// Update is called once per frame
	void Update()
	{

	}

	void Initialize()
	{
		m_icon = UITools.GetUIComponet<Image>(gameObject, "icon");
	}

	public void Initialize(ElementType ele)
	{
		Initialize();

		Setup(ele);
	}

	public void Setup(ElementType type)
	{
		elementType = type;
		Setup();
	}

	public void Setup()
	{
		switch (elementType)
		{
			case ElementType.Fire:
				m_icon.sprite = colorData.FireIcon;
				break;
			case ElementType.Water:
				m_icon.sprite = colorData.WaterIcon;
				break;
			case ElementType.Wind:
				m_icon.sprite = colorData.WindIcon;
				break;
			case ElementType.Thunder:
				m_icon.sprite = colorData.ThunderIcon;
				break;
			case ElementType.Grass:
				m_icon.sprite = colorData.GrassIcon;
				break;
			case ElementType.Ice:
				m_icon.sprite = colorData.IceIcon;
				break;
			case ElementType.Rock:
				m_icon.sprite = colorData.RockIcon;
				break;
			default:
				m_icon.sprite = null;
				break;

		}
	}
}