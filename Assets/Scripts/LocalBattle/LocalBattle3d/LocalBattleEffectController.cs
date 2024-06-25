using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

namespace LocalBattle3d
{
	public class LocalBattleEffectController : MonoBehaviour
	{
		public static LocalBattleEffectController Instance => _instance;
		private static LocalBattleEffectController _instance;

		public float AnimationLifeTime;
		public float SpawnTimeRandom;

		[SerializeField] private GameObject _attackAnimationPrefab;
		[SerializeField] private GameObject _toughnessDefendAnimationPrefab;
		[SerializeField] private GameObject _saveDefendAnimationPrefab;
		[SerializeField] private GameObject _woundAnimationPrefab;
		[SerializeField] private GameObject _missAnimationPrefab;
		[SerializeField] private Transform _effectHolder;

		public ObjectPool<GameObject> AttackAnimationPool;
		public ObjectPool<GameObject> ToughnessDefendAnimationPool;
		public ObjectPool<GameObject> SaveDefendAnimationPool;
		public ObjectPool<GameObject> WoundAnimationPool;
		public ObjectPool<GameObject> MissAnimationPool;

		private void Awake()
		{
			_instance = this;
		}

		private void Start()
		{
			AttackAnimationPool = new ObjectPool<GameObject>(
				createFunc: () =>
				{
					var effect = Instantiate(_attackAnimationPrefab, _effectHolder, false);
					effect.GetComponent<ReturnToPool>().SetAnimationPool(AttackAnimationPool);
					return effect;
				},
				actionOnGet: x => x.SetActive(true),
				actionOnRelease: x => x.SetActive(false),
				actionOnDestroy: Destroy);

			ToughnessDefendAnimationPool = new ObjectPool<GameObject>(
				createFunc: () =>
				{
					var effect = Instantiate(_toughnessDefendAnimationPrefab, _effectHolder, false);
					effect.GetComponent<ReturnToPool>().SetAnimationPool(ToughnessDefendAnimationPool);
					return effect;
				},
				actionOnGet: x => x.SetActive(true),
				actionOnRelease: x => x.SetActive(false),
				actionOnDestroy: Destroy);

			SaveDefendAnimationPool = new ObjectPool<GameObject>(
				createFunc: () =>
				{
					var effect = Instantiate(_saveDefendAnimationPrefab, _effectHolder, false);
					effect.GetComponent<ReturnToPool>().SetAnimationPool(SaveDefendAnimationPool);
					return effect;
				},
				actionOnGet: x => x.SetActive(true),
				actionOnRelease: x =>
				{
					try
					{
						x.SetActive(false);
					}
					catch (Exception e)
					{
						Debug.LogException(e);
						throw;
					}
				},
				actionOnDestroy: x =>
				{
					Destroy(x);
				});

			WoundAnimationPool = new ObjectPool<GameObject>(
				createFunc: () =>
				{
					var effect = Instantiate(_woundAnimationPrefab, _effectHolder, false);
					effect.GetComponent<ReturnToPool>().SetAnimationPool(WoundAnimationPool);
					return effect;
				},
				actionOnGet: x => x.SetActive(true),
				actionOnRelease: x => x.SetActive(false),
				actionOnDestroy: Destroy);

			MissAnimationPool = new ObjectPool<GameObject>(
				createFunc: () =>
				{
					var effect = Instantiate(_missAnimationPrefab, _effectHolder, false);
					effect.GetComponent<ReturnToPool>().SetAnimationPool(MissAnimationPool);
					return effect;
				},
				actionOnGet: x => x.SetActive(true),
				actionOnRelease: x => x.SetActive(false),
				actionOnDestroy: Destroy);
		}

		public void ShowEffect(ObjectPool<GameObject> animationPool, Vector3 spawnPosition)
		{
			var effect = animationPool.Get();
			effect.transform.position = spawnPosition;
		}
	}
}