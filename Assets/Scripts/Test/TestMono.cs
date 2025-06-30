using UnityEngine;

public class TestMono : MonoBehaviour
{
	bool isFirst = true;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		Debug.Log(name + ": " + Time.frameCount + "Start");
	}

	private void Awake()
	{
		Debug.Log(name + ": " + Time.frameCount + "Awake");
	}

	private void OnEnable()
	{
		Debug.Log(name + ": " + Time.frameCount + "OnEnable");
	}

	// Update is called once per frame
	void Update()
	{
		if (isFirst)
		{
			Debug.Log(name + ": " + Time.frameCount + "Update");
			isFirst = false;
		}
	}
}
