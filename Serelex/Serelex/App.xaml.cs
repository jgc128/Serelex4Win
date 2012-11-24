using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Serelex.Common;
using Serelex.Pages;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// Шаблон пустого приложения задокументирован по адресу http://go.microsoft.com/fwlink/?LinkId=234227

namespace Serelex
{
	/// <summary>
	/// Обеспечивает зависящее от конкретного приложения поведение, дополняющее класс Application по умолчанию.
	/// </summary>
	sealed partial class App : Application
	{
		/// <summary>
		/// Initializes the singleton Application object.  This is the first line of authored code
		/// кода; поэтому она является логическим эквивалентом main() или WinMain().
		/// </summary>
		public App()
		{
			this.InitializeComponent();
			this.Suspending += OnSuspending;
		}

		/// <summary>
		/// Вызывается при обычном запуске приложения пользователем.  Будут использоваться другие точки входа,
		/// если приложение запускается для открытия конкретного файла, отображения
		/// результатов поиска и т. д.
		/// </summary>
		/// <param name="args">Сведения о запросе и обработке запуска.</param>
		protected override async void OnLaunched(LaunchActivatedEventArgs args)
		{
			// Do not repeat app initialization when already running, just ensure that
			// the window is active
			if (args.PreviousExecutionState == ApplicationExecutionState.Running)
			{
				Window.Current.Activate();
				return;
			}

			// Create a Frame to act as the navigation context and associate it with
			// a SuspensionManager key
			var rootFrame = new Frame();
			SuspensionManager.RegisterFrame(rootFrame, "AppFrame");

			if (args.PreviousExecutionState == ApplicationExecutionState.Terminated)
			{
				// Restore the saved session state only when appropriate
				await SuspensionManager.RestoreAsync();
			}

			if (rootFrame.Content == null)
			{
				// When the navigation stack isn't restored navigate to the first page,
				// настройка новой страницы путем передачи необходимой информации в качестве параметра
				// навигации
				if (!rootFrame.Navigate(typeof(MainPage), "AllGroups"))
				{
					throw new Exception("Failed to create initial page");
				}
			}

			// Размещение фрейма в текущем окне и проверка того, что он активен
			Window.Current.Content = rootFrame;
			Window.Current.Activate();
		}

		/// <summary>
		/// Вызывается при приостановке выполнения приложения. Состояние приложения сохраняется
		/// без учета информации о том, будет ли оно завершено или возобновлено с неизменным
		/// содержимым памяти.
		/// </summary>
		/// <param name="sender">Источник запроса приостановки.</param>
		/// <param name="e">Сведения о запросе приостановки.</param>
		private async void OnSuspending(object sender, SuspendingEventArgs e)
		{
			var deferral = e.SuspendingOperation.GetDeferral();
			await SuspensionManager.SaveAsync();
			deferral.Complete();
		}
	}
}
