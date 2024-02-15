using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace SalesAdventure3000_UI
{
    public class Startscreen
    {
        public static int SelectedCommand { get; set; }

        public Startscreen() 
        { 
            SelectedCommand = 0;
        }

        public void PrintStartMenu()
        {
            string[] commands = { "New game", "Load game", "Save game", "Exit" };

            Console.WriteLine("\n\t    R\n" +
                                "\t   E\n" +
                                "\t  P  SalesAdventure 3000\n" +
                                "\t U\n" +
                                "\tS\n\n" +
                                "".PadRight(42*2, '-') + "\n\n");
            for (int i = 0; i < commands.Length; i++)
            {
                string[] selection = i == SelectedCommand ? new string[] {"[","]"} : new string[] { " "," "};
                Console.Write($"\t{selection[0]}{commands[i]}{selection[1]}\n\n");
            }

            int input = Convert.ToInt32(Console.ReadLine());
            switch (input)
            {
                default:
                    break;
            }
        }
    }
}
