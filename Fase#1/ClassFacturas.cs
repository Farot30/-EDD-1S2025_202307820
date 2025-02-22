namespace Facturas
{
    public unsafe struct Facturas
    {
        public int ID;
        public int ID_Orden; 
        public double Total;
        public Facturas* Siguiente;

    }
}