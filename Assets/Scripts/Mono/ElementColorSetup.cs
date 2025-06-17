using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ElementColorSetup : MonoBehaviour
{
	public ElementColorData colorData;
	public ElementType elementType;

	SpriteRenderer spriteRenderer;
	// Start is called before the first frame update
	void Start()
	{
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		switch (elementType)
		{
			case ElementType.Fire:
				spriteRenderer.color = colorData.FireColor;
				spriteRenderer.sprite = colorData.FireIcon;
				break;
			case ElementType.Water:
				spriteRenderer.color = colorData.WaterColor;
				spriteRenderer.sprite = colorData.WaterIcon;
				break;
			case ElementType.Wind:
				spriteRenderer.color = colorData.WindColor;
				spriteRenderer.sprite = colorData.WindIcon;
				break;
			case ElementType.Thunder:
				spriteRenderer.color = colorData.ThunderColor;
				spriteRenderer.sprite = colorData.ThunderIcon;
				break;
			case ElementType.Grass:
				spriteRenderer.color = colorData.GrassColor;
				spriteRenderer.sprite = colorData.GrassIcon;
				break;
			case ElementType.Ice:
				spriteRenderer.color = colorData.IceColor;
				spriteRenderer.sprite = colorData.IceIcon;
				break;
			case ElementType.Rock:
				spriteRenderer.color = colorData.RockColor;
				spriteRenderer.sprite = colorData.RockIcon;
				break;
			default:
				break;

		}
	}
}
