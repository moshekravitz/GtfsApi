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

            if (routesCollection.EstimatedDocumentCount() != 0)
            {
                await routesCollection.DeleteManyAsync(new BsonDocument());
            }
            await routesCollection.InsertManyAsync(routesList);
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
            return await (await extendedRoutesCollection.FindAsync(filter)).FirstOrDefaultAsync();
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
            if (extendedRoutesCollection.EstimatedDocumentCount() != 0)
            {
                await extendedRoutesCollection.DeleteManyAsync(new BsonDocument());
            }
            await extendedRoutesCollection.InsertManyAsync(extendedRoutesList);
        }

        public async Task DeleteAllAsync()
        {
            await extendedRoutesCollection.DeleteManyAsync(new BsonDocument());
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

            if (StopInfoCollection.EstimatedDocumentCount() != 0)
            {
                await StopInfoCollection.DeleteManyAsync(new BsonDocument());
            }
            await StopInfoCollection.InsertManyAsync(stopInfo);
        }

        public async Task DeleteAllAsync()
        {
            await StopInfoCollection.DeleteManyAsync(new BsonDocument());
        }
    }

    public class MongoDbShapesDepository : IShapesRepo
    {
        private const string databaseName = "catalog";
        private const string collectionName = "shapes";

        private readonly IMongoCollection<Shapes> shapesCollection;
        private readonly FilterDefinitionBuilder<Shapes> filterBuilder = Builders<Shapes>.Filter;

        public MongoDbShapesDepository(IMongoClient mongoClient)
        {
            IMongoDatabase database = mongoClient.GetDatabase(databaseName);
            shapesCollection = database.GetCollection<Shapes>(collectionName);

        }

        public async Task<Shapes> GetSingleAsync(int myShapeId)
        {
            var filter = filterBuilder.Eq(shape => shape.ShapeId, myShapeId);
            return await (await shapesCollection.FindAsync(filter)).FirstOrDefaultAsync();
        }

        public async Task UpdateListAsync(List<Shapes> myList)
        {
            if (shapesCollection.EstimatedDocumentCount() > 0)
            {
                var it = myList.GetEnumerator();

                it.MoveNext();
                var option = new FindOneAndReplaceOptions<Shapes>();
                option.IsUpsert = true;

                while (it.Current != null)
                {
                    await shapesCollection.FindOneAndReplaceAsync(
                        filterBuilder.Eq(shape => shape.ShapeId, it.Current.ShapeId),
                        it.Current,
                        option);
                    it.MoveNext();
                }
            }
            else
            {
                await shapesCollection.InsertManyAsync(myList);
            }
        }

        public async Task DeleteManyAsync(List<Shapes> myList)
        {
            var it = myList.GetEnumerator();
            it.MoveNext();
            while (it.Current != null)
            {
                await shapesCollection.FindOneAndDeleteAsync(
                    filterBuilder.Eq(shape => shape.ShapeId, it.Current.ShapeId));
                it.MoveNext();
            }
        }

        public async Task CreateListAsync(List<Shapes> shapesList)
        {
            if (shapesCollection.EstimatedDocumentCount() != 0)
            {
                await shapesCollection.DeleteManyAsync(new BsonDocument());
            }
            await shapesCollection.InsertManyAsync(shapesList);
        }

        public async Task DeleteAllAsync()
        {
            await shapesCollection.DeleteManyAsync(new BsonDocument());
        }
    }

}

