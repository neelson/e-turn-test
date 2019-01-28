using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GrafoAPI.Models
{
    public class Station
    {
        /// <summary>
        /// Nome da estação
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Dictionary (Próxima estação/Distancia)
        /// </summary>
        public Dictionary<Station,int> NextStation { get; set; }

        public Station(string name)
        {
            this.Name = name;
            this.NextStation = new Dictionary<Station, int>(); 
        }
    }
}