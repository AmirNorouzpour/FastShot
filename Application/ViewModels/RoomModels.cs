
namespace Application.ViewModels
{
  public class RoomsListModel
    {
        public long RoomDefId { get; set; }
        public long RoomRunId { get; set; }
        public DateTime StartTime { get; set; }
        public int Status { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle{ get; set; }
        public string Text{ get; set; }
        public string Title{ get; set; }
        public int RemianCapacity{ get; set; }
        public decimal EntryCostWithOff { get; set; }
        public decimal EntryCost { get; set; }
    }
}
