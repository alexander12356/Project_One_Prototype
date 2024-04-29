using System;
using EventBusSystem;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float MoveSpeed;
	public static Vector3 PositionInstance { get; private set; }

	public static Player Instance;
	public GameObject Renderer;

	private bool _isPrevMove = false;

	private void Awake()
	{
		Instance = this;
	}

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

		var isMoving = movement != Vector3.zero;
		if (isMoving != _isPrevMove)
		{
			GameController.Instance.IsSquadsStops = !isMoving;
			EventBus.RaiseEvent<IWorldUi>(x => x.SetPause(!isMoving));
		}

		transform.position += movement * MoveSpeed * Time.deltaTime;
		PositionInstance = transform.position;

		_isPrevMove = isMoving;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Enemy"))
		{
			GameController.Instance.StartLocalBattle(other.gameObject);
			return;
		}


		if (other.CompareTag("City"))
		{
			GameController.Instance.StartCity(other.gameObject);
			return;
		}
	}

	public void SetPosition(Vector3 newPosition)
	{
		transform.position = newPosition;
	}

	public void SetVisible(bool value)
	{
		Renderer.SetActive(value);
	}
}