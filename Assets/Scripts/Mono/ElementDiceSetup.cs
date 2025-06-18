using UnityEngine;
using UnityEngine.UI;

public class ElementDiceSetup : MonoBehaviour
{
	public ElementColorData colorData;
	public ElementType elementType = ElementType.Fire;
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
				image.sprite = colorData.FireCostIcon;
				break;
			case ElementType.Water:
				image.sprite = colorData.WaterCostIcon;
				break;
			case ElementType.Wind:
				image.sprite = colorData.WindCostIcon;
				break;
			case ElementType.Thunder:
				image.sprite = colorData.ThunderCostIcon;
				break;
			case ElementType.Grass:
				image.sprite = colorData.GrassCostIcon;
				break;
			case ElementType.Ice:
				image.sprite = colorData.IceCostIcon;
				break;
			case ElementType.Rock:
				image.sprite = colorData.RockCostIcon;
				break;
			case ElementType.All:
				image.sprite = colorData.AllDiceIcon;
				break;                         
			default:
				break;

		}
	}
}
