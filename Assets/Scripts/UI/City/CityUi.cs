using EventBusSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class CityUi : MonoBehaviour, ICityUI
{
	public CanvasGroup CanvasGroup;
	public StoreUi StoreUi;
	[FormerlySerializedAs("HireSquadsUi")] public GuildUi _guildUi;

	private City _city;

	private void Awake()
	{
		EventBus.Subscribe(this);
	}

	private void OnDestroy()
	{
		EventBus.Unsubscribe(this);
	}

	public void Show(City city)
	{
		_city = city;
		
		SetVisible(true);
	}

	public void OpenStore()
	{
		StoreUi.Open(_city);
	}

	public void OpenGuild()
	{
		_guildUi.Open(_city);
	}

	public void Close()
	{
		SetVisible(false);
		_guildUi.Close();
		_city.Leave();
	}

	private void SetVisible(bool value)
	{
		CanvasGroup.alpha = value ? 1f : 0f;
		CanvasGroup.blocksRaycasts = value;
	}
}

public interface ICityUI : IGlobalSubscriber
{
	void Show(City city);
}