using System;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace ElimishContacts.Tests
{
	public class AppInitializer
	{
		public static IApp StartApp(Platform platform)
		{
			if (platform == Platform.Android)
			{
                return ConfigureApp.Android.InstalledApp(@"com.companyname").StartApp(Xamarin.UITest.Configuration.AppDataMode.DoNotClear);
			}

			return ConfigureApp.iOS.StartApp();
		}
	}
}