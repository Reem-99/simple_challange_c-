using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Programming1hw
{
    class Program
    {
        static void Main(string[] args)
        {
            String[] qusetions = new String[3];//store all the type of questions
            qusetions[0] = "how many times even numbers appear  in the following characters";
            qusetions[1] = "how many times odd numbers appear  in the following characters";
            qusetions[2] = "how many times primary numbers appear  in the following characters";
            System.Console.WriteLine("please enter the maximun number of questios");
            int no_questios; //store the number of questions
            String s = Console.ReadLine();//for enter values
            no_questios = int.Parse(s);
            String[] mystrings = new String[no_questios];//to store the strings of characters
            int[] type = new int[no_questios];//stroe the type of question where 1 refer to qusetion about even number 2 refer to qusetion about odd number 3 refer to qusetion about primary number
            int[] answers = new int[no_questios];//store the user answers
            int[] resault = new int[no_questios];//store the correct answers
            int[] evaluation = new int[no_questios];//store the evaluate of user answers 0 if the answer is wrong 1 if the answer is correct
            System.Console.WriteLine("_______________________________________________");
            System.Console.WriteLine();
            for (int i = 0; i < no_questios; i++)
            {
                System.Console.WriteLine("please enter an integer value between 3 and 100 which dtermine the difficulty of the question");
                s = Console.ReadLine();
                int difficulty = int.Parse(s);//determine the level of question difficulty and it is equal the number of characters
                Random rand = new Random();
                int index = rand.Next(0, 3);//generate random number between 0 and 2 to choose a random question
                System.Console.WriteLine(qusetions[index]);
                String mystring = generateRandomString(difficulty);//this function generate random string with number of characters equla difficulty value 
                mystrings[i] = mystring;//stroe the generated string in mystring array
                //store the type of function in type array
                if (index == 0)
                {
                    type[i] = 1;
                }
                else if (index == 1)
                {
                    type[i] = 2;
                }
                else if (index == 2)
                {
                    type[i] = 3;
                }
                System.Console.WriteLine(mystring);//print the generated string
                System.Console.WriteLine("to ignore the question type Ignore");
                s = Console.ReadLine();//to enter the answer of user
                while (s != "Ignore" && !Regex.IsMatch(s, @"^\d+$"))//the answer must be number or "Ignore" so this while loop check that
                {
                    System.Console.WriteLine("please type number or type Ignore");
                    s = Console.ReadLine();
                }
                if (s == "Ignore")//if the answer of user was "ignore" so we will store 101 in the answer arrya which is alwasy false because it is more than the maximum number of characters of the generated string
                {
                    answers[i] = 101;
                }
                else//store the user answer
                {
                    answers[i] = int.Parse(s);
                }

                resault[i] = findasnwer(mystring, type[i]);//calculate the corrcet answer with the function findanswer and store it in resault array
                evaluation[i] = evalutionAnswer(answers[i], resault[i]);//calculate the evalution of user answer with the function evalutionAnswer and store it in evaluation array
                System.Console.WriteLine("_______________________________________________");
            }
            String choise;//store the chiose of user
            do
            {
                System.Console.WriteLine("To get the number of wrong answers, type 1");
                System.Console.WriteLine("To get the number of right answers, type 2");
                System.Console.WriteLine("To view all the questions with correct and answered responses, type 3");
                System.Console.WriteLine("To exit, type exit");
                choise = Console.ReadLine();
                System.Console.WriteLine("_______________________________________________");
                //this while loop to check that the choise of user is equal 1 or 2 or 3 or exit
                while (choise != "1" && choise != "2" && choise != "3" && choise != "exit")
                {
                    System.Console.WriteLine("To get the number of wrong answers, type 1");
                    System.Console.WriteLine("To get the number of right answers, type 2");
                    System.Console.WriteLine("To view all the questions with correct and answered responses, type 3");
                    System.Console.WriteLine("To exit, type exit");
                    choise = Console.ReadLine();
                    System.Console.WriteLine("_______________________________________________");
                }
                if (choise == "1")
                {
                    System.Console.WriteLine("the number of wrong answers is : " + numberOfWrongAnswes(evaluation));//print the number of wrong answers which the numberOfWrongAnswes function calculate them
                    System.Console.WriteLine("_______________________________________________");
                }
                else if (choise == "2")
                {
                    System.Console.WriteLine("the number of right answers is : " + numberOfCorrectAnswes(evaluation));//print the number of wrong answers which the numberOfCorrectAnswes function calculate them
                    System.Console.WriteLine("_______________________________________________");
                }
                else if (choise == "3")
                {
                    //print the question text and the string of that question and the user answer on it and the correct answer
                    for(int i=0;i<no_questios;i++){
                        if (type[i] == 1)
                        {
                            System.Console.WriteLine("the " + (i+1) + " question is : " + qusetions[0]+" "+mystrings[i]);
                        }
                        else if (type[i] == 2)
                        {
                            System.Console.WriteLine("the " + (i + 1) + " question is : " + qusetions[1] + " " + mystrings[i]);
                        }
                        else if (type[i] == 3)
                        {
                            System.Console.WriteLine("the " + (i + 1) + " question is : " + qusetions[2] + " " + mystrings[i]);
                        }
                        System.Console.WriteLine("the user answer is : " + answers[i]);
                        System.Console.WriteLine("the right answer is : " + resault[i]);
                        System.Console.WriteLine("_______________________________________________");
                    }
                    
                }

            } while (choise!="exit");
        }
         static String generateRandomString(int length)//this fuction generats random string consists of big letters and small letters and numbers between 0 and 9
        {
            String chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789abcdefghijklmnopqrstuvwxyz";//all symbols that out generated string can contains
            Random random = new Random();
            Char[] stringChar = new Char[length];
            for (int i = 0; i < length; i++)
            {
                //choise random synbol from chars and store it in stringchar arrya
                stringChar[i] = chars[random.Next(chars.Length)];
            }
            //convert the stringChar array to string and return it
            String finalString = new String(stringChar);
            return finalString;
        }
        //this function take the string and the type of question and call the suitable function to find the answer
        public static int findasnwer(String mystring , int type){
            int count=0;
            for (int i = 0; i < mystring.Length; i++)
            {
                if (type == 1)//if the of question type is even it will call isEven function for each character of the string and edit the count value according to the resault of return value from isEven
                {
                    if (Char.IsDigit(mystring[i]) && isEven((int)Char.GetNumericValue(mystring[i])))
                    {
                        count++;
                    }
                }
                else if (type == 2)//if the of question type is odd it will call negation of isEven function for each character of the string and edit the count value according to the resault of return value from the negation of isEven
                {
                    if (Char.IsDigit(mystring[i]) && !isEven(((int)Char.GetNumericValue(mystring[i]))))
                    {
                        count++;
                    }
                }
                else if (type == 3)
                {
                    if (Char.IsDigit(mystring[i]) && isPrimary(((int)Char.GetNumericValue(mystring[i]))))//if the of question type is primary it will call isPrimary function for each character of the string and edit the count value according to the resault of return value from isPrimary
                    {
                        count++;
                    }
                }
            }
                return count;
    }
        public static Boolean isEven(int number)//take integer value and return true if it is even , false if it is odd
        {
            if (number % 2 == 0)
            {
                return true;
            }
            return false;
        }
        public static Boolean isPrimary(int number)//take integer value and return true if it is primary , false if not
        {
            if (number == 1)
            {
                return true;
            }
            else if (number == 0)
            {
                return false;
            }
            else
            {
                for (int i = number - 1; i >= 2; i--)
                {
                    if (number % i == 0)
                        return false;
                }
                return true;
            }
        }
        public static int evalutionAnswer(int a, int b)//it compare between the correct answer and user answer for each quetion and return 1 if the user answer is true or return 0 if the user answer is false
        {
            if (a != b)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }
        //this fuction calculate the correct answers by count the 1 values in avaliation array and return the number of them
        public static int numberOfCorrectAnswes(int []a){
            int count=0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == 1)
                {
                    count++;
                }
            }
                return count;
        }
        //this fuction calculate the wrong answers by count the 0 values in avaliation array and return the number of them
        public static int numberOfWrongAnswes(int[] a)
        {
            int count = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] == 0)
                {
                    count++;
                }
            }
            return count;
        }
    }
}
