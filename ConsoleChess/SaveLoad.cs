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
        private readonly static string fileName = "State.json";
        public static void Save(Game game)
        {
            if(!System.IO.File.Exists(fileName))
            {
                using FileStream createStream = File.Create(fileName);
            }
            string jsonString = JsonSerializer.Serialize(game);
            File.WriteAllText(fileName, jsonString);
            //Console.WriteLine(File.ReadAllText(fileName)); //for debug
        }

        public static Game Load()
        {
            string jsonstring = "";
            using (StreamReader reader = new StreamReader(fileName))
            {
                jsonstring += reader.ReadLine();
            }
            Game game =
               JsonSerializer.Deserialize<Game>(jsonstring);
            return game;
        }
    }
}
