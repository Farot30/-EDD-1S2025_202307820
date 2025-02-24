UNIVERSIDAD DE SAN CARLOS DE GUATEMALA

FACULTAD DE INGENIERIA

ESTRUCTURA DE DATOS

CATEDRÁTICO: ING. ESPINO

============================================================================================

  
  <div style="text-align: center;">
    <h1 style="color: blue; text-decoration: underline; text-decoration-color: black;" >MANUAL TÉCNICO</h1>
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

<br>
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

Importaciones
