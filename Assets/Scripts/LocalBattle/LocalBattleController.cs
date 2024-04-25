using System;
using System.Collections.Generic;
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
}