﻿using System;
using System.Security.Cryptography.X509Certificates;
using Models.Models;

namespace SalesAdventure3000
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DrawWorld();

            static void DrawWorld()
            {
                string map =
                ":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.##################" +
                ":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.################:.:.:." +
                ":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.##########:.:.:.:.:." +
                ":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.######:.:.:.:.:.:." +
                ":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.##:.:.:.:.:.:.:." +
                "##:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                "####:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                "########:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                "##########:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                "################:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                "####################:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                ":.##################:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                ":.:.:.:.##############:.:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                ":.:.:.:.:.:.:.:.:.########:.:.:.:.:.:.:.:.:.:.:.:.:." +
                ":.:.:.:.:.:.:.:.:.:.:.##:.:.:.:.:.:.:.:.:.:.:.:.:.:." +
                ":.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.:.";

                for (int i = 0; i < 15; i++)
                {
                    for(int j = 0; j < 25; j++)
                    {
                        Console.Write(":.");
                    }
                    Console.WriteLine();
                }
            }
            Tile ruta = new Tile();
        }
    }
}