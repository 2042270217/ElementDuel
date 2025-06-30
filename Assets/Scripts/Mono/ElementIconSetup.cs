using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ElementIconSetup : MonoBehaviour
{
	public ElementColorData colorData;
	public ElementType elementType;

	Image image;
	// Start is called before the first frame update
	void Start()
	{
		image = GetComponent<Image>();
	}

	private void OnValidate()
	{
		image = GetComponent<Image>();
		switch (elementType)
		{
			case ElementType.Fire:
				image.sprite = colorData.FireIcon;
				break;
			case ElementType.Water:
				image.sprite = colorData.WaterIcon;
				break;
			case ElementType.Wind:
				image.sprite = colorData.WindIcon;
				break;
			case ElementType.Thunder:
				image.sprite = colorData.ThunderIcon;
				break;
			case ElementType.Grass:
				image.sprite = colorData.GrassIcon;
				break;
			case ElementType.Ice:
				image.sprite = colorData.IceIcon;
				break;
			case ElementType.Rock:
				image.sprite = colorData.RockIcon;
				break;
			default:
				image.sprite = null;
				break;

		}
	}

	// Update is called once per frame
	void Update()
	{
		switch (elementType)
		{
			case ElementType.Fire:
				image.sprite = colorData.FireIcon;
				break;
			case ElementType.Water:
				image.sprite = colorData.WaterIcon;
				break;
			case ElementType.Wind:
				image.sprite = colorData.WindIcon;
				break;
			case ElementType.Thunder:
				image.sprite = colorData.ThunderIcon;
				break;
			case ElementType.Grass:
				image.sprite = colorData.GrassIcon;
				break;
			case ElementType.Ice:
				image.sprite = colorData.IceIcon;
				break;
			case ElementType.Rock:
				image.sprite = colorData.RockIcon;
				break;
			default:
				Color c = image.color;
				c.a = 0.0f;
				image.color = c;
				break;

		}
	}
}
