using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EDebug
{
	/// <summary>
	/// ֻ��levelMask������LogLevel�Ż���ʾ
	/// </summary>
	public static LogLevel levelMask = LogLevel.All;
	public static bool globalDebugEnabled = true;


	public static void AddLogLevel(LogLevel level)
	{
		levelMask |= level;
	}

	/// <summary>
	/// ���� <paramref name="levelMask"/> Ϊ <paramref name="level"/>
	/// </summary>
	public static void LogLevelFilter(LogLevel level, bool showGlobal = false)
	{
		levelMask = level;
		globalDebugEnabled = showGlobal;
	}

	public static void Log(string message, LogLevel level = LogLevel.All)
	{
		if (levelMask.HasFlag(level))
		{
			if (!globalDebugEnabled && level==LogLevel.All)
			{
				return;
			}
			Debug.Log(message);
		}
	}

}
