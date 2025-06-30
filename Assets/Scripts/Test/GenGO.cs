using UnityEngine;

public class GenGO : MonoBehaviour
{
	public GameObject go;
	// Start is called once before the first execution of Update after the MonoBehaviour is created
	void Start()
	{
		Debug.Log(Time.frameCount + "GenGOStart");
		Instantiate(go);
		Debug.Log(Time.frameCount + "GenGOStartFinish");
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			Debug.Log(Time.frameCount);
			Instantiate(go);
			Debug.Log(Time.frameCount + "finished");
		}
	}
}
