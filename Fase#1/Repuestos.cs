namespace Repuestos
{
    public class Repuesto
    {
        public int Id { get; set; }
        public string NRepuesto { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public Repuesto(int id, string repuesto, string descripcion, double precio)
        {
            Id = id;
            NRepuesto = repuesto;
            Descripcion = descripcion;
            Precio = precio;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre Repuesto: {NRepuesto}, Descripcion: {Descripcion}, Precio: {Precio}";
        }
    }
}