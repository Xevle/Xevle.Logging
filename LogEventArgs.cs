using System;

namespace Xevle.Logging
{
	/// <summary>
	/// Log event arguments.
	/// </summary>
	public class LogEventArgs : EventArgs
	{
		/// <summary>
		/// Gets the timecode.
		/// </summary>
		/// <value>The timecode.</value>
		public DateTime Timecode { get; private set; }

		/// <summary>
		/// Gets the level.
		/// </summary>
		/// <value>The level.</value>
		public LogLevel Level { get; private set; }

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>The message.</value>
		public string Message { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Xevle.Logging.LogEventArgs"/> class.
		/// </summary>
		/// <param name="level">Level.</param>
		/// <param name="message">Message.</param>
		public LogEventArgs(LogLevel level, string message)
		{
			Timecode = DateTime.Now;
			Level = level;
			Message = message;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Xevle.Logging.LogEventArgs"/> class.
		/// </summary>
		/// <param name="e">E.</param>
		public LogEventArgs(LogEventArgs e)
		{
			this.Timecode = e.Timecode;
			this.Level = e.Level;
			this.Message = e.Message;
		}

		/// <summary>
		/// Returns a <see cref="T:System.String"/> that represents the current <see cref="T:Xevle.Logging.LogEventArgs"/>.
		/// </summary>
		/// <returns>A <see cref="T:System.String"/> that represents the current <see cref="T:Xevle.Logging.LogEventArgs"/>.</returns>
		public override string ToString()
		{
			return string.Format("[{0:D4}.{1:D2}.{2:D2} {3:D2}:{4:D2}:{5:D2},{6:D3}] - {7} - {8}",
				Timecode.Year, Timecode.Month, Timecode.Day,
				Timecode.Hour, Timecode.Minute, Timecode.Second, Timecode.Millisecond,
				(Level & LogLevel.CriticalToVerbose).ToString().ToUpper(), Message);
		}

		/// <summary>
		/// Tos the shorten string.
		/// </summary>
		/// <returns>The shorten string.</returns>
		public string ToShortenString()
		{
			return string.Format("[{0:D2}:{1:D2}:{2:D2},{3:D3}] - {4} - {5}",
				Timecode.Hour, Timecode.Minute, Timecode.Second, Timecode.Millisecond,
				(Level & LogLevel.CriticalToVerbose).ToString().ToUpper(), Message);
		}
	}
}

