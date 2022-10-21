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
        
        public bool dirtyBit {set; get; }
    }

    [BsonIgnoreExtraElements]
    public record ExtendedRoutes
    {

        [Key]
        public int RouteId { get; set; }

        public int AgencyId { set; get; }

        public string RouteShortName { set; get; }

        public string RouteLongName { set; get; }

        public string RouteType { set; get; }

        public string ShapeStr { set; get; }

        public bool dirtyBit {set; get; }
    }

    [BsonIgnoreExtraElements]
    public record StopTime
    {

        public string ArrivalTime { get; set; }//TODO: change to string?

        public string DepartureTime { get; set; }

        public int StopId { get; set; }

        public int StopSequence { get; set; }

        public int PickupType { get; set; }

        public int DropOffType { get; set; }
    }

    [BsonIgnoreExtraElements]
    public record StopTimesList
    {

        [Key]
        public int RouteId { get; set; }

        public List<StopTime> StopTimes { get; set; }

        public bool dirtyBit {set; get; }

    }

    [BsonIgnoreExtraElements]
    public record StopInfo
    {

        [Key]
        public int StopId { get; set; }

        public int StopCode { get; set; }

        public string StopName { get; set; }

        public string StopDesc { get; set; }

        public double StopLat { get; set; }

        public double StopLon { get; set; }

        public int LocationType { get; set; }

        public int ParentStation { get; set; }

        public int ZoneId { get; set; }

        public bool dirtyBit {set; get; }

    }

    [BsonIgnoreExtraElements]
    public record RouteDate
    {
        public int Direction { get; set; }

        public DateTime FromDate { get; set; }

        public DateTime ToDate { get; set; }

        public int DayInWeek { get; set; }

        public string DepartureTime { get; set; }

        public bool dirtyBit {set; get; }
    }

    public record RouteToDate
    {
        public int LineDetailRecordId { get; set; }

        public List<RouteDate> RoutesDatesList { get; set; }

        public bool dirtyBit {set; get; }
    }
}