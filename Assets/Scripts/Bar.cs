using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
	float posY;



	// Start is called before the first frame update
	void Start()
	{
		posY = transform.position.y;
		GameObject wallLeft = GameObject.Find("Wall_Left");
		GameObject wallRight = GameObject.Find("Wall_Right");

	}

	// Update is called once per frame
	void Update()
	{
		Vector3 pos = Input.mousePosition;
		Vector3 targetPos = Camera.main.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 10));
		targetPos.x = Mathf.Clamp(targetPos.x, -1.4f, 1.4f);
		targetPos.y = posY;
		transform.position = targetPos;
	}
}
