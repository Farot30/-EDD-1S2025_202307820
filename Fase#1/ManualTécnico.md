UNIVERSIDAD DE SAN CARLOS DE GUATEMALA

FACULTAD DE INGENIERIA

ESTRUCTURA DE DATOS

CATEDRÁTICO: ING. ESPINO

============================================================================================

  
  <div style="text-align: center;">
    <h1 style="color: blue; text-decoration: underline; text-decoration-color: black;" >MANUAL TÉCNICO AUTOGEST PRO</h1>
  </div>

============================================================================================

<p align="right">
OTTONIEL FABRICIO VÁSQUEZ PINEDA
<br>
CARNÉ: 202307820
<br>
SECCIÓN C
</p>

<P align="center">GUATEMALA 24 DE SEPTIEMBRE DEL 2025</P>

============================================================================================

<br>
<br>
<br>
<h2 align="center"> Introducción </h2>
En este proyecto se desarrolló un sistema para la gestión de talleres automotrices. El sistema permite la administración de usuarios, vehículos, repuestos y servicios, utilizando diversas estructuras de datos como listas simples, listas doblemente enlazadas, listas circulares, colas y pilas. Cada una de estas estructuras se implementó y se utilizó para almacenar y manipular la información de manera eficiente.

<br>
El objetivo principal del sistema es facilitar la gestión de los talleres automotrices, permitiendo un control detallado de los servicios realizados, los repuestos utilizados y los vehículos atendidos. Además, el sistema incluye funcionalidades para la generación de reportes y gráficas que representan visualmente el estado de las estructuras de datos, utilizando Graphviz y también la utilización de la carga masiva.

<br>
Este manual técnico proporciona una descripción detallada de la implementación del sistema, incluyendo la estructura del código, las clases y métodos utilizados, y las instrucciones para la ejecución y uso del sistema. También se incluyen ejemplos de uso y explicaciones sobre el funcionamiento interno de las estructuras de datos implementadas.

<h2 align="center"> Requisitos </h2>

### software:
- C# (con uso de punteros y unsafe code), GTK para la interfaz gráfica.
### Sistema Operativo:
- Linux (distribución libre).
### IDE: 
- Cualquier IDE compatible con C#.


<br>
<h2 align="center"> Objetivos del sistema </h2>

### General
- Desarrollar un sistema integral para la gestión de talleres automotrices que permita la administración eficiente de usuarios, vehículos, repuestos y servicios, utilizando diversas estructuras de datos y facilitando la generación de reportes y gráficas pero todo esto utilizando punteros y también unsafe, que es lo que más se tubo que utilizar en el proyecto.


### Específicos

1. Implementar estructuras de datos eficientes como listas simples, listas doblemente enlazadas, listas circulares, colas y pilas para almacenar y manipular la información del taller automotriz.
2. Desarrollar funcionalidades que permitan la administración detallada de usuarios, vehículos, repuestos y servicios, asegurando un control preciso y actualizado de la información.
3. Integrar herramientas de visualización como Graphviz para generar reportes y gráficas que representen visualmente el estado de las estructuras de datos, facilitando el análisis y la toma de decisiones.
4. También implementar la carga masiva de archivos .JSON para la lectura más facil de usuarios, vehículos y repuestos.

<h1 align="center"> Interfaz </h1>
En la interfaz utilizamos una librería llamada GTK en la cual esta nos ayuda a tener una mejor compresión del sistema y así podamos trabajar mejor, también nos ayuda a tener una mejor visualización de todo el sistema.

<br>
No se describira lo que hace cada función de lo que hace el frontend ya que con los titulos ya se pueden dar una ídea de que hace cada cosa.

### Importaciones
~~~C#
    <PackageReference Include="GtkSharp" Version="3.24.24.95" />
    <PackageReference Include="Newtonsoft.Json" Version="13.0.3" />
~~~

2. Descripción del Proyecto
AutoGest Pro es una aplicación integral para talleres de reparación de vehículos que permite la gestión de vehículos, repuestos, servicios y facturación.

<h1 align="center"> Componentes del sistema</h1>

