using Akavache;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Reactive.Concurrency;

namespace SchoolTripPlannerXamarin.Services.General
{
    class BlobCache : IBlobCache
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Insert(string key, byte[] data, DateTimeOffset? absoluteExpiration = null)
        {
            throw new NotImplementedException();
        }

        public IObservable<byte[]> Get(string key)
        {
            throw new NotImplementedException();
        }

        public IObservable<IEnumerable<string>> GetAllKeys()
        {
            throw new NotImplementedException();
        }

        public IObservable<DateTimeOffset?> GetCreatedAt(string key)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Flush()
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Invalidate(string key)
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> InvalidateAll()
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Vacuum()
        {
            throw new NotImplementedException();
        }

        public IObservable<Unit> Shutdown { get; }
        public IScheduler Scheduler { get; }
        public DateTimeKind? ForcedDateTimeKind { get; set; }
    }
}
