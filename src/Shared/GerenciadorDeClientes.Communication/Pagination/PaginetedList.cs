﻿namespace GerenciadorDeClientes.Communication.Pagination;

public class PaginetedList
{
	const int maxPageSize = 100;
	public int PageNumber { get; set; } = 1;
	private int _pageSize;
	public int PageSize { get { return _pageSize; } set { _pageSize = (value > maxPageSize) ? maxPageSize : value; } }
}
