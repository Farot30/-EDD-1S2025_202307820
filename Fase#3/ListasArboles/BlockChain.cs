using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Clases.Blocks;
using Clases.Usuarios;

namespace Listas.Blockchains
{
    public class Blockchain
    {
        public List<Block> Chain { get; private set; }
        public Blockchain()
        {
            Chain = new List<Block>();
            // Crear el bloque génesis
            var genesisBlock = new Block(0, new Usuario(), "0000");
            Chain.Insert(0, genesisBlock);
        }
        public void AddBlock(Usuario data)
        {
            if (VerificarId(data.Id))
            {
                Console.WriteLine($"El ID {data.Id} ya existe en la cadena.");
                return;
            }

            if (VerificarCorreo(data.Correo))
            {
                Console.WriteLine($"El correo {data.Correo} ya existe en la cadena.");
                return;
            }

            // Encriptar la contraseña del usuario antes de agregarlo
            data.Contrasena = EncriptacionSHa256(data.Contrasena);

            int index = Chain[0].Index + 1;
            string previousHash = Chain[0].Hash;
            var newBlock = new Block(index, data, previousHash);
            Chain.Insert(0, newBlock);
        }

        public bool ValidarUsuario(string correo, string contrasena)
        {
            // Encriptar la contraseña ingresada
            string contrasenaEncriptada = EncriptacionSHa256(contrasena);

            // Verificar si el correo y la contraseña encriptada coinciden
            return Chain.Any(block => block.Data.Correo == correo && block.Data.Contrasena == contrasenaEncriptada);
        }
        public static string EncriptacionSHa256(string text)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // ComputeHash - returns byte array
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(text));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }

        public bool VerificarId(int id)
        {
            // Recorrer la cadena para verificar si el ID ya existe
            return Chain.Any(block => block.Data.Id == id);
        }

        public bool VerificarCorreo(string correo)
        {
            // Recorrer la cadena para verificar si el correo ya existe
            return Chain.Any(block => block.Data.Correo == correo);
        }

        public Usuario? BuscarUsuarioPorId(int id)
        {
            // Buscar un usuario en la blockchain por su ID
            var block = Chain.FirstOrDefault(b => b.Data.Id == id);
            return block?.Data;
        }

        public Usuario? BuscarUsuarioPorCorreo(string correo)
        {
            // Buscar un usuario en la blockchain por su correo
            var block = Chain.FirstOrDefault(b => b.Data.Correo == correo);
            return block?.Data;
        }

        public void GenerarDot(string nombreArchivo)
        {
            try
            {
                // Crear la carpeta "Reportes" en el nivel actual
                string rutaBase = Path.Combine("Reportes");
                if (!Directory.Exists(rutaBase))
                {
                    Directory.CreateDirectory(rutaBase);
                }

                // Ruta completa para el archivo .dot
                string rutaDot = Path.Combine(rutaBase, $"{nombreArchivo}.dot");

                // Generar el contenido del archivo .dot
                StringBuilder dot = new StringBuilder();
                dot.AppendLine("digraph Blockchain {");
                dot.AppendLine("    node [shape=box, style=filled, fontname=\"Arial\"];");

                for (int i = 0; i < Chain.Count; i++)
                {
                    Block block = Chain[i];
                    Usuario user = block.Data;

                    // Usamos solo partes del hash para que no sea tan largo el label
                    string hashShort = block.Hash.Substring(0, 6);
                    string prevHashShort = block.PreviousHash.Length >= 6 ? block.PreviousHash.Substring(0, 6) : block.PreviousHash;

                    // Cambiar el formato del label para usar saltos de línea
                    dot.AppendLine($"    Block{i} [label=\"INDEX: {block.Index}\\nTIMESTAMP: {block.Timestamp}\\nData: {{ID: {user.Id} Nombre: {user.Nombre} Apellido: {user.Apellido} Correo: {user.Correo} Edad: {user.Edad}\\nContrasenia: {user.Contrasena}}}\\nNONCE: {block.Nonce}\\nPREVIOUS HASH: {prevHashShort}\\nHASH: {hashShort}\"];");

                    if (i > 0)
                    {
                        dot.AppendLine($"    Block{i} -> Block{i - 1};");
                    }
                }

                dot.AppendLine("}");

                // Guardar el archivo .dot
                File.WriteAllText(rutaDot, dot.ToString());
                Console.WriteLine($"Archivo DOT generado: {rutaDot}");

                // Ruta completa para la imagen
                string rutaImagen = Path.Combine(rutaBase, $"{nombreArchivo}.png");

                // Generar la imagen utilizando Graphviz
                var process = new Process();
                process.StartInfo.FileName = "dot";
                process.StartInfo.Arguments = $"-Tpng \"{rutaDot}\" -o \"{rutaImagen}\"";
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
                    Console.WriteLine($"Imagen generada correctamente: {rutaImagen}");
                }
                else
                {
                    Console.WriteLine($"Error al generar la imagen: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al generar el grafo: {ex.Message}");
            }
        }

    }
}