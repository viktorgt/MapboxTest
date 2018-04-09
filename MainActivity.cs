using System.Collections.Generic;
using Android.App;
using Android.Locations;
using Android.Widget;
using Android.OS;
using Android.Support.V7.App;
using Com.Mapbox.Geojson;
using Com.Mapbox.Mapboxsdk;
using Com.Mapbox.Services.Android.Navigation.UI.V5;
using Com.Mapbox.Services.Android.Navigation.UI.V5.Listeners;
using Com.Mapbox.Services.Android.Navigation.V5.Routeprogress;

namespace MapBoxTest
{
    [Activity(Label = "MapBoxTest", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : AppCompatActivity, IOnNavigationReadyCallback, INavigationListener, IProgressChangeListener
    {
        int count = 1;

        private List<Point> points = new List<Point>();

        private Com.Mapbox.Services.Android.Navigation.UI.V5.NavigationView navigationView;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            Mapbox.GetInstance(ApplicationContext,
                "YOURKEY");
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            //// Get our button from the layout resource,
            //// and attach an event to it
            //Button button = FindViewById<Button>(Resource.Id.myButton);

            //button.Click += delegate { button.Text = string.Format("{0} clicks!", count++); };
            points.Add(Point.FromLngLat(8.1032309417335, 51.57621496387662));
            points.Add(Point.FromLngLat(8.1045, 51.574539));
            points.Add(Point.FromLngLat(8.096010749073798, 51.571959309858755));
            SetContentView(Resource.Layout.activity_navigation);
            navigationView = FindViewById<NavigationView>(Resource.Id.navigationView);
            navigationView.OnCreate(savedInstanceState);
            navigationView.GetNavigationAsync(this);
        }

        public void OnNavigationReady()
        {
            var firstPoint = points[0];
            points.RemoveAt(0);
            navigationView.StartNavigation(SetupOptions(firstPoint));
        }

        private NavigationViewOptions SetupOptions(Point origin)
        {

            NavigationViewOptions.Builder options = NavigationViewOptions.InvokeBuilder();
            options.NavigationListener(this);
            options.ProgressChangeListener(this);
            options.Origin(origin);

            var firstPoint = points[0];
            points.RemoveAt(0);
            options.Destination(firstPoint);
            options.ShouldSimulateRoute(true);
            return options.Build();

        }

        public void OnCancelNavigation()
        {
        }

        public void OnNavigationFinished()
        {
        }

        public void OnNavigationRunning()
        {
        }

        public void OnProgressChange(Location p0, RouteProgress p1)
        {
        }
    }
}

