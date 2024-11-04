using System.Collections.Generic;

public class ClassPoolSystem<T>where T : new()
{
	
	private Stack<T> _pool;
	private readonly int MAXSIZE;
	private readonly int MINSIZE;
	private int _poolSize;

	public Stack<T> Pool => _pool;
	public bool IsPoolEmpty => _pool.Count == 0;

	public ClassPoolSystem(int minSize=0,int maxSize = 100)
	{
		_pool = new Stack<T>();
		MINSIZE = minSize;
		MAXSIZE = maxSize;
		_poolSize = 0;
	}

	private T CreatePooledItem()
	{
		T item = new T();
		Relase(item);
		return item;
	}

	public T TakeFromPool()
	{
		if (IsPoolEmpty)
		{
			CreatePooledItem();
		}
		T item = _pool.Pop();
		return item;
	}
	public void Relase(T item)
	{
        if (_poolSize == MAXSIZE)
        {
			return;
        }
        _pool.Push(item);
	}

}
