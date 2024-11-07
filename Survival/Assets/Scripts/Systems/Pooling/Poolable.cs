using UnityEngine;

public class Poolable : MonoBehaviour
{
	private PoolingSystem _pool;
	public void Init(PoolingSystem pool, int id = 0)
	{
		_pool = pool;
		if (gameObject.TryGetComponent<ILoadable>(out ILoadable lodable))
		{
			lodable.Load(id);
		}
	}
	private void OnDisable()
	{
		_pool.Release(this.gameObject);
	}
}
