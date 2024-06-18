using System.Collections;
using System.Linq;
using Data;
using UnityEngine;

namespace LocalBattle3d
{
	public class ModelObject : MonoBehaviour
	{
		public MechBalance MechBalance;
		public SpriteRenderer Icon;
		public Transform AnimationHolder;
		public GameObject AttackAnimationPrefab;
		public GameObject ToughnessDefendAnimationPrefab;
		public GameObject SaveDefendAnimationPrefab;
		public GameObject WoundAnimationPrefab;
		public GameObject MissAnimationPrefab;
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
			CreateEffect(MissAnimationPrefab);
		}

		private void ShowToughnessDefenseAnimation()
		{
			CreateEffect(ToughnessDefendAnimationPrefab);
		}

		private void ShowSaveDefenseAnimation()
		{
			CreateEffect(SaveDefendAnimationPrefab);
		}

		private void ShowWoundAnimation()
		{
			CreateEffect(WoundAnimationPrefab);
		}

		private void ShowAttackAnimation()
		{
			CreateEffect(AttackAnimationPrefab);
		}

		private void CreateEffect(GameObject prefab, bool withRandom = true)
		{
			StartCoroutine(CreateEffectCoroutine());

			IEnumerator CreateEffectCoroutine()
			{
				var spawnTime = Random.Range(0f, SpawnTimeRandom);
				yield return new WaitForSeconds(spawnTime);

				var effect = Instantiate(prefab, AnimationHolder);
				if (withRandom)
				{
					effect.transform.localPosition = Random.insideUnitCircle * 0.2f;
				}

				Destroy(effect, AnimationLifeTime);
			}
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