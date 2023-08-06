using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace BankConsole;

/*
       Hecho por: David Alejandro Gonzalez Velazquez.

*/
public class User{ //Creamos la clase User la cual usaremos para hacer todas las peticiones que nesesitemos.
    
    public int ID {get; set;}                 /*  Son los atributos que identifican a un Usuario.    */
    [JsonProperty]
    private String Nombre {get; set;}
    [JsonProperty]
    private String Email {get; set;}
    [JsonProperty]
    private double Saldo {get; set;}
    [JsonProperty]
    private String Rol {get; set;}

     private static readonly List<User> listaUsuarios = new();

    
    public void Menu(){  //Este es el Menu principal donde se hacen todas las cosas que se pueden hacer. 
        Boolean salir=true;
        int opcion;
            while(salir){
                
                Console.WriteLine("-----Banco Cedis-------");
                Console.WriteLine("Selecione una opcion:\n 1.Dar de alta un usuario.\n 2.Dar de baja un usuario.\n 3.Salir.\n Sleccione su respuesta:");
                opcion =int.Parse(Console.ReadLine());
                Opcion(opcion);
                salir= Salir();
            }

    }

    public void Opcion(int opcion){ //Aqui es donde se ramifica para resolver ya sea para Crear o Borrar.
        

        switch(opcion){
            case 1:
                AltaUsuario(listaUsuarios);
                break;
            case 2:
                BorrarUsuario(listaUsuarios);
                break;
            case 3:
                break;
            default:
                Console.WriteLine("\nNo existe esa opcion favor de leer bien el menu.");
                break;
        }
    }


        public void AltaUsuario(List<User> listaUsuarios){ //Como su nombre lo indica crea a un usuario con sus respectivas validaciones. 
            Boolean salir = true;

            while(salir){
                Console.Clear();
                Console.WriteLine("\nIngres el id del usuario");
                var id= int.Parse(Console.ReadLine());
                if (listaUsuarios.Any(usuario => usuario.ID == id) || id<0)
        {
            Console.WriteLine("\nEl ID del usuario ya está registrado. Intente nuevamente.");
            Console.ReadKey();
            continue;
        }
                Console.WriteLine("\nIngres su nombre");
                var nombre= Console.ReadLine();

                string emailPattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
                Console.WriteLine("\nIngrese su email");
                var email= Console.ReadLine();
                if (!Regex.IsMatch(email, emailPattern))
        {
            Console.WriteLine("\nEl email ingresado no es válido. Intente nuevamente.");
            Console.ReadKey();
            continue;
        }
                
                Console.WriteLine("\nIngres el Saldo del Cliente");
                var saldo= double.Parse(Console.ReadLine());
                if(saldo <0)
                {
                Console.WriteLine("\nError, ingrese bien porfavor su saldo.");
                Console.ReadKey();
                continue;
                }


                Console.WriteLine("\nIngrese que rol es Empleado e  o Cliente c");
                String rol= Console.ReadLine();
                rol= rol.ToLower();
            if ( rol != "e" && rol != "c")
                {
                Console.WriteLine("\nError, Solo se puede ingresar c o e si es Cliente o Empleado respectivamente.");
                Console.ReadKey();
                continue;
                }

            User user1 = new()
            {
                ID = id,
                Nombre = nombre,
                Email = email,
                Saldo = saldo,
                Rol = rol
            };

            Storage.Adduser(user1); //Aqui lo agrega en el json.
                listaUsuarios.Add(user1);
                salir= Salir();
            }

        }

        public void BorrarUsuario(List<User> lista1){ //Como su nombre lo indica borra al usuario y tambien en el json.

            Console.WriteLine("\nIngrese el Id del usuario ");
            var id = int.Parse(Console.ReadLine());
            User usuarioBorrar= EncontrarUsuario(lista1, id);
            if(usuarioBorrar != null)
            {
                lista1.Remove(usuarioBorrar);
                Storage.Deleteuser(id);

            }
         }

    public bool Salir(){ //esta funcion solo pregunta si quieres seguir en el menu.

        Console.WriteLine("Desea salir? SI=1 NO=0\n Ingrese su opcion:");
        var res = int.Parse(Console.ReadLine());

        if (res != 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public User EncontrarUsuario(List<User> lista1, int id){ //Esta funcion sirve para encontrar a un usuario y despues eliminarlo en la lista igual en el json.
        bool encontrado= false;
        User usuario2 = null;

        foreach(User Usaurio1 in lista1){
              if(Usaurio1.ID == id){
                encontrado= true;
                usuario2= Usaurio1;
                break;
              }
        }

        if(!encontrado){
            Console.WriteLine($"\nUsuario con el ID {id} no está registrado");
            Console.ReadKey();
        }

        return usuario2;
    }

    public void ImprimirListaDeUsuarios(List<User> lista) //Este solo imprime a los usuarios es para saber si se estan insertando correctamente.
    {
        foreach (User usuario in lista)
        {
            Console.WriteLine($"ID: {usuario.ID}, Nombre: {usuario.Nombre}, Email: {usuario.Email}, Saldo: {usuario.Saldo}, Rol: {usuario.Rol}");
        }
    }

}