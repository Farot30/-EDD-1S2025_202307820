using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace CLases.ControlLogeo
{
    public class LogUsuario
    {
        public string Usuario { get; set; }
        public DateTime Entrada { get; set; }
        public DateTime? Salida { get; set; }

        public LogUsuario(string usuario, DateTime entrada)
        {
            Usuario = usuario;
            Entrada = entrada;
            Salida = null;
        }
    }

    public static class ControlLogeo
    {
        public static void ExportarLogUsuarios(List<LogUsuario> logUsuarios)
        {
            string json = JsonConvert.SerializeObject(logUsuarios, Formatting.Indented);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "log_usuarios.json");
            File.WriteAllText(path, json);
            Console.WriteLine("Log de usuarios exportado con éxito.");
        }
    }
}