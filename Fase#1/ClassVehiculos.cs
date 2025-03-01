using System;

namespace Vehiculos
{
    public unsafe struct Node
    {
        public int Id;
        public int IdUsuario;
        public fixed char Marca[50];
        public int Modelo;
        public fixed char Placa[50];
        public Node* Next;
        public Node* Prev;

        public unsafe Node(int id, int idUsuario, string marca, int modelo, string placa)
        {
            Id = id;
            IdUsuario = idUsuario;
            Modelo = modelo;
            Next = null;
            Prev = null;

            fixed (char* m = Marca)
            {
                for (int i = 0; i < marca.Length && i < 50 - 1; i++)
                    m[i] = marca[i];
                m[marca.Length] = '\0';
            }

            fixed (char* p = Placa)
            {
                for (int i = 0; i < placa.Length && i < 50 - 1; i++)
                    p[i] = placa[i];
                p[placa.Length] = '\0';
            }
        }

        // Método ToString para mostrar los datos del nodo
        public override string ToString()
        {
            fixed (char* m = Marca, p = Placa)
            {
                return $"ID: {Id}, ID Usuario: {IdUsuario}, Marca: {new string(m)}, Modelo: {Modelo}, Placa: {new string(p)}";
            }
        }
    }
}