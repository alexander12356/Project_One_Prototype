using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Mech.UI
{
	public class LineConnectorTarget : MonoBehaviour
	{
		[SerializeField, ReadOnly] private Transform _targetTransform;

		public void SetTarget(Transform targetTransform)
		{
			_targetTransform = targetTransform;
		}

		private void Update()
		{
			if (_targetTransform != null)
			{
				transform.position = _targetTransform.position;
			}
		}
	}
}