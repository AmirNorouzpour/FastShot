using Dapper.Contrib.Extensions;

namespace Domain.Models
{
    public class UserActiveRoomRun
    {
        public long Id { get; set; }
        public Guid UserId { get; set; }
        public string Team { get; set; }
        public string Title { get; set; }
        public DateTime StartTime { get; set; }
        public int Status { get; set; }
    }

    [Table("RoomDefs")]
    public class RoomDef
    {
        public long Id { get; set; }
        public string CategoryTitle { get; set; }
        public int Category { get; set; }
        public string Title { get; set; }
        public DateTime CreateDateTime { get; set; }
        public int Capacity { get; set; }
        public int TeamsUsersCount { get; set; }
        public decimal EntryCost { get; set; }
        public string Desc { get; set; }
        public bool Active { get; set; }
        public decimal EntryCostWithOff { get; set; }
    }

    [Table("RoomRuns")]
    public class RoomRun
    {
        public long Id { get; set; }
        public long RoomDefId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime UpdateDateTime { get; set; }
        public Guid CreatorId { get; set; }
        public int Status { get; set; }
        public decimal EntryCost { get; set; }
        public decimal EntryCostWithOff { get; set; }
    }

    [Table("RoomRunUsers")]
    public class RoomRunUser
    {
        public long Id { get; set; }
        public long RoomRunId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string Team { get; set; }
    }

    [Table("RoomRunResults")]
    public class RoomRunResult
    {
        public long Id { get; set; }
        public long RoomRunId { get; set; }
        public Guid UserId { get; set; }
        public string UserName { get; set; }
        public DateTime DateTime { get; set; }
        public Guid CreatorId { get; set; }
        public decimal Amount { get; set; }
        public int Rank { get; set; }
        public bool IsWinner { get; set; }
    }

    [Table("RoomRunWinnerTemplates")]
    public class RoomRunWinnerTemplate
    {
        public long Id { get; set; }
        public long RoomRunId { get; set; }
        public Guid UserId { get; set; }
        public DateTime DateTime { get; set; }
        public string Amounts { get; set; } = "1:400;2:300;3:200";
    }

    public class RoomRunFlat
    {
        public long Id { get; set; }
        public DateTime StartTime { get; set; }
        public int Status { get; set; }
        public int Category { get; set; }
        public string CategoryTitle { get; set; }
        public int Capacity { get; set; }
        public decimal EntryCostWithOff { get; set; }
        public decimal EntryCost { get; set; }
        public int UsersCount { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public List<UserTeamModel> Users { get; set; } = new List<UserTeamModel>();
        public int RemianCapacity { get { return Capacity - UsersCount; } }
    }
     
    public class UserTeamModel
    {
        public Guid UserId { get; set; }
        public string Team { get; set; }
    }

    public class RoomRunGropped
    {
        public string Key { get; set; }
        public List<RoomRunFlat> Items { get; set; }
    }

    public class LeadersBoardResult
    {
        public Guid UserId { get; set; }
        public List<LeadersBoardResult> LeadersBoard { get; set; }
        public string UserName { get; set; }
        public int Count { get; set; }
        public int Rank { get; set; }
    }

}
