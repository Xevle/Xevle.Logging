using System;

namespace Xevle.Logging
{
	/// <summary>
	/// Interface for logging event reviever
	/// </summary>
	public interface ILogger
	{
		/// <summary>
		/// Recieve the specified sender and e.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void Recieve(object sender, LogEventArgs e);
	}
}

