using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
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