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
            
            if (userStr.Length>0)
            {
                //Spilt of input based on special characerr delmiters as follows
                string[] UserStrArr = userStr.Split(new char[]{' ','.',',','<','>','?','/',':',';',']','[','{','}','|','!','@','#','$','%','^','&','*','(',')','_','-','+','=','€'}, StringSplitOptions.RemoveEmptyEntries);            
                
                //function for Splitting of words from the user input
                bool result = CounterParam(UserStrArr);
                //print invalid sentence sentence could not broken into words
                if (!result)
                    Console.WriteLine("Invalid sentence, words cannot be break");
            }
            else
                Console.WriteLine("Please enter value");
        }

        //function for Splitting of words from the user input 
        static bool CounterParam(string[] UserStrArr)
        {
            bool result = false;
            try
            {
                if (UserStrArr.Length>0)
                {
                    //Initialize dictionary colleciton for storing words
                    Dictionary<int, string> wordcounts= new Dictionary<int, string>();
                    //Temp varialbe to store double quoted string values temporarily
                    string temp= String.Empty;                                           
                    foreach(var word in UserStrArr)            
                    {
                        if (temp!=String.Empty)
                        {
                            if (word.EndsWith('"') || word.EndsWith('\''))
                            {
                                temp=temp +" " +word;                        
                                CheckKeyAndWordToDic(temp.Substring(1, temp.Length-2), wordcounts);                        
                                temp= String.Empty;                        
                            }
                            else
                                temp=temp +" " +word;
                        }
                        else if (word.StartsWith('"') || word.StartsWith('\''))                
                            temp = word;                
                        else                
                            CheckKeyAndWordToDic(word, wordcounts);                                   
                    }

                    //sort dictionary
                    if (wordcounts.Count>0)
                    {
                        var sortedKeys = wordcounts.Keys.ToList();
                        sortedKeys.Sort();
                        int MaxCharacter = sortedKeys.Last();

                        //print all the found and not found number of words
                        for (int i = 1; i <= MaxCharacter; ++i)
                        {
                            if (wordcounts.ContainsKey(i))                              
                                Console.WriteLine("words with {0} letters occured {1} times (words={2})", i,wordcounts[i].Split(';').Length, wordcounts[i]);                                                        
                            else
                                Console.WriteLine("words with {0} letter did not occur",i);
                        }
                        result=true;
                    }
                    else
                        result = false;
                }
                return result;
            }
            catch (System.Exception)
            {                
                throw;
            }
        }
        
        //add words to the dictionary
        static bool CheckKeyAndWordToDic(string word, Dictionary<int, string> wordcounts)
        {
            bool result = false;
            try
            {
                //if no alphanumerical charactes or only digits
                if (!word.Any(char.IsDigit) || word.All(char.IsDigit))
                {
                    if (!wordcounts.ContainsKey(word.Length))
                        wordcounts.Add(word.Length, word);
                    else
                        wordcounts[word.Length]+=";" + word;

                    result= true;
                }
                return result;
            }
            catch (System.Exception)
            {                
                throw;
            }
        }
    }
}