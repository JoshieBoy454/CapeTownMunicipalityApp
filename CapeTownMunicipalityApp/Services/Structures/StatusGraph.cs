using System;
using System.Collections.Generic;
using System.Linq;
using CapeTownMunicipalityApp.Models;

namespace CapeTownMunicipalityApp.Services
{
    public class StatusGraph
    {
        private readonly Dictionary<ReportStatus, List<ReportStatus>> _adjacency = new();
        private readonly List<ReportStatus> _stages;

        public StatusGraph()
        {
            // Define allowed transitions and linear stages for progress
            _stages = new List<ReportStatus>
            {
                ReportStatus.Pending,
                ReportStatus.Resolved,
                ReportStatus.Closed
            };

            void Add(ReportStatus from, params ReportStatus[] to)
            {
                if (!_adjacency.ContainsKey(from)) _adjacency[from] = new List<ReportStatus>();
                _adjacency[from].AddRange(to);
            }

            Add(ReportStatus.Pending, ReportStatus.Resolved, ReportStatus.Closed);
            Add(ReportStatus.Resolved, ReportStatus.Closed);
        }

        public IEnumerable<ReportStatus> Neighbors(ReportStatus status) =>
            _adjacency.TryGetValue(status, out var list) ? list : Enumerable.Empty<ReportStatus>();

        public IReadOnlyList<ReportStatus> Stages => _stages;

        public double GetProgressPercent(ReportStatus current)
        {
            var idx = _stages.IndexOf(current);
            if (idx < 0) return 0;
            return (_stages.Count <= 1) ? 100 : (idx * 100.0) / (_stages.Count - 1);
        }

        public List<ReportStatus> ShortestPath(ReportStatus from, ReportStatus to)
        {
            var queue = new Queue<ReportStatus>();
            var prev = new Dictionary<ReportStatus, ReportStatus?>();
            queue.Enqueue(from);
            prev[from] = null;
            while (queue.Count > 0)
            {
                var s = queue.Dequeue();
                if (s.Equals(to)) break;
                foreach (var n in Neighbors(s))
                {
                    if (prev.ContainsKey(n)) continue;
                    prev[n] = s;
                    queue.Enqueue(n);
                }
            }
            if (!prev.ContainsKey(to)) return new List<ReportStatus> { from };
            var path = new List<ReportStatus>();
            var cur = to;
            while (true)
            {
                path.Add(cur);
                if (prev[cur] == null) break;
                cur = prev[cur]!.Value;
            }
            path.Reverse();
            return path;
        }
    }
}


