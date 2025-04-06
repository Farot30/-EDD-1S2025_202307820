using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Clases.ControlLogeo
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

    public class LogUsuarioConverter : JsonConverter<LogUsuario>
    {
        public override void WriteJson(JsonWriter writer, LogUsuario? value, JsonSerializer serializer)
        {
            if (value == null)
            {
                writer.WriteNull();
                return;
            }

            writer.WriteStartObject();
            writer.WritePropertyName("usuario");
            writer.WriteValue(value.Usuario);
            writer.WritePropertyName("entrada");
            writer.WriteValue(value.Entrada.ToString("yyyy-MM-dd HH:mm:ss.00"));
            writer.WritePropertyName("salida");
            writer.WriteValue(value.Salida?.ToString("yyyy-MM-dd HH:mm:ss.00"));
            writer.WriteEndObject();
        }

        public override LogUsuario ReadJson(JsonReader reader, Type objectType, LogUsuario? existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }

    public static class ControlLogeo
    {
        public static void ExportarLogUsuarios(List<LogUsuario> logUsuarios)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.Formatting = Formatting.Indented;
            settings.Converters.Add(new LogUsuarioConverter());

            string json = JsonConvert.SerializeObject(logUsuarios, settings);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "Reportes", "log_usuarios.json");
            File.WriteAllText(path, json);
            Console.WriteLine("Log de usuarios exportado con éxito.");
        }
    }
}