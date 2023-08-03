using GtfsApi.Entities;

namespace GtfsApi.Dots
{
    public record RouteDto(int RouteId, String RouteShortName,String RouteLongName);
    public record ExtendedRouteDto(int routeId, string routeHeadSign, string shapeStr, List<StopTime> stopTimes);
    public record StopInfoDto(int stopId, string stopName, double stopLat, double stopLon);

    public static class Extensions {
        public static RouteDto AsDto(this Routes route)
        {
            return new RouteDto(route.RouteId,route.RouteLongName,route.RouteShortName);
        }

        public static ExtendedRouteDto AsDto(this ExtendedRoutes route)
        {
            return new ExtendedRouteDto(route.RouteId, route.RouteHeadSign, route.ShapeStr, route.StopTimes);
        }

        public static IEnumerable<StopInfoDto> AsDto(this IEnumerable<StopInfo> stopInfoL)
        {
            List<StopInfoDto> SidList = new List<StopInfoDto> {};
            var stopInfo = stopInfoL.GetEnumerator();
            while(stopInfo.MoveNext())
            {
                SidList.Add(new StopInfoDto(stopInfo.Current.StopId, stopInfo.Current.StopName, stopInfo.Current.StopLat, stopInfo.Current.StopLon));
            }
            return SidList;
        }
    }
}