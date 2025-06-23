using ElementDuel;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class DiceView : MonoBehaviour, IClickReceiver
{
	public bool CanBeClicked = false;
	public bool isSelected = false;

	Image m_selectImage;
	ElementDiceSetup m_setup;

	void Start()
	{
		m_selectImage = UITools.GetUIComponet<Image>(gameObject, "isSelected");
		m_setup = UITools.GetUIComponet<ElementDiceSetup>(gameObject, "Image");
	}

	void Update()
	{

	}

	public void OnClick()
	{
		if (CanBeClicked)
		{
			isSelected = !isSelected;
			Color color = m_selectImage.color;
			color.a = isSelected ? 1f : 0f;
			m_selectImage.color = color;
		}
	}

	public void SetElement(ElementType e)
	{
		m_setup.elementType = e;
	}
}
