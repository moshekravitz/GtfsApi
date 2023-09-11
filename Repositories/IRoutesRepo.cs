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

    public interface IStopInfoRepo
    {
        public Task<IEnumerable<StopInfo>> GetListAsync(List<int> stopIdList);

        public Task CreateListAsync(List<StopInfo> stopInfoList);

        public Task UpdateListAsync(List<StopInfo> stopInfoList);

        public Task DeleteManyAsync(List<StopInfo> stopInfoList);

        public Task DeleteAllAsync();
    }

    public interface IShapesRepo
    {
        public Task<Shapes> GetSingleAsync(int shapeId);

        public Task CreateListAsync(List<Shapes> shapesList);

        public Task UpdateListAsync(List<Shapes> shapesList);

        public Task DeleteManyAsync(List<Shapes> shapesList);

        public Task DeleteAllAsync();
    }
}
