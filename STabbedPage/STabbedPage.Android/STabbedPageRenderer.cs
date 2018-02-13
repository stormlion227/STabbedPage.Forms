using Android.Content;
using Android.Support.Design.Widget;
using Android.Support.V4.View;
using Stormlion.STabbedPage;
using Stormlion.STabbedPage.Droid;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(STabbedPage), typeof(STabbedPageRenderer))]
namespace Stormlion.STabbedPage.Droid
{
    public class STabbedPageRenderer : TabbedPageRenderer
    {
        protected TabLayout TabBar
        {
            get
            {
                for (int i = 0; i < ChildCount; i++)
                {
                    if (GetChildAt(i) is TabLayout)
                    {
                        return GetChildAt(i) as TabLayout;
                    }
                }
                return null;
            }
        }

        protected ViewPager Pager
        {
            get
            {
                for (int i = 0; i < ChildCount; i++)
                {
                    if (GetChildAt(i) is ViewPager)
                    {
                        return GetChildAt(i) as ViewPager;
                    }
                }
                return null;
            }
        }

        protected IVisualElementRenderer _tabBarRenderer;

        public STabbedPageRenderer(Context context) : base(context)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                if(_tabBarRenderer == null)
                {
                    _tabBarRenderer = Xamarin.Forms.Platform.Android.Platform.GetRenderer((Element as STabbedPage).TabBarView);
                    if(_tabBarRenderer == null)
                    {
                        _tabBarRenderer = Xamarin.Forms.Platform.Android.Platform.CreateRendererWithContext((Element as STabbedPage).TabBarView, Context);
                        Xamarin.Forms.Platform.Android.Platform.SetRenderer((Element as STabbedPage).TabBarView, _tabBarRenderer);
                    }

                    AddView(_tabBarRenderer.View);

                    TabBar.Visibility = Android.Views.ViewStates.Gone;

                    ChangedCurrentPage();
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(TabbedPage.CurrentPage))
            {
                ChangedCurrentPage();
            }
        }

        void ChangedCurrentPage()
        {
            if (Element.CurrentPage == null)
                return;
        }

        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            TabBar.Visibility = Android.Views.ViewStates.Gone;

            base.OnLayout(changed, l, t, r, b - (int)((Element as STabbedPage).TabBarHeight * Context.Resources.DisplayMetrics.Density));

            if ((Element as STabbedPage).TabBarPosition == STabbedPage.TabBarPositionType.Top)
            {
                Pager.Layout(0,
                    (int)((Element as STabbedPage).TabBarHeight * Context.Resources.DisplayMetrics.Density),
                    r,
                    b);
                Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(
                    _tabBarRenderer.Element,
                    new Rectangle(
                        0,
                        0,
                        Context.FromPixels(r),
                        (Element as STabbedPage).TabBarHeight));
            }
            else
            {
                Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(
                    _tabBarRenderer.Element,
                    new Rectangle(
                        0,
                        Context.FromPixels(b) - (Element as STabbedPage).TabBarHeight,
                        Context.FromPixels(r),
                        (Element as STabbedPage).TabBarHeight));
            }

            _tabBarRenderer.UpdateLayout();
        }
    }
}