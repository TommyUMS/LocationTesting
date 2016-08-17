using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FreshMvvm;
using TestingLocation.Helpers;
using TestingLocation.Interfaces;
using TestingLocation.Services;
using TestingLocation.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Acr.UserDialogs;
using TestingLocation.Views;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace TestingLocation
{
    public class App : Application
    {
        public App()
        {
            // The root page of your application
            FreshPageModelResolver.PageModelMapper = new FreshPageModelMapperMod();
            FreshIOC.Container.Register<ILocationDataService, LocationDataService>();
            FreshIOC.Container.Register(UserDialogs.Instance);
            FreshIOC.Container.Register<IMissionServerService, MissionServerService>();
            //FreshIOC.Container.Resolve<ILog>();
            
            
            
            var page = FreshPageModelResolver.ResolvePageModel<LocationViewModel>();
            var basicNavContainer = new FreshNavigationContainer(page);
            MainPage = basicNavContainer;
            
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
