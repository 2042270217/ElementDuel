using ElementDuel.GamePhase;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ElementDuel;

public class GameLoop : MonoBehaviour
{
	ElementDuelGame m_EDGame = new ElementDuelGame();
	private void Awake()
	{
		GameObject.DontDestroyOnLoad(this.gameObject);
		EDebug.LogLevelFilter(LogLevel.GameSystem, true);
	}
	// Start is called before the first frame update
	void Start()
	{
		m_EDGame.Initialize();
	}

	// Update is called once per frame
	void Update()
	{
		m_EDGame.Update();
		EDebug.Log("GameSystemDebug", LogLevel.GameSystem);
		EDebug.Log("GlobalDebug");
	}
}
