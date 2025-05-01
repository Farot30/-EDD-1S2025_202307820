namespace Nodos.NofoHUffman
{
    [Serializable]
    public class NodoHuffman : IComparable<NodoHuffman>
    {
        public char Character { get; set; }
        public int Frequency { get; set; }
        public NodoHuffman Left { get; set; }
        public NodoHuffman Right { get; set; }

        public int CompareTo(NodoHuffman other)
        {
            return Frequency.CompareTo(other.Frequency);
        }
    }
}