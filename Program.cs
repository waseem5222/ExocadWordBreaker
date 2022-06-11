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
            string temp= String.Empty;
                               
            foreach(var item in UserStrArr)            
            {         
                Console.WriteLine(item);                    
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