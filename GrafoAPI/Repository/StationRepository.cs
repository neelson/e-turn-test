using GrafoAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafoAPI.Repository
{
    /// <summary>
    /// Classe criada para simular um acesso a um repositorio de estações 
    /// </summary>
    public class StationRepository
    {
        private List<Station> Stations { get; set; }

        public StationRepository()
        {
            CreateStations();
        }

        public List<Station> GetAll()
        {
            return Stations;
        }

        public Station GetByName(string name)
        {
            return Stations.Where(p => p.Name == name).FirstOrDefault();
        }

        private void CreateStations()
        {
            var stationA = new Station("A");
            var stationB = new Station("B");
            var stationC = new Station("C");
            var stationD = new Station("D");
            var stationE = new Station("E");

            stationA.NextStation.Add(stationB, 5);
            stationA.NextStation.Add(stationD, 5);
            stationA.NextStation.Add(stationE, 7);
            
            stationB.NextStation.Add(stationC, 4);

            stationC.NextStation.Add(stationD, 8);
            stationC.NextStation.Add(stationE, 2);
            
            stationD.NextStation.Add(stationE, 6);
            stationD.NextStation.Add(stationC, 8);

            stationE.NextStation.Add(stationB, 3);

            Stations = new List<Station>();
            Stations.Add(stationA);
            Stations.Add(stationB);
            Stations.Add(stationC);
            Stations.Add(stationD);
            Stations.Add(stationE);
        }
        
    }
}