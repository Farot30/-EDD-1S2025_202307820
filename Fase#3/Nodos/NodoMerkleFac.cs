using System;
using Clases.Facturas;
using System.Security.Cryptography;
using System.Text;

namespace Nodos.NodoFactura
{
    public class NodoFacturas
    {
        public string Hash { get; set; }         // Hash del nodo 
        public NodoFacturas? Left { get; set; }     // Hijo izquierdo
        public NodoFacturas? Right { get; set; }    // Hijo derecho
        public Factura? Factura { get; set; }     // Factura asociada 

        // Constructor para nodos hoja
        public NodoFacturas(Factura factura)
        {
            Factura = factura;
            Hash = factura.GetHash();
            Left = null;
            Right = null;
        }

        // Constructor para nodos internos (combinación de hijos)
        public NodoFacturas(NodoFacturas left, NodoFacturas right)
        {
            Factura = null;
            Left = left;
            Right = right;
            Hash = CalculateHash(left.Hash, right.Hash); // Si right es null, usa solo left
        }

        // Método para calcular el hash combinado de dos nodos
        private string CalculateHash(string leftHash, string rightHash)
        {
            string combined = leftHash + (rightHash ?? leftHash); // Si no hay right, duplicar left
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}