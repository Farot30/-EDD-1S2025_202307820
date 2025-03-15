namespace Nodos.NodoUsuario
{
    public class NodoUsuario<T>
    {
        public T Data {get; set;}
        public NodoUsuario<T>? despues { get; set; }

        public NodoUsuario(T data)
        {
            Data = data;
            despues = null;
        }
    }
}