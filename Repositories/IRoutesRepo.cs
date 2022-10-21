using GtfsApi.Entities;

namespace Catalog.repositories
{
    public interface IRoutesRepo
    {
        public  Task<List<Routes>> GetAllAsync();

        public Task CreateListAsync(List<Routes> routesList);

        public Task UpdateListAsync(List<Routes> routesList);

        public Task DeleteManyAsync(List<Routes> routesList);

        public Task DeleteAllAsync();
        
    }

    public interface IExtendedRoutesRepo
    {
        public Task<ExtendedRoutes> GetSingleAsync(int routeId);

        public Task CreateListAsync(List<ExtendedRoutes> routesList);

        public Task UpdateListAsync(List<ExtendedRoutes> routesList);

        public Task DeleteManyAsync(List<ExtendedRoutes> routesList);

        public Task DeleteAllAsync();

    }
    
    public interface IStopTimesRepo
    {
        public Task<StopTimesList> GetSingleAsync(int routeId);

        public Task CreateListAsync(List<StopTimesList> stopTimesList);

        public Task UpdateListAsync(List<StopTimesList> routesList);

        public Task DeleteManyAsync(List<StopTimesList> routesList);

        public Task DeleteAllAsync();
    }

    public interface IStopInfoRepo
    {
        public Task<IEnumerable<StopInfo>> GetListAsync(List<int> stopIdList);

        public Task CreateListAsync(List<StopInfo> StopInfoList);

        public Task UpdateListAsync(List<StopInfo> routesList);

        public Task DeleteManyAsync(List<StopInfo> routesList);

        public Task DeleteAllAsync();
    }

    public interface IRouteToDateRepo
    {
        public Task<RouteToDate> GetSingleAsync(int routeId);

        public Task CreateListAsync(List<RouteToDate> StopInfoList);

        public Task UpdateListAsync(List<RouteToDate> routesList);

        public Task DeleteManyAsync(List<RouteToDate> routesList);

        public Task DeleteAllAsync();
    }

}