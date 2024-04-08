using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Paging;

public abstract class BasePageableModel
{
	public int Size { get; set; }
	public int Index { get; set; }
	public int Count { get; set; }
	public int Pages { get; set; }
	public bool HasPreviousPage { get; set; }
	public bool HasNextPage { get; set; }
}
