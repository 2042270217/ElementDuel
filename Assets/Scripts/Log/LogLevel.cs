using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum LogLevel
{
	All = 0,
	GamePhaseState = 1,
	GameSystem = 1 << 1,
}
