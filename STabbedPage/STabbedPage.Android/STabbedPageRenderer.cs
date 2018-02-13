using Android.Content;
using Stormlion.STabbedPage;
using Stormlion.STabbedPage.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(STabbedPage), typeof(STabbedPageRenderer))]
namespace Stormlion.STabbedPage.Droid
{
    public class STabbedPageRenderer : TabbedPageRenderer
    {
        public STabbedPageRenderer(Context context) : base(context)
        {

        }
    }
}