using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GtfsApi.Entities
{
    [BsonIgnoreExtraElements]
    public record Routes
    {

        [Key]
        public int RouteId { get; set; }

        public string RouteShortName { set; get; }

        public string RouteLongName { set; get; }
    }

    [BsonIgnoreExtraElements]
    public record ExtendedRoutes
    {

        [Key]
        public int RouteId { get; set; }

        public string RouteHeadSign { set; get; }

        public string ShapeStr { set; get; }

        public List<StopTime> StopTimes { set; get; }
    }

    [BsonIgnoreExtraElements]
    public record StopTime
    {

        public int StopId { get; set; }

        public string StopInterval { get; set; }
    }

    [BsonIgnoreExtraElements]
    public record StopInfo
    {

        [Key]
        public int StopId { get; set; }

        public string StopName { get; set; }

        public double StopLat { get; set; }

        public double StopLon { get; set; }
    }
}