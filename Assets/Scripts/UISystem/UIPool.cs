using System.Collections.Generic;
using UnityEngine;

public class UIPool<T> where T : Component
{
	Queue<T> pool = new();
	T prefab;
	Transform parent;

	public UIPool(T prefab, Transform parent)
	{
		this.prefab = prefab;
		this.parent = parent;
	}

	public T Get()
	{
		if (pool.Count > 0)
		{
			var item = pool.Dequeue();
			item.gameObject.SetActive(true);
			return item;
		}
		return GameObject.Instantiate(prefab, parent);
	}

	public void Release(T instance)
	{
		instance.gameObject.SetActive(false);
		pool.Enqueue(instance);
	}

	public void ReleaseAll(List<T> list)
	{
		if (list == null || list.Count == 0) return;
		foreach (var item in list)
		{
			Release(item);
		}
	}
}
