using System;

namespace Xevle.Logging
{
	/// <summary>
	/// Log levels as enum. Supports compund types.
	/// </summary>
	[Flags]
	public enum LogLevel
	{
		Fatal = 0x1,
		Error = 0x2,
		Warning = 0x4,
		Information = 0x8,
		Verbose = 0x10,
		Debug = 0x4000,
		Trace = 0x8000,

		CriticalToVerbose = Fatal | Error | Warning | Information | Verbose,
		CriticalToInformation = Fatal | Error | Warning | Information,
		CriticalToWarning = Fatal | Error | Warning,
		CriticalToError = Fatal | Error,

		DebugAndMessageBox = Trace | Debug,
	}
}