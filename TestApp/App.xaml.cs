using System;
using System.Threading;
using System.Windows;

namespace TestApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Id Mutex.
        /// </summary>
        private const string MUTEX_ID = @"Global\EasyMorph.App";
        private Mutex _instanceMutex;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // Check 
            if (TryRunNewInstance() == false)
                Environment.Exit(0);
        }

        /// <inheritdoc />
        protected override void OnExit(ExitEventArgs e)
        {
            Console.Beep();
            _instanceMutex?.ReleaseMutex();
            _instanceMutex?.Dispose();

            base.OnExit(e);
        }

        /// <summary>
        /// Attempts to run a new instance of the application.
        /// </summary>
        /// <returns>
        /// Returns <see langword="true" /> if the application is running as the only instance; 
        /// <see langword="false" /> if another instance is already running.
        /// </returns>
        public bool TryRunNewInstance()
        {
            _instanceMutex = new Mutex(true, MUTEX_ID, out var isCreatedNew);

            if (!isCreatedNew)
            {
                _instanceMutex = null;
                return false;
            }

            return true;
        }
    }
}
