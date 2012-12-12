using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serelex.Common;

namespace Serelex.Classes
{
	public class QuerySearchResults : BindableBase
	{
		private ObservableCollection<PictureSearchResult> _result;
		public ObservableCollection<PictureSearchResult> Results
		{
			get { return _result; }
			set { this.SetProperty(ref this._result, value); }
		}

		private string _query;
		public string Query
		{
			get { return _query; }
			set { this.SetProperty(ref this._query, value); }
		}

		private bool _isSuccess;
		public bool IsSuccess
		{
			get { return _isSuccess; }
			set { this.SetProperty(ref this._isSuccess, value); }
		}
	}
}
