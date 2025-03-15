using System;
using System.Diagnostics;
using System.IO;

namespace GeneracionGrafica
{
    public static class Grafica
    {
        // Función que genera un archivo .dot en la carpeta "reports" con el nombre y contenido especificado
        public static void GenerarArchivoDot(string nombre, string contenido, string titulo)
        {
            try
            {
                string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Graficas");
                if (!Directory.Exists(carpeta))
                {
                    Directory.CreateDirectory(carpeta);
                }

                if (string.IsNullOrEmpty(nombre)) // Verificar que el nombre no sea nulo o vacío
                {
                    Console.WriteLine("El nombre del archivo no puede ser nulo o vacío.");
                    return;
                }

                if (!nombre.EndsWith(".dot"))
                {
                    nombre += ".dot";
                }

                string rutaArchivo = Path.Combine(carpeta, nombre);
                string contenidoConTitulo;

                // Verificar si el contenido ya tiene la declaración "digraph G {"
                if (contenido.TrimStart().StartsWith("digraph G {"))
                {
                    contenidoConTitulo = contenido;
                }
                else
                {
                    contenidoConTitulo = $"digraph G {{\nlabel=\"{titulo}\";\nlabelloc=\"t\";\n{contenido}\n}}";
                }

                File.WriteAllText(rutaArchivo, contenidoConTitulo);

                Console.WriteLine($"Archivo generado con éxito en: {rutaArchivo}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al generar el archivo: {ex.Message}");
            }
        }

        // Función que convierte un archivo .dot en una imagen PNG
        public static void ConvertirDotAImagen(string nombreReporte)
        {
            try
            {
                string carpeta = Path.Combine(Directory.GetCurrentDirectory(), "Graficas");
                string archivoDot = Path.Combine(carpeta, nombreReporte);

                if (string.IsNullOrEmpty(archivoDot))
                {
                    Console.WriteLine("El archivo no tiene una ruta válida.");
                    return;
                }

                if (!File.Exists(archivoDot))
                {
                    Console.WriteLine($"El archivo {nombreReporte} no existe en la carpeta 'Graficas'.");
                    return;
                }

                string archivoImagen = Path.ChangeExtension(archivoDot, ".png");

                ProcessStartInfo processStartInfo = new ProcessStartInfo
                {
                    FileName = "dot",
                    Arguments = $"-Tpng \"{archivoDot}\" -o \"{archivoImagen}\"",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };

                Process? proceso = Process.Start(processStartInfo);

                if (proceso == null)
                {
                    Console.WriteLine("No se pudo iniciar el proceso para convertir el archivo .dot.");
                    return;
                }

                string output = proceso.StandardOutput.ReadToEnd();
                string error = proceso.StandardError.ReadToEnd();
                proceso.WaitForExit();

                if (proceso.ExitCode == 0)
                {
                    Console.WriteLine($"Conversión exitosa. La imagen se guardó en: {archivoImagen}");
                }
                else
                {
                    Console.WriteLine("Hubo un error al intentar convertir el archivo .dot a imagen.");
                    Console.WriteLine($"Error: {error}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al convertir el archivo .dot a imagen: {ex.Message}");
            }
        }
    }
    
}