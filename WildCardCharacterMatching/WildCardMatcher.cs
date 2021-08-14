using System;

namespace WildCardCharacterMatching
{
    class WildCardMatcher
    {
        static void Main(string[] args)
        {
            WildCardMatcher wildCardMatcher = new WildCardMatcher();
            //Console.WriteLine("Should Pass: " + wildCardMatcher.IsMatch("abceb", "*a***b*c*"));
            Console.WriteLine("Should Pass: "+ wildCardMatcher.IsMatch("adceb", "*a*b"));
            Console.WriteLine("Should Fail: "+ wildCardMatcher.IsMatch("acdcb", "a*c?b"));
            Console.WriteLine("Should Fail: "+ wildCardMatcher.IsMatch("cb", "?a"));
            Console.WriteLine("Should Fail: "+ wildCardMatcher.IsMatch("aa", "a"));
            Console.WriteLine("Should Pass: "+ wildCardMatcher.IsMatch("aa", "*"));
        }
        public WildCardMatcher() { }

        public bool IsMatch(string s, string p)
        {
            //Optimization
            string pattern = "";
            bool isFirst = true;
            for(int i = 0; i < p.Length; i++)
            {
                if(p[i] == '*')
                {
                    if (isFirst)
                    {
                        pattern = pattern + p[i];
                        isFirst = false;
                    }
                }
                else
                {
                    pattern = pattern + p[i];
                    isFirst = true;
                }
            }


            //This is the Main Solution for DP
            bool [,] matches = new bool[s.Length + 1,p.Length + 1];

            matches[0,0] = true;
            if(p[0] == '*' && s.Length > 0)
            {
                matches[0, 1] = true;
            }

            for(int i = 1; i < s.Length + 1; i++)
            {
                for(int j = 1; j < p.Length + 1; j++)
                {
                    if(p[j - 1] == '?' || s[i - 1] == p[j - 1])
                    {
                        matches[i,j] = matches[i - 1, j - 1];
                    }
                    else if(p[j-1] == '*')
                    {
                        matches[i,j] = matches[i - 1,j] || matches[i, j - 1];
                    }
                }
            }

            //for(int i = 0; i < matches.Length; i++){
            //    for (int j = 0; j < matches.GetLength(1); j++)
            //    {
            //        Console.Write(matches[i, j]);
            //    }
            //    Console.WriteLine();
            //}
            return matches[s.Length, p.Length];
        }

        /*
        public bool IsMatch(string s, string p)
        {
            var prevMatches = new bool[s.Length + 1];
            prevMatches[0] = true;

            for (int i = 0; i < p.Length; i++)
            {
                var matches = new bool[s.Length + 1];

                for (int j = 0; j < s.Length + 1; j++)
                {
                    if (p[i] == '*')
                    {
                        matches[j] = prevMatches[j] || (j > 0 && matches[j - 1]);
                    }
                    else
                    {
                        matches[j] = j > 0 && prevMatches[j - 1] && (p[i] == '?' || p[i] == s[j - 1]);
                    }
                }

                prevMatches = matches;
            }

            return prevMatches[s.Length];
        }
        */
    } 
}
