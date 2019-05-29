using CoreLocation;
using MapKit;
using MapOverlay;
using MapOverlay.iOS;
using ObjCRuntime;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRenderer))]
namespace MapOverlay.iOS
{
    public class CustomMapRenderer : MapRenderer
    {
        MKPolylineRenderer polylineRenderer;
        CustomMap formsMap;
        MKMapView nativeMap;

        MKPolyline polyline;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                if (nativeMap != null)
                {
                    nativeMap.RemoveOverlays(nativeMap.Overlays);
                    nativeMap.OverlayRenderer = null;
                    polylineRenderer = null;
                }
            }

            if (e.NewElement != null)
            {
                formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MKMapView;
                UpdatePolyLine();
            }
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null || this.Control == null)
                return;

            //if (e.PropertyName == CustomMap.RouteCoordinatesProperty.PropertyName)
            if ((e.PropertyName == "RouteCoordinates" || e.PropertyName == "VisibleRegion"))
            {
                formsMap = (CustomMap)sender;
                nativeMap = Control as MKMapView;
                UpdatePolyLine();
            }

        }


        private void UpdatePolyLine()
        {

            //var nativeMap = Control as MKMapView;

            if (polyline != null)
            {
                nativeMap.RemoveOverlay(polyline);
                polyline.Dispose();
            }

            nativeMap.OverlayRenderer = GetOverlayRenderer;

            CLLocationCoordinate2D[] coords = new CLLocationCoordinate2D[formsMap.RouteCoordinates.Count];

            int index = 0;
            foreach (var position in formsMap.RouteCoordinates)
            {
                coords[index] = new CLLocationCoordinate2D(position.Latitude, position.Longitude);
                index++;
            }

            var routeOverlay = MKPolyline.FromCoordinates(coords);
            nativeMap.AddOverlay(routeOverlay);

            polyline = routeOverlay;
        }


        //[Foundation.Export("mapView:rendererForOverlay:")]
        MKOverlayRenderer GetOverlayRenderer(MKMapView mapView, IMKOverlay overlayWrapper)
        {
            if (polylineRenderer != null)
            {
                polylineRenderer = null;
            }

            var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as IMKOverlay;
            //var overlay = Runtime.GetNSObject(overlayWrapper.Handle) as MKPolyline;

            polylineRenderer = new MKPolylineRenderer(overlay as MKPolyline)
            {
                FillColor = UIColor.Yellow,
                StrokeColor = UIColor.Red,
                LineWidth = 3,
                Alpha = 0.4f
            };

            return polylineRenderer;
        }
    }
}
