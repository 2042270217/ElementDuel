using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillView : MonoBehaviour
{
	GameObject m_main;
	GameObject m_costBlock;

	Image m_icon;
	[SerializeField] GameObject m_costDicePrefab;

	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		m_main = UnityTools.FindChildGameObject(gameObject, "Main");
		m_costBlock = UnityTools.FindChildGameObject(gameObject, "CostBlock");

		m_icon = UITools.GetUIComponet<Image>(m_main, "icon");
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void GenerateCostDice(List<SkillCostCondition> cdts)
	{
		foreach (SkillCostCondition cdt in cdts)
		{
			var go = Instantiate(m_costDicePrefab, transform);
			go.GetComponent<CostDiceView>().Setup(cdt);
		}
	}


}
