using Stormlion.STabbedPage;
using Stormlion.STabbedPage.iOS;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(STabbedPage), typeof(STabbedPageRenderer))]
namespace Stormlion.STabbedPage.iOS
{
    public class STabbedPageRenderer : TabbedRenderer
    {
        IVisualElementRenderer _tabBarRenderer;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if(e.NewElement != null)
            {
                if(_tabBarRenderer == null)
                {
                    _tabBarRenderer = Xamarin.Forms.Platform.iOS.Platform.GetRenderer((Element as STabbedPage).TabBarView);
                    if(_tabBarRenderer == null)
                    {
                        _tabBarRenderer = Xamarin.Forms.Platform.iOS.Platform.CreateRenderer((Element as STabbedPage).TabBarView);
                        Xamarin.Forms.Platform.iOS.Platform.SetRenderer((Element as STabbedPage).TabBarView, _tabBarRenderer);
                    }
                    View.AddSubview(_tabBarRenderer.NativeView);
                    View.SetNeedsLayout();

                    Element.PropertyChanged += HandlePropertyChanged;
                }
            }
        }

        public override void ViewDidLayoutSubviews()
        {
            TabBar.Hidden = true;
            TabBar.Frame = new System.Drawing.RectangleF(0, 0, 0, 0);
            base.ViewDidLayoutSubviews();

            if (Element == null)
                return;

            if ((Element as STabbedPage).TabBarPosition == STabbedPage.TabBarPositionType.Top)
            {
                (Element as Page).ContainerArea = new Rectangle(
                    0,
                    (Element as STabbedPage).TabBarHeight,
                    View.Frame.Width,
                    View.Frame.Height - (Element as STabbedPage).TabBarHeight
                    );

                _tabBarRenderer.NativeView.Frame = new CoreGraphics.CGRect(
                    Element.X,
                    Element.Y,
                    Element.Width,
                    (Element as STabbedPage).TabBarHeight
                    );

                Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(
                    _tabBarRenderer.Element,
                    new Rectangle(
                        Element.X,
                        Element.Y,
                        Element.Width,
                        (Element as STabbedPage).TabBarHeight));
            }
            else
            {
                (Element as Page).ContainerArea = new Rectangle(
                    0,
                    0,
                    View.Frame.Width,
                    View.Frame.Height - (Element as STabbedPage).TabBarHeight
                    );

                _tabBarRenderer.NativeView.Frame = new CoreGraphics.CGRect(
                    Element.X,
                    Element.Height - (Element as STabbedPage).TabBarHeight,
                    Element.Width,
                    (Element as STabbedPage).TabBarHeight);

                Xamarin.Forms.Layout.LayoutChildIntoBoundingRegion(
                    _tabBarRenderer.Element,
                    new Rectangle(
                        Element.X,
                        Element.Height - (Element as STabbedPage).TabBarHeight,
                        Element.Width,
                        (Element as STabbedPage).TabBarHeight));
            }
        }

        void HandlePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == NavigationPage.CurrentPageProperty.PropertyName)
            {
                View.SetNeedsLayout();
            }
        }
    }
}
