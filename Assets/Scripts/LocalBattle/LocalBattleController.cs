using System.Collections.Generic;
using UnityEngine;

public class LocalBattleController : MonoBehaviour
{
	public Transform PlayerSquadsHolder;
	public SquadObject SquadObjectPrefab;
	public float _xSpawnDelta;

	public List<SquadObject> SquadObjects;

	private void Start()
	{
		for (var i = 0; i < PlayerData.Instance.Squad.Count; i++)
		{
			var squadData = PlayerData.Instance.Squad[i];
			var squadObject = Instantiate(SquadObjectPrefab, PlayerSquadsHolder);
			squadObject.SetData(squadData);
			squadObject.transform.localPosition += Vector3.zero + i *Vector3.right * _xSpawnDelta;
			SquadObjects.Add(squadObject);
		}
	}
}