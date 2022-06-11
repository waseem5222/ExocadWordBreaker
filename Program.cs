using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace ExocadWordBreaker
{
    class Program
    {
        static void Main(string[] args)
        {
            string userStr; 
            //User input of the Text           
            Console.WriteLine("Please enter your sentence:");
            userStr =Console.ReadLine();

            //Spilt of input based on special characerr delmiters as follows
            string[] UserStrArr = userStr.Split(new char[]{' ','.',',','<','>','?','/',':',';',']','[','{','}','|','!','@','#','$','%','^','&','*','(',')','_','-','+','=','\''}, StringSplitOptions.RemoveEmptyEntries);            
            //call function for Splitting of words from the user input 
            CounterParam(UserStrArr);
        }

        //function for Splitting of words from the user input 
        static void CounterParam(string[] UserStrArr)
        {
            //Initialize dictionary colleciton for storing words
            Dictionary<int, string> wordcounts= new Dictionary<int, string>();
            //Temp varialbe to store double quoted string values temporarily
            string temp= String.Empty;                                           
            foreach(var item in UserStrArr)            
            {                      
                if (temp!=String.Empty)
                {
                    if (item.EndsWith('"'))
                    {
                        temp=temp +" " +item;                        
                        CheckKeyAndWordToDic(temp.Substring(1, temp.Length-2), wordcounts);                        
                        temp= String.Empty;                        
                    }
                    else
                        temp=temp +" " +item;
                }
                else if (item.StartsWith('"'))                
                    temp = item;                
                else                
                    CheckKeyAndWordToDic(item, wordcounts);                                   
            }

            var sortedKeys = wordcounts.Keys.ToList();
            sortedKeys.Sort();
            int MaxCharacter = sortedKeys.Last();

            for (int i = 1; i <= MaxCharacter; ++i)
            {
                if (wordcounts.ContainsKey(i))                              
                    Console.WriteLine("words with {0} letters occured {1} times (words={2})", i,wordcounts[i].Split(';').Length, wordcounts[i]);                                                        
                else
                    Console.WriteLine("words with {0} letter did not occur",i);
            }
        }

        static void CheckKeyAndWordToDic(string word, Dictionary<int, string> wordcounts)
        {
            if (!word.Any(char.IsDigit) || word.All(char.IsDigit))
            {
                if (!wordcounts.ContainsKey(word.Length))
                    wordcounts.Add(word.Length, word);
                else
                    wordcounts[word.Length]+=";" + word;
            }
        }
    }
}