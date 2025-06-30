using System;
using System.Collections.Generic;
public static class IdRegistry
{
	private static Dictionary<Type, int> m_typeToId = new();
	private static Dictionary<int, Type> m_idToType = new();
	private static int m_nextId = 1;

	public static int GetId<T>() where T : class => GetId(typeof(T));

	public static int GetId(Type type)
	{
		if (m_typeToId.TryGetValue(type, out int id))
			return id;

		id = m_nextId++;
		m_typeToId[type] = id;
		m_idToType[id] = type;
		return id;
	}

	public static Type GetTypeById(int id)
	{
		m_idToType.TryGetValue(id, out var type);
		return type;
	}
}
