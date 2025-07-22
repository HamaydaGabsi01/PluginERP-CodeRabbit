using System;
using DevExpress.XtraSplashScreen;

namespace ETAT_READ
{
    public class SafeSplashScreenManager
    {
        private readonly SplashScreenManager _splashScreenManager;
        private bool _isWaitFormOpen = false;
        private readonly object _lock = new object();

        public SafeSplashScreenManager(SplashScreenManager splashScreenManager)
        {
            _splashScreenManager = splashScreenManager ?? throw new ArgumentNullException(nameof(splashScreenManager));
        }

        public void ShowWaitForm()
        {
            lock (_lock)
            {
                try
                {
                    if (!_isWaitFormOpen)
                    {
                        _splashScreenManager.ShowWaitForm();
                        _isWaitFormOpen = true;
                    }
                }
                catch (Exception)
                {
                    // If showing fails for any reason, ensure state is consistent
                    _isWaitFormOpen = false;
                }
            }
        }

        public void CloseWaitForm()
        {
            lock (_lock)
            {
                try
                {
                    if (_isWaitFormOpen)
                    {
                        _splashScreenManager.CloseWaitForm();
                        _isWaitFormOpen = false;
                    }
                }
                catch (Exception)
                {
                    // If closing fails for any reason, ensure state is reset
                    _isWaitFormOpen = false;
                }
            }
        }

        public void SetWaitFormCaption(string caption)
        {
            lock (_lock)
            {
                try
                {
                    if (_isWaitFormOpen)
                    {
                        _splashScreenManager.SetWaitFormCaption(caption);
                    }
                }
                catch (Exception)
                {
                    // Ignore errors when setting caption
                }
            }
        }

        public void SetWaitFormDescription(string description)
        {
            lock (_lock)
            {
                try
                {
                    if (_isWaitFormOpen)
                    {
                        _splashScreenManager.SetWaitFormDescription(description);
                    }
                }
                catch (Exception)
                {
                    // Ignore errors when setting description
                }
            }
        }

        public void EnsureClosed()
        {
            lock (_lock)
            {
                try
                {
                    if (_isWaitFormOpen)
                    {
                        _splashScreenManager.CloseWaitForm();
                    }
                }
                catch (Exception)
                {
                    // Ignore any errors
                }
                finally
                {
                    _isWaitFormOpen = false;
                }
            }
        }

        public bool IsWaitFormOpen
        {
            get
            {
                lock (_lock)
                {
                    return _isWaitFormOpen;
                }
            }
        }

        // Disposable pattern to ensure cleanup
        public void Dispose()
        {
            EnsureClosed();
        }
    }
} 