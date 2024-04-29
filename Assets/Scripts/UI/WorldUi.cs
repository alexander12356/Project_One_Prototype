using System;
using DG.Tweening;
using EventBusSystem;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldUi : MonoBehaviour, IWorldUi
{
	public GameObject _pauseUi;
	public Toggle _pauseToggle;
	public Toggle _resumeToggle;
	public TMP_Text _dateTimeText;
	public bool IsNight;
	public Image NightEffect;
	public string SuppliesTextFormat;
	public TMP_Text SuppliesText;
	public TMP_Text EatedSuppliesText;

	private void Awake()
	{
		EventBus.Subscribe(this);
	}

	private void OnDestroy()
	{
		EventBus.Unsubscribe(this);
	}

	public void OpenSquadManagementWindow()
	{
		GameController.Instance.OpenSquadManagementWindow();
	}
	
	public void SetPause(bool value)
	{
		_pauseUi.gameObject.SetActive(value);
		if (!value)
		{
			_resumeToggle.SetIsOnWithoutNotify(true);
		}
		else
		{
			_pauseToggle.SetIsOnWithoutNotify(true);
		}
	}

	public void OpenCityUi(GameObject cityObject)
	{
		var city = cityObject.GetComponent<City>();
		EventBus.RaiseEvent<ICityUI>(x => x.Show(city));
	}

	public void HandlePause(bool value)
	{
		EventBus.RaiseEvent<IGameController>(x => x.SetPause(value));
		_pauseUi.gameObject.SetActive(value);
	}

	public void SetDateTime(DateTime currentTimeSpan)
	{
		_dateTimeText.text = currentTimeSpan.ToString("yyyy-MM-dd HH:mm");
	}

	public void SetNight(bool value)
	{
		if (IsNight == value)
		{
			return;
		}
		
		IsNight = value;
		if (IsNight)
		{
			NightEffect.DOFade(0.5f, 1f);
		}
		else
		{
			NightEffect.DOFade(0f, 1f);
		}
	}

	public void ShowSupplies(int instanceSupplies)
	{
		SuppliesText.text = string.Format(SuppliesTextFormat, instanceSupplies);
	}

	public void ShowEatedSupplies(int squadNeedSupplies)
	{
		EatedSuppliesText.text = $"-{squadNeedSupplies}";
		EatedSuppliesText.alpha = 1f;
		EatedSuppliesText.transform.localPosition = Vector3.zero;
		EatedSuppliesText.transform.DOLocalMoveY(50, 0.5f);
		EatedSuppliesText.DOFade(0, 1.5f);
	}
}

public interface IWorldUi : IGlobalSubscriber
{
	void SetPause(bool value);
	void OpenCityUi(GameObject city);
	void SetDateTime(DateTime currentTimeSpan);
	void SetNight(bool p0);
	void ShowSupplies(int instanceSupplies);
	void ShowEatedSupplies(int squadNeedSupplies);
}