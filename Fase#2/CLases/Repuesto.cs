namespace CLases.Repuesto
{
    public struct Repuesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }

        public Repuesto(int id, string nombre, string descripcion, double precio)
        {
            Id = id;
            Nombre = nombre;
            Descripcion = descripcion;
            Precio = precio;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Nombre: {Nombre}, Descripcion: {Descripcion}, Precio: {Precio}";
        }
    }
    
}