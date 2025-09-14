using System;
using System.Runtime.CompilerServices;

namespace YetAnotherSilkSongPlugin.Patches
{
    static class MaxHealthTracker
    {
        static ConditionalWeakTable<HealthManager, TrackerObj> trackerPool = new ConditionalWeakTable<HealthManager, TrackerObj>();
        class TrackerObj
        {
            public int value;
            public TrackerObj(int value)
            {
                this.value = value;
            }
            public void Update(int newVal)
            {
                value = Math.Max(newVal, value);
            }
        }

        public static void Track(HealthManager target)
        {
            if (trackerPool.TryGetValue(target, out var tracker))
            {
                tracker.Update(target.hp);
            }
            else
            {
                trackerPool.Add(target, new TrackerObj(target.hp));
            }
        }

        public static int Get(HealthManager target, int ifNull)
        {
            if (trackerPool.TryGetValue(target, out var tracker))
            {
                return tracker.value;
            }
            return ifNull;
        }
    }
}
