namespace Domain.Models
{
    public enum FinanceType
    {
        Variz = 0,
        Bardasht = 1,
        SherkatDarRoom = 2,
        VarizJayezeh = 3
    }
    public enum FinanceSide
    {
        Up = 0,
        Down = 1
    }

    public enum FinanceStatus
    {
        Registered = 0,
        Done = 1,
        SentToBank = 2,
        Canceled = 3,
    }

    public enum AuthorizeType
    {
        Level1 = 0,
        Level2 = 1
    }

    public enum TicketStatus
    {
        Register = 0,
        Awnsered = 1,
        UserAwnserd = 2,
        Closed = 3
    }

    public enum MsgType
    {
        Warning = 0,
        Info = 1,
        Critical = 2
    }
}
