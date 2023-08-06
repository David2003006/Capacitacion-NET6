using Newtonsoft.Json;

namespace BankConsole;

public static class Storage
{

    static string filepath = AppDomain.CurrentDomain.BaseDirectory + @"\users.json";

public static void Adduser(User user) //Esta funcion ingresa a un usuario en el json con los atributos ya dichos
{ 
    string json="", userInfile="";

    if(File.Exists(filepath))
        userInfile= File.ReadAllText(filepath);

    var Listuser= JsonConvert.DeserializeObject<List<User>>(userInfile);

    if (Listuser == null)
    {
        Listuser= new List<User>();
    }

    Listuser.Add(user);

    json= JsonConvert.SerializeObject(Listuser);

    File.WriteAllText(filepath, json);
}

public static void Deleteuser(int id) //Esta funcion elimina a los usuarios del json ya mencionado
{
   string json = "", userInfile = "";

            if (File.Exists(filepath))
                userInfile = File.ReadAllText(filepath);

            var Listuser = JsonConvert.DeserializeObject<List<User>>(userInfile);

            if (Listuser == null)
            {
                Console.WriteLine("No hay usuarios registrados en el archivo.");
                Console.ReadKey();
                return;
            }

            var userToDelete = Listuser.FirstOrDefault(u => u.ID == id);

            if (userToDelete == null)
            {
                Console.WriteLine($"No se encontró ningún usuario con el ID {id}.");
                Console.ReadKey();
                return;
            }

            Listuser.Remove(userToDelete);

            json = JsonConvert.SerializeObject(Listuser);

            File.WriteAllText(filepath, json);

            Console.WriteLine($"Usuario con ID {id} eliminado correctamente.");

}


}