using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleChess
{
    static class SaveLoad
    {
        public static void Save(Game game)
        {
            string fileName = "State.json";
            if(!System.IO.File.Exists(fileName))
            {
                using FileStream createStream = File.Create(fileName);
            }
            string jsonString = JsonSerializer.Serialize(game);
            File.WriteAllText(fileName, jsonString);
            Console.WriteLine(File.ReadAllText(fileName));
        }

        //public static Game Load()
        //{
        //    Game? game =
        //       JsonSerializer.Deserialize<Game>(jsonString);

        //}
    }
}
