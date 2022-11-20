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
            
            string userInput = "";
            int howMany;//the amount of numbers played
            int ifAny = 0; //the max range of the random pool
            // Input valadation 
            int theDefault = 5; 
            // int aDefault = 50;
            int lowestValue = 1; 
            int highestValue = 10; 
            int matches;
          
            Random random = new Random();

            Console.WriteLine("Please enter how many number to be played.");
            Console.WriteLine("If left blank the default will be five. ");
            Console.WriteLine("Type 'slickpick' to select game options at random");
            userInput = Console.ReadLine();
            howMany = InputValadation(userInput, theDefault, highestValue);
            if (howMany < 0 ) //if less than zero 
            {
                ifAny = -1; //will than set ifany to less than zero so the ifaAny will be randomised
                howMany = random.Next(lowestValue, highestValue);
            }
            Console.WriteLine($"Numbers Played will be {howMany}\n");
            if (ifAny == 0)// at this point if ifany is at zero will ask for input
            {
                Console.WriteLine("Please enter the pool the numbers will be picked from.");
                Console.WriteLine("If left blank the default will be one hundred. ");
                Console.WriteLine("Type 'slickpick' to select game options at random");
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

            /* 
             * Now need for loop to populate array with unique numbers
            */
            int counter = 1;
            theDefault = 0;
            int possNumber = 0;
            bool checkDuplicate = true;
            Console.WriteLine($"Choose a number for selection {counter}");
            Console.WriteLine("If left blank will ramdonise the number");
            Console.WriteLine("Type 'slickpick' to select the numbers at random");
            userInput = Console.ReadLine();
            possNumber = InputValadation(userInput, theDefault, ifAny);
                        
            if ( possNumber > 0 ) 
            {
                playerArray[0] = possNumber;            
            }

            if (possNumber == 0)//if equal to zero, randomises one choice
            {
                playerArray[0] = random.Next(lowestValue = 1, ifAny);
                possNumber = 0;
            }

            if (possNumber < 0 )//if less than zero, slickpick option randomises all choices
            {
                playerArray[0] = random.Next(lowestValue = 1, ifAny);
                possNumber = -1;
            }
            
            TheGameArray[0] = random.Next(lowestValue = 1, ifAny);

            /*
             * Checks possNumber if zero or above will ask for user input if at minus will do 
             * the slickpick routeen
             */

            for (int i = 1; i < playerArray.Length; i++)
            {
                int possTheGame = 0;
                counter++;
                checkDuplicate = true;

                if (possNumber > 0)
                {
                    possNumber = 0;
                }

                while (checkDuplicate == true) //populates TheGameArray
                    {
                        possTheGame = random.Next(lowestValue = 1, ifAny);
                        checkDuplicate = UniqueIn(TheGameArray, possTheGame);
                    }
                
                Console.WriteLine("The game number " + possTheGame);
                TheGameArray[i] = possTheGame;
                
                checkDuplicate = true;

                if (possNumber == 0)//choice of choosing or randomising a number 
                {
                    Console.WriteLine($"Choose a number for selection {counter}");
                    Console.WriteLine("If left blank will ramdonise the number");
                    Console.WriteLine("Type 'slickpick' to select the numbers at random");
                    userInput = Console.ReadLine();
                    possNumber = InputValadation(userInput, theDefault, ifAny);

                    //while not unique and possNumber is not 0 or -1
                    while (checkDuplicate == true && possNumber > 0)
                    {                        
                        checkDuplicate = UniqueIn(playerArray, possNumber);
                        if (checkDuplicate == true)
                        {
                            Console.WriteLine($"Choose a number for selection {counter}");
                            Console.WriteLine("The number needs to be unique!");
                            Console.WriteLine("If left blank will ramdonise the number");
                            Console.WriteLine("Type 'slickpick' to select the numbers at random");
                            userInput = Console.ReadLine();
                            possNumber = InputValadation(userInput, theDefault, ifAny);
                        }
                        if (possNumber <= 0)
                        {
                            break;
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
                    }

                    playerArray[i] = possNumber;
                    possNumber = -1;
                }
            Console.WriteLine(playerArray[i] + $" {counter} player number");
            }//end of for
                                      
            Array.Sort(TheGameArray);
                            
            string player = "";
            string game = "";
            for (int i = 0; i < playerArray.Length; i++)
			{
                player = player + " " + playerArray[i];
                game = game + " " + TheGameArray[i];
			}
            Console.WriteLine(player + " player contents");            
            Console.WriteLine(game + " game contents");
                       
            matches = BinarySearch(playerArray,TheGameArray);
            Console.WriteLine($"Matches = {matches}");

            Console.ReadLine();            
           
        }
    
        static int BinarySearch(int[] playArray, int[] gameArray)
        {
            int arrayLenght = playArray.Length;
            int check = 0; //variable to check
            int min = 0; //the index
            int middle = 0;
            int Max = gameArray.Length - 1; //the index
            int matched = 0;
            for (int i = 0; i < arrayLenght; i++)
			{
                check = playArray[i];
                min = 0; //the index
                Max = (gameArray.Length) - 1; //the index

                if (check == gameArray[min])
                    {
                        matched++;
                        continue;
                    }
                if (check == gameArray[Max])
                    {
                        matched++;
                        continue;
                    }

                while (min < Max) // maybe make min is less than Max as the while condition
                {                                                           
                    // find and check middle
                    middle = (min + Max) / 2;
                    if (check == gameArray[middle])
                    {
                        matched++;
                        break;
                    }
                    // less that check
                    if (gameArray[middle] < check)
                    {
                        min = middle + 1;
                        if ( check == gameArray[min] )
                        {
                            matched++;
                            break;
                        }
                        Max = Max - 1;
                        if ( check == gameArray[Max] )
                        {
                            matched++;
                            break;
                        }
                    }                    
                    // more that check
                    if ( gameArray[middle] > check )
                    {
                        Max = middle - 1;
                        if ( check == gameArray[Max] )
                        {
                            matched++;
                            break;
                        }
                        min = min + 1;
                        if ( check == gameArray[min])
                        {
                            matched++;
                            break;
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
                    return true;
                }
            }
            return false;
        }

        static int InputValadation( string userInput, int aDefault = 5, int highBounds = 0, int lowBounds = 1)
        {
            int validOutput = 0;
            int possably_valid;

            if (!int.TryParse(userInput, out possably_valid))
                {
                    if (string.IsNullOrEmpty(userInput))
                    {
                        return aDefault;                     
                    }

                    if (userInput.ToLower() == "slickpick")
                    {
                        validOutput = -1;
                        return validOutput;
                    }
                    Console.WriteLine("Please enter a number ");
                }
                
                if (possably_valid < lowBounds )
                {
                    possably_valid = 0;
                    Console.WriteLine("Please enter a number greater than zero");                         
                }
                               
                if (highBounds > 0)
                {
                    if (possably_valid > highBounds)
                    {
                        possably_valid = 0;
                        Console.WriteLine("Please enter a number within the high bounds");                         
                    }
                    
                }
                
                if (possably_valid > 0)
                {
                    validOutput = possably_valid;
                }

            while (validOutput == 0)
            {
                userInput = Console.ReadLine();

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
                
                if (possably_valid < lowBounds )
                {
                    possably_valid = 0;
                    Console.WriteLine("Please enter a number greater than zero");                         
                }
                               
                if (highBounds > 0)
                {
                    if (possably_valid > highBounds)
                    {
                        possably_valid = 0;
                        Console.WriteLine("Please enter a number within the high bounds");                         
                    }
                    
                }
                
                if (possably_valid > 0)
                {
                    validOutput = possably_valid;
                }
                
            }
            return validOutput;
        }
        
        /*
     * //test for loop
            string player = "";
            string game = "";
            for (int i = 0; i < playerArray.Length; i++)
			{
                player = player + " " + playerArray[i];
                game = game + " " + TheGameArray[i];
			}
            Console.WriteLine(player + " player contents");            
            Console.WriteLine(game + " game contents");            
            Console.WriteLine(playerArray.Length + " playerArray length");
            

            playerArray = new int[] { 1,2,3,4,5};
            TheGameArray = new int[] { 1,2,3,4,5};

            player = "";
            game = "";
            for (int i = 0; i < playerArray.Length; i++)
			{
                player = player + " " + playerArray[i];
                game = game + " " + TheGameArray[i];
			}
            Console.WriteLine(player + " player contents");            
            Console.WriteLine(game + " game contents");         
    */
    }
}

