using System;
using System.Collections.Generic;
using Mech.Data.Global;
using UnityEngine;
using UnityEngine.Pool;

namespace LocalBattle3d
{
	public class LocalBattleEffectController : MonoBehaviour
	{
		public static LocalBattleEffectController Instance => _instance;
		private static LocalBattleEffectController _instance;

		[SerializeField] private Transform _effectHolder;
		[SerializeField] private VfxGlobalDataList _vfxGlobalDataList;

		private Dictionary<VfxType, ObjectPool<GameObject>> _vfxObjectPoolList;

		private void Awake()
		{
			_instance = this;
		}

		private void Start()
		{
			_vfxObjectPoolList = new Dictionary<VfxType, ObjectPool<GameObject>>();
		}

		public void ShowEffect(VfxType type, Vector3 spawnPosition)
		{
			if (!_vfxObjectPoolList.ContainsKey(type))
			{
				var prefab = _vfxGlobalDataList.GetVfxPrefab(type);
				_vfxObjectPoolList.Add(type, new ObjectPool<GameObject>
				(
					createFunc: () =>
					{
						var effect = Instantiate(prefab, _effectHolder, false);
						effect.GetComponent<ReturnToPool>().SetAnimationPool(_vfxObjectPoolList[type]);
						return effect;
					},
					actionOnGet: x => x.SetActive(true),
					actionOnRelease: x => x.SetActive(false),
					actionOnDestroy: Destroy));
			}
			var effect = _vfxObjectPoolList[type].Get();
			effect.transform.position = spawnPosition;
		}
	}
}