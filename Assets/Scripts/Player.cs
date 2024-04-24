using UnityEngine;

public class Player : MonoBehaviour
{
	public float MoveSpeed;
	public static Vector3 PositionInstance { get; private set; }

	private void Start()
	{
		PositionInstance = transform.position;
	}

	void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		transform.position += new Vector3(horizontal, vertical, 0f) * MoveSpeed * Time.deltaTime;

		PositionInstance = transform.position;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Enemy"))
		{
			return;
		}

		GameController.Instance.StartLocalBattle();
	}
}