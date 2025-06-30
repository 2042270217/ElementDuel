using ElementDuel;
using UnityEngine;

public class ActionCard : ScriptableObject
{
	public ActionCardData data;

	protected PlayerSystem m_owner;
	protected ElementDuelGame m_game;
	public virtual void Use(PlayerSystem owner, ElementDuelGame game)
	{
		m_owner = owner;
		m_game = game;
		Debug.Log(this.GetType().Name);
	}
}
