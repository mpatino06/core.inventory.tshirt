using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App3.core.tshirt.Views;
using Xamarin.Forms;

namespace App3.core.tshirt
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
			MainPage = new NavigationPage(new MainMenu("USER P01"));
			//MainPage = new NavigationPage(new Login());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
