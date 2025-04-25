using Nodos.NodoFactura;
using System.Security.Cryptography;
using System.Text;
using Clases.Facturas;

namespace ListasArboles.ArbolMerkles
{
    public class ArbolMerklesFac
    {
        private List<NodoFacturas> nodosHojas;
        private NodoFacturas? RaizArbol;

        public ArbolMerklesFac()
        {
            nodosHojas = new List<NodoFacturas>();
            RaizArbol = null;
        }

        public void InsertarFactura(Factura factura)
        {
            foreach (var nodo in nodosHojas)
            {
                if (nodo.Factura.Value.ID == factura.ID)
                {
                    Console.WriteLine("Factura ya existe en el árbol.");
                    return;
                }
            }

            NodoFacturas nuevoNodo = new NodoFacturas(factura);
            nodosHojas.Add(nuevoNodo);

            ContrucArbol();
        }

        private void ContrucArbol()
        {
            if (nodosHojas.Count == 0)
            {
                RaizArbol = null;
                return;
            }

            List<NodoFacturas> currentLevel = new List<NodoFacturas>(nodosHojas);

            while (currentLevel.Count > 1)
            {
                List<NodoFacturas> nextLevel = new List<NodoFacturas>();

                for (int i = 0; i < currentLevel.Count; i += 2)
                {
                    NodoFacturas left = currentLevel[i];
                    NodoFacturas? right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : null;
                    NodoFacturas parent = new NodoFacturas(left, right);
                    nextLevel.Add(parent);
                }

                currentLevel = nextLevel;
            }

            RaizArbol = currentLevel[0]; // La raíz es el único nodo que queda
        }

        public string GenerateDot()
        {
            StringBuilder dot = new StringBuilder();
            dot.AppendLine("digraph MerkleTree {"); 
            dot.AppendLine("  node [shape=record];"); 
            dot.AppendLine("  graph [rankdir=TB];"); 
            dot.AppendLine("  subgraph cluster_0 {"); 
            dot.AppendLine("    label=\"Facturas\";");

            if (RaizArbol == null)
            {
                dot.AppendLine("    empty [label=\"Árbol vacío\"];");
            }
            else
            {
                Dictionary<string, int> nodeIds = new Dictionary<string, int>(); 
                int idCounter = 0;
                GenerateDotRecursive(RaizArbol, dot, nodeIds, ref idCounter);
            }

            dot.AppendLine("  }");
            dot.AppendLine("}"); 
            return dot.ToString();
        }

        private void GenerateDotRecursive(NodoFacturas node, StringBuilder dot, Dictionary<string, int> nodeIds, ref int idCounter)
        {
            if (node == null) return;

            if (!nodeIds.ContainsKey(node.Hash))
            {
                nodeIds[node.Hash] = idCounter++;
            }
            int nodeId = nodeIds[node.Hash];

            string label;
            if (node.Factura != null) 
            {
                label = $"\"ID: {node.Factura.Value.ID}\\n ID_Servicio: {node.Factura.Value.ID_servicio} \\nTotal: {node.Factura.Value.Total} \\n Fecha: {node.Factura.Value.Fecha} \\n Método: {node.Factura.Value.Metodo_pago}\\nHash: {node.Hash.Substring(0, 8)}...\"";
            }
            else 
            {
                label = $"\"Hash: {node.Hash.Substring(0, 8)}...\"";
            }
            dot.AppendLine($"  node{nodeId} [label={label}];");

            if (node.Left != null)
            {
                if (!nodeIds.ContainsKey(node.Left.Hash))
                {
                    nodeIds[node.Left.Hash] = idCounter++;
                }
                int leftId = nodeIds[node.Left.Hash];
                dot.AppendLine($"  node{nodeId} -> node{leftId};");
                GenerateDotRecursive(node.Left, dot, nodeIds, ref idCounter);
            }

            if (node.Right != null)
            {
                if (!nodeIds.ContainsKey(node.Right.Hash))
                {
                    nodeIds[node.Right.Hash] = idCounter++;
                }
                int rightId = nodeIds[node.Right.Hash];
                dot.AppendLine($"  node{nodeId} -> node{rightId};");
                GenerateDotRecursive(node.Right, dot, nodeIds, ref idCounter);
            }
        }

        public void GenerateDotAndConvertToImage(string outputFileName, string reportesPath)
        {
            try
                {
                // Asegurarse de que la carpeta Reportes exista
                    if (!Directory.Exists(reportesPath))
                    {
                        Directory.CreateDirectory(reportesPath);
                    }

                // Generar el contenido del archivo .dot
                StringBuilder dot = new StringBuilder();
                dot.AppendLine("digraph MerkleTree {");
                dot.AppendLine("  node [shape=record];");
                dot.AppendLine("  graph [rankdir=TB];");
                dot.AppendLine("  subgraph cluster_0 {");
                dot.AppendLine("    label=\"Facturas\";");

                if (RaizArbol == null)
                {
                    dot.AppendLine("    empty [label=\"Árbol vacío\"];");
                }
                else
                {
                    Dictionary<string, int> nodeIds = new Dictionary<string, int>();
                    int idCounter = 0;
                    GenerateDotRecursive(RaizArbol, dot, nodeIds, ref idCounter);
                }

                dot.AppendLine("  }");
                dot.AppendLine("}");

                // Guardar el archivo .dot
                string dotFilePath = Path.Combine(reportesPath, $"{outputFileName}.dot");

                File.WriteAllText(dotFilePath, dot.ToString());
                Console.WriteLine($"Archivo DOT generado: {dotFilePath}");

                // Convertir el archivo .dot a una imagen usando Graphviz
                string imageFilePath = Path.Combine(reportesPath, $"{outputFileName}.png");
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "dot";
                process.StartInfo.Arguments = $"-Tpng \"{dotFilePath}\" -o \"{imageFilePath}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.Start();
                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();
                process.WaitForExit();

                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"Imagen generada correctamente: {imageFilePath}");
                }
                else
                {
                    Console.WriteLine($"Error al generar la imagen: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al generar el Arbol Merkle: {ex.Message}");
            }
        }
    }
}