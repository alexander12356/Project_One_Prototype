using System;
using EventBusSystem;
using TMPro;
using UnityEngine;

public class CityUi : MonoBehaviour, ICityUI
{
	public CanvasGroup CanvasGroup;
	public TMP_Text Caption;
	public StoreUi StoreUi;

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
		
		CanvasGroup.blocksRaycasts = true;
		CanvasGroup.alpha = 1f;
		Caption.text = city.CityName;
	}

	public void OpenStore()
	{
		StoreUi.Open(_city);
	}

	public void Close()
	{
		CanvasGroup.blocksRaycasts = false;
		CanvasGroup.alpha = 0f;
		EventBus.RaiseEvent<IGameController>(x => x.CloseCity(_city));
	}
}

public interface ICityUI : IGlobalSubscriber
{
	void Show(City city);
}