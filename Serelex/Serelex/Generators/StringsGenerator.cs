using Windows.ApplicationModel.Resources;

namespace Serelex.Resources
{
    public static class Strings 
    {
        private static readonly ResourceLoader Loader = new ResourceLoader();

		public static string ApplicationTitleText { get { return Loader.GetString("ApplicationTitle/Text"); } } //Serelex
		public static string ViewPrivacyPolicy { get { return Loader.GetString("ViewPrivacyPolicy"); } } //View privacy policy
	}
}