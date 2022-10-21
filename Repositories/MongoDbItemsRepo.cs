using MongoDB.Driver;
using MongoDB.Bson;
using Catalog.repositories;
using GtfsApi.Entities;

namespace GtfsApi.Repositories
{
    public class MongoDbRoutesDepository : IRoutesRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "routes";
        private readonly IMongoCollection<Routes> routesCollection;
        private readonly FilterDefinitionBuilder<Routes> filterBuilder = Builders<Routes>.Filter;
        public MongoDbRoutesDepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            routesCollection = database.GetCollection<Routes>(collectionName);
        }

        public async Task<List<Routes>> GetAllAsync()
        {
            return await (await routesCollection.FindAsync(new BsonDocument())).ToListAsync();
        }

        public async Task UpdateListAsync(List<Routes> routesList)
        {
            if (routesCollection.EstimatedDocumentCount() > 0)
            {
                var it = routesList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<Routes>();
                option.IsUpsert = true;

                while (it.Current != null)
                {
                    await routesCollection.FindOneAndReplaceAsync(
                        filterBuilder.Eq(route => route.RouteId, it.Current.RouteId),
                        it.Current,
                        option);
                    it.MoveNext();
                }
            }
            else
            {
                await routesCollection.InsertManyAsync(routesList);
            }
        }

        public async Task DeleteManyAsync(List<Routes> routesList)
        {
            var it = routesList.GetEnumerator();
            it.MoveNext();
            while (it.Current != null)
            {
                await routesCollection.FindOneAndDeleteAsync(
                    filterBuilder.Eq(route => route.RouteId, it.Current.RouteId));
                it.MoveNext();
            }
        }

        public async Task CreateListAsync(List<Routes> routesList)
        {

            routesCollection.UpdateMany(new BsonDocument(), Builders<Routes>.Update.Set(route => route.dirtyBit, false));
            if (routesCollection.EstimatedDocumentCount() > 0)
            {
                var it = routesList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<Routes>();
                option.IsUpsert = true;
                // option.Sort = Builders<Routes>.Sort.Ascending(route => route.RouteId);//TODO: remove
                while (it.Current != null)
                {
                    routesCollection.FindOneAndReplace(
                        filterBuilder.Eq(route => route.RouteId, it.Current.RouteId),
                        it.Current,
                        option);
                    it.MoveNext();
                }

                routesCollection.DeleteMany(filterBuilder.Where(route => route.dirtyBit == false));
            }
            else
            {
                await routesCollection.InsertManyAsync(routesList);
            }
        }

        public async Task DeleteAllAsync()
        {
            // FilterDefinition<Routes> filter = filterBuilder.Where(_ => true);
            await routesCollection.DeleteManyAsync(new BsonDocument());
        }
    }

    public class MongoDbExtendedRoutesDepository : IExtendedRoutesRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "extended routes";

        private readonly IMongoCollection<ExtendedRoutes> extendedRoutesCollection;
        private readonly FilterDefinitionBuilder<ExtendedRoutes> filterBuilder = Builders<ExtendedRoutes>.Filter;

        public MongoDbExtendedRoutesDepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            extendedRoutesCollection = database.GetCollection<ExtendedRoutes>(collectionName);

        }

        public async Task<ExtendedRoutes> GetSingleAsync(int myRouteId)
        {
            var filter = filterBuilder.Eq(route => route.RouteId, myRouteId);
            return await (await extendedRoutesCollection.FindAsync(filter)).SingleOrDefaultAsync();
        }

        public async Task UpdateListAsync(List<ExtendedRoutes> myList)
        {
            if (extendedRoutesCollection.EstimatedDocumentCount() > 0)
            {
                var it = myList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<ExtendedRoutes>();
                option.IsUpsert = true;

                while (it.Current != null)
                {
                    await extendedRoutesCollection.FindOneAndReplaceAsync(
                        filterBuilder.Eq(route => route.RouteId, it.Current.RouteId),
                        it.Current,
                        option);
                    it.MoveNext();
                }
            }
            else
            {
                await extendedRoutesCollection.InsertManyAsync(myList);
            }
        }

        public async Task DeleteManyAsync(List<ExtendedRoutes> myList)
        {
            var it = myList.GetEnumerator();
            it.MoveNext();
            while (it.Current != null)
            {
                await extendedRoutesCollection.FindOneAndDeleteAsync(
                    filterBuilder.Eq(route => route.RouteId, it.Current.RouteId));
                it.MoveNext();
            }
        }

        public async Task CreateListAsync(List<ExtendedRoutes> extendedRoutesList)
        {
            extendedRoutesCollection.UpdateMany(new BsonDocument(), Builders<ExtendedRoutes>.Update.Set(eRoute => eRoute.dirtyBit, false));
            if (extendedRoutesCollection.EstimatedDocumentCount() > 0)
            {
                var it = extendedRoutesList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<ExtendedRoutes>();
                option.IsUpsert = true;
                while (it.Current != null)
                {
                    extendedRoutesCollection.FindOneAndReplace(
                        filterBuilder.Eq(eRoute => eRoute.RouteId, it.Current.RouteId),
                        it.Current,
                        new FindOneAndReplaceOptions<ExtendedRoutes> { IsUpsert = true });
                    it.MoveNext();
                }

                extendedRoutesCollection.DeleteMany(filterBuilder.Where(eRoute => eRoute.dirtyBit == false));
            }
            else
            {
                await extendedRoutesCollection.InsertManyAsync(extendedRoutesList);
            }
        }

        public async Task DeleteAllAsync()
        {
            await extendedRoutesCollection.DeleteManyAsync(new BsonDocument());
        }
    }

    public class MongoDbStopTimesListDepository : IStopTimesRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "stop times list";

        private readonly IMongoCollection<StopTimesList> StopTimesListCollection;

        private readonly FilterDefinitionBuilder<StopTimesList> filterBuilder = Builders<StopTimesList>.Filter;

        public MongoDbStopTimesListDepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            StopTimesListCollection = database.GetCollection<StopTimesList>(collectionName);

        }

        public async Task<StopTimesList> GetSingleAsync(int myRouteId)
        {
            var filter = filterBuilder.Eq(route => route.RouteId, myRouteId);
            return await (await StopTimesListCollection.FindAsync(filter)).SingleOrDefaultAsync();
        }

        public async Task UpdateListAsync(List<StopTimesList> myList)
        {
            if (StopTimesListCollection.EstimatedDocumentCount() > 0)
            {
                var it = myList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<StopTimesList>();
                option.IsUpsert = true;

                while (it.Current != null)
                {
                    await StopTimesListCollection.FindOneAndReplaceAsync(
                        filterBuilder.Eq(route => route.RouteId, it.Current.RouteId),
                        it.Current,
                        option);
                    it.MoveNext();
                }
            }
            else
            {
                await StopTimesListCollection.InsertManyAsync(myList);
            }
        }

        public async Task DeleteManyAsync(List<StopTimesList> myList)
        {
            var it = myList.GetEnumerator();
            it.MoveNext();
            while (it.Current != null)
            {
                await StopTimesListCollection.FindOneAndDeleteAsync(
                    filterBuilder.Eq(route => route.RouteId, it.Current.RouteId));
                it.MoveNext();
            }
        }

        public async Task CreateListAsync(List<StopTimesList> stopTimesList)
        {

            StopTimesListCollection.UpdateMany(new BsonDocument(), Builders<StopTimesList>.Update.Set(stopTimeL => stopTimeL.dirtyBit, false));
            if (StopTimesListCollection.EstimatedDocumentCount() > 0)
            {
                var it = stopTimesList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<StopTimesList>();
                option.IsUpsert = true;
                while (it.Current != null)
                {
                    StopTimesListCollection.FindOneAndReplace(
                        filterBuilder.Eq(stopTimeL => stopTimeL.RouteId, it.Current.RouteId),
                        it.Current,
                        new FindOneAndReplaceOptions<StopTimesList> { IsUpsert = true });
                    it.MoveNext();
                }

                StopTimesListCollection.DeleteMany(filterBuilder.Where(eRoute => eRoute.dirtyBit == false));
            }
            else
            {
                await StopTimesListCollection.InsertManyAsync(stopTimesList);
            }
        }

        public async Task DeleteAllAsync()
        {
            await StopTimesListCollection.DeleteManyAsync(new BsonDocument());
        }
    }

    public class MongoDbStopInfoDepository : IStopInfoRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "stop info";

        private readonly IMongoCollection<StopInfo> StopInfoCollection;
        private readonly FilterDefinitionBuilder<StopInfo> filterBuilder = Builders<StopInfo>.Filter;

        public MongoDbStopInfoDepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            StopInfoCollection = database.GetCollection<StopInfo>(collectionName);

        }

        public async Task UpdateListAsync(List<StopInfo> myList)
        {
            if (StopInfoCollection.EstimatedDocumentCount() > 0)
            {
                var it = myList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<StopInfo>();
                option.IsUpsert = true;

                while (it.Current != null)
                {
                    await StopInfoCollection.FindOneAndReplaceAsync(
                        filterBuilder.Eq(stop => stop.StopId, it.Current.StopId),
                        it.Current,
                        option);
                    it.MoveNext();
                }
            }
            else
            {
                await StopInfoCollection.InsertManyAsync(myList);
            }
        }

        public async Task DeleteManyAsync(List<StopInfo> myList)
        {
            var it = myList.GetEnumerator();
            it.MoveNext();
            while (it.Current != null)
            {
                await StopInfoCollection.FindOneAndDeleteAsync(
                    filterBuilder.Eq(stop => stop.StopId, it.Current.StopId));
                it.MoveNext();
            }
        }
        public async Task<IEnumerable<StopInfo>> GetListAsync(List<int> stopIdList)
        {
            List<StopInfo> newStopIList = new List<StopInfo> { };
            var it = stopIdList.GetEnumerator();
            while (it.MoveNext())
            {
                newStopIList.Add(await (await StopInfoCollection.FindAsync(filterBuilder.Eq(stop => stop.StopId, it.Current))).SingleOrDefaultAsync());
            }
            return newStopIList;
        }

        public async Task CreateListAsync(List<StopInfo> stopInfo)
        {

            StopInfoCollection.UpdateMany(new BsonDocument(), Builders<StopInfo>.Update.Set(stopI => stopI.dirtyBit, false));
            if (StopInfoCollection.EstimatedDocumentCount() > 0)
            {
                var it = stopInfo.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<StopInfo>();
                option.IsUpsert = true;
                while (it.Current != null)
                {
                    StopInfoCollection.FindOneAndReplace(
                        filterBuilder.Eq(stopI => stopI.StopId, it.Current.StopId),
                        it.Current,
                        new FindOneAndReplaceOptions<StopInfo> { IsUpsert = true });
                    it.MoveNext();
                }

                StopInfoCollection.DeleteMany(filterBuilder.Where(stopI => stopI.dirtyBit == false));
            }
            else
            {
                await StopInfoCollection.InsertManyAsync(stopInfo);
            }
        }

        public async Task DeleteAllAsync()
        {
            await StopInfoCollection.DeleteManyAsync(new BsonDocument());
        }
    }

    public class MongoDbRouteToDateDepository : IRouteToDateRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "route to date";

        private readonly IMongoCollection<RouteToDate> RouteToDateCollection;
        private readonly FilterDefinitionBuilder<RouteToDate> filterBuilder = Builders<RouteToDate>.Filter;

        public MongoDbRouteToDateDepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            RouteToDateCollection = database.GetCollection<RouteToDate>(collectionName);

        }

        public async Task UpdateListAsync(List<RouteToDate> myList)
        {
            if (RouteToDateCollection.EstimatedDocumentCount() > 0)
            {
                var it = myList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<RouteToDate>();
                option.IsUpsert = true;

                while (it.Current != null)
                {
                    await RouteToDateCollection.FindOneAndReplaceAsync(
                        filterBuilder.Eq(route => route.LineDetailRecordId, it.Current.LineDetailRecordId),
                        it.Current,
                        option);
                    it.MoveNext();
                }
            }
            else
            {
                await RouteToDateCollection.InsertManyAsync(myList);
            }
        }

        public async Task DeleteManyAsync(List<RouteToDate> myList)
        {
            var it = myList.GetEnumerator();
            it.MoveNext();
            while (it.Current != null)
            {
                await RouteToDateCollection.FindOneAndDeleteAsync(
                    filterBuilder.Eq(route => route.LineDetailRecordId, it.Current.LineDetailRecordId));
                it.MoveNext();
            }
        }
        public async Task<RouteToDate> GetSingleAsync(int myRouteId)
        {
            var filter = filterBuilder.Eq(route => route.LineDetailRecordId, myRouteId);
            return await (await RouteToDateCollection.FindAsync(filter)).SingleOrDefaultAsync();
        }

        public async Task CreateListAsync(List<RouteToDate> routeToDate)
        {
            RouteToDateCollection.UpdateMany(new BsonDocument(), Builders<RouteToDate>.Update.Set(routeTD => routeTD.dirtyBit, false));
            if (RouteToDateCollection.EstimatedDocumentCount() > 0)
            {
                var it = routeToDate.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<StopInfo>();
                option.IsUpsert = true;
                while (it.Current != null)
                {
                    RouteToDateCollection.FindOneAndReplace(
                        filterBuilder.Eq(routeTD => routeTD.LineDetailRecordId, it.Current.LineDetailRecordId),
                        it.Current,
                        new FindOneAndReplaceOptions<RouteToDate> { IsUpsert = true });
                    it.MoveNext();
                }

                RouteToDateCollection.DeleteMany(filterBuilder.Where(routeTD => routeTD.dirtyBit == false));
            }
            else
            {
                await RouteToDateCollection.InsertManyAsync(routeToDate);
            }
        }

        public async Task DeleteAllAsync()
        {
            await RouteToDateCollection.DeleteManyAsync(new BsonDocument());
        }
    }
}

