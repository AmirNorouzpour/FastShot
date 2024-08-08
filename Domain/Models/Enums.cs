﻿namespace Domain.Models
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
}
