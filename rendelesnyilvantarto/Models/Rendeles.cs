using System;

namespace rendelesnyilvantarto.Models
{
    public class Rendeles
    {
        public int Id { get; set; }
        public string? Etel { get; set; }
        public string? Leiras { get; set; }
        public DateTime RendelesIdopont { get; set; }
        public DateTime FrissitesIdopont { get; set; }
        public int VendegId { get; set; }
    }
}
