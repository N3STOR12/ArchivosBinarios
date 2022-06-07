using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace ArchivosBinarios
{
    class ArchivosBinarioEmpleados
    {
        //Declaracion de flujos 
        BinaryWriter bw = null; //Flujo de salida - escritura de datos 
        BinaryWriter br = null;// Fujo de entrada - lectura datos 

        //Campos de la clase 
        string Nombre, Direccion;
        long Telefono;
        int NumEmp, DiasTrabajados;
        float SalarioDiario;

        public void CrearArchivo(string Archivo)
        {
            //Variable local metodo
            char resp;

            try
            {
                //Creacion del flujo para escribir datos al archivo 
                bw = new BinaryWriter(new FileStream(Archivo, FileMode.Create, FileAccess.Write));

                //captura de datos 
                do
                {
                    Console.Clear();
                    Console.Write("Numero del empleado: ");
                    NumEmp = Int32.Parse(Console.ReadLine());

                    Console.Write("Nombre del empleado: ");
                    Nombre = Console.ReadLine();

                    Console.Write("Direccion del empleado: ");
                    Direccion = Console.ReadLine();

                    Console.Write("Telefono del empleado: ");
                    Telefono = Int64.Parse(Console.ReadLine());

                    Console.Write("Dias trabajados: ");
                    DiasTrabajados = Int32.Parse(Console.ReadLine());

                    Console.Write("Salario del empleado: ");
                    SalarioDiario = Single.Parse(Console.ReadLine());

                    //Escribe los datos del archivo 
                    bw.Write(NumEmp);
                    bw.Write(Nombre);
                    bw.Write(Direccion);
                    bw.Write(Telefono);
                    bw.Write(DiasTrabajados);
                    bw.Write(SalarioDiario);

                    Console.Write("\n\n Deseas almacenar otro registro (s/n)");
                    resp = char.Parse(Console.ReadLine());
                } while ((resp != 's') || (resp != 's'));
            }
            catch (IOException ex)
            {
                Console.WriteLine("\nError: " + ex.Message);
                Console.WriteLine("\nRuta: " + ex.StackTrace);
            }
            finally
            {
                if(bw != null) bw.Close();
                //Cierra el flujo - escritura de datos y refresea al menu
                Console.ReadKey();
            }  
       }


        public void MostrarArchivo(string Archivos)
        {
            try
            {
                //Verifica si existe el archivo 
                if (File.Exists(Archivos))
                {
                    BinaryReader
                  //Creacion flujo para leer datos del archivo 
                  br = new BinaryReader(new FileStream(Archivos, FileMode.Open, FileAccess.Read));

                    //Despliegue datos de consola 
                    Console.Clear();
                    do
                    {
                        //Lectura de registros mientras no llegue a EndOfFile
                        NumEmp = br.ReadInt32();
                        Nombre = br.ReadString();
                        Direccion = br.ReadString();
                        Telefono = br.ReadInt64();
                        DiasTrabajados = br.ReadInt32();
                        SalarioDiario = br.ReadSingle();

                        //Muestra los datos 
                        Console.WriteLine("Numero del empleado: " + NumEmp);
                        Console.WriteLine("Nombre del empleado: " + Nombre);
                        Console.WriteLine("Direccion del emplado: " + Direccion);
                        Console.WriteLine("Telefono del empleado: " + Telefono);
                        Console.WriteLine("Dias trabajados del empleado: " + DiasTrabajados);
                        Console.WriteLine("Sueldo total del empleado: " + SalarioDiario * DiasTrabajados);
                    } while (true);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\n El archivo" + Archivos + "No existe.");
                    Console.WriteLine("Presione Enter para continuar.");
                    Console.ReadKey();
                }
            }
            catch (EndOfStreamException)
            {
                Console.WriteLine("\n\n Fin del listado de empleados");
                Console.Write("\nPresione Enter para continuar.");
                Console.ReadKey();
            }
            finally
            {
                if (br != null) br.Close(); //cierre de flujo 
                Console.Write("\nPresione Enter para terminar la lectura de datos y regresar al menu.");
                Console.ReadKey();
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            //Declaracion variables auxiliares 
            string Arch = null;
            int opc; 

            //Creacion del objeto
            ArchivosBinarioEmpleados a1 = new ArchivosBinarioEmpleados();

            //Menu de opciones
            do
            {
                Console.Clear();
                Console.WriteLine("\n ------ Archivo Binario Empleados ------");
                Console.WriteLine("1- Creacion del archivo");
                Console.WriteLine("2- Lectura del archivo");
                Console.WriteLine("3- Salida del programa");
                Console.Write("Seleccione la opc que desea: ");
                opc = Int16.Parse(Console.ReadLine());

                switch (opc)
                {
                    case 1:
                        try
                        {
                            //Captura nombre archivo 
                            Console.Write("\nAlimenta el Nombre del archivo a crear: ");
                            Arch = Console.ReadLine();

                            //Verifica si existe el archivo 
                            char resp = 's';
                            if (File.Exists(Arch))
                            {
                                Console.Write("\nEl Archivo existe, deseas sobreescribirlo (s/n)");
                                resp =  char.Parse(Console.ReadLine());
                            }
                            if((resp == 's') || ((resp == 'S')))  a1.CrearArchivo(Arch);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            Console.WriteLine("Ruta: " + ex.StackTrace);
                        }
                        break;

                       case 2:
                        //Bloque de lectura
                        try
                        {
                            //Captura del nombre 
                            Console.Write("Alimenta el Nombre del Archivo:");
                            Arch = Console.ReadLine();
                            a1.MostrarArchivo(Arch);
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine("Error: " + ex.Message);
                            Console.WriteLine("Ruta: " + ex.Message);
                        }
                        break;
                    case 3:
                        Console.Write("\nPresione <enter> para Salir del Programa.");
                        Console.ReadKey();
                        break;

                        default:
                        Console.WriteLine("\nEsa opcion no existe, presione enter para continuar");
                        Console.ReadKey();
                        break;
                }
            }while (opc != 3);
        }
    }
}
