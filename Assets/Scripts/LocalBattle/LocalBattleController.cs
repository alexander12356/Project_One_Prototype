using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LocalBattleController : MonoBehaviour
{
	public Transform PlayerSquadsHolder;
	public Transform EnemySquadsHolder;
	public SquadObject SquadObjectPrefab;
	public float _xSpawnDelta;

	public List<SquadObject> PlayerSquadObjects;
	public List<SquadObject> EnemySquadObjects;

	public static LocalBattleController Instance;

	public float BeforeTurnDelay;
	public float AnimationDelay;
	public float BeforeResultDelay;

	private void Awake()
	{
		Instance = this;
	}

	private void Start()
	{
		PlayerSquadObjects = CreateSquad(PlayerData.Instance.Squad, PlayerSquadsHolder, true);
	}

	public List<SquadObject> CreateSquad(List<PlayerData.SquadLocalData> enemySquad, Transform holder, bool toRight)
	{
		var squadObjectList = new List<SquadObject>();

		for (var i = 0; i < enemySquad.Count; i++)
		{
			var squadData = enemySquad[i];
			var squadObject = Instantiate(SquadObjectPrefab, holder);
			squadObject.SetData(squadData);
			var orientation = toRight ? Vector3.right : Vector3.left;
			squadObject.transform.localPosition += Vector3.zero + i * orientation * _xSpawnDelta;
			squadObjectList.Add(squadObject);
		}

		return squadObjectList;
	}

	public void SetEnemySquad(List<PlayerData.SquadLocalData> enemySquadData)
	{
		EnemySquadObjects = CreateSquad(enemySquadData, EnemySquadsHolder, false);
	}

	public void StartBattle()
	{
		StartCoroutine(StartBattleCoroutine());
	}

	private IEnumerator StartBattleCoroutine()
	{
		var attackerSquads = PlayerSquadObjects;
		var defenderSquads = EnemySquadObjects;
		var isBattleEnd = false;

		while (!isBattleEnd)
		{
			yield return new WaitForSeconds(BeforeTurnDelay);

			int j = 0;
			for (int i = 0; i < attackerSquads.Count; i++)
			{
				if (attackerSquads[i].IsDead())
				{
					continue;
				}

				if (i < defenderSquads.Count)
				{
					attackerSquads[i].Attack(defenderSquads[i]);
				}
				else
				{
					if (j >= defenderSquads.Count)
					{
						j = 0;
					}
					attackerSquads[i].Attack(defenderSquads[j]);
					j++;
				}
			}

			yield return new WaitForSeconds(AnimationDelay);

			var defenderDead = true;

			for (int i = defenderSquads.Count - 1; i >= 0; i--)
			{
				if (defenderSquads[i].IsDead())
				{
					Destroy(defenderSquads[i].gameObject);
					defenderSquads.Remove(defenderSquads[i]);
				}
				else
				{
					defenderDead = false;
				}
			}

			isBattleEnd = defenderDead;

			if (!isBattleEnd)
			{
				(attackerSquads, defenderSquads) = (defenderSquads, attackerSquads);
			}
		}

		yield return new WaitForSeconds(BeforeResultDelay);

		for (int i = PlayerData.Instance.Squad.Count - 1; i >= 0; i--)
		{
			var squadId = PlayerData.Instance.Squad[i].Id;
			if (!PlayerSquadObjects.Exists(x => x.SquadLocalData.Id == squadId))
			{
				PlayerData.Instance.Squad.RemoveAt(i);
			}
		}

		if (attackerSquads == PlayerSquadObjects)
		{
			Win();
		}
		else
		{
			Lose();
		}
	}

	private void Win()
	{
		LocalBattleUi.Instance.ShowWinWindow(PlayerSquadObjects);
	}

	private void Lose()
	{
		LocalBattleUi.Instance.ShowLoseWindow();
	}
}