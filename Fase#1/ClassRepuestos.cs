namespace Repuestos
{
    public unsafe struct Repuesto
    {
        public int Id { get; set; }
        public char NRepuesto { get; set; }
        public char Descripcion { get; set; }
        public double Precio { get; set; }

        public Repuesto(int id, char repuesto, char descripcion, double precio)
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