using System;
using System.Linq;
using DefaultNamespace;
using Mech.Data.Global;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GuildItemUi : MonoBehaviour
{
	[SerializeField] private Image _iconImage;
	[SerializeField] private TMP_Text _nameText;
	[SerializeField] private ModelGlobalDataList _modelGlobalDataList;

	private Action<GuildItemUi> _tryHireAction;
	private ModelType _modelType;

	public void SetData(ModelType modelType, Action<GuildItemUi> onTryHire)
	{
		_modelType = modelType;
		_tryHireAction = onTryHire;
		_iconImage.sprite = _modelGlobalDataList.GetModelData(_modelType).Icon;
		_nameText.text = _modelGlobalDataList.GetModelData(_modelType).Title;
	}

	public void HireButtonPress()
	{
		_tryHireAction?.Invoke(this);
	}

	public void Dispose()
	{
		Destroy(gameObject);
	}

	public ModelType GetModelType()
	{
		return _modelType;
	}
}