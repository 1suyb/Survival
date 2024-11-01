using UnityEngine;

public class Poolable<T> : MonoBehaviour where T : MonoBehaviour
{
	private PoolingSystem<T> _pool;
	public void Init(PoolingSystem<T> pool, int id = 0)
	{
		_pool = pool;
		if (gameObject.TryGetComponent<ILoadable>(out ILoadable lodable))
		{
			lodable.Load(id);
		}
	}
	private void OnDisable()
	{
		_pool.Release(this.GetComponent<T>());
	}
}
