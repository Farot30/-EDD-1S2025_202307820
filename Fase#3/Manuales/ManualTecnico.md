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

Este documento describe la arquitectura, los módulos, las estructuras de datos y los algoritmos implementados en la Fase 3 de **AutoGest Pro**, un sistema de gestión avanzado para talleres de reparación de vehículos. Se detallan las decisiones de diseño, formatos de almacenamiento, protocolos de seguridad y la integración de tecnologías como Blockchain, Árbol de Merkle, compresión Huffman y Graphviz.

## 2. Objetivos del Proyecto

### Objetivo General
Desarrollar un sistema que administre entidades (usuarios, vehículos, repuestos, servicios, facturas) optimizando organización, seguridad y consulta de información.

### Objetivos Específicos

- Modelar relaciones vehículo–repuesto con un grafo no dirigido.

- Comprimir reportes en texto plano usando compresión Huffman.

- Almacenar usuarios en Blockchain con SHA-256.

- Gestionar facturas con un Árbol de Merkle.

- Mantener estructuras: lista doblemente enlazada (vehículos), árbol AVL (repuestos) y árbol binario (servicios).

- Generar reportes gráficos con Graphviz.

---
## 3. Requerimientos del Sistema

### Requisitos Funcionales
- **Inicio de Sesión:**
  - Implementación de autenticación para usuarios y administrador (credenciales: `admin@usac.com` / `admin123`), el cual no puede ser registrado manualmente ni mediante carga masiva.
  
- **Gestión de Usuarios:**
  - Registro y visualización de usuarios con BlockChain, asegurando la unicidad de los campos ID y correo.

- **Gestión de Vehículos:**
  - Visualización de vehículos  conforme al ID de usuario 

- **Gestión de Repuestos:**
  - Manejo de repuestos mediante árbol AVL para garantizar búsquedas, inserciones y eliminaciones rápidas, verificando la unicidad del ID.

- **Gestión de Servicios y Facturación:**
  - Los servicios se administran mediante un árbol binario y las facturas se gestionan con un árbol Merkle. Se valida la existencia de los ID asociados antes de generar un servicio o factura.

- **Carga Masiva:**
  - Capacidad para importar datos de usuarios, vehículos y repuestos a través de archivos JSON.

- **Reportes:**
  - Generación de reportes visuales utilizando Graphviz, almacenándolos en la carpeta `/Reportes`.

- **Control de Logueo:**
  - Registro de entradas y salidas de los usuarios, exportable en formato JSON y en la interfaz gráfica.

### Requisitos No Funcionales
- **Lenguaje de Programación:** C#
- **Interfaz Gráfica:** Librería GTK.
- **Herramienta para Reportes:** Graphviz.
- **Sistema Operativo:** Linux (distribución libre).
- **IDE:** Libre, con soporte para desarrollo en C#.

## 4. Arquitectura y Diseño del Sistema

La aplicación está desarrollada en C# sobre Linux, con interfaces gráficas basadas en GTK. El sistema sigue una arquitectura modular, separando lógica de negocio, persistencia y presentación. Cada módulo opera sobre su propia estructura de datos en memoria y expone operaciones CRUD que son invocadas desde la interfaz.

### 4.1 Módulos del Sistema

1. **Módulo de Autenticación:**
   - Gestión del inicio de sesión para usuarios y administrador, con validación de credenciales.

2. **Módulo de Gestión de Entidades:**
   - **Usuarios:** Registro, visualización administrandolo con BlockChain.
   - **Vehículos:** Visualización y administración mediante lista doblemente enlazada.
   - **Repuestos:** Gestión a través de árbol AVL, incluyendo actualización y visualización en distintos recorridos.
   - **Servicios:** Registro y consulta utilizando árbol binario.
   - **Facturación:** Generación y administración mediante arbol Merkle.

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
  Opciones para carga masiva, gestión de usuarios (visualización), visualización de repuestos, generación de servicios y facturas, visualización de logueo y generación de reportes.
