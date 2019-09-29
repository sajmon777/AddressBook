using System;
using System.Collections.Generic;
using System.Text;
using AddressBook.Business.Model;

namespace AddressBook.Business.Infrastructure
{
	public class GridInfo<T>
	{
		public int Skip
		{
			get
			{
				return (Page - 1) * PageSize;
			}
		}
		public int Page { get; set; } = 1;
		public int PageSize { get; set; } = 10;
		public T Filter { get; set; }
	}

	public class GridResult<T>
	{
		public List<T> Data { get; set; }
		public int RowCount { get; set; }


		public GridResult(List<T> Data, int RowCount)
		{
			this.Data = Data;
			this.RowCount = RowCount;
		}
	}
}