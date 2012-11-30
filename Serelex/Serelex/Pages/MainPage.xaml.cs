using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Serelex.Classes;
using Serelex.DataModel;
using SerelexClient;
using SerelexClient.Image;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.ApplicationSettings;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон элемента страницы элементов задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234233

namespace Serelex.Pages
{
	/// <summary>
	/// Страница, на которой отображается коллекция эскизов элементов.  В приложении с разделением эта страница
	/// служит для отображения и выбора одной из доступных групп.
	/// </summary>
	public sealed partial class MainPage : Serelex.Common.LayoutAwarePage
	{
		SerelexClient.Serelex serelex = new SerelexClient.Serelex();
		JpgToPictureProvider pp = new JpgToPictureProvider();

		List<QuerySearchResults> backStack;
		QuerySearchResults prevResults;

		bool isSearchProcess;

		public MainPage()
		{
			this.InitializeComponent();
		}

		/// <summary>
		/// Заполняет страницу содержимым, передаваемым в процессе навигации. Также предоставляется любое сохраненное состояние
		/// при повторном создании страницы из предыдущего сеанса.
		/// </summary>
		/// <param name="navigationParameter">Значение параметра, передаваемое
		/// <see cref="Frame.Navigate(Type, Object)"/> при первоначальном запросе этой страницы.
		/// </param>
		/// <param name="pageState">Словарь состояния, сохраненного данной страницей в ходе предыдущего
		/// сеанса. Это значение будет равно NULL при первом посещении страницы.</param>
		protected override void LoadState(Object navigationParameter, Dictionary<String, Object> pageState)
		{
			this.DefaultViewModel["CanGoBack"] = false;
			this.DefaultViewModel["WordToSearch"] = ExampleSearchSource.RandomSearchExample;

			VisualStateManager.GoToState(this, "Start", true);

			backStack = new List<QuerySearchResults>();
			prevResults = null;
			isSearchProcess = false;
		}
		protected override void SaveState(Dictionary<string, object> pageState)
		{
			//if (backStack.Count != 0)
			//{
			//	pageState["back_stack"] = backStack;
			//}
		}

		protected override void OnNavigatedTo(NavigationEventArgs e)
		{
			SettingsPane.GetForCurrentView().CommandsRequested += MainPage_CommandsRequested;
			base.OnNavigatedTo(e);
		}

		protected override void OnNavigatedFrom(NavigationEventArgs e)
		{
			SettingsPane.GetForCurrentView().CommandsRequested -= MainPage_CommandsRequested;
			base.OnNavigatedFrom(e);
		}

		private async Task<ObservableCollection<PictureSearchResult>> processSearch(string Query)
		{
			SearchResults searchResults = null;
			try
			{
				searchResults = await serelex.Search(Query);
			}
			catch (Exception ex)
			{
				MessageDialog md = new MessageDialog(ex.Message);
				md.ShowAsync();
				return null;
			}

			int i = 1;

			ObservableCollection<PictureSearchResult> results = new ObservableCollection<PictureSearchResult>();
			foreach (var r in searchResults.Relations)
			{
				PictureSearchResult pr = new PictureSearchResult();
				pr.Index = i++;
				pr.SearchResult = r;
				LoadResultImage(pr);
				results.Add(pr);
			}
			return results;
		}
		private async Task startSearch(string Query, bool AddToStack = true)
		{
			if (isSearchProcess)
				return;

			VisualStateManager.GoToState(this, "Search", true);
			isSearchProcess = true;

			if (prevResults != null)
				if (prevResults.Query == Query)
					return;

			// Add query to back stack
			if (AddToStack && prevResults != null)
			{
				backStack.Add(prevResults);
				this.DefaultViewModel["CanGoBack"] = true;
			}
	
			this.DefaultViewModel["Items"] = null;

			ObservableCollection<PictureSearchResult> results = await processSearch(Query);

			VisualStateManager.GoToState(this, "Normal", true);
			this.DefaultViewModel["Items"] = results;

			prevResults = new QuerySearchResults();
			prevResults.Query = Query;
			prevResults.Results = results;

			isSearchProcess = false;
		}

		private async void LoadResultImage(PictureSearchResult Result)
		{
			Result.Image = await pp.GetPictureFromQuery(Result.SearchResult.Word);
		}

		protected override void GoBack(object sender, RoutedEventArgs e)
		{
			int lastIndex = backStack.Count - 1;
			QuerySearchResults result = backStack[lastIndex];
			backStack.RemoveAt(lastIndex);

			if (backStack.Count == 0)
				this.DefaultViewModel["CanGoBack"] = false;

			this.DefaultViewModel["Items"] = result.Results;
			this.DefaultViewModel["WordToSearch"] = result.Query;
		}

		private async void btnStartSearch_Click_1(object sender, RoutedEventArgs e)
		{
			string query = (string)this.DefaultViewModel["WordToSearch"];
			await startSearch(query);
		}

		private async void tbSearch_KeyDown(object sender, KeyRoutedEventArgs e)
		{
			if (e.Key == Windows.System.VirtualKey.Enter && e.KeyStatus.RepeatCount == 0)
			{
				//string query = (string)this.DefaultViewModel["WordToSearch"];
				string query = tbSearch.Text;
				if (!String.IsNullOrWhiteSpace(query))
					await startSearch(query);

				e.Handled = true;
			}
		}

		private async void itemSearch_ItemClick_1(object sender, ItemClickEventArgs e)
		{
			PictureSearchResult r = e.ClickedItem as PictureSearchResult;

			string newQuery = r.SearchResult.Word;
			this.DefaultViewModel["WordToSearch"] = newQuery;

			await startSearch(newQuery);
		}

		void MainPage_CommandsRequested(SettingsPane sender, SettingsPaneCommandsRequestedEventArgs args)
		{
			SettingsCommand openPrivacyPolicyCommand = new SettingsCommand(1, Serelex.Resources.Strings.ViewPrivacyPolicy, async cmd =>
			{
				await Launcher.LaunchUriAsync(new Uri("http://mem.azurewebsites.net/Privacy policy.htm", UriKind.Absolute));
			});

			args.Request.ApplicationCommands.Add(openPrivacyPolicyCommand);
		}

	}
}
