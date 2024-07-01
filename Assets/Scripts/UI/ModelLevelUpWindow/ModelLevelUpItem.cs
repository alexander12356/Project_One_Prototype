using Mech.Data.Global;
using UnityEngine;

namespace Mech.UI
{
	public class ModelLevelUpItem : MonoBehaviour
	{
		[SerializeField] private RectTransform _childRectTransform;
		
		public void Init(ModelType childModel)
		{
		}

		public RectTransform ChildTransform => _childRectTransform;
	}
}