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
		if (!GameController.Instance.IsWorldControl)
		{
			return;
		}
		
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		var movement = new Vector3(horizontal, vertical, 0f);

		GameController.Instance.IsSquadsStops = movement == Vector3.zero;
		
		transform.position += movement * MoveSpeed * Time.deltaTime;

		PositionInstance = transform.position;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.CompareTag("Enemy"))
		{
			return;
		}

		GameController.Instance.StartLocalBattle(other.gameObject);
	}
}