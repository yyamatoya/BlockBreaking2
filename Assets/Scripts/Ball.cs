using Fusion;
using UnityEngine;

public class Ball : NetworkBehaviour
{
	Rigidbody rb;
	public float speed = 3.0f;  //	‘¬“x
	public float accelSpeed = 0.25f; // ‰Á‘¬“x
	GameObject gameSetUI;
	bool isFinish = false;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody>();
		gameSetUI = Resources.Load<GameObject>("GameSetText");
		gameSetUI.SetActive(false);
	}

	public void AddForce()
	{
		rb.AddForce(new Vector3(1, -1, 0) * speed, ForceMode.VelocityChange);
	}

	public void End()
	{
		rb.isKinematic = true;
	}

	private void FixedUpdate()
	{
		if (!isFinish && GameObject.FindGameObjectsWithTag("Block").Length == 0)
		{
			NetworkRunner runner = FindObjectOfType<NetworkRunner>();
			End();
			runner.Disconnect(runner.LocalPlayer);
			isFinish = true;
		}

	}

	private void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Block"))
		{
			Destroy(collision.gameObject);

		}
		if (collision.gameObject.name == "Bar")
		{
			speed += accelSpeed;
			Vector3 vec = transform.position - collision.transform.position;
			rb.velocity = Vector3.zero;
			rb.AddForce(vec.normalized * speed, ForceMode.VelocityChange);
		}
	}
}
