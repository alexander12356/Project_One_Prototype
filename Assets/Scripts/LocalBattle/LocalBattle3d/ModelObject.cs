using System.Linq;
using Data;
using UnityEngine;

namespace LocalBattle3d
{
	public class ModelObject : MonoBehaviour
	{
		public MechBalance MechBalance;
		public SpriteRenderer Icon;

		public void SetPosition(float columnOffset, float rowOffset)
		{
			transform.localPosition = new Vector3(columnOffset, 0f, rowOffset);
		}

		public void SetType(ModelType modelType)
		{
			var balance = MechBalance.Balances.FirstOrDefault(x => x.ModelType == modelType);
			Icon.sprite = balance.Icon;
		}
	}
}