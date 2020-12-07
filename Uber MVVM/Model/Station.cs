using Microsoft.Maps.MapControl.WPF;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace Uber_MVVM.Model
{
    interface IWorkWithJson
    {
        void WriteToJson();
    }
    public class Station : IWorkWithJson
    {
        public string Name { get; set; }
        public Location Location { get; set; }
        public ImageSource ImageSource { get; set; }

        public Station()
        {
            if (!File.Exists("Stations.json"))
                File.Create("Stations.json");
        }
        public static ICollection<Station> ReadFromJson()
        {
            var str = File.ReadAllText("Stations.json");
            return JsonConvert.DeserializeObject<ICollection<Station>>(str);
        }

        public void WriteToJson()
        {
            List<Station> stations = null;
            var str = File.ReadAllText("Stations.json");
            stations = JsonConvert.DeserializeObject<List<Station>>(str);
            if (stations != null)
            {
                stations.Add(this);
            }
            else
            {
                stations = new List<Station>();
                stations.Add(this);
            }

            var ser = JsonConvert.SerializeObject(stations, Formatting.Indented);
            File.WriteAllText("Stations.json", ser);
        }
    }
}
