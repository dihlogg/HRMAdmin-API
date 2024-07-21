﻿namespace AdminHRM.Dtos;
public class PagedResult<T>
{
    public List<T> Items { get; set; } = new List<T>();
    public int TotalCount { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
    public string[]? SortFields { get; set; }
    public string[]? SortOrders { get; set; }
}