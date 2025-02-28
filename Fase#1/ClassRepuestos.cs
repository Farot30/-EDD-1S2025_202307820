using System;

namespace Repuestos
{
    public unsafe struct Repuesto
    {
        public int Id;
        public fixed char Repuestos[100];
        public fixed char Descripcion[200];
        public double Precio;
        public Repuesto* Next;

        public Repuesto(int id, string repuestos, string descripcion, double precio)
        {
            Id = id;
            Next = null;
            Precio = precio;

            fixed (char* r = Repuestos)
            {
                for (int i = 0; i < 100; i++)
                {
                    r[i] = i < repuestos.Length ? repuestos[i] : '\0';
                }
            }

            fixed (char* d = Descripcion)
            {
                for (int i = 0; i < 200; i++)
                {
                    d[i] = i < descripcion.Length ? descripcion[i] : '\0';
                }
            }
        }

        public override string ToString()
        {
            fixed (char* ps = Repuestos, pd = Descripcion)
            {
                return $"ID: {Id}, Repuestos: {new string(ps)}, Descripcion: {new string(pd)}, Precio: {Precio}";
            }
        }
    }
}