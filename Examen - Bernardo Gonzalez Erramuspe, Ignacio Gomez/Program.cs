using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Examen___Bernardo_Gonzalez_Erramuspe__Ignacio_Gomez
{
    internal class Program
    {
        static Biblioteca biblioteca = new Biblioteca();
        static void Main(string[] args)
        {
            MenuInicio();
            Console.WriteLine("Ingrese Nombre de la Bibloteca: ");
            biblioteca.Nombre = Console.ReadLine();
            do
            {
                ProcesarMenu(MenuOpciones());
            } while (true);
        }

        static void MenuInicio() {
            Console.WriteLine("Examen -  Bibloteca ");
            Console.WriteLine("Alumnos: Bernardo Andres Gonzalez Erramuspe, Ignacio Gomez");
            Console.WriteLine("");
            Console.WriteLine("Presione cualquier tecla para continuar...");
            Console.ReadKey();
            Console.Clear();
        }
        static int MenuOpciones() {
            Console.Clear();
            Console.WriteLine($"Biblioteca: {biblioteca.Nombre}.");
            Console.WriteLine(@"Ingrese una opcion a realizar.
1- Registrar ejemplar
2- Registrar préstamo
3- Registrar devolución
4- Consultar disponibilidad
5- Listado de ejemplares pendientes de devolución
6- Listado de ejemplares prestados
7- Salir");
            return int.Parse(Console.ReadLine());
        }

        static void ProcesarMenu(int Sel) {
            switch (Sel) {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Ingrese el Tipo de Ejemplar:");
                    Console.WriteLine("1 - DVD");
                    Console.WriteLine("2 - Libro");
                    Console.WriteLine("3 - Revista");
                    switch (int.Parse(Console.ReadLine())) {
                        case 1:
                            biblioteca.AniadirEjemplar(RegistrarDVD());
                            break;
                        case 2:
                            biblioteca.AniadirEjemplar(RegistrarLibro());
                            break;
                        case 3:
                            biblioteca.AniadirEjemplar(RegistrarRevista());
                            break;
                        default:
                            break;
                    }
                    break;
                case 2:
                    Prestamo pre = RegistrarPrestamo();
                    if(pre != null)
                    {
                        biblioteca.AniadirPrestamo(pre);
                    }
                    break;
                case 3:
                    RegistrarDevolucion();
                    break;
                case 4:
                    ConsultarDisponibilidad();
                    break;
                case 5:
                    ListadoPendientesAtrasado();
                    break;
                case 6:
                    EjemplaresPrestados();
                    break;
                case 7:
                    Environment.Exit(0);
                    break;
                default:
                    break;
            }
        }

        static void RegistrarEjemplar(Ejemplar ejemplar) {
            Console.Clear();
            Console.WriteLine("Ingresar el Codigo del Ejemplar:");
            ejemplar.Codigo = int.Parse(Console.ReadLine());
            foreach (Prestamo pre in biblioteca.Prestamos) { 
                if( ejemplar.Codigo == pre.CodigoEjemplar && pre.Devuelto == false)
                {
                    Console.WriteLine("El ejemplar no esta disponible.");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("Ingresar el Titulo del Ejemplar:");
            ejemplar.Titulo = Console.ReadLine();
            Console.WriteLine("Ingresar el Autor del Ejemplar:");
            ejemplar.Autor = Console.ReadLine();
            Console.WriteLine("Ingrese el Genero del Ejemplar:");
            ejemplar.Genero = Console.ReadLine();
        }

        static DVD RegistrarDVD() {
            DVD dvd = new DVD();
            RegistrarEjemplar(dvd);
            Console.WriteLine("Ingrese la Duración del DVD:");
            dvd.Duracion = int.Parse(Console.ReadLine());
            return dvd;
        }

        static Libro RegistrarLibro() {
            Libro libro = new Libro();
            RegistrarEjemplar(libro);
            Console.WriteLine("Ingrese el ISBN del Libro:");
            libro.ISBN = Console.ReadLine();
            Console.WriteLine("Ingrese el Año de la Publicacion:");
            libro.AnioPublicacion = Console.ReadLine();
            return libro;
        }

        static Revista RegistrarRevista() {
            Revista revista = new Revista();
            RegistrarEjemplar(revista);
            Console.WriteLine("Ingrese la Fecha de la Revista:");
            revista.Fecha = DateTime.Parse(Console.ReadLine());
            Console.WriteLine("Ingrese el Numero de la Revista:");
            revista.Numero = int.Parse(Console.ReadLine());
            return revista;
        }

        static Prestamo RegistrarPrestamo() {
            Prestamo prestamo = new Prestamo();
            Console.Clear();
            Console.WriteLine("Ingrese Codigo Ejemplar del Prestamo:");
            prestamo.CodigoEjemplar = int.Parse(Console.ReadLine());
            foreach (Ejemplar ejm in biblioteca.Ejemplares) {
                if (ejm.Codigo == prestamo.CodigoEjemplar)
                {
                    Console.WriteLine("Ingrese Fecha del Prestamo:");
                    prestamo.Fecha = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese Fecha esperada de Devolucion del Prestamo:");
                    prestamo.FechaDevolucion = DateTime.Parse(Console.ReadLine());
                    Console.WriteLine("Ingrese Socio al que se le presto el Ejemplar:");
                    prestamo.Socio = Console.ReadLine();
                    return prestamo;
                }
            }
            Console.WriteLine("No se encontro el Ejemplar");
            Console.ReadKey();
            return null;
        }

        static void RegistrarDevolucion() {
            Console.Clear();
            Console.WriteLine("Ingrese el Codigo del Ejemplar a Devolver:");
            int Codigo = int.Parse(Console.ReadLine());
            foreach (Prestamo pre in biblioteca.Prestamos) {
                if (pre.CodigoEjemplar == Codigo) {
                    pre.Devuelto = true;
                    Console.WriteLine("Se registro la devolución del Ejemplar: " + Codigo + ".");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("El Ejemplar no esta en Prestamo o no existe");
            Console.ReadKey();
        }

        static void ConsultarDisponibilidad() {
            Console.Clear();
            Console.WriteLine("Ingrese el codigo del Ejemplar:");
            int Codigo = int.Parse(Console.ReadLine());
            foreach(Prestamo pre in biblioteca.Prestamos)
            {
                if(pre.CodigoEjemplar == Codigo)
                {
                    if(pre.Devuelto == false){
                        Console.WriteLine("El ejemplar no se encuentra disponible.");
                        Console.ReadKey();
                        return;
                    }

                }
            }
            foreach(Ejemplar ej in biblioteca.Ejemplares)
            {
                if (Codigo == ej.Codigo)
                {
                    Console.WriteLine("El ejemplar se encuentra disponible.");
                    Console.ReadKey();
                    return;
                }
            }
            Console.WriteLine("No se encontro el Ejemplar.");
            Console.ReadKey();
            return;
        }

        static void ListadoPendientesAtrasado() {
            Console.Clear();
            Console.WriteLine("La lista de Prestamos atrasados son:");
            foreach (Prestamo pre in biblioteca.Prestamos) {
                if (pre.Devuelto == false) {
                    if (pre.FechaDevolucion < DateTime.Now) {
                        Console.WriteLine($"El Ejemplar ({pre.CodigoEjemplar}) del {pre.Socio} esta Pendiente y Atrasado");
                    }
                }
            }
            Console.ReadKey();
        }

        static void EjemplaresPrestados() {
            Console.Clear();
            Console.WriteLine("La lista de Ejemplares prestados son:");
            foreach(Ejemplar ejm in biblioteca.Ejemplares)
            {
                foreach(Prestamo pre in biblioteca.Prestamos)
                {
                    if(pre.CodigoEjemplar == ejm.Codigo && pre.Devuelto == false)
                    {
                        Console.WriteLine($"El ejemplar {ejm.Codigo} esta en Prestamo.");
                    }
                }
            }
            Console.ReadKey();
        }
    }
}
