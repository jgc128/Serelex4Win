using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serelex.Common;
using SerelexClient;

namespace Serelex.Classes
{
	public class PictureSearchResult : BindableBase
	{
		private int _index;
		public int Index
		{
			get { return _index; }
			set { this.SetProperty(ref this._index, value); }
		}

		private SearchResultItem _searchResult;
		public SearchResultItem SearchResult
		{
			get { return _searchResult; }
			set { this.SetProperty(ref this._searchResult, value); }
		}

		private Uri _image;
		public Uri Image
		{
			get { return _image; }
			set { this.SetProperty(ref this._image, value); }
		}
	}
}
