using UnityEngine;

namespace LocalBattle3d
{
	public class ModelObject : MonoBehaviour
	{
		public void SetPosition(float columnOffset, float rowOffset)
		{
			transform.localPosition = new Vector3(columnOffset, 0f, rowOffset);
		}
	}
}