- **Carga Masiva:**  
  Funcionalidad para importar archivos JSON correspondientes a cada entidad (excepto el usuario administrador).
- **Gestión de Entidades y Reportes:**  
  Visualización de las estructuras en distintos recorridos y generación de imágenes gráficas con Graphviz.
  **Cargar Backup:""
  Generar backup de Usuarios, vehiculos y repuestos con el método de Huffman

#### Rol Usuario
- **Visualización de Vehículos:**  
  Permite a los usuarios visualizar vehículos asociados a su cuenta.
- **Visualización de Servicios y Facturas:**  
  Consulta de servicios realizados y facturas pendientes mediante tablas y distintos filtros de recorrido.

---



## 5 Estructuras de Datos Empleadas

### 5.1 Blockchain para Usuarios
- Bloque: contiene Index, Timestamp, Data (ID, nombres, apellidos, correo, edad, contraseña en SHA-256), Nonce, PreviousHash, Hash.

- Prueba de trabajo: el Nonce se itera hasta que Hash comience con "0000".

- Enlace: cada bloque almacena PreviousHash, garantizando inmutabilidad.

- Operaciones:

    - InsertUser(user): crea un bloque en cabeza de la cadena con validación de PoW.

    - ValidateChain(): recorre la cadena verificando integridad de hashes.


### 5.2 Lista Doblemente Enlazada para Vehículos  
- Cada nodo almacena Id, Id_Usuario, Marca, Modelo, Placa.

- Permite inserción al final y recorridos en ambos sentidos.

- Validación: ID único y existencia del Id_Usuario antes de insertar
  
### 5.3 Árbol AVL para Repuestos
- Nodo con Id, Repuesto, Detalles, Costo.

- Mantiene balance con factor -1, 0, +1.

- Rotaciones simples y dobles tras inserción/eliminación.

- Validación: ID único en sistema

### 5.4 Árbol Binario para Servicios
- Nodo con Id, Id_Repuesto, Id_Vehiculo, Detalles, Costo.

- Ordenamiento configurable (por ID o costo).

- Acciones:

    - Inserción: verificar existencia de Id_Repuesto y Id_Vehiculo.

    - Recorridos: preorden, inorden, postorden. 

### 5.5 Árbol de Merkle para Facturas
- Cada hoja: hash de los datos de la factura (ID, ID_Servicio, Total, Fecha, Método de Pago).

- Nodos internos: hash de la concatenación de hijos.

- Raíz: identificador único que garantiza integridad.

- Operaciones:

    - InsertInvoice(factura): agregar hoja y recalcular hashes.

    - VerifyInvoice(id): prueba de inclusión en Merkle tree

### 5.6 Grafo No Dirigido para Relaciones

- Nodos: IDs de vehículos y repuestos.

- Aristas: cada servicio genera una conexión vehículo–repuesto.

- Representación: listas de adyacencia.

- Consultas típicas: encontrar repuestos frecuentes por vehículo y viceversa.

### 5.7 Compresión Huffman para Backup

- Generación de árbol de frecuencia sobre archivos JSON de vehículos y repuestos.

- Codificación bit a bit y escritura en .edd.

- Descompresión: reconstrucción del árbol y decodificación de datos.
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

---

## 6. Detalles de Implementación

### 6.1 Lenguaje y Herramientas
- **C#:** Lenguaje principal para el desarrollo del sistema.
- **GTK:** Librería utilizada para el desarrollo de la interfaz gráfica.
- **Graphviz:** Herramienta para la generación de reportes gráficos.
- **JSON:** Formato empleado para la carga masiva de datos y la exportación del control de logueo.

