# CampusNet – Red Social Académica

## ¿De qué trata el proyecto?

CampusNet es una pequeña red social académica desarrollada en C#. La idea del proyecto es representar cómo los usuarios de una comunidad universitaria pueden seguir a otras personas mediante un grafo dirigido.

En este sistema, cada usuario representa un vértice y cada relación de seguimiento representa una arista. Por ejemplo, si Ana sigue a Carlos, se crea una conexión de Ana hacia Carlos. Sin embargo, esto no significa que Carlos también siga a Ana.

El objetivo es aplicar los conceptos de grafos dirigidos, sus recorridos y sus operaciones principales, utilizando una estructura organizada bajo el patrón MVC.

## Tecnologías utilizadas

- C# como lenguaje de programación.
- .NET 10 para ejecutar la aplicación.
- Visual Studio Code como entorno de desarrollo.
- Git y GitHub para el control de versiones y almacenamiento del proyecto.

## Organización del proyecto

Para desarrollar CampusNet se utilizó la arquitectura Modelo-Vista-Controlador (MVC). De esta manera, cada parte del programa tiene una responsabilidad diferente.

**Models (Modelo)**

En esta carpeta se encuentra la lógica principal del grafo:

- `Vertex.cs`: representa a cada usuario, almacenando su identificador, nombre y rol.
- `Edge.cs`: representa las relaciones entre los usuarios.
- `Graph.cs`: contiene la estructura del grafo y los métodos para agregar, eliminar y actualizar usuarios y relaciones, además de realizar los recorridos y consultas.

**Views (Vista)**

- `ConsoleView.cs`: se encarga de mostrar en la consola la información del grafo y los resultados de las operaciones.

**Controllers (Controlador)**

- `GraphController.cs`: coordina el funcionamiento del programa. Construye el grafo inicial, ejecuta las consultas y comunica el modelo con la vista.

**Program.cs**

Es el punto de entrada de la aplicación. Desde este archivo se inicia el controlador principal.

## ¿Qué puede hacer CampusNet?

El programa cuenta con las siguientes funcionalidades:

### Construcción de la red

- Crear un grafo dirigido.
- Registrar 12 usuarios con diferentes roles: estudiantes, profesores y egresados.
- Establecer 18 relaciones de seguimiento.
- Mostrar las conexiones mediante una lista de adyacencia.

### Recorridos del grafo

- Realizar recorridos BFS desde tres usuarios diferentes.
- Realizar un recorrido DFS completo.
- Mostrar el orden en que se visitan los usuarios.
- Detectar la presencia de ciclos dirigidos.

### Consultas sobre la red social

- Encontrar usuarios que no tienen seguidores.
- Identificar al usuario con mayor número de seguidores.
- Identificar al usuario que sigue a más personas.
- Comprobar si existe un camino entre dos usuarios mediante BFS.

### Operaciones CRUD

También se implementaron operaciones para administrar los datos de la red:

- Agregar usuarios.
- Agregar relaciones entre usuarios.
- Actualizar el nombre y el rol de un usuario.
- Eliminar relaciones.
- Eliminar usuarios junto con sus relaciones asociadas.

## Estructura de