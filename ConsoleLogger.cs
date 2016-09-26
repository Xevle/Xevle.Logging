using System;

namespace Xevle.Logging
{
	/// <summary>
	/// Logger logs to standard console.
	/// </summary>
	public class ConsoleLogger : ILogger
	{
		/// <summary>
		/// Recieve the specified sender and e.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		public void Recieve(object sender, LogEventArgs e)
		{
			Console.WriteLine(e.ToShortenString());
		}
	}
}

