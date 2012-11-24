using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerelexClient.Image
{
	public interface IPictureProvider
	{
		Task<Uri> GetPictureFromQuery(string Query);
	}
}
