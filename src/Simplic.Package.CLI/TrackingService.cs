using Simplic.Tracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simplic.Package.CLI
{
    /// <summary>
    /// Implements all business logic methods for the tracking system
    /// </summary>
    public class TrackingService : ITrackingService
    {
        public long GetNewTrackingId()
        {
            throw new NotImplementedException();
        }

        public Tracking.Tracking GetTracking(long trackingId)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TrackingEntry> GetTrackingEntriesByTrackingId(long trackingId)
        {
            throw new NotImplementedException();
        }

        public bool SaveEntries(IList<TrackingEntry> newEntries)
        {
            throw new NotImplementedException();
        }

        public bool SaveTracking(Tracking.Tracking tracking)
        {
            throw new NotImplementedException();
        }

        public void TrackChanges(object oldObj, object newObj, string tableName, Guid instanceDataGuid)
        {
            throw new NotImplementedException();
        }
    }
}
