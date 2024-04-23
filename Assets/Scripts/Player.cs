using UnityEngine;

public class Player : MonoBehaviour
{
	public float MoveSpeed;

	void Update()
	{
		var horizontal = Input.GetAxis("Horizontal");
		var vertical = Input.GetAxis("Vertical");
		transform.position += new Vector3(horizontal, vertical, 0f) * MoveSpeed * Time.deltaTime;
	}
}