using ElementDuel;
using UnityEngine;

public class AssistUI : IUserInterface
{
	bool m_isDown;

	AssistGroupView m_assistGroupView;

	public AssistUI(ElementDuelGame edGame, bool isDown) : base(edGame)
	{
		m_isDown = isDown;
		Initialize();
	}

	public override void Initialize()
	{
		string parentName;
		if (m_isDown)
			parentName = "Player1Group";
		else
			parentName = "Player2Group";
		Debug.Log(parentName);
		m_Root = UITools.FindUIGameObject(parentName);
		m_Root = UnityTools.FindChildGameObject(m_Root, "AssistUI");

		m_assistGroupView = UITools.GetUIComponet<AssistGroupView>(m_Root, "AssistGroup");

	}

	public override void Release()
	{

	}

	public override void Update()
	{
		m_assistGroupView.ReleaseAll();
		m_assistGroupView.Initialize(m_EDGame.GetPlayer(m_isDown).assistList);
	}
}
