using GrafoAPI.Models;
using GrafoAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafoAPI.Business
{
    public class TripBusiness
    {
        private StationRepository Repository = new StationRepository();

        /// <summary>
        /// Consulta a menor rota entre dois pontos
        /// </summary>
        /// <param name="origin">Estação de origem</param>
        /// <param name="destiny">Estação de destino</param>
        /// <returns></returns>
        public int SmallerRoute(string origin, string destiny)
        {
            return SmallerCrawler(Repository.GetByName(origin), destiny, new List<string>());
        }

        /// <summary>
        /// Crawler responsável por encontrar menor percurso entre dois pontos
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <param name="historic"></param>
        /// <returns></returns>
        private int SmallerCrawler(Station origin, string destiny, List<string> historic)
        {
            var nextStation = origin.NextStation.Where(i => i.Key.Name == destiny).ToDictionary(i => i.Key, i => i.Value);
            if (nextStation.Count != 0)
                return origin.NextStation.Where(i => i.Key.Name == destiny).Select(i => i.Value).FirstOrDefault();
            else if (origin.NextStation.Count == 0)
                return -1; 
            else
            {
                var aux = 0;
                var iteration = 0;

                foreach (var item in origin.NextStation)
                {
                    if (historic.Where(p => p == origin.Name + item.Key.Name).Count()>0)
                        return -1;
                    historic.Add(origin.Name + item.Key.Name);
                    var tripDistance = SmallerCrawler(item.Key, destiny, historic);
                    if (tripDistance != 0)
                    {
                        tripDistance = origin.NextStation.Where(i => i.Key.Name == item.Key.Name).Select(i => i.Value).FirstOrDefault() + tripDistance;
                        if (iteration == 0)
                        {
                            aux = tripDistance;
                            iteration++;
                        }
                        if (tripDistance < aux && tripDistance != -1)
                            aux = tripDistance;
                    }
                    
                }
                return aux;
            }
        }

        /// <summary>
        /// Consulta viagens que possuem a quantidade de paradas igual a especificada
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public int EqualStopsRoutes(string origin, string destiny)
        {
            var stationO = Repository.GetByName(origin);
            var count = 0;
            if (stationO.NextStation.Count > 0)
            {
                foreach (var item in stationO.NextStation)
                {
                    var arrived = MaxStopsCrawler(item.Key, destiny, 0, true, 4);
                    if (arrived > 0)
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Consulta viagens que possuem a quantidade de paradas igual ou menos a especificada
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <returns></returns>
        public int MaxStopsRoutes(string origin, string destiny)
        {
            var stationO = Repository.GetByName(origin);
            var count = 0;
            if (stationO.NextStation.Count > 0)
            {
                foreach (var item in stationO.NextStation)
                {

                    var arrived = MaxStopsCrawler(item.Key, destiny,0,false,3);
                    if (arrived > 0)
                        count++;
                }
            }
            return count;
        }

        /// <summary>
        /// Crawler de busca entre dois pontos 
        /// </summary>
        /// <param name="origin"></param>
        /// <param name="destiny"></param>
        /// <param name="count"></param>
        /// <param name="isEqual"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private int MaxStopsCrawler(Station origin, string destiny, int count, bool isEqual, int max)
        {
            if (!isEqual)
            {
                if (count >= max)
                    return -1;
            } else
            {
                if (count > max)
                    return -1;
            }
            var nextStation = origin.NextStation.Where(i => i.Key.Name == destiny).ToDictionary(i => i.Key, i => i.Value);
            if (nextStation.Count != 0)
                return 1;
            else if (origin.NextStation.Count == 0)
                return -1;
            else
            {
                var arrived = 0;
                foreach (var item in origin.NextStation)
                {
                     arrived =  MaxStopsCrawler(item.Key, destiny, count++, isEqual, max);
                }
                return arrived;
            }
        }
        
        /// <summary>
        /// Consulta a distancia para uma rota informada
        /// </summary>
        /// <param name="route"></param>
        /// <returns></returns>
        public int DistanceRoute(string route)
        {
            return DistanceCrawler(route.Split('-'), 0);
        }

        private int DistanceCrawler(string[] stations, int position)
        {
            if ((position + 1) >= stations.Length) return 0;
            var station = Repository.GetByName(stations[position]);
            var nextStation = station.NextStation.Where(i=>i.Key.Name==stations[position+1]).ToDictionary(i=>i.Key,i=>i.Value);
            if (nextStation.Count == 0)
                throw new Exception("Rota não existe...");

            return DistanceCrawler(stations, position + 1) + nextStation.FirstOrDefault().Value;
        }

    }
}