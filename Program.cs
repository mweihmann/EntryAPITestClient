using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EntryAPITestClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // client als variable welche http requests und responses via uri handelt
            HttpClient client = new HttpClient();
            
            Console.WriteLine("Enter the ID:");
            // die variable int nimmt die ID für den http response entgegen
            int id = Convert.ToInt32(Console.ReadLine());

            // in die variable responsetask wird der HTTPClient Task GetAsync geschrieben und dann auf die ausführung mit .wait gewartet
            var responseTask = client.GetAsync("http://localhost:1254/api/entries/" + id);

            responseTask.Wait();
            // wenn responseTask fertig ist erhält result das ergebnis
            if (responseTask.IsCompleted)
            {
                var result = responseTask.Result;
                // ist das ergebnis ein erfolgreicher http response -
                if (result.IsSuccessStatusCode)
                {
                    //wird in messageTask das ergebnis bzw der inhalt des http content nach einer umwandlung in einen string geschrieben
                    var messageTask = result.Content.ReadAsStringAsync();
                    messageTask.Wait();
                    //nach dem wait für die umwandlung in einen string wird die nachricht angezeigt.
                    Console.WriteLine("Message from WebAPI :" + messageTask.Result);
                    Console.ReadLine();
                }
            }



        }
    }
}
