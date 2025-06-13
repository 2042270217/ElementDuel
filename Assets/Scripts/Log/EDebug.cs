using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EDebug
{
	/// <summary>
	/// 只有levelMask包含的LogLevel才会显示
	/// </summary>
	public static LogLevel levelMask = LogLevel.All;
	public static bool globalDebugEnabled = true;


	public static void AddLogLevel(LogLevel level)
	{
		levelMask |= level;
	}

	/// <summary>
	/// 重置 <paramref name="levelMask"/> 为 <paramref name="level"/>
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