### 6.2 Estructura del Proyecto
La organización del proyecto es la siguiente:
- **/Clases:** Contiene el código de clases.
- **/InterfacesAdmin:** Contiene el código de la interfaz Administrados.
- **/INterfacesUsuario:** Contiene el código de la interfaz de usuario.
- **/ListasArboles:** Contiene las listas generadas y arboles.
- **/Nodos:** Contiene los nodos usados.
- **/Reportes:** Almacena las imágenes generadas por Graphviz.
- **Fase2.csproj:** Archivo de proyecto para compilar el sistema.
- **Program.cs:** Punto de entrada del programa.

### Ejemplo de clase arboles

Arbol Merkle

~~~C#
public class ArbolMerklesFac
    {
        int contador = 0;
        private List<NodoFacturas> nodosHojas;
        private NodoFacturas? RaizArbol;

        public ArbolMerklesFac()
        {
            nodosHojas = new List<NodoFacturas>();
            RaizArbol = null;
        }
        private int GenerarIdUnico()
        {
            return contador++; // Incrementa el contador y devuelve el valor actual
        }

        // Método para insertar una nueva factura
        public void InsertarFactura(int idServicio, double total, string metodoPago)
        {
            int idFactura = GenerarIdUnico();

            foreach (var nodo in nodosHojas)
            {
                if (nodo.Factura.HasValue && nodo.Factura.Value.ID == idFactura)
                {
                    Console.WriteLine($"Factura con ID {idFactura} ya existe en el árbol.");
                    return; // Salir del método si la factura ya existe
                }
            }
            // Crear la factura y el nodo hoja
            Factura factura = new Factura(idFactura, idServicio, total, metodoPago);
            NodoFacturas newLeaf = new NodoFacturas(factura);
            nodosHojas.Add(newLeaf);

            // Reconstruir el árbol con las hojas actuales
            ConstrucMek();
        }

        public void MostrarFacturas()
        {
            if (nodosHojas.Count == 0)
            {
                Console.WriteLine("No hay facturas registradas en el árbol.");
                return;
            }

            Console.WriteLine("Facturas registradas:");
            foreach (var nodo in nodosHojas)
            {
                if (nodo.Factura != null)
                {
                    Console.WriteLine($"ID: {nodo.Factura.Value.ID}, " +
                                    $"ID Servicio: {nodo.Factura.Value.ID_servicio}, " +
                                    $"Total: {nodo.Factura.Value.Total}, " +
                                    $"Fecha: {nodo.Factura.Value.Fecha}, " +
                                    $"Método de Pago: {nodo.Factura.Value.Metodo_pago}");
                }
            }
        }

        private void ConstrucMek()
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
                    NodoFacturas right = (i + 1 < currentLevel.Count) ? currentLevel[i + 1] : null;
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
   El administrador realiza cargas masivas y administra los registros (visualización) de las diferentes entidades.
3. **Procesos de Servicio y Facturación:**  
   Al generar un servicio, se valida la existencia de los datos asociados y se genera automáticamente una factura.
4. **Generación de Reportes:**  
   Las estructuras de datos se recorren y se representan gráficamente utilizando Graphviz, con los reportes almacenados en la carpeta designada.
5. **Generación de Backup:**
   Las entidades de Usuarios, vehiculos y repeustos, se les genera un backup para guardar sus datos y tenerlos asegurados.


---

## 7. Conclusiones

El sistema **AutoGest Pro** integra múltiples estructuras de datos y herramientas tecnológicas para ofrecer una solución integral en la gestión de talleres de reparación de vehículos. Con una interfaz intuitiva, un manejo eficiente de la información y la capacidad de generar reportes gráficos, el proyecto se presenta como una solución robusta y escalable, cumpliendo con los requisitos establecidos en la asignatura de Estructuras de Datos.

La correcta implementación de las estructuras (listas, árbol AVL, árbol binario y árbol B), combinada con el uso de C#, GTK y Graphviz, garantiza que el sistema no solo sea funcional, sino también óptimo para el manejo de grandes volúmenes de datos, lo cual es esencial en un entorno real de taller.

---