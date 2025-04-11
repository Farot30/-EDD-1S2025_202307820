using System;
using System.IO;
using Nodos.IdRepuestoId;
using Nodos.IdVehiculoId;

namespace ListasArboles.GrafoNodiri
{

    public class ListaDeListas
    {
        public NodoVehiculosID? Cabeza { get; set; }
        public NodoVehiculosID? Cola { get; set; }

        public ListaDeListas()
        {
            Cabeza = null;
            Cola = null;
        }

        public void EliminarRepuestoDeVehiculo(int idVehiculo, int idRepuesto)
        {
            NodoVehiculosID? vehiculo = BuscarVehiculo(idVehiculo);
            if (vehiculo == null)
            {
                Console.WriteLine($"Vehículo con ID {idVehiculo} no encontrado.");
                return;
            }

            NodoRepuestosID? actual = vehiculo.ListaRepuestos;
            NodoRepuestosID? anterior = null;

            while (actual != null)
            {
                if (actual.ID_repuesto == idRepuesto)
                {
                    if (anterior == null)
                    {
                        // Eliminar el primer repuesto
                        vehiculo.ListaRepuestos = actual.Siguiente;
                    }
                    else
                    {
                        // Eliminar un repuesto intermedio o final
                        anterior.Siguiente = actual.Siguiente;
                    }

                    Console.WriteLine($"Repuesto con ID {idRepuesto} eliminado del vehículo con ID {idVehiculo}.");
                    EliminarVehiculoSiSinRepuestos(idVehiculo);
                    return;
                }

                anterior = actual;
                actual = actual.Siguiente;
            }

            Console.WriteLine($"Repuesto con ID {idRepuesto} no encontrado en el vehículo con ID {idVehiculo}.");
        }

        private void EliminarVehiculoSiSinRepuestos(int idVehiculo)
        {
            NodoVehiculosID? vehiculo = BuscarVehiculo(idVehiculo);
            if (vehiculo != null && vehiculo.ListaRepuestos == null)
            {
                // Eliminar el vehículo de la lista
                if (vehiculo == Cabeza && vehiculo == Cola)
                {
                    // El único vehículo en la lista
                    Cabeza = null;
                    Cola = null;
                }
                else if (vehiculo == Cabeza)
                {
                    // Eliminar el primer vehículo
                    Cabeza = vehiculo.Siguiente;
                    if (Cabeza != null)
                    {
                        Cabeza.Anterior = null;
                    }
                }
                else if (vehiculo == Cola)
                {
                    // Eliminar el último vehículo
                    Cola = vehiculo.Anterior;
                    if (Cola != null)
                    {
                        Cola.Siguiente = null;
                    }
                }
                else
                {
                    // Eliminar un vehículo intermedio
                    vehiculo.Anterior!.Siguiente = vehiculo.Siguiente;
                    vehiculo.Siguiente!.Anterior = vehiculo.Anterior;
                }

                Console.WriteLine($"Vehículo con ID {idVehiculo} eliminado porque ya no tiene repuestos.");
            }
        }

        public NodoVehiculosID? BuscarVehiculo(int idVehiculo)
        {
            NodoVehiculosID? actual = Cabeza;
            while (actual != null)
            {
                if (actual.ID_vehiculo == idVehiculo)
                {
                    return actual;
                }
                actual = actual.Siguiente;
            }
            return null;
        }

        public void AgregarVehiculoConRepuesto(int idVehiculo, int idRepuesto)
        {
            NodoVehiculosID? vehiculo = BuscarVehiculo(idVehiculo);

            if (vehiculo != null)
            {
                // Si el vehículo ya existe, agregar el repuesto
                vehiculo.AgregarRepuesto(idRepuesto);
                Console.WriteLine($"Repuesto con ID {idRepuesto} agregado al vehículo con ID {idVehiculo}.");
            }
            else
            {
                // Si el vehículo no existe, crearlo y agregar el repuesto
                NodoVehiculosID nuevoVehiculo = new NodoVehiculosID(idVehiculo);
                nuevoVehiculo.AgregarRepuesto(idRepuesto);

                if (Cabeza == null)
                {
                    Cabeza = nuevoVehiculo;
                    Cola = nuevoVehiculo;
                }
                else
                {
                    Cola!.Siguiente = nuevoVehiculo;
                    nuevoVehiculo.Anterior = Cola;
                    Cola = nuevoVehiculo;
                }

                Console.WriteLine($"Vehículo con ID {idVehiculo} creado y repuesto con ID {idRepuesto} agregado.");
            }
        }

        public void GenerarGrafo(string reportesPath)
        {
            try
            {
                // Asegurarse de que la carpeta Reportes exista
                if (!Directory.Exists(reportesPath))
                {
                    Directory.CreateDirectory(reportesPath);
                }

                // Rutas para los archivos .dot y .png
                string dotFilePath = Path.Combine(reportesPath, "grafo.dot");
                string imageFilePath = Path.Combine(reportesPath, "grafo.png");

                // Generar el archivo .dot
                using (StreamWriter writer = new StreamWriter(dotFilePath))
                {
                    writer.WriteLine("graph G {");
                    NodoVehiculosID? actualVehiculo = Cabeza;

                    while (actualVehiculo != null)
                    {
                        // Nodo del vehículo
                        writer.WriteLine($"  V{actualVehiculo.ID_vehiculo} [label=\"V {actualVehiculo.ID_vehiculo}\"];");

                        // Conexiones con los repuestos
                        NodoRepuestosID? actualRepuesto = actualVehiculo.ListaRepuestos;
                        while (actualRepuesto != null)
                        {
                            writer.WriteLine($"  R{actualRepuesto.ID_repuesto} [label=\"R {actualRepuesto.ID_repuesto}\"];");
                            writer.WriteLine($"  V{actualVehiculo.ID_vehiculo} -- R{actualRepuesto.ID_repuesto};");
                            actualRepuesto = actualRepuesto.Siguiente;
                        }

                        actualVehiculo = actualVehiculo.Siguiente;
                    }

                    writer.WriteLine("}");
                }

                Console.WriteLine($"Archivo DOT generado: {dotFilePath}");

                // Generar la imagen usando Graphviz
                var process = new System.Diagnostics.Process();
                process.StartInfo.FileName = "dot";
                process.StartInfo.Arguments = $"-Tpng \"{dotFilePath}\" -o \"{imageFilePath}\"";
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.Start();

                process.WaitForExit();
                if (process.ExitCode == 0)
                {
                    Console.WriteLine($"Imagen del grafo generada exitosamente: {imageFilePath}");
                }
                else
                {
                    Console.WriteLine("Error al generar la imagen del grafo.");
                    Console.WriteLine(process.StandardError.ReadToEnd());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ocurrió un error al generar el grafo: {ex.Message}");
            }
        }

        public void ImprimirLista()
        {
            NodoVehiculosID? actualVehiculo = Cabeza;
            while (actualVehiculo != null)
            {
                Console.WriteLine($"Vehículo ID: {actualVehiculo.ID_vehiculo}");
                actualVehiculo.ImprimirRepuestos();
                actualVehiculo = actualVehiculo.Siguiente;
            }
        }
    }

}