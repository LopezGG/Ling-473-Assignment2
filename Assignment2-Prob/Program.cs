using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2_Prob
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] A = new string[8] { "monkey","donkey","yak","kangaroo","aardvark","antelope","puma","cheetah" };
            string[] B = new string[4] { "whale", "shark", "dolphin", "eel" };

            //Probability that that either of the words contain a ‘y’ 
            double P_EinA = (GetCount(A, 'y') / ( float )A.Length);
            double P_EinB = (GetCount(B, 'y') / ( float )B.Length);
                //P(A or B) = P(A) + P(B) - P(A Intersect B)
            double P_E = P_EinA  + P_EinB - (P_EinB * P_EinA) ;

            //Probability that that both of the words contain a ‘e’ : P(A Intersect B) = P(A) * P(B)
            double P_F = (GetCount(A, 'e') / ( float )A.Length) * (GetCount(B, 'e') / ( float )B.Length);

            //Prob  that both words contain the same number of letters 
            double P_G = GetLetterCount(A, B);

            //Prob that  that either (or both) of the words contains more than two vowels { a e i o u }. this must be equal to 1- p(A')* p(b') i.e 1 - prob of both words not containing vowel

            double P_H = 1 -   ((1- (TripleVowelWords(A) / ( float )A.Length))  * (1- (TripleVowelWords(B) / ( float )B.Length))) ;

            //Prob that either words contains a 'y' union either (or both) words have more than two vowels
            //P(A U B) = P(A) +P(B) because they are mutually exclusive
            double P_EUH = P_E + P_H;

            double P_FintH = P_F * P_H;

            //This is equal to zero because no word that contains y and e matches with the length of the other words in set B.Monkey and donkey are 6 letter words with no match in set B
            double P_EintFintG = P_E * P_F * P_G;

            //cheetah and dolphin have 7 letters each
            double P_HUG = P_H + P_G - (P_H * P_G);

            //kangaroo has more than 2 vowels and does not contain a e
            double P_HintFc = P_H * (1 - P_F);

            Console.WriteLine("P_E : " + P_E);
            Console.WriteLine("P_F : " + P_F);
            Console.WriteLine("P_G : " + P_G);
            Console.WriteLine("P_H : " + P_H);
            Console.WriteLine("P_EUH : " + P_EUH);
            Console.WriteLine("P_FintH : " + P_FintH);
            Console.WriteLine("P_EintFintG : " + P_EintFintG);
            Console.WriteLine("P_HUG : " + P_HUG);
            Console.WriteLine("P_HintFc : " + P_HintFc);

#if DEBUG
            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
#endif
        }


        //gets the number of words which contain a given letter
        static int GetCount (string[] set, char letter)
        {
            int count = 0;
           
            for (int i = 0; i < set.Length; i++)
            {
                if (set[i].ToLower().Contains(Char.ToLower(letter))) { ++count; }
            }
            return count;
        }
        //Gets the probabiblity that we pick two words of same length from both sets
        static double GetLetterCount (string[] A, string[] B)
        {
            double prob = 0.0;
            float ALength = A.Length;
            float BLength = B.Length;
            Dictionary<int, int> dA = new Dictionary<int, int>();
//create a dictionary that has a count of the length of elements
            foreach (string s in A)
            {
                int length = s.Length;
                if(dA.ContainsKey(length))
                {
                    dA[length] = dA[length]+ 1;
                }
                else
                {
                    dA.Add(length, 1);
                }
            }

            Dictionary<int, int> dB = new Dictionary<int, int>();
            //create a dictionary that has a count of the length of elements
            foreach (string s in B)
            {
                int length = s.Length;
                if (dB.ContainsKey(length))
                {
                    dB[length] = dB[length] + 1;
                }
                else
                {
                    dB.Add(length, 1);
                }
            }

            if(dA.Count <= dB.Count)
            {
                prob= MultiplyDict(dA, dB, ALength, BLength);
            }
            else
            {
                prob = MultiplyDict(dB, dA, BLength, ALength);
            }

            return prob;
        }
        //Iterate through smaller dictionary and multiply if same length words occur in both dictionary
        static double MultiplyDict(Dictionary<int,int> small,Dictionary<int,int> big,float smallLength, float BigLength)
        {
            double prob = 0;
            double temp = 0;

            foreach (KeyValuePair<int, int> entry in small)
            {

                if (big.ContainsKey(entry.Key))
                {
                    temp = (entry.Value / smallLength) * (big[entry.Key] / BigLength);
                    prob += temp;
                }

            }

            return prob;
        }
        //Let H be the event that either (or both) of the words contains more than two vowels { a e i o u }.This count includes repeated uses of the same vowel. This function returns number of words that contains more than 2 vowels
        static int TripleVowelWords(string[] A)
        {

            int total = 0;
            string vowels = "aeiou";
            //for each word in the string array where the count (overloaded) has more than one vowel
            List<string> vowelWords = A.Where(word => word.Count(c => vowels.Contains(c)) > 2)
                      .ToList();
            //vowelWords.ForEach(Console.WriteLine);
            total = vowelWords.Count;
            return total;
        }
    }
}
