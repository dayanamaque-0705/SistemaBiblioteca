# SistemaBiblioteca

## Descripción General del Sistema
Este proyecto es una aplicación de consola desarrollada en C# (.NET 10) que simula de manera integral la administración de una biblioteca pública. El programa gestiona el catálogo de libros, el registro de usuarios y el control en tiempo real de los préstamos y devoluciones de ejemplares.

## Objetivos y Arquitectura Aplicada
- Programación Orientada a Objetos (POO): modelado de entidades principales (`Libro`, `Usuario`, `Prestamo`) aplicando el principio de abstracción, encapsulamiento e implementación de interfaces (`IPrestable`).
- Control de Excepciones: uso de un bloque `try/catch` global dentro del menú interactivo para capturar excepciones de negocio (`InvalidOperationException`, `KeyNotFoundException`) e informar al usuario sin detener el programa.

## Requisitos Previos e Instalación

### Requisitos Técnicos
- SDK de .NET 8.0 o .NET 10 instalado.
- Git (herramienta de control de versiones).
- Editor de código recomendado: Visual Studio Code o Visual Studio 2022.

### Pasos para Ejecutar Localmente
```bash
git clone https://github.com/dayanamaque-0705/SistemaBiblioteca.git
cd SistemaBiblioteca
dotnet run
```

## Menú Interactivo de la Aplicación
Al iniciar la aplicación con `dotnet run`, el usuario visualizará la siguiente interfaz interactiva de 10 opciones:

```text
----------------------
    SISTEMA DE GESTIÓN DE BIBLIOTECA
1. Registrar un nuevo libro
2. Registrar un nuevo usuario
3. Ver libros (ordenados por título)
4. Buscar libros (por autor o categoría)
5. Ver qué libros están disponibles
6. Realizar un préstamo
7. Registrar la devolución de un libro
8. Ver préstamos activos en este momento
9. Eliminar un libro
10. Salir del sistema
-----------------------
```

## Guía Detallada y Literal de Prueba Paso a Paso
Siga este flujo de prueba paso a paso para verificar cada funcionalidad y regla de negocio del sistema durante la evaluación.

### Registro de Libros (Opción 1) y Control de Duplicados
Paso 1.1: Registrar el primer libro correctamente

Entradas requeridas:
- ¿Cuál es el título?
- ¿Quién es el autor?
- ¿A qué categoría pertenece?
- Ingresa su código único

Nota:
- Si el código ya fue registrado, no podrá registrar.

### Registro de Usuarios (Opción 2)
Entradas requeridas:
- Ingresa su número de ID
- Nombre completo
- Correo electrónico

Nota:
- Si el número de ID ya existe, no se podrá registrar.

### Consultas, Ordenamiento y Búsqueda de Libros (Opciones 3, 4 y 5)
1. Ver libros ordenados por título (Opción 3)
2. Buscar libros por Autor o Categoría (Opción 4)
   - Entrada: ingrese la categoría o un autor del libro que busca
3. Ver libros disponibles (Opción 5)

### Control de Préstamos y Devoluciones (Opciones 6, 8 y 7)
1. Prestar un libro disponible (Opción 6)
   - Entradas:
     - Código del libro a prestar
     - ID del usuario que lo solicita
2. Validar intento de prestar un libro ya prestado
   - Datos a ingresar:
     - Código del libro
     - ID del usuario
3. Ver préstamos activos (Opción 7)
   - Se observarán los préstamos que siguen en curso.
4. Devolución de libro (Opción 8)
   - Entrada requerida: ingresa el código del libro que están devolviendo.

### Eliminación de Libro y Salida (Opción 9)
1. Eliminar un libro del sistema
   - Entrada: código del libro
2. Salida limpia del programa (Opción 10)

## Reglas de Negocio Implementadas
- No se permite duplicar códigos de libros ni identificadores de usuario.
- No se puede prestar un libro cuyo atributo `Disponible` sea `false`.
- No se puede registrar la devolución de un libro que no se encuentre actualmente en la lista de préstamos activos.

## Estructura de Desarrollo en Git / GitHub
- `main`: rama principal que contiene la versión estable y funcional final.
- `feature/libros`: módulo del catálogo y operaciones CRUD de libros.
- `feature/usuarios`: módulo de administración de usuarios.
- `feature/prestamos`: lógica de transacciones con record, interfaz interactiva en `Program.cs` y fusión final vía PR hacia `main`.

## Notas Finales
Este proyecto fue desarrollado como una práctica de gestión bibliotecaria con enfoque en programación orientada a objetos, validación de reglas de negocio y experiencia de usuario en consola.
