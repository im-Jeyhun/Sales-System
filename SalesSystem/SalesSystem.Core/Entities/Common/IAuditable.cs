﻿namespace SalesSystem.Core.DataBase.Models.Common;

public interface IAuditable
{
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}