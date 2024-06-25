using System.Collections;
using System.Linq;
using Data;
using UnityEngine;
using UnityEngine.Pool;

namespace LocalBattle3d
{
	public class ModelObject : MonoBehaviour
	{
		public MechBalance MechBalance;
		public SpriteRenderer Icon;
		public Transform AnimationHolder;

		public float AnimationLifeTime;
		public float SpawnTimeRandom;
		public int GettedExp;
		public ModelType ModelType;

		private int W;

		public void SetPosition(float columnOffset, float rowOffset)
		{
			transform.localPosition = new Vector3(columnOffset, 0f, rowOffset);
		}

		public void SetType(ModelType modelType)
		{
			ModelType = modelType;
			var balance = MechBalance.Balances.FirstOrDefault(x => x.ModelType == modelType);
			Icon.sprite = balance.Icon;
			W = balance.W;
		}

		public void Attack(ModelObject defenderModel)
		{
			var attackerBalance = MechBalance.GetMechBalance(ModelType);
			var defenderBalance = MechBalance.GetMechBalance(defenderModel.ModelType);

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
					GettedExp += MechBalance.GetExpFrom(defenderModel.ModelType);
				}
			}
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
	}
}