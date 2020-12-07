using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Uber_MVVM.ModelView;

namespace Uber_MVVM.View
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : UserControl
    {        
        private DragPin StartPin; 
        private DragPin EndPin;
        private MapLayer RouteLayer;
        private string SessionKey;

        private int pushpinCount = 0;
        private Location startLocation;

        //public LocationCollection locs { get; set; }

        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void StartBtn_Click(object sender, RoutedEventArgs e)
        {
            carPushpin.Visibility = Visibility.Visible;            
        }

        private void PopupCancelBtnClick(object sender, RoutedEventArgs e)
        {
            ProgressPopup.IsOpen = false;
            reviewTB.Text = string.Empty;

            MessageBox.Show("Thanks for using Ibober", "Ibober", MessageBoxButton.OK, MessageBoxImage.Information);
            ResetPage();
        }

        private void PopupSendBtnClick(object sender, RoutedEventArgs e)
        {
            ProgressPopup.IsOpen = false;
            reviewTB.Text = string.Empty;
            ResetPage();

            MessageBox.Show("Thanks for your review...", "Ibober", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void ResetBtnClicked(object sender, RoutedEventArgs e)
        {
            ResetPage();
        }

        private void ResetPage()
        {
            pushpinCount = 0;
            StartBtn.IsEnabled = false;
            priceTB.Content = 0;
            myMap.Children.Clear();
        }

        private void MapWithPushpins_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (pushpinCount == 2) return;
            e.Handled = true;

            Location pinLocation = myMap.ViewportPointToLocation(e.GetPosition(this));
            Pushpin pin = new Pushpin();
            pin.Location = pinLocation;
            if (pushpinCount == 0)
            {
                pushpinCount++;
                startLocation = pinLocation;
                pin.Style = (Style)FindResource("RedPushpin");
            }
            else if (pushpinCount == 1)
            {
                StartBtn.IsEnabled = true;
                pushpinCount++;
                DrawRouteOnMap(pinLocation);
                pin.Style = (Style)FindResource("GreenPushpin");
            }
            myMap.Children.Add(pin);
            myMap.LocationToViewportPoint(pin.Location);
        }

        private void DrawRouteOnMap(Location endLocation)
        {
            myMap.CredentialsProvider.GetCredentials((c) =>
            {
                SessionKey = c.ApplicationId;
                RouteLayer = new MapLayer();
                myMap.Children.Add(RouteLayer);

                StartPin = new DragPin(this.myMap)
                {
                    Location = startLocation
                };
                StartPin.DragEnd += UpdateRoute;

                myMap.Children.Add(StartPin);

                EndPin = new DragPin(this.myMap)
                {
                    Location = new Location(endLocation.Latitude, endLocation.Longitude)
                };

                EndPin.DragEnd += UpdateRoute;

                myMap.Children.Add(EndPin);

                UpdateRoute(null);
            });
        }

        private async void UpdateRoute(Location loc)
        {
            RouteLayer.Children.Clear();

            var startCoord = LocationToCoordinate(StartPin.Location);
            var endCoord = LocationToCoordinate(EndPin.Location);

            var response = await BingMapsRESTToolkit.ServiceManager.GetResponseAsync(new BingMapsRESTToolkit.RouteRequest()
            {
                Waypoints = new List<BingMapsRESTToolkit.SimpleWaypoint>()
                {
                    new BingMapsRESTToolkit.SimpleWaypoint(startCoord),
                    new BingMapsRESTToolkit.SimpleWaypoint(endCoord)
                },
                BingMapsKey = SessionKey,
                RouteOptions = new BingMapsRESTToolkit.RouteOptions()
                {
                    RouteAttributes = new List<BingMapsRESTToolkit.RouteAttributeType>
                    {
                        BingMapsRESTToolkit.RouteAttributeType.RoutePath
                    }
                }
            });

            if (response != null &&
                response.ResourceSets != null &&
                response.ResourceSets.Length > 0 &&
                response.ResourceSets[0].Resources != null &&
                response.ResourceSets[0].Resources.Length > 0)
            {
                var route = response.ResourceSets[0].Resources[0] as BingMapsRESTToolkit.Route;

                RouteLocs.Locs = new LocationCollection();

                for (var i = 0; i < route.RoutePath.Line.Coordinates.Length; i++)
                {
                    RouteLocs.Locs.Add(new Location(route.RoutePath.Line.Coordinates[i][0], route.RoutePath.Line.Coordinates[i][1]));
                }

                priceTB.Content = Convert.ToDouble(RouteLocs.Locs.Count * 0.2);

                var routeLine = new MapPolyline()
                {
                    Locations = RouteLocs.Locs,
                    Stroke = new SolidColorBrush(Colors.Blue),
                    StrokeThickness = 5,

                };

                RouteLayer.Children.Add(routeLine);
            }
        }

        private BingMapsRESTToolkit.Coordinate LocationToCoordinate(Location loc)
        {
            return new BingMapsRESTToolkit.Coordinate(loc.Latitude, loc.Longitude);
        }
    }
}
