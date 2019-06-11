using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using jsonSerz;
namespace Task3
{

    class Program
    {
        static bool IsString(string str)
        {
            foreach (char c in str)
            {
                if (c >= '0' && c <= '9')
                    return false;
            }

            return true;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Enter the city name/ID or \"end\" to stop."); //Asking user for the town name/ID
            string town = Console.ReadLine();
            while(town.ToLower() != "end" && town != "")//checking if he want to exit
            {
                if(!IsString(town))
                {
                    Console.WriteLine("Wrong input! Try again.");
                    Console.WriteLine("Enter the city name or \"end\" to stop.");
                    town = Console.ReadLine(); //Cicle 
                    continue;
                }
                try //bad try-catch just for safety 
                {
                    WebRequest request = WebRequest.Create("http://api.openweathermap.org/data/2.5/weather?q=" + town + "&APPID=a8b409a9e419b7abfdcd4f89af8c4893");
                    WebResponse response = request.GetResponse();
                    //Standart request to URI
                    using (StreamReader stream = new StreamReader(response.GetResponseStream()))
                    {
                        string info = stream.ReadToEnd();
                        Serialize serialize = new Serialize(info); //asking library to deserialize what we get
                        try //Try-catch saving from wrong names and file path problems 
                        {
                            Console.WriteLine(serialize.ToString()); //trying to write and save our results
                            serialize.writeInfo();
                        }
                        catch (Exception) { Console.WriteLine("There is no Town with this name/ID, or file info.txt is in use."); }
                    }
                }
                catch (Exception)
                {
                    Console.WriteLine("Something gone wrong, try again!");
                }
                Console.WriteLine("Enter the city name or \"end\" to stop.");
                town = Console.ReadLine(); //Cicle 
            }
                
        }
    }
}
