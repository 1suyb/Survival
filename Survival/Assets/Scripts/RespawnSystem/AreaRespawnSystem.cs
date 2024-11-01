using System.Collections.Generic;
using UnityEngine;

public class AreaRespawnSystem : MonoBehaviour
{
	[Header("������ �ݰ�")]
	[SerializeField] private float _spawnAreaRadius;
	[Header("���� ����. ������ �������� ���� ��� �˳��ϰ� ���� �������ּ���.")]
	[SerializeField] private float _standardHeight = 10;
	[Header("�ش� ��ġ�� ������ ������Ʈ")]
	[SerializeField] private GameObject _targetObject;
	[Header("���� �����͸� DB���� �����Ѵٸ� ���ϴ� ������Ʈ�� id")]
	[SerializeField] private int _id;
	[Header("������ ������Ʈ�� ����")]
	[SerializeField] private int _spawnCount;

	private PoolingSystem _pool;

	private void Awake()
	{
		_pool = new PoolingSystem(_targetObject,
									minSize: _spawnCount,
									id:_id,
									poolManagerTransform:this.transform);
	}

	public void Spawn()
	{
		HashSet<Vector3> positions  = new HashSet<Vector3>();
		while(positions.Count <= _spawnCount)
		{
			positions.Add(GetRandomPosition());
		}
		foreach(Vector2 position in positions)
		{
			GameObject go = _pool.TakeFromPool();
			go.transform.position = position;
		}
	}

	private Vector3 GetRandomPosition()
	{
		return new Vector3(GetRandomValue(), _standardHeight, GetRandomValue());
	}

	private float GetRandomValue()
	{
		return Random.Range(-_spawnAreaRadius, _spawnAreaRadius);
	}
}
