using GtfsApi.Entities;

namespace GtfsApi.Dots
{
    public record RouteDto(int RouteId, String RouteShortName,String RouteLongName);
    public record ExtendedRouteDto(int routeId, int agencyId, string routeShortName, string routeLongName, string routeType, string shapeStr);
    public record StopTimesListDto(int routeId, List<StopTime> stopTimes);
    public record StopInfoDto(int stopId, int stopCode, string stopName, string stopDesc, double stopLat, double stopLon, int locationType, int parentStation, int zoneId);
    public record RouteToDateDto(int lineDetailRecordId, List<RouteDate> routesDatesList);

    public static class Extensions {
        public static RouteDto AsDto(this Routes route)
        {
            return new RouteDto(route.RouteId,route.RouteLongName,route.RouteShortName);
        }

        public static ExtendedRouteDto AsDto(this ExtendedRoutes route)
        {
            return new ExtendedRouteDto(route.RouteId, route.AgencyId, route.RouteLongName, route.RouteShortName, route.RouteType, route.ShapeStr);
        }

        public static StopTimesListDto AsDto(this StopTimesList stopTimesList)
        {
            return new StopTimesListDto(stopTimesList.RouteId, stopTimesList.StopTimes);
        }

        public static IEnumerable<StopInfoDto> AsDto(this IEnumerable<StopInfo> stopInfoL)
        {
            List<StopInfoDto> SidList = new List<StopInfoDto> {};
            var stopInfo = stopInfoL.GetEnumerator();
            while(stopInfo.MoveNext())
            {
                SidList.Add(new StopInfoDto(stopInfo.Current.StopId,stopInfo.Current.StopCode, stopInfo.Current.StopName, stopInfo.Current.StopDesc, stopInfo.Current.StopLat, stopInfo.Current.StopLon, stopInfo.Current.LocationType, stopInfo.Current.ParentStation, stopInfo.Current.ZoneId));
            }
            return SidList;
        }

        public static RouteToDateDto AsDto(this RouteToDate routeToDate)
        {
            return new RouteToDateDto(routeToDate.LineDetailRecordId, routeToDate.RoutesDatesList);
        }

    }
}