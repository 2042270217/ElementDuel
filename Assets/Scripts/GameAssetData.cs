using UnityEngine;

[CreateAssetMenu(menuName = "ElementDuel/GameAsset")]
public class GameAssetData : ScriptableObject
{
	public GameObject DicePrefab;
	public ElementColorData ElementColor;
	public DeckConfig Config;
	public GameObject HandsViewPrefab;
}
