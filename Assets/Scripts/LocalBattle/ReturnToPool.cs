using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour
{
	private ObjectPool<GameObject> _animationPool;

	public float _delayTime;

	// Start is called before the first frame update
	private IEnumerator Start()
	{
		yield return new WaitForSeconds(_delayTime);
		_animationPool.Release(gameObject);
	}

	public void SetAnimationPool(ObjectPool<GameObject> pool)
	{
		_animationPool = pool;
	}
}