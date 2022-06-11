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
            Console.WriteLine("Please enter your sentence:");
            userStr =Console.ReadLine();            
            string[] UserStrArr = userStr.Split(new char[]{' ','.',',','<','>','?','/',':',';',']','[','{','}','|','!','@','#','$','%','^','&','*','(',')','_','-','+','='}, StringSplitOptions.RemoveEmptyEntries);
            CounterParam(UserStrArr);
        }

        static void CounterParam(string[] UserStrArr)
        {
            Dictionary<int, string> wordcounts= new Dictionary<int, string>();              
            foreach (string item in UserStrArr)
            {
                if (!item.Any(char.IsDigit) || item.All(char.IsDigit))
                {
                    if (!wordcounts.ContainsKey(item.Length))                                            
                        wordcounts.Add(item.Length, item);                    
                    else                    
                        wordcounts[item.Length]+=";" + item;                    
                }
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
     }
}