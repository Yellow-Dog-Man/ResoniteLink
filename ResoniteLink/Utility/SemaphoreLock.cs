using System;
using System.Threading;
using System.Threading.Tasks;

namespace ResoniteLink
{
    /// <summary>
    /// A lock around a semaphore that when disposed, releases the lock count.
    ///
    /// This value is not designed to be passed around.
    /// </summary>
    /// <example>
    ///  using(await SemaphoreLock.AcquireAsync(sem, cancellationToken))
    ///  {
    ///    // sensitive operations
    ///  }
    /// </example>
    internal struct SemaphoreLock : IDisposable
    {
        private readonly SemaphoreSlim _sem;
        private bool _disposed;
        private SemaphoreLock(SemaphoreSlim sem)
        {
            _sem = sem;
            _disposed = false;
        }
        
        public static async ValueTask<SemaphoreLock> AcquireAsync(SemaphoreSlim sem, CancellationToken cancellationToken)
        {
            await sem.WaitAsync(cancellationToken);
            return new SemaphoreLock(sem);
        }
        
        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _sem?.Release();
            }
        }
    }
}