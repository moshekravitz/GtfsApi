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

        public int ShapeId { set; get; }

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

        public Double StopLat { get; set; }

        public Double StopLon { get; set; }
    }

    [BsonIgnoreExtraElements]
    public record Coords
    {
        public Double shapeLat { get; set; }

        public Double shapeLon { get; set; }
    }

    [BsonIgnoreExtraElements]
    public record Shapes
    {
        [Key]
        public int ShapeId { get; set; }

        public List<Coords> ShapeCoords  { set; get; }
    }
}
