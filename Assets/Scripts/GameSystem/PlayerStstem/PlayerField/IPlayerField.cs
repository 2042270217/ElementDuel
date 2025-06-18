public abstract class IPlayerField
{
	protected PlayerSystem m_Player;

	public IPlayerField(PlayerSystem player)
	{
		m_Player = player;
	}

	public abstract void Initialize();

	public abstract void Update();

	public abstract void Release();
}
