using ElementDuel;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class DiceView : MonoBehaviour, IClickReceiver
{
	public bool CanBeClicked = false;
	public bool isSelected = false;

	Image m_selectImage;

	void Start()
	{
		m_selectImage = UITools.GetUIComponet<Image>(gameObject, "isSelected");
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
}
