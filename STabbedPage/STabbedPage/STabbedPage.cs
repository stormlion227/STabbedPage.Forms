using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Stormlion.STabbedPage
{
    public class STabbedPage : TabbedPage
    {
        public static readonly BindableProperty TabCellProperty = BindableProperty.CreateAttached(
            "TabCell",
            typeof(View),
            typeof(Page),
            null
            );
        public static View GetTabCell(BindableObject page) => (View)page.GetValue(TabCellProperty);
        public static void SetTabCell(BindableObject page, View view) => page.SetValue(TabCellProperty, view);

        public static readonly BindableProperty SplitterColorProperty = BindableProperty.Create(
            nameof(SplitterColor),
            typeof(Color),
            typeof(STabbedPage),
            Color.LightGray
            );
        public Color SplitterColor { get => (Color)GetValue(SplitterColorProperty); set => SetValue(SplitterColorProperty, value); }

        public static readonly BindableProperty SplitterWidthProperty = BindableProperty.Create(
            nameof(SplitterWidth),
            typeof(double),
            typeof(STabbedPage),
            1
            );
        public double SplitterWidth { get => (double)GetValue(SplitterWidthProperty); set => SetValue(SplitterWidthProperty, value); }

        public static readonly BindableProperty TopBarColorProperty = BindableProperty.Create(
            nameof(TopBarColor),
            typeof(Color),
            typeof(STabbedPage),
            Color.LightGray
            );
        public Color TopBarColor { get => (Color)GetValue(TopBarColorProperty); set => SetValue(TopBarColorProperty, value); }

        public static readonly BindableProperty TopBarHeightProperty = BindableProperty.Create(
            nameof(TopBarHeight),
            typeof(double),
            typeof(STabbedPage),
            1
            );
        public double TopBarHeight { get => (double)GetValue(TopBarHeightProperty); set => SetValue(TopBarHeightProperty, value); }

        public static readonly BindableProperty BottomBarColorProperty = BindableProperty.Create(
            nameof(BottomBarColor),
            typeof(Color),
            typeof(STabbedPage),
            Color.LightGray
            );
        public Color BottomBarColor { get => (Color)GetValue(BottomBarColorProperty); set => SetValue(BottomBarColorProperty, value); }

        public static readonly BindableProperty BottomBarHeightProperty = BindableProperty.Create(
            nameof(BottomBarHeight),
            typeof(double),
            typeof(STabbedPage),
            1
            );
        public double BottomBarHeight { get => (double)GetValue(BottomBarHeightProperty); set => SetValue(BottomBarHeightProperty, value); }

        public static readonly BindableProperty TabBarHeightProperty = BindableProperty.Create(
            nameof(TabBarHeight),
            typeof(double),
            typeof(STabbedPage),
            70
            );
        public double TabBarHeight { get => (double)GetValue(TabBarHeightProperty); set => SetValue(TabBarHeightProperty, value); }

        protected Grid _tabBarView = null;

        protected void createTabBar()
        {
            _tabBarView = new Grid
            {
                HeightRequest = TabBarHeight,
                RowSpacing = 0
            };

            _tabBarView.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(TopBarHeight, GridUnitType.Absolute)
            });
            _tabBarView.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(1, GridUnitType.Star)
            });
            _tabBarView.RowDefinitions.Add(new RowDefinition
            {
                Height = new GridLength(BottomBarHeight, GridUnitType.Absolute)
            });

            _tabBarView.Children.Add(new BoxView
            {
                BackgroundColor = TopBarColor
            }, 0, 0);

            _tabBarView.Children.Add(new BoxView
            {
                BackgroundColor = BottomBarColor
            }, 0, 2);

            Grid gridTabs = new Grid()
            {
                ColumnSpacing = 0
            };
            int i = 0;
            foreach(Page page in Children)
            {
                gridTabs.ColumnDefinitions.Add(new ColumnDefinition
                {
                    Width = new GridLength(1, GridUnitType.Star)
                });
                gridTabs.Children.Add(GetTabCell(page), i, 0);
                i++;
            }
        }

        public Grid TabBarView
        {
            get
            {
                if (_tabBarView == null)
                    createTabBar();
                return _tabBarView;
            }
        }


    }
}
