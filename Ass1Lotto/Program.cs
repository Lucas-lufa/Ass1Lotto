using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ass1Lotto
{
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
             * ask how many numbers and what is the random number range,
             * default will be five numbers and a range of fifty
             * need to validate that input is good.
             * 
             * Ctrl + k then Ctrl + c to comment out
             * to undo ctrl + k then Ctrl + u
            */
            string userInput = "";
            int howMany;//the amount of numbers played
            int ifAny = 0; //the max range of the random pool            
            // Input valadation 
            int theDefault = 5;
            // int aDefault = 50;
            int lowestValue = 0;
            int highestValue = 10;

            Random random = new Random();

            Console.WriteLine("Please enter how many number to be played.");
            Console.WriteLine("If left blank the default will be five. ");
            Console.WriteLine("If enter slickpick all option will be selected by random");
            userInput = Console.ReadLine();
            howMany = InputValadation(userInput, theDefault, highestValue);
            if (howMany < 0 )
            {
                ifAny = howMany;
                howMany = random.Next(lowestValue, highestValue);
            }
            Console.WriteLine($"Numbers Played will be {howMany}\n");
            if (ifAny == 0)// at this point if ifany is at zero will ask for input
            {
                Console.WriteLine("Please enter the pool the numbers will be picked from.");
                Console.WriteLine("If left blank the default will be one hundred. ");
                Console.WriteLine("If enter slickpick all option will be selected by random");
                userInput = Console.ReadLine();
                ifAny = InputValadation(userInput, theDefault = 100);
            }
                        
            if (ifAny < 0)// if ifany is minus the user has slickpick (ramdom) all the options
            {
                lowestValue = 20;
                highestValue = 200;
                ifAny = random.Next(lowestValue, highestValue);
            }
            Console.WriteLine($"Pool to be Played from {ifAny}\n");

            int[] playerArray = new int[howMany];
            int[] TheGameArray = new int[howMany];

            TheGameArray[0] = random.Next(lowestValue = 1, ifAny);

            /* 
             * Now need for loop to populate array with unique numbers
            */
            int counter = 1;
            theDefault = 0;
            int possNumber = 0;
            bool checkDuplicate = true;
            Console.WriteLine($"Choose a number for selection {counter}");
            Console.WriteLine("If left blank will ramdonise the number");
            Console.WriteLine("If enter slickpick all option will be selected by random");
            userInput = Console.ReadLine();
            possNumber = InputValadation(userInput, theDefault, ifAny);
            
            Console.WriteLine(possNumber + " possNumber");
            Console.WriteLine(playerArray[0] + " playerArray[0]\n");

            if ( possNumber > 0 ) 
            {
                playerArray[0] = possNumber;            
            }

            if (possNumber == 0)//if equal to zero, randomises one choice
            {
                playerArray[0] = random.Next(lowestValue = 1, ifAny);
                possNumber = 0;
                Console.WriteLine(playerArray[0] + " playerArray[0]\n");
            }

            if (possNumber < 0 )//if less than zero, slickpick option randomises all choices
            {
                playerArray[0] = random.Next(lowestValue = 1, ifAny);
                possNumber = -1;
                Console.WriteLine(playerArray[0] + " playerArray[0]\n");
            }
            
            // The for loop will three paths in it. 
            // slickpick where it will choose each number at random
            // When left blank will choose one number at rendom
            // player chooses the number
            // each must be unique
             // Ctrl + k then Ctrl + c to comment out
             // to undo ctrl + k then Ctrl + u

            /*
             * Checks possNumber if zero or above will ask for user input if at minus will do 
             * the slickpick routeen
             */
            for (int i = 1; i < playerArray.Length; i++)
            {
                int possTheGame = 0;
                counter++;
                checkDuplicate = true;

                while (checkDuplicate == true)
                    {
                        possTheGame = random.Next(lowestValue = 1, ifAny);
                        checkDuplicate = UniqueIn(playerArray, possNumber);
                    }
                TheGameArray[i] = possTheGame;
                Console.WriteLine("The game number " + possTheGame);
                checkDuplicate = true;

                if (possNumber >= 0)//choice of choosing or randomising a number 
                {
                    Console.WriteLine($"Choose a number for selection {counter}");
                    Console.WriteLine("If left blank will ramdonise the number");
                    Console.WriteLine("If enter slickpick all numbers will be selected by random");
                    userInput = Console.ReadLine();
                    possNumber = InputValadation(userInput, theDefault, ifAny);
                    Console.WriteLine( possNumber + " for loop possNumber");


                    //while not unique or choose random == 0 or choose all random == -1
                    while (checkDuplicate == true && possNumber < 0)
                    {
                        checkDuplicate = UniqueIn(playerArray, possNumber);
                        if (checkDuplicate == true)
                        {
                            Console.WriteLine($"Choose a number for selection {counter}");
                            Console.WriteLine("The number needs to be unique!");
                            Console.WriteLine("If left blank will ramdonise the number");
                            Console.WriteLine("If enter slickpick all numbers will be selected by random");
                            userInput = Console.ReadLine();
                            possNumber = InputValadation(userInput, theDefault, ifAny);
                        }

                    }
                    if (possNumber > 0 )
                    {
                        playerArray[i] = possNumber;
                    }
                }
                /*
                 * if possNumber is equal to zero
                 * find a unique random number and put it into the array
                 * reset possNumber to zero
                 */
                if ( possNumber == 0)
                {
                    while (checkDuplicate == true)
                    {
                        possNumber = random.Next(lowestValue = 1, ifAny);
                        checkDuplicate = UniqueIn(playerArray, possNumber);
                        Console.WriteLine("debug finding a unique random " + possNumber);
                    }

                    playerArray[i] = possNumber;
                    possNumber = 0;
                }
                /*
                 * if possNumber is less than zero
                 * find a unique random number and put it into the array
                 * reset possNumber to -1
                 */
                if ( possNumber < 0 )
                {
                    while (checkDuplicate == true)
                    {
                        possNumber = random.Next(lowestValue = 1, ifAny);
                        checkDuplicate = UniqueIn(playerArray, possNumber);
                        Console.WriteLine("debug finding a unique random " + possNumber);
                    }

                    playerArray[i] = possNumber;
                    possNumber = -1;
                }
            Console.WriteLine(playerArray[i] + $" {counter} player number");
            }
               
            Console.WriteLine(howMany + " howMany");
            Console.WriteLine(ifAny + " ifAny");
            
            Array.Sort(playerArray);
            Array.Sort(TheGameArray);


            //test for loop
            string player = "";
            string game = "";
            for (int i = 0; i < playerArray.Length; i++)
			{
                player = player + " " + playerArray[i];
                game = game + " " + TheGameArray[i];
			}
            
            Console.WriteLine(player + " player contents");            
            Console.WriteLine(game + " game cntents");            
            Console.WriteLine(playerArray.Length + " playerArray length");            
            Console.ReadLine();
            
            /*
             * for loop to go through the the array.
             *  select numbers to fill the array.
             *   if any selected by random use the lowest and highest bounds
             *   validate is a number, within bounds 
             *   and unique
             *   if these not meet reselect number or get new random number
             * then sort the array  
             * 
             * make the drawer arrey
             *  
             */
           
        }

    /*
     * To do the binary search need to loop through the the array and try to find the number in the other arry
        * for goes through the player array.
             * when i == to index zero, sets the min to index zero and the Max to array.lenght
             * or before the for starts
        * while not found or min and max are not equal
             * finds min checks if it is the number - if so returns true
             * finds Max checks if it is the number - if so returns true
             * finds midle checks if it is the number - if so returns true
             * checks if midle is less or greater than what we are looking for
             * - if is less than makes min the middle value and moves it up one, Max moves down one
             *   - Check if min equals Max, if returns did not find the number
             * - if is more than makes Max the middle value and moves it down one, min moves up one
             *   - Check if min equals Max, if returns did not find the number
             * if min wquals Max brakes the while and returns false, the number was not found
    */
        static int BinarySearch(int[] playArray, int[] gameArray)
        {
            int arrayLength = playArray.Lenght
            int check = 0; //variable to check
            int min = 0; //the index
            int middle = 0;
            int Max = gameArray.Length; //the index
            int matched = 0;
            for (int i = 0; i < arrayLength; i++)
			{
                check = playArray[i];
                while (min != Max) // maybe make min is less than Max as the while condition
                {
                    if (check == gameArray[min])
                    {
                        matched++;
                    }                   
                    if (check == gameArray[Max])
                    {
                        matched++;
                    }
                    // find and check middle
                    middle = min + (min - Max) / 2;
                    if (check == gameArray[middle])
                    {
                        matched++;
                    }
                    // less that check
                    if (gameArray[middle] < check)
                    {
                        min = middle + 1;
                        if ( min == Max)
                        {
                            matched++;
                        }
                        Max = Max - 1;
                        if ( min == Max)
                        {
                            matched++;
                        }
                    }                    
                    // more that check
                    if (gameArray[middle] > check)
                    {
                        Max = middle - 1;
                        if ( min == Max)
                        {
                            matched++;
                        }
                        min = min + 1;
                        if ( min == Max)
                        {
                            matched++;
                        }
                    }
                }
			}
            return matched;
        }
        
        /*
         * Need something to check if all last number is unique
         * inputs possNumber, and end point using the counter variable 
         * maybe a cell check to see if empty to know when to stop the loop
         */
        static bool UniqueIn(int[] array, int possNumber)
        {
            int arrayLength = array.Length;
            bool isDuplicate;
            for (int i = 0; i < arrayLength; i++)
            {
                // check if a value in index if not break and return false
                if (array[i] == null)
                {
                    return false;
                }

                isDuplicate = true;
                if (array[i] == possNumber)
                {
                    isDuplicate = true;
                    return true;
                }
            }
            return false;
        }

        static int InputValadation( string userInput, int aDefault = 5, int highBounds = 0, int lowBounds = 0)
        {
            int validOutput = 0;
            int possably_valid;

            while (validOutput == 0)
            {
                if (!int.TryParse(userInput, out possably_valid))
                {
                    if (string.IsNullOrEmpty(userInput))
                    {
                        return aDefault;
                        
                    }
                    if (userInput.ToLower() == "slickpick")
                    {
                        validOutput = -1;
                        continue;
                    }
                    Console.WriteLine("Please enter a number ");
                }
               
                if (highBounds > 0)
                {
                    if (possably_valid > highBounds)
                    {
                        possably_valid = 0;
                        Console.WriteLine("Please enter a number within the bounds");                         
                    }
                    else
                    {
                        validOutput = possably_valid;
                    }
                }

                if (possably_valid > 0)
                {
                    validOutput = possably_valid;
                }

                if (validOutput == 0)
                {
                    userInput = Console.ReadLine();
                }
                
            }

            return validOutput;
        }

    }
    //while (int.TryParse(userInput, out validOutput))
    //{               
    //    if (highBounds > 0)
    //    {
    //        if (validOutput > highBounds)
    //        {
    //            Console.WriteLine("Please enter a number within the bounds");
    //            userInput = Console.ReadLine();
    //        }
    //        else
    //        {
    //            return validOutput;
    //        }                    
    //    }

    //}
    //while (!int.TryParse(userInput, out validOutput))
    //{
    //    if (string.IsNullOrEmpty(userInput))
    //    {                    
    //        validOutput = aDefault;
    //        break;
    //    }
    //    if (userInput.ToLower() == "slickpick")
    //    {
    //        validOutput = -1;
    //        break;
    //    }                
    //    Console.WriteLine("Please enter a number ");
    //    userInput = Console.ReadLine();
    //}

    //if (string.IsNullOrEmpty(userInput)) { howMany = theDefault; }
    //else if (userInput == "slickpick")
    //{

    //    howMany = random.Next(lowestValue, highestValue);
    //}
    //else
    //{
    //    while (!int.TryParse(userInput, out howMany))
    //    {
    //        Console.WriteLine("Please enter a number ");
    //        userInput = Console.ReadLine();
    //    }
    //}





}

