﻿using System;

namespace GameLauncher_Console
{
	/// <summary>
	/// Entry point class.
	/// Configure logging and go further into the app
	/// </summary>
	class Program
	{
		static void Main(string[] args)
		{
#if DEBUG
			// Log unhandled exceptions
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Logger.CLogger.ExceptionHandleEvent);
#endif
			Logger.CLogger.Configure("GameLauncherConsole.log"); // Create a log file

			// Entry 
			CDock gameDock = new CDock();
			gameDock.Run();			
		}
	}
}
