using ConsoleAppMarsRover.BussinesCore;
using ConsoleAppMarsRover.Contracts;
using ConsoleAppMarsRover.Enums;
using ConsoleAppMarsRover.Models;
using System;
using System.Collections.Generic;

namespace ConsoleAppMarsRover
{
    class Program
    {
        private const int rangeRover = 1;
        private const CardinalEnum initDirection = CardinalEnum.N;
        private const int nInit = 0;
        private const int eInit = 0;       

        static void Main(string[] args)
        {


            //first line plateau limit
            GridPlateau plateau = CreatePlateau();
            bool exit = false;
            do
            {
                //proccess rover commands
                ProccessRover(plateau);
                bool solicitResponse = false;
                do
                {                   
                    Console.WriteLine("Do you want to add another Rover? Y/N");
                    string response = Console.ReadLine();
                    if (response == "N")
                    {
                       exit = true;
                       break;
                    }
                    else
                   {
                        //continues main cicle
                        if (response == "Y")
                        {
                            solicitResponse = false;
                            exit = false;
                        }
                        else
                        {
                            Console.WriteLine("Wrong response, please try again");
                            solicitResponse = true;
                        }
                            
                    }
                } while (solicitResponse == true);

            } while (exit == false);


        }

        /// <summary>
        /// This method procces the  Rover
        /// Executes in a loop, frist read the position. If position is wrong, read the data again.
        /// Then executes command. After each command execution, ask to user if want to continue with another command.
        /// </summary>
        private static void ProccessRover(GridPlateau plateau)

        {
            IRover rover = RoverController.CreateRover(plateau.origin, initDirection, rangeRover);
            IRoverController controller = new RoverController(rover, plateau);
            bool avanza = true;
            do
            {
                //Read position
                string initPosition = Console.ReadLine();
                var result = controller.confirmPosition(initPosition);
                if (result.result == false)
                {
                    Console.WriteLine(result.Message);
                    Console.WriteLine("Try enter position again");
                    avanza = false;
                    continue;
                }
                else
                {
                    //Executes command
                    string finalPosition = ProccessCommand(controller);
                    Console.WriteLine(finalPosition);
                    bool solicitResponse = false;
                    do
                    {
                                               
                        Console.WriteLine("Do you want to execute another command for this Rover? Y/N");
                        string response = Console.ReadLine();
                        if (response == "N")
                        {
                            plateau.rovers.Add(rover);
                            return;
                        }
                        else
                        {
                            if (response == "Y")
                            {
                                solicitResponse = false;
                                avanza = false;
                            }
                            else
                            {
                                Console.WriteLine("Wrong response, please try again");
                                solicitResponse = true;
                            }
                                
                        }
                    } while (solicitResponse == true);
                }

            } while (avanza == false);
        }

        /// <summary>
        /// This method procces the line containg the batch. 
        /// Executes ProccessBatch on controller and return actual position after the batch execution
        /// </summary>
        private static string ProccessCommand(IRoverController controller)

        {
            string command = Console.ReadLine();
            controller.ProccessBatch(command);            
            return controller.GetRoverPosition();
        }

        /// <summary>
        /// This method creates the Plateau. If the input data is in a wrong format, executes the reading again.
        /// </summary>
        private static GridPlateau CreatePlateau()
        {
            GridPlateau plateau;
            bool avanza = true;
            do {               
                var limit = Console.ReadLine();                
                var pControl = new GridPlateauController();
                Point origin = new Point(nInit, eInit);
                var result = pControl.CreateGridPlateau(origin, limit);
                if (result.result == false)
                {
                    Console.WriteLine(result.Message);
                    Console.WriteLine("Try again");
                    avanza = false;
                }
                else
                {
                    plateau = pControl.plateau;
                    return plateau;
                   
                }
            } while (avanza==false);
            return null;
        }

       
    }
}
