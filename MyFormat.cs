using System;

namespace MyFormat
{
    public class MyFormat
    {
        public static MyFormat()
        {
            private static string MyReplacer(string prime_expr, string change_what, string change_into)
            {
                prime_expr = prime_expr.Replace(" ", "");
                int index1 = prime_expr.IndexOf(change_what);
                int index2 = index1 + change_what.Length;
                string result = prime_expr;
                if (index1 != -1)
                {
                    result = "";
                    for (int i = 0; i < index1; i++) result += prime_expr[i];
                    result += change_into;
                    for (int i = index2; i < prime_expr.Length; i++) result += prime_expr[i];
                }
                return result;
            }
            private static string MyReplacer(string prime_expr, int from_where, int to_where, string change_into)
            {
                prime_expr = prime_expr.Replace(" ", "");
                string result = "";
                for (int i = 0; i < from_where; i++) result += prime_expr[i];
                result += change_into;
                for (int i = to_where + 1; i < prime_expr.Length; i++) result += prime_expr[i];
                return result;
            }
        }
    }
}
