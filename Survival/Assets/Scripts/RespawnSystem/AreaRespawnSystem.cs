using System.Collections.Generic;
using UnityEngine;

public class AreaRespawnSystem : MonoBehaviour
{
	[Tooltip("리스폰 반경")]
	[SerializeField] private float _spawnAreaRadius;
	[Tooltip("생성 높이. 지형이 평평하지 않을 경우 넉넉하게 높게 설정해주세요.")]
	[SerializeField] private float _standardHeight = 10;
	[Tooltip("해당 위치에 스폰될 오브젝트")]
	[SerializeField] private GameObject _targetObject;
	[Tooltip("만약 데이터를 DB에서 관리한다면 원하는 오브젝트의 id")]
	[SerializeField] private int _id;
	[Tooltip("스폰될 오브젝트의 개수")]
	[SerializeField] private int _spawnCount;

	private PoolingSystem _pool;
	private GameObject[] _spawnedObjects;

	private void Awake()
	{
		_pool = new PoolingSystem(_targetObject,
									minSize: _spawnCount,
									id:_id,
									poolManagerTransform:this.transform);

	}
	private void Start()
	{
		DayNightCycle.Instance.OnChangeDayEvent += Spawn;
	}

	public void Spawn()
	{
		HashSet<Vector3> positions  = new HashSet<Vector3>();
		while(positions.Count <= _spawnCount)
		{
			positions.Add(GetRandomPosition());
		}
        foreach (Vector3 position in positions)
        {
			if (_pool.IsPoolEmpty) { break; }
			GameObject go = _pool.TakeFromPool();
			go.transform.position = position;
		}

	}

	private Vector3 GetRandomPosition()
	{
		return new Vector3(GetRandomValue() + this.transform.position.x, _standardHeight+this.transform.position.y, GetRandomValue()+this.transform.position.z);
	}

	private float GetRandomValue()
	{
		return Random.Range(-_spawnAreaRadius, _spawnAreaRadius);
	}
}
