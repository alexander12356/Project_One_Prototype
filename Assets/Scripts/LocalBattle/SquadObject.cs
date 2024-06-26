using System;
using System.Collections;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SquadObject : MonoBehaviour
{
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

	[FormerlySerializedAs("_squadLocalData")]
	public PlayerData.SquadLocalData SquadLocalData;

	public void SetData(PlayerData.SquadLocalData squadData)
	{
		Icon.sprite = BalanceController.Instance.GetSquadGlobalData(squadData.Id).Icon;
		SquadLocalData = squadData;
		SquadLocalData.Wound = BalanceController.Instance.GetSquadGlobalData(squadData.Id).W;
	}

	public void Attack(SquadObject enemySquadObject)
	{
		/*
		var attackerBalance = BalanceController.Instance.GetSquadGlobalData(SquadLocalData.Id);
		var defenderBalance = BalanceController.Instance.GetSquadGlobalData(enemySquadObject.SquadLocalData.Id);

		for (var i = 0; i < attackerBalance.A; i++)
		{
			ShowAttackAnimation();

			var isHit = Roll(attackerBalance.BS);
			if (isHit)
			{
				var targetRoll = 6;
				if (attackerBalance.S / 2 > defenderBalance.T)
				{
					targetRoll = 2;
				}
				else if (attackerBalance.S > defenderBalance.T)
				{
					targetRoll = 3;
				}
				else if (attackerBalance.S == defenderBalance.T)
				{
					targetRoll = 4;
				}
				else if (attackerBalance.S / 2 < defenderBalance.T)
				{
					targetRoll = 6;
				}
				else
				{
					targetRoll = 5;
				}

				var isWound = Roll(targetRoll);

				if (isWound)
				{
					var isSave = Roll(defenderBalance.Sv);

					if (!isSave)
					{
						enemySquadObject.ShowWoundAnimation();
						if (!enemySquadObject.IsDead())
						{
							enemySquadObject.Wound(attackerBalance.D);
							if (enemySquadObject.IsDead())
							{
								GettedExp += BalanceController.Instance.GetExpFrom(enemySquadObject.SquadLocalData.Id);
							}
						}
					}
					else
					{
						enemySquadObject.ShowSaveDefenseAnimation();
					}
				}
				else
				{
					enemySquadObject.ShowToughnessDefenseAnimation();
				}
			}
			else
			{
				enemySquadObject.ShowMissAnimation();
			}
		}
		*/
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
		SquadLocalData.Wound -= attackerBalanceD;
	}

	public bool IsDead()
	{
		return SquadLocalData.Wound <= 0;
	}

	private bool Roll(int targetRoll)
	{
		var rollValue = Random.Range(0, 7);
		return rollValue >= targetRoll;
	}
}