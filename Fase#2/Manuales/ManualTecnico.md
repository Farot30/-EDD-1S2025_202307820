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

<P align="center">GUATEMALA 25 DE Marzo DEL 2025</P>


# Manual Técnico – AutoGest Pro

## 1. Introducción

**AutoGest Pro** es un sistema integral de gestión diseñado para optimizar las operaciones de talleres de reparación de vehículos. El sistema facilita la administración de usuarios, vehículos, repuestos, servicios y facturación. Se implementan diversas estructuras de datos avanzadas que garantizan un manejo eficiente de la información, y se utiliza una interfaz gráfica desarrollada en C# con la librería GTK. Además, se aprovecha Graphviz para la generación de reportes visuales que representan las estructuras de datos implementadas.

---

## 2. Objetivos del Proyecto

### Objetivo General

### Objetivos Específicos
- **Estructuras de Datos:**
  - Utilizar lista simplemente enlazada para almacenar información de usuarios.
  - Utilizar lista doblemente enlazada para gestionar vehículos.
  - Implementar un árbol AVL para optimizar la búsqueda y gestión de repuestos.
  - Organizar los servicios en un árbol binario, permitiendo diferentes tipos de recorridos.
  - Emplear un árbol B (orden 5) para la administración y búsqueda eficiente de facturas.
  
- **Interfaz Gráfica:**
  - Desarrollar una interfaz intuitiva y funcional utilizando GTK para facilitar la interacción del usuario.

- **Carga Masiva y Reportes:**
  - Permitir la carga masiva de datos mediante archivos JSON.
  - Generar reportes visuales de las estructuras implementadas usando Graphviz.

---

## 3. Requerimientos del Sistema

### Requisitos Funcionales
- **Inicio de Sesión:**
  - Implementación de autenticación para usuarios y administrador (credenciales: `admin@usac.com` / `admin123`), el cual no puede ser registrado manualmente ni mediante carga masiva.
  
- **Gestión de Usuarios:**
  - Registro, visualización y eliminación de usuarios, asegurando la unicidad de los campos ID y correo.

- **Gestión de Vehículos:**
  - Registro y administración de vehículos utilizando lista doblemente enlazada, validando que cada vehículo esté asociado a un usuario existente.

- **Gestión de Repuestos:**
  - Manejo de repuestos mediante árbol AVL para garantizar búsquedas, inserciones y eliminaciones rápidas, verificando la unicidad del ID.

- **Gestión de Servicios y Facturación:**
  - Los servicios se administran mediante un árbol binario y las facturas se gestionan con un árbol B de orden 5. Se valida la existencia de los ID asociados antes de generar un servicio o factura.

- **Carga Masiva:**
  - Capacidad para importar datos de usuarios, vehículos y repuestos a través de archivos JSON.

- **Reportes:**
  - Generación de reportes visuales utilizando Graphviz, almacenándolos en la carpeta `/Reportes`.

- **Control de Logueo:**
  - Registro de entradas y salidas de los usuarios, exportable en formato JSON.

### Requisitos No Funcionales
- **Lenguaje de Programación:** C#
- **Interfaz Gráfica:** Librería GTK.
- **Herramienta para Reportes:** Graphviz.
- **Sistema Operativo:** Linux (distribución libre).
- **IDE:** Libre, con soporte para desarrollo en C#.

---

## 4. Arquitectura y Diseño del Sistema

### 4.1 Estructuras de Datos Empleadas
- **Lista Simplemente Enlazada (Usuarios):**  
  Permite inserciones y eliminaciones dinámicas sin necesidad de memoria contigua.
  
- **Lista Doblemente Enlazada (Vehículos):**  
  Facilita recorridos bidireccionales para una gestión flexible de la información.
  
- **Árbol AVL (Repuestos):**  
  Asegura el equilibrio de la estructura, garantizando operaciones de búsqueda, inserción y eliminación en tiempo O(log n).
  
- **Árbol Binario (Servicios):**  
  Organiza datos de manera jerárquica y permite la implementación de distintos recorridos (pre-orden, in-orden, post-orden).
  
- **Árbol B (Facturas, orden 5):**  
  Optimiza la inserción y búsqueda en grandes volúmenes de datos, ideal para la gestión de facturación.

### 4.2 Módulos del Sistema

1. **Módulo de Autenticación:**
   - Gestión del inicio de sesión para usuarios y administrador, con validación de credenciales.

2. **Módulo de Gestión de Entidades:**
   - **Usuarios:** Registro, visualización y eliminación.
   - **Vehículos:** Registro y administración mediante lista doblemente enlazada.
   - **Repuestos:** Gestión a través de árbol AVL, incluyendo actualización y visualización en distintos recorridos.
   - **Servicios:** Registro y consulta utilizando árbol binario.
   - **Facturación:** Generación y administración mediante árbol B.

3. **Módulo de Carga Masiva:**
   - Permite la importación de datos a partir de archivos JSON para usuarios, vehículos y repuestos.

4. **Módulo de Reportes:**
   - Utiliza Graphviz para generar reportes gráficos de las estructuras de datos, almacenando los resultados en la carpeta `/Reportes`.

5. **Módulo de Control de Logueo:**
   - Registro de entradas y salidas de los usuarios, exportable en formato JSON.

### 4.3 Interfaz de Usuario

#### Rol Administrador
- **Inicio de Sesión:**  
  Pantalla de login para ingreso de credenciales.
- **Menú Principal:**  
  Opciones para carga masiva, gestión de entidades (visualización y eliminación), actualización de repuestos, generación de servicios y facturas, y generación de reportes.
- **Carga Masiva:**  
  Funcionalidad para importar archivos JSON correspondientes a cada entidad (excepto el usuario administrador).
- **Gestión de Entidades y Reportes:**  
  Visualización de las estructuras en distintos recorridos y generación de imágenes gráficas con Graphviz.

#### Rol Usuario
- **Registro de Vehículos:**  
  Permite a los usuarios insertar vehículos asociados a su cuenta.
- **Visualización de Servicios y Facturas:**  
  Consulta de servicios realizados y facturas pendientes mediante tablas y distintos filtros de recorrido.
- **Cancelación de Facturas:**  
  Opción para realizar el pago y eliminación de facturas registradas.

---

## 5. Detalles de Implementación

### 5.1 Lenguaje y Herramientas
- **C#:** Lenguaje principal para el desarrollo del sistema.
- **GTK:** Librería utilizada para el desarrollo de la interfaz gráfica.
- **Graphviz:** Herramienta para la generación de reportes gráficos.
- **JSON:** Formato empleado para la carga masiva de datos y la exportación del control de logueo.

### 5.2 Estructura del Proyecto
La organización del proyecto es la siguiente:
- **/CLases:** Contiene el código de clases.
- **/InterfacesAdmin:** Contiene el código de la interfaz Administrados.
- **/InterfacesUsuario:** Contiene el código de la interfaz de usuario.
- **/Listas:** Contiene las listas generadas y arboles.
- **/Nodos:** Contiene los nodos usados.
- **/Reportes:** Almacena las imágenes generadas por Graphviz.
- **Fase2.csproj:** Archivo de proyecto para compilar el sistema.
- **Program.cs:** Punto de entrada del programa.


### Ejemplo de clase arboles

Arbol Binario

~~~C#
using System;
using Nodos.NodoBinarios;
using CLases.Servicios;

namespace Listas.BinarioTree
{
    public class Binariostree
    {
        public NodoBinario? raiz;

        public Binariostree()
        {
            raiz = null;
        }

        public void Insertar(Servicio servicio)
        {
            if (raiz == null)
            {
                raiz = new NodoBinario(servicio);
            }
            else
            {
                Insertar(raiz, servicio);
            }
        }

        private void Insertar(NodoBinario nodo, Servicio servicio)
        {
            if (servicio.ID < nodo.servicios.ID)
            {
                if (nodo.izquierdo == null)
                {
                    nodo.izquierdo = new NodoBinario(servicio);
                }
                else
                {
                    Insertar(nodo.izquierdo, servicio);
                }
            }
            else
            {
                if (nodo.derecho == null)
                {
                    nodo.derecho = new NodoBinario(servicio);
                }
                else
                {
                    Insertar(nodo.derecho, servicio);
                }
            }
        }

        public void PreOrden(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                Console.WriteLine(nodo.servicios);
                PreOrden(nodo.izquierdo);
                PreOrden(nodo.derecho);
            }
        }

