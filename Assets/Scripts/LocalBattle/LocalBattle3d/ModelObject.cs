using System;
using System.Collections;
using System.Linq;
using Mech.Data.Global;
using Pathfinding;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Serialization;

namespace LocalBattle3d
{
	public class ModelObject : MonoBehaviour
	{
		public ModelGlobalDataList _modelGlobalDataList;
		[FormerlySerializedAs("_rangeWeaponGlobalDataList")] public WeaponGlobalDataList _weaponGlobalDataList;
		public SpriteRenderer Icon;
		public Transform AnimationHolder;

		public float AnimationLifeTime;
		public float SpawnTimeRandom;
		public int GettedExp;
		public ModelType ModelType;
		public float CommonSpeed;
		public float ChargeSpeed;

		private int W;
		private IAstarAI _astarAI;
		private Vector3 _startPositions;

		private void Awake()
		{
			_astarAI = GetComponent<IAstarAI>();
		}

		private void Start()
		{
			_startPositions = transform.position;
		}

		public void SetPosition(float columnOffset, float rowOffset)
		{
			transform.localPosition = new Vector3(columnOffset, 0f, rowOffset);
		}

		public void SetType(ModelType modelType)
		{
			ModelType = modelType;
			var balance = _modelGlobalDataList.GetModelData(modelType);
			Icon.sprite = balance.Icon;
			W = balance.W;
		}

		public void RangeAttack(ModelObject defenderModel)
		{
			var attackerRangeWeaponData = _modelGlobalDataList.GetModelRangeWeaponData(ModelType);
			var weaponData = _weaponGlobalDataList.GetRangeWeaponData(attackerRangeWeaponData.Type);
			WeaponAttack(defenderModel, attackerRangeWeaponData.A, attackerRangeWeaponData.BS, weaponData, VfxType.RangeAttack);
		}

		public void MeleeAttack(ModelObject defenderModel)
		{
			var attackerRangeWeaponData = _modelGlobalDataList.GetModelMeleeWeaponData(ModelType);
			var weaponData = _weaponGlobalDataList.GetMeleeWeaponData(attackerRangeWeaponData.Type);
			WeaponAttack(defenderModel, attackerRangeWeaponData.A, attackerRangeWeaponData.WS, weaponData, VfxType.MeleeAttack);
		}

		private void WeaponAttack(ModelObject defenderModel, int attackCount, int skill, WeaponGlobalData attackerWeaponGlobalData, VfxType attackVfx)
		{
			var defenderBalance = _modelGlobalDataList.GetModelData(defenderModel.ModelType);

			for (var i = 0; i < attackCount; i++)
			{
				ShowEffect(attackVfx);

				var isHit = BattleRollHelper.Roll(skill);
				if (!isHit)
				{
					ShowEffect(VfxType.Miss);
					return;
				}

				var isWound = BattleRollHelper.WoundRoll(attackerWeaponGlobalData.S, defenderBalance.T);
				if (!isWound)
				{
					ShowEffect(VfxType.ToughnessDefense);
					return;
				}

				var isSave = BattleRollHelper.Roll(defenderBalance.Sv);
				if (isSave)
				{
					ShowEffect(VfxType.SaveDefense);
					return;
				}

				ShowEffect(VfxType.Wound);
				if (!defenderModel.IsDead())
				{
					defenderModel.Wound(attackerWeaponGlobalData.D);
				}

				if (defenderModel.IsDead())
				{
					GettedExp += _modelGlobalDataList.GetExpFrom(defenderModel.ModelType);
				}
			}
		}

		private void ShowEffect(VfxType type)
		{
			LocalBattleEffectController.Instance.ShowEffect(type, AnimationHolder.position);
		}

		private void Wound(int attackerBalanceD)
		{
			W -= attackerBalanceD;
		}

		public bool IsDead()
		{
			return W <= 0;
		}

		public void Move(float moveForwardDistance)
		{
			_astarAI.isStopped = false;
			_astarAI.maxSpeed = CommonSpeed;
			_astarAI.destination = transform.position + transform.forward * moveForwardDistance;
		}

		public void Charge()
		{
			_astarAI.maxSpeed = ChargeSpeed;
		}

		public void ReturnToStartPositions()
		{
			_astarAI.isStopped = true;
			_astarAI.Teleport(_startPositions);
		}
	}
}