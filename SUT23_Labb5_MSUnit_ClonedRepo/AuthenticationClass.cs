using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Group_Project_Loop_Legends
{
    internal class AuthenticationClass
    {
        public static (string, string) AuthenticatorMethod(string auth, string authQuest)
        {
            //No existing authenticator
            if (auth == "")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("No authenticator detected.\nDo you want to setup an authenticator?");
                Console.ResetColor();
                Console.WriteLine("\n     Yes \n     No");

                int cursorPos = 3;
                Console.SetCursorPosition(0, cursorPos);
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();
                ConsoleKeyInfo navigator;
                do
                {
                    navigator = Console.ReadKey();
                    Console.SetCursorPosition(0, cursorPos);
                    Console.Write("   ");
                    if (navigator.Key == ConsoleKey.UpArrow && cursorPos > 3)
                    {
                        cursorPos--;
                    }
                    else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 4)
                    {
                        cursorPos++;
                    }
                    Console.SetCursorPosition(0, cursorPos);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-->");
                    Console.ResetColor();
                } while (navigator.Key != ConsoleKey.Enter);

                Console.Clear();
                Console.CursorVisible = true;

                if (cursorPos == 3)
                {
                    Console.Clear();
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Choose authenticator question:");
                    Console.ResetColor();
                    Console.WriteLine("\n     What's your favourite resturant?");
                    Console.WriteLine("     What's the name of your hometown?");
                    Console.WriteLine("     What's the name of your mother?");
                    Console.WriteLine("     What's the name of your father?");
                    Console.WriteLine("     What was your nickname in school?");

                    cursorPos = 2;
                    Console.SetCursorPosition(0, cursorPos);
                    Console.CursorVisible = false;
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-->");
                    Console.ResetColor();
                    //ConsoleKeyInfo navigator;
                    do
                    {
                        navigator = Console.ReadKey();
                        Console.SetCursorPosition(0, cursorPos);
                        Console.Write("   ");
                        if (navigator.Key == ConsoleKey.UpArrow && cursorPos > 2)
                        {
                            cursorPos--;
                        }
                        else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 6)
                        {
                            cursorPos++;
                        }
                        Console.SetCursorPosition(0, cursorPos);
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("-->");
                        Console.ResetColor();
                    } while (navigator.Key != ConsoleKey.Enter);

                    Console.Clear();
                    Console.CursorVisible = true;

                    string answer = "";
                    switch (cursorPos)
                    {
                        case 2:
                            Console.WriteLine("Answer your choosen authenticator question. . .\n");
                            Console.WriteLine("What's your favourite resturant?");
                            answer = Console.ReadLine();
                            return (answer, "What's your favourite resturant?");
                        case 3:
                            Console.WriteLine("Answer your choosen authenticator question. . .\n");
                            Console.WriteLine("What's the name of your hometown?");
                            answer = Console.ReadLine();
                            return (answer, "What's the name of your hometown?");
                        case 4:
                            Console.WriteLine("Answer your choosen authenticator question. . .\n");
                            Console.WriteLine("What's the name of your mother?");
                            answer = Console.ReadLine();
                            return (answer, "What's the name of your mother?");
                        case 5:
                            Console.WriteLine("Answer your choosen authenticator question. . .\n");
                            Console.WriteLine("What's the name of your father?");
                            answer = Console.ReadLine();
                            return (answer, "What's the name of your father?");
                        case 6:
                            Console.WriteLine("Answer your choosen authenticator question. . .\n");
                            Console.WriteLine("What was your nickname in school?");
                            answer = Console.ReadLine();
                            return (answer, "What was your nickname in school?");
                    }


                }
                //No authenticator and No as answer
                else if (cursorPos == 4)
                {
                    return ("", "");
                }
            }

            //If authenticator already exists
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Authenticator detected.\n");
                Console.ResetColor();
                Console.WriteLine("     Change authenticator question");
                Console.WriteLine("     Remove authenticator");
                Console.WriteLine("     Back to menu");

                int cursorPos = 2;
                Console.SetCursorPosition(0, cursorPos);
                Console.CursorVisible = false;
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("-->");
                Console.ResetColor();
                ConsoleKeyInfo navigator;
                do
                {
                    navigator = Console.ReadKey();
                    Console.SetCursorPosition(0, cursorPos);
                    Console.Write("   ");
                    if (navigator.Key == ConsoleKey.UpArrow && cursorPos > 2)
                    {
                        cursorPos--;
                    }
                    else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 4)
                    {
                        cursorPos++;
                    }
                    Console.SetCursorPosition(0, cursorPos);
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("-->");
                    Console.ResetColor();
                } while (navigator.Key != ConsoleKey.Enter);

                Console.Clear();
                Console.CursorVisible = true;

                if (cursorPos == 2)
                {
                    Console.Clear();
                    Console.WriteLine("To change your authentication question you need to confirm your identity . . .\n");
                    Console.WriteLine(authQuest);
                    string idCheck = Console.ReadLine();

                    if (idCheck == auth)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nIdentity confirmed . . .");
                        Console.ResetColor();
                        Thread.Sleep(1500);

                        Console.Clear();
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine("Choose authenticator question:");
                        Console.ResetColor();
                        Console.WriteLine("\n     What's your favourite resturant?");
                        Console.WriteLine("     What's the name of your hometown?");
                        Console.WriteLine("     What's the name of your mother?");
                        Console.WriteLine("     What's the name of your father?");
                        Console.WriteLine("     What was your nickname in school?");

                        cursorPos = 2;
                        Console.SetCursorPosition(0, cursorPos);
                        Console.CursorVisible = false;
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.Write("-->");
                        Console.ResetColor();
                        //ConsoleKeyInfo navigator;
                        do
                        {
                            navigator = Console.ReadKey();
                            Console.SetCursorPosition(0, cursorPos);
                            Console.Write("   ");
                            if (navigator.Key == ConsoleKey.UpArrow && cursorPos > 2)
                            {
                                cursorPos--;
                            }
                            else if (navigator.Key == ConsoleKey.DownArrow && cursorPos < 6)
                            {
                                cursorPos++;
                            }
                            Console.SetCursorPosition(0, cursorPos);
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.Write("-->");
                            Console.ResetColor();
                        } while (navigator.Key != ConsoleKey.Enter);

                        Console.Clear();
                        Console.CursorVisible = true;

                        string answer = "";
                        switch (cursorPos)
                        {
                            case 2:
                                Console.WriteLine("Answer your choosen authenticator question. . .\n");
                                Console.WriteLine("What's your favourite resturant?");
                                answer = Console.ReadLine();
                                return (answer, "What's your favourite resturant?");
                            case 3:
                                Console.WriteLine("Answer your choosen authenticator question. . .\n");
                                Console.WriteLine("What's the name of your hometown?");
                                answer = Console.ReadLine();
                                return (answer, "What's the name of your hometown?");
                            case 4:
                                Console.WriteLine("Answer your choosen authenticator question. . .\n");
                                Console.WriteLine("What's the name of your mother?");
                                answer = Console.ReadLine();
                                return (answer, "What's the name of your mother?");
                            case 5:
                                Console.WriteLine("Answer your choosen authenticator question. . .\n");
                                Console.WriteLine("What's the name of your father?");
                                answer = Console.ReadLine();
                                return (answer, "What's the name of your father?");
                            case 6:
                                Console.WriteLine("Answer your choosen authenticator question. . .\n");
                                Console.WriteLine("What was your nickname in school?");
                                answer = Console.ReadLine();
                                return (answer, "What was your nickname in school?");
                        }

                    }
                }
                else if (cursorPos == 3)
                {
                    Console.Clear();
                    Console.WriteLine("To remove your authentication question you need to confirm your identity . . .\n");
                    Console.WriteLine(authQuest);
                    string idCheck = Console.ReadLine();
                    if (idCheck == auth)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nIdentity confirmed . . .");
                        Console.ResetColor();
                        Thread.Sleep(1500);

                        return ("", "");
                    }
                }
                else
                {
                    return (auth, authQuest);
                }
            }
            return (auth, authQuest);
        }
    }
}