        public void InOrder(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                InOrder(nodo.izquierdo);
                Console.WriteLine(nodo.servicios);
                InOrder(nodo.derecho);
            }
        }

        public void PostOrden(NodoBinario? nodo)
        {
            if (nodo != null)
            {
                PostOrden(nodo.izquierdo);
                PostOrden(nodo.derecho);
                Console.WriteLine(nodo.servicios);
            }
        }

        public void Eliminar(int id)
        {
            raiz = Eliminar(raiz, id);
        }

        private NodoBinario? Eliminar(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return null;
            }

            if (id < nodo.servicios.ID)
            {
                nodo.izquierdo = Eliminar(nodo.izquierdo, id);
            }
            else if (id > nodo.servicios.ID)
            {
                nodo.derecho = Eliminar(nodo.derecho, id);
            }
            else
            {
                if (nodo.izquierdo == null)
                {
                    return nodo.derecho;
                }
                else if (nodo.derecho == null)
                {
                    return nodo.izquierdo;
                }

                nodo.servicios = Minimo(nodo.derecho).servicios;
                nodo.derecho = Eliminar(nodo.derecho, nodo.servicios.ID);
            }

            return nodo;
        }

        private NodoBinario Minimo(NodoBinario? nodo)
        {
            NodoBinario actual = nodo!;
            while (actual.izquierdo != null)
            {
                actual = actual.izquierdo;
            }
            return actual;
        }

        public bool Existeid(int id)
        {
            return Existeid(raiz, id);
        }

        private bool Existeid(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return false;
            }

            if (nodo.servicios.ID == id)
            {
                return true;
            }
            else if (id < nodo.servicios.ID)
            {
                return Existeid(nodo.izquierdo, id);
            }
            else
            {
                return Existeid(nodo.derecho, id);
            }
        }

        public Servicio? BuscarServicioPorId(int id)
        {
            return BuscarServicioPorId(raiz, id);
        }

        private Servicio? BuscarServicioPorId(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return null;
            }

            if (nodo.servicios.ID == id)
            {
                return nodo.servicios;
            }
            else if (id < nodo.servicios.ID)
            {
                return BuscarServicioPorId(nodo.izquierdo, id);
            }
            else
            {
                return BuscarServicioPorId(nodo.derecho, id);
            }
        }

        public string GenerarGraphviz()
        {
            var graphviz = new System.Text.StringBuilder();
            graphviz.AppendLine("digraph G {");
            GenerarGraphviz(raiz, graphviz);
            graphviz.AppendLine("}");
            return graphviz.ToString();
        }

        private void GenerarGraphviz(NodoBinario? nodo, System.Text.StringBuilder graphviz)
        {
            if (nodo != null)
            {
                string nodoLabel = $"ID: {nodo.servicios.ID}\\nID Repuesto: {nodo.servicios.ID_repuesto}\\nID Vehiculo: {nodo.servicios.ID_vehiculo}\\nDetalle: {nodo.servicios.Detalle}\\nCosto: {nodo.servicios.Costo}";
                graphviz.AppendLine($"    \"{nodo.servicios.ID}\" [label=\"{nodoLabel}\"];");

                if (nodo.izquierdo != null)
                {
                    graphviz.AppendLine($"    \"{nodo.servicios.ID}\" -> \"{nodo.izquierdo.servicios.ID}\";");
                    GenerarGraphviz(nodo.izquierdo, graphviz);
                }
                if (nodo.derecho != null)
                {
                    graphviz.AppendLine($"    \"{nodo.servicios.ID}\" -> \"{nodo.derecho.servicios.ID}\";");
                    GenerarGraphviz(nodo.derecho, graphviz);
                }
            }
        }
    }
}
~~~

### 5.3 Validaciones y Control de Errores
- **Validaciones de Unicidad:**  
  Se asegura que los campos ID y correo (en el caso de usuarios) sean únicos.
- **Verificación de Dependencias:**  
  Antes de registrar un vehículo, servicio o factura, se comprueba la existencia de los identificadores asociados (ID_Usuario, ID_Repuesto, ID_Vehiculo).
- **Manejo de Excepciones:**  
  Se implementa un manejo de errores que previene la inserción de datos inválidos y notifica al usuario con mensajes descriptivos.

### 5.4 Flujo de Datos
1. **Ingreso y Autenticación:**  
   El usuario accede al sistema a través del formulario de login.
2. **Operaciones CRUD:**  
   El administrador realiza cargas masivas y administra los registros (visualización, eliminación) de las diferentes entidades.
3. **Procesos de Servicio y Facturación:**  
   Al generar un servicio, se valida la existencia de los datos asociados y se genera automáticamente una factura.
4. **Generación de Reportes:**  
   Las estructuras de datos se recorren y se representan gráficamente utilizando Graphviz, con los reportes almacenados en la carpeta designada.

---

## 6. Consideraciones Adicionales

- **Colaboración y Control de Versiones:**  
  Cada estudiante debe crear un repositorio en GitHub con la nomenclatura `[EDD]1S2025_carnet`, agrupando las diferentes fases del proyecto. Se debe agregar al auxiliar correspondiente como colaborador según la sección asignada.

- **Entrega y Evaluación:**  
  La entrega se realizará a través de la plataforma UEDI antes de la fecha límite (28/03/2025 23:59 PM). Durante la evaluación se verificará tanto la funcionalidad como el conocimiento teórico del sistema, por lo que es fundamental comprender en detalle su funcionamiento interno.

- **Entorno de Desarrollo:**  
  Se recomienda utilizar un entorno Linux para asegurar la compatibilidad con GTK y Graphviz. La configuración del entorno y las dependencias necesarias deben documentarse para facilitar la instalación y compilación del proyecto.

---

## 7. Conclusiones

El sistema **AutoGest Pro** integra múltiples estructuras de datos y herramientas tecnológicas para ofrecer una solución integral en la gestión de talleres de reparación de vehículos. Con una interfaz intuitiva, un manejo eficiente de la información y la capacidad de generar reportes gráficos, el proyecto se presenta como una solución robusta y escalable, cumpliendo con los requisitos establecidos en la asignatura de Estructuras de Datos.

La correcta implementación de las estructuras (listas, árbol AVL, árbol binario y árbol B), combinada con el uso de C#, GTK y Graphviz, garantiza que el sistema no solo sea funcional, sino también óptimo para el manejo de grandes volúmenes de datos, lo cual es esencial en un entorno real de taller.

---
