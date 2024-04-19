using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CiscoLab.EntityLayer
{
    public class Reservacion
    {
        public int ID { get; set; }
        public string Hora { get; set; }
        public string Fecha { get; set; }
        public string NombreCompleto { get; set; }
        public string Username{ get; set; }
    }
}
