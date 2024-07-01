using DefaultNamespace;
using EventBusSystem;
using Mech.Data.Global;
using Mech.Data.Local;
using TMPro;
using UnityEngine;

namespace Mech.World
{
	public class Player : MonoBehaviour
	{
		public static Vector3 PositionInstance { get; private set; }
		public static Player Instance;

		[SerializeField] private TMP_Text _squadTitleText;
		[SerializeField] private string _squadTitleTextFormat;

		public float MoveSpeed;
		public GameObject Renderer;

		private bool _isPrevMove = false;

		private void Awake()
		{
			Instance = this;
		}

		private void Start()
		{
			PositionInstance = transform.position;
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowSupplies(PlayerData.Instance.InventoryLocalData.GetItemCount(ItemType.Supply)));
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowMoneys(PlayerData.Instance.Moneys));
			UpdateView();
		}

		public void UpdateView()
		{
			_squadTitleText.text = string.Format(_squadTitleTextFormat, PlayerData.Instance.GetAllModelCount());
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
				GameController.Instance.IsPause = !isMoving;
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
				GameController.Instance.StartDialog(other.gameObject);
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

		public void Eat()
		{
			var playerSupplies = PlayerData.Instance.InventoryLocalData.GetItemCount(ItemType.Supply);
			var squadNeedSupplies = PlayerData.Instance.GetAllModelCount();
			playerSupplies = Mathf.Max(0, playerSupplies - squadNeedSupplies);
			PlayerData.Instance.InventoryLocalData.SetItemCount(ItemType.Supply, playerSupplies);
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowSupplies(PlayerData.Instance.InventoryLocalData.GetItemCount(ItemType.Supply)));
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowEatedSupplies(squadNeedSupplies));
		}

		public void Pay()
		{
			var playerMoneys = PlayerData.Instance.Moneys;
			var salary = PlayerData.Instance.GetSalary();

			playerMoneys = Mathf.Max(0, playerMoneys - salary);
			PlayerData.Instance.Moneys = playerMoneys;
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowMoneys(PlayerData.Instance.Moneys));
			EventBus.RaiseEvent<IWorldUi>(x => x.ShowPayedMoneys(salary));
		}
	}
}