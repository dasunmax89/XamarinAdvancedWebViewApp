using System;
using System.Collections.Generic;
using XamarinAdvancedWebViewApp.Xamarin.Core.ViewModels.Base;
using XamarinAdvancedWebViewApp.Xamarin.Core.Views.Base;
using Xamarin.Forms;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Views
{
    public partial class AboutAppView : BaseContentPage
    {
        public AboutAppView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            var vm = BindingContext as ViewModelBase;

            if (vm != null)
            {
                vm.UnSubscribeToEvents();
                vm.SubscribeToEvents();
                await vm.OnAppearing();
            }
        }

        protected override void OnDisappearing()
        {
            var vm = BindingContext as ViewModelBase;

            vm?.UnSubscribeToEvents();

            vm?.OnDisappearing();
        }
    }
}
