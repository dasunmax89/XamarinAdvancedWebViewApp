﻿using System;
using System.Collections.Generic;
using System.Windows.Input;
using XamarinAdvancedWebViewApp.Xamarin.Core.Models;
using Xamarin.Forms;

namespace XamarinAdvancedWebViewApp.Xamarin.Core.Views
{
    public partial class ShopItemCell : ContentView
    {
        public static readonly BindableProperty SelectedCommandProperty =
        BindableProperty.Create("SelectedCommand",
                         typeof(ICommand),
                         typeof(ShopItemCell),
                         null,
                         BindingMode.OneWay);

        public ICommand SelectedCommand
        {
            get
            {
                return (ICommand)GetValue(SelectedCommandProperty);
            }
            set
            {
                SetValue(SelectedCommandProperty, value);
            }
        }

        public ShopItemCell()
        {
            InitializeComponent();
        }

        private void ExtendedButton_Clicked(System.Object sender, System.EventArgs e)
        {
            var item = BindingContext as GalleryListItemModel;

            SelectedCommand?.Execute(item);
        }
    }
}
