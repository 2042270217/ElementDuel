using UnityEngine;
using UnityEngine.UI;

public class EnergyView : MonoBehaviour
{
	Image m_flashImage;

	[SerializeField] bool m_isActive = false;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnValidate()
	{
		Initialize();
		UpdateActiveState();
	}

	void Initialize()
	{
		if (m_flashImage == null)
			m_flashImage = UITools.GetUIComponet<Image>(gameObject, "flash");
	}

	public void Initialize(bool active)
	{
		Initialize();
		SetActive(active);
	}

	public void SetActive(bool active)
	{
		if (m_isActive == active) return;
		m_isActive = active;
		UpdateActiveState();
	}

	void UpdateActiveState()
	{
		Color c = m_flashImage.color;
		c.a = m_isActive ? 1.0f : 0.0f;
		m_flashImage.color = c;
	}
}
