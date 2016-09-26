using System;

namespace Xevle.Logging
{
	/// <summary>
	/// Generic logger with support for multiple output modules.
	/// </summary>
	public class Logger : IDisposable
	{
		/// <summary>
		/// The current log level.
		/// </summary>
		LogLevel logLevel = LogLevel.CriticalToInformation;

		/// <summary>
		/// Gets or sets the log level.
		/// </summary>
		/// <value>The log level.</value>
		public LogLevel LogLevel {
			get {
				return logLevel;
			}
			set {
				logLevel = value & LogLevel.CriticalToVerbose;
			}
		}

		/// <summary>
		/// The internal event handler.
		/// </summary>
		EventHandler<LogEventArgs> internalEventHandler;

		/// <summary>
		/// The internal event handler lock.
		/// </summary>
		readonly object internalEventHandlerLock = new object();

		/// <summary>
		/// Initializes a new instance of the <see cref="T:Xevle.Logging.Logger"/> class.
		/// </summary>
		/// <param name="logLevel">Log level.</param>
		public Logger(LogLevel logLevel = LogLevel.CriticalToInformation)
		{
			LogLevel = logLevel;
		}

		/// <summary>
		/// Adds the output.
		/// </summary>
		/// <param name="logger">Logger.</param>
		public void AddOutput(ILogger logger)
		{
			lock (internalEventHandlerLock) {
				internalEventHandler += logger.Recieve;
			}
		}

		/// <summary>
		/// Log the specified logLevel, message and args.
		/// </summary>
		/// <param name="logLevel">Log level.</param>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Log(LogLevel logLevel, string message, params object [] args)
		{
			if ((logLevel & LogLevel.DebugAndMessageBox) != 0) {
#if !DEBUG
				return;
#else
				if ((logLevel & LogLevel.CriticalToVerbose) == 0) logLevel |= LogLevel.Information;
#endif
			}

			if ((logLevel & LogLevel) == 0) return; // Ignore message, log level of message is to low.
			string text = message;

			if (args.Length > 0) 
			{
				text = string.Format(message, args);
			}

			Fire(this, new LogEventArgs(logLevel, text));
		}

		/// <summary>
		/// Log the specified message and args as fatal log event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Fatal(String message, params object [] args)
		{
			Log(LogLevel.Fatal, message, args);
		}

		/// <summary>
		/// Log the specified message and args as eror log event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Error(String message, params object [] args)
		{
			Log(LogLevel.Error, message, args);
		}

		/// <summary>
		/// Log the specified message and args as fatal log event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Warning(String message, params object [] args)
		{
			Log(LogLevel.Warning, message, args);
		}

		/// <summary>
		/// Log the specified message and args as information log event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Information(String message, params object [] args)
		{
			Log(LogLevel.Information, message, args);
		}

		/// <summary>
		/// Log the specified message and args as verbose log event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Verbose(String message, params object [] args)
		{
			Log(LogLevel.Verbose, message, args);
		}

		/// <summary>
		/// Log the specified message and args as debug log event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Debug(String message, params object [] args)
		{
			Log(LogLevel.Debug, message, args);
		}

		/// <summary>
		/// Log the specified message and args as trace log event.
		/// </summary>
		/// <param name="message">Message.</param>
		/// <param name="args">Arguments.</param>
		public void Trace(String message, params object [] args)
		{
			Log(LogLevel.Trace, message, args);
		}

		/// <summary>
		/// Fire the specified sender and e.
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void Fire(object sender, LogEventArgs e)
		{
			EventHandler<LogEventArgs> handler;
			lock (internalEventHandlerLock) {
				handler = internalEventHandler;
			}
			if (handler != null) {
				handler(sender, e);
			}
		}

		/// <summary>
		/// Removes all outputs.
		/// </summary>
		public void RemoveAllOutputs()
		{
			lock (internalEventHandlerLock) {
				internalEventHandler = null;
			}
		}

		/// <summary>
		/// Releases all resource used by the <see cref="T:Xevle.Logging.Logger"/> object.
		/// </summary>
		/// <remarks>Call <see cref="Dispose"/> when you are finished using the <see cref="T:Xevle.Logging.Logger"/>. The
		/// <see cref="Dispose"/> method leaves the <see cref="T:Xevle.Logging.Logger"/> in an unusable state. After calling
		/// <see cref="Dispose"/>, you must release all references to the <see cref="T:Xevle.Logging.Logger"/> so the garbage
		/// collector can reclaim the memory that the <see cref="T:Xevle.Logging.Logger"/> was occupying.</remarks>
		public void Dispose()
		{
			RemoveAllOutputs();
		}
	}
}

