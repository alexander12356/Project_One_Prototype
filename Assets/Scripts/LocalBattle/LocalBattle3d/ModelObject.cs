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
		[FormerlySerializedAs("MechBalance")] public ModelGlobalDataList _modelGlobalDataList;
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
			var balance = _modelGlobalDataList.Balances.FirstOrDefault(x => x.ModelType == modelType);
			Icon.sprite = balance.Icon;
			W = balance.W;
		}

		public void RangeAttack(ModelObject defenderModel)
		{
			var attackerBalance = _modelGlobalDataList.GetMechBalance(ModelType);
			var defenderBalance = _modelGlobalDataList.GetMechBalance(defenderModel.ModelType);

			for (var i = 0; i < attackerBalance.A; i++)
			{
				ShowAttackAnimation();

				var isHit = BattleRollHelper.Roll(attackerBalance.BS);
				if (!isHit)
				{
					defenderModel.ShowMissAnimation();
					return;
				}

				var isWound = BattleRollHelper.WoundRoll(attackerBalance.S, defenderBalance.T);
				if (!isWound)
				{
					defenderModel.ShowToughnessDefenseAnimation();
					return;
				}

				var isSave = BattleRollHelper.Roll(defenderBalance.Sv);
				if (isSave)
				{
					defenderModel.ShowSaveDefenseAnimation();
					return;
				}

				defenderModel.ShowWoundAnimation();
				if (!defenderModel.IsDead())
				{
					defenderModel.Wound(attackerBalance.D);
				}

				if (defenderModel.IsDead())
				{
					GettedExp += _modelGlobalDataList.GetExpFrom(defenderModel.ModelType);
				}
			}
		}

		public void MeleeAttack(ModelObject defenderModel)
		{
		}

		private void ShowMissAnimation()
		{
			LocalBattleEffectController.Instance.ShowEffect(LocalBattleEffectController.Instance.MissAnimationPool, AnimationHolder.position);
		}

		private void ShowToughnessDefenseAnimation()
		{
			LocalBattleEffectController.Instance.ShowEffect(LocalBattleEffectController.Instance.ToughnessDefendAnimationPool, AnimationHolder.position);
		}

		private void ShowSaveDefenseAnimation()
		{
			LocalBattleEffectController.Instance.ShowEffect(LocalBattleEffectController.Instance.SaveDefendAnimationPool, AnimationHolder.position);
		}

		private void ShowWoundAnimation()
		{
			LocalBattleEffectController.Instance.ShowEffect(LocalBattleEffectController.Instance.WoundAnimationPool, AnimationHolder.position);
		}

		private void ShowAttackAnimation()
		{
			LocalBattleEffectController.Instance.ShowEffect(LocalBattleEffectController.Instance.AttackAnimationPool, AnimationHolder.position);
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
			_astarAI.maxSpeed = CommonSpeed;
			_astarAI.destination = transform.position + transform.forward * moveForwardDistance;
		}

		public void Charge()
		{
			_astarAI.maxSpeed = ChargeSpeed;
		}

		public void ReturnToStartPositions()
		{
			_astarAI.Teleport(_startPositions);
		}
	}
}