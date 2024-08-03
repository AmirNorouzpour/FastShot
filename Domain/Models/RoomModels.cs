using Dapper.Contrib.Extensions;

namespace Domain.Models
{
    public class UserActiveRoomRun
    {
        public long Id { get; set; }
        public Guid PlayerId { get; set; }
        public string Team { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public int Status { get; set; }
    }

    [Table("RoomDefs")]
    public class RoomDefs
    {
        public long Id { get; set; }
        public string CategoryTitle { get; set; }
        public int Category { get; set; }
        public string Title { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Capacity { get; set; }
        public decimal EntryCost { get; set; }
        public string Desc { get; set; }
        public bool Active { get; set; }
        public decimal EntryCostWithOff { get; set; }
    }

    [Table("RoomRuns")]
    public class RoomRuns
    {
        public long Id { get; set; }
        public long RoomDefId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Guid CreatorId { get; set; }
        public int Status { get; set; }
    }

    [Table("RoomRunUsers")]
    public class RoomRunUsers
    {
        public long Id { get; set; }
        public long RoomRunId { get; set; }
        public Guid PlayerId { get; set; }
        public DateTime DateTime { get; set; }
        public string Team { get; set; }
    }

}
