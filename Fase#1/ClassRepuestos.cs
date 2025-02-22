using System;

namespace Repuestos
{
    public unsafe struct Repuesto
    {
        public int Id;
        public fixed char Repuestos[20];
        public fixed char Descripcion[20];
        public double Precio;
        public Repuesto* Next;

        public Repuesto(int id, string repuestos, string descripcion, double precio)
        {
            Id = id;
            Next = null;
            Precio = precio;

            fixed(char* r = Repuestos)
            {
                repuestos.AsSpan().CopyTo(new Span<char>(r, 20));
            }

            fixed(char* d = Descripcion)
            {
                descripcion.AsSpan().CopyTo(new Span<char>(d, 20));
            }
        }

        public override string ToString()
        {
            fixed(char* ps = Repuestos, pd = Descripcion)
            {
                return $"ID: {Id}, Repuestos: {new string(ps)}, Descripcion: {new string(pd)}, Precio: {Precio}";
            }
        }
    }
}