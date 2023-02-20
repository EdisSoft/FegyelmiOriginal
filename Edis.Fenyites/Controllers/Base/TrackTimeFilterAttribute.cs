using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Diagnostics;
using System.Web.Mvc;
using System.Web.Routing;

namespace Edis.Fenyites.Controllers.Base
{
    public class TrackTimeFilter : ActionFilterAttribute
    {
        Stopwatch stopWatch;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            stopWatch = new Stopwatch();
            stopWatch.Start();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            try
            {
                stopWatch.Stop();
                
                int configAverage = int.TryParse(ConfigurationManager.AppSettings["TrackTimeLog"], out configAverage) ? configAverage : 0;
                int configCount = int.TryParse(ConfigurationManager.AppSettings["TrackCountLog"], out configCount) ? configCount : 0;

                if (configAverage >= 0 && ElapsedTime.ErrorCounter < ElapsedTime.ErrorCounterLimit)
                    Log(filterContext.RouteData, stopWatch.ElapsedMilliseconds, configAverage, configCount);
            }
            catch (Exception e)
            {
                if (ElapsedTime.ErrorCounter >= ElapsedTime.ErrorCounterLimit)
                    return;

                ElapsedTime.ErrorCounter++;
                Diagnostics.Log.Error($"Hiba az időmérés során! Próbálkozás: {ElapsedTime.ErrorCounter}/10" + (ElapsedTime.ErrorCounter == 10 ? " - Ajjaj valami nagyon nem jó! Mára leállítva a TrackTime! Hiba:" : " - Hiba:"), e);
                
            }        
        }

        private void Log(RouteData routeData, long time, int configAverage, int configCount)
        {
            var controllerName = routeData.Values["controller"];
            var actionName = routeData.Values["action"];
            var type = System.Web.HttpContext.Current.Request.HttpMethod;
            ElapsedTime.InsertEntity(time, controllerName + "\\" + actionName + "\\" + type, configAverage, configCount);
        }

        private static class ElapsedTime
        {
            public static int ErrorCounter = 0;
            public const int ErrorCounterLimit = 10;
            public static ConcurrentDictionary<string, ElapsedTimeEntity> IdentifiedElapseds = new ConcurrentDictionary<string, ElapsedTimeEntity>();

            public static void InsertEntity(long milliseconds, string identifier, int configAverage, int configCount)
            {
                lock (IdentifiedElapseds)
                {
                    ElapsedTimeEntity entry;
                    if (!IdentifiedElapseds.TryGetValue(identifier, out entry))
                    {
                        entry = new ElapsedTimeEntity();
                        IdentifiedElapseds.TryAdd(identifier, entry);
                    }

                    if (entry.MinDate != default(DateTime) && entry.MinDate.Date < DateTime.Today)
                    {
                        IdentifiedElapseds = new ConcurrentDictionary<string, ElapsedTimeEntity>();
                        ErrorCounter = 0;
                    }

                    entry.LastTime = milliseconds;
                    entry.LastDate = DateTime.UtcNow;
                    entry.TotalTime += milliseconds;
                    entry.Count++;

                    if (milliseconds < entry.MinTime)
                        entry.MinTime = milliseconds; entry.MinDate = DateTime.UtcNow;
                    if (milliseconds > entry.MaxTime)
                        entry.MaxTime = milliseconds; entry.MaxDate = DateTime.UtcNow;

                    if (entry.Average >= configAverage && entry.Count >= configCount)
                        Diagnostics.Log.Warning($"A függvény túl lassú: {identifier} - Átlagos futási idő az utolsó {entry.Count} alkalommal: {entry.Average}ms - min futási idő: {entry.MinTime}ms - jelenlegi futási idő: {milliseconds}ms - max futási idő: {entry.MaxTime}ms");
                }
            }
        }

        private class ElapsedTimeEntity
        {
            public long LastTime = 0;
            public long TotalTime = 0;
            public int Count = 0;
            public long MinTime = long.MaxValue;
            public long MaxTime = long.MinValue;
            public double Average
            {
                get { return (TotalTime / Count); }
            }

            public DateTime MinDate;
            public DateTime MaxDate;
            public DateTime LastDate;
        }
    }


}