using UnityEngine;

public class GameSetup : MonoBehaviour
{
	public GameAssetData AssetData;
	static GameSetup m_instance;
	public static GameSetup Instance
	{
		get
		{
			if (m_instance == null)
			{
				m_instance = FindFirstObjectByType<GameSetup>();
			}
			return m_instance;
		}
	}
}