### Usuarios:
- Almacenados en una lista simplemente enlazada.

### Vehículos:
- Almacenados en una lista simplemente enlazada.

### Repuestos:
- Usan una lista circular.

### Servicios:
- Administrados en una cola.

### Facturas:
- Almacenadas en una pila.

### Bitácora:
- Implementada mediante una matriz dispersa.

<h1 align="center"> Instalación y configuración</h1>

### Guía de instalación

1. Instalar .NET en un entorno Linux.

2. Instalar la librería GTK para interfaces gráficas.

3. Descargar el código fuente del repositorio en GitHub.

4. Configurar las dependencias en el archivo App.config.

5. Instalar la libreria Newtonsoft.Json.

### Dependencias
+ **GTK:** Necesario para la interfaz gráfica.

+ **Newtonsoft.Json:** Necesario para la deserialización de datos.

+ **Graphviz:** Para la generación de reportes visuales.

+ **C#:** El lenguaje principal de desarrollo.

<h1 align="center"> Estructura del código</h1>

La estructura general del proyecto es la siguiente:

* **Entidades:** Clases que representan las entidades principales, como Usuarios, Vehículos, Repuestos, Servicios y Facturas.

### Ejemplo de clase Usuarios en C#

~~~C#
using System;

namespace Usuarios
{
    public unsafe struct Usuario
    {
        public int Id;
        public fixed char Nombre[50];
        public fixed char Apellido[50];
        public fixed char Correo[50];
        public fixed char Contraseña[50];

        public Usuario* sig;

        public Usuario(int id, string nombre, string apellido, string correo, string contraseña)
        {
            this.Id = id;
            sig = null;
            fixed (char* n = this.Nombre, l=this.Apellido, c = this.Correo, co = this.Contraseña)
            {
                setFixedString(nombre, n, 50);
                setFixedString(apellido, l, 50);
                setFixedString(correo, c, 50);
                setFixedString(contraseña, co, 50);
            }
        }

        private static void setFixedString(string str, char* fixedStr, int maxLength)
        {
            int i;
            for (i = 0; i < str.Length && i < maxLength - 1; i++)
            {
                fixedStr[i] = str[i];
            }
            fixedStr[i] = '\0';
        }

        private static string getFixedString(char* fixedStr)
        {
            string str = "";
            for (int i = 0; fixedStr[i] != '\0'; i++)
            {
                str += fixedStr[i];
            }
            return str;
        }

        public override string ToString()
        {   
            fixed(char* n = Nombre, l = Apellido, co = Correo, c = Contraseña)
            {
                return $"ID: {Id}, Nombre: {getFixedString(n)}, Apellido: {getFixedString(l)}, Correo: {getFixedString(co)}, Contraseña: {getFixedString(c)}";
            }
        }
    }
}
~~~

* **Listas, Pilas y Colas:** Implementadas con estructuras de datos dinámicas (listas enlazadas, colas y pilas).

### Ejemplo de la pila

~~~ C#
using System.Runtime.InteropServices;

namespace Pila
{
    public unsafe class Pila
    {
        private Facturas.Facturas* cima;

        // Metodo Constructor
        public Pila()
        {
            cima = null;
        }


        //Metodo para apilar un nuevo nodo
        public void Apila(int id, int id_orden, double total)
        {
            Facturas.Facturas* nuevo = (Facturas.Facturas*)Marshal.AllocHGlobal(sizeof(Facturas.Facturas));
            nuevo->ID = id;
            nuevo->ID_Orden = id_orden;
            nuevo->Total = total;
            nuevo->Siguiente = cima;
            cima = nuevo;
        }

        //Metodo para desapilar un nodo
        public void Desapilar()
        {
            if (cima != null)
            {
                Facturas.Facturas* aux = cima;
                cima = cima->Siguiente; // La nueva cima es el siguiente nodo en la lista

                // liberamos la memoria del nodo que se desapila
                Marshal.FreeHGlobal((IntPtr)aux);
            }
        }

