using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ElementColorSetup : MonoBehaviour
{
	public ElementColorData colorData;
	public ElementType elementType;

	Image image;
	// Start is called before the first frame update
	void Start()
	{
		image = GetComponent<Image>();
	}

	// Update is called once per frame
	void Update()
	{
		switch (elementType)
		{
			case ElementType.Fire:
				image.color = colorData.FireColor;
				image.sprite = colorData.FireIcon;
				break;
			case ElementType.Water:
				image.color = colorData.WaterColor;
				image.sprite = colorData.WaterIcon;
				break;
			case ElementType.Wind:
				image.color = colorData.WindColor;
				image.sprite = colorData.WindIcon;
				break;
			case ElementType.Thunder:
				image.color = colorData.ThunderColor;
				image.sprite = colorData.ThunderIcon;
				break;
			case ElementType.Grass:
				image.color = colorData.GrassColor;
				image.sprite = colorData.GrassIcon;
				break;
			case ElementType.Ice:
				image.color = colorData.IceColor;
				image.sprite = colorData.IceIcon;
				break;
			case ElementType.Rock:
				image.color = colorData.RockColor;
				image.sprite = colorData.RockIcon;
				break;
			default:
				break;

		}
	}
}
