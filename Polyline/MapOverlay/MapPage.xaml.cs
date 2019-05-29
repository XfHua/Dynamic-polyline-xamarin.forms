using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace MapOverlay
{
	public partial class MapPage : ContentPage
	{
		public MapPage ()
		{
			InitializeComponent ();

			customMap.RouteCoordinates.Add (new Position (37.797534, -122.401827));
			customMap.RouteCoordinates.Add (new Position (37.797510, -122.402060));
			customMap.RouteCoordinates.Add (new Position (37.790269, -122.400589));
			customMap.RouteCoordinates.Add (new Position (37.790265, -122.400474));
			customMap.RouteCoordinates.Add (new Position (37.790228, -122.400391));
			customMap.RouteCoordinates.Add (new Position (37.790126, -122.400360));
			customMap.RouteCoordinates.Add (new Position (37.789250, -122.401451));
    

        customMap.MoveToRegion (MapSpan.FromCenterAndRadius (new Position (37.79752, -122.40183), Distance.FromMiles (1.0)));


            double a = 37.789250;

            double b = -122.401451;

            Device.StartTimer(TimeSpan.FromSeconds(3), () =>
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                          a -= 0.000321;
                          b += 0.000222;
                          customMap.RouteCoordinates = new List<Position>
                          {
                              new Position (37.797534, -122.401827),
                              new Position(37.797510, -122.402060),
                              new Position(37.790269, -122.400589),
                              new Position(37.790265, -122.400474),
                              new Position(37.790228, -122.400391),
                              new Position(37.790126, -122.400360),
                              new Position(37.789250, -122.401451),
                              new Position(a, b)
                          };
                });

            return true;
            });
        }
	}
}