        //Metodo para mostrar la pila
        public void Mostrar()
        {
            Facturas.Facturas* aux = cima;
            while (aux != null)
            {
                Console.WriteLine("ID: {0}, ID_Orden: {1}, Total: {2}", aux->ID, aux->ID_Orden, aux->Total);
                aux = aux->Siguiente;
            }
        }

        public void Liberar()
        {
            Facturas.Facturas* presente = cima;
            while (presente != null)
            {
                Facturas.Facturas* temp = presente;
                presente = presente->Siguiente;
                Marshal.FreeHGlobal((IntPtr)temp);
                Desapilar();
            }
            cima = null;
        }

        // Metodo para generar el código Graphviz de la pila
        public string GenerarGrafica()
        {
            // si la pila está vacia, se muestra solo un nodo "null"
            if (cima == null)
            {
                return "digraph G {\n    node [shape=record];\n    NULL [label = \"{NULL}\"];\n}\n";
            }

            // si la pila no está vacia, se genera el código Graphviz
            var graphviz = "digraph G {\n";
            graphviz += "    node [shape=record];\n";
            graphviz += "    rankdir=TB;\n";  // Definir el flujo de arriba hacia abajo para la pila
            graphviz += "    subgraph cluster_0 {\n";
            graphviz += "        label = \"Pila\";\n";

            // Iterar sobre los nodos de la pila y constructores de los nodos
            Facturas.Facturas* aux = cima;
            int ind = 0;

            while (aux != null)
            {
                graphviz += $"    node{ind} [label = \"{{ID: {aux->ID}\\nID_Orden: {aux->ID_Orden}\\nTotal: {aux->Total:C}}}\"];\n";
                aux = aux->Siguiente;
                ind++;
            }

            aux = cima;
            for (int i = 0; aux != null && aux->Siguiente != null; i++)
            {
                graphviz += $"    node{i} -> node{i + 1};\n";
                aux = aux->Siguiente;
            }

            graphviz += "    }\n";
            graphviz += "}\n";
            return graphviz;
        }

        public unsafe Facturas.Facturas* ObtenerUltimaFactura()
        {
            return cima;
        }
    }
}
~~~
<br>

* **Funciones y Métodos:** Métodos principales como lista.Insertar(new Usuario(id, Nombres.Text, Apellidos.Text, Correo.Text, Contraseña.Text)), listaAutos.Insertar(id, idUsuario, Marcas.Text, modelos, Placas.Text), y listaRepuestos.Insertar(id, Repuesto.Text, Descripcion.Text, costo).


<h1 align="center"> Guía de uso</h1>

- **Ingreso Manual:** El usuario root puede ingresar entidades manualmente desde la interfaz de usuario, que se gestiona mediante formularios GTK.

  Ejemplo de código para el ingreso manual de un vehículo:

~~~ C#
public void IngresarVehiculo(int id, int idUsuario, string marca, string modelo, string placa)

listaAutos.Insertar(id, idUsuario, Marcas.Text, modelos, Placas.Text)
~~~

<h1 align="center"> Pruebas y Depuración </h1>

* **Estrategias de Pruebas:**
  * Verificar el registro de vehículos, usuarios y repuestos.
  * Comprobar la funcionalidad de la cola para generar servicios.
  * Validar la creación y cancelación de facturas

<h1 align="center"> Mantenimiento y Actualizaciones </h1>

* **Instrucciones para mantenimiento:**
  * Para agregar nuevas funcionalidades, seguir la estructura modular del código, asegurando que se usen las estructuras de datos correctas.

  * Mantener actualizado el repositorio con nuevas versiones.

* **Actualización de versiones:** Los cambios de versión se manejarán utilizando control de versiones en GitHub.

<h1 align="center"> Conclusiones </h1>

* **Lecciones Aprendidas:**

  * Uso adecuado de punteros y estructuras dinámicas en C#.

  * La importancia de una arquitectura modular para facilitar la extensión del sistema.

  * Entender mejor la estructura de trabajar con linux y C# ya que es algo nuevo ya que normalmente se ha trabajado con windows y no Linux, ya que las 2 tienen diferentes estructuras de trabajo.