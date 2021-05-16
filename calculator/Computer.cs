using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator
{
    public class Computer
    {
        public static readonly char[] ValidChars = " .0123456789+-*/xX()".ToCharArray();
        public static readonly char[] NumberValidChars = ".0123456789".ToCharArray();
        public static readonly char[] OperatorsValidChars = "+-*/xX".ToCharArray();
        public static readonly char[] MDValidChars = "*/xX".ToCharArray();
        public static readonly char[] ASValidChars = "+-".ToCharArray();
        private CalculateChain Chain;

        public Computer()
        {
            Chain = new CalculateChain();
        }
        public decimal Compute(string str)
        {
            if (!Validate(str))
                throw new Exception("The string is not in the right format!");
            str = reduce(str);
            string temp = "";
            //adds to chain.
            for (int i = 0; i < str.Length; i++)
            {
                //(5*5)+2,  (5+5*(3-5))/2
                if(str[i]=='(')
                {
                    int closerI = IndexOfCloser(i, str);
                    string newS = str.Substring(i + 1, closerI - i - 1);
                    Computer bc = new Computer();
                    decimal result = bc.Compute(newS);
                    Chain.AddNumber(result);
                    str = str.Substring(0, i) + str.Substring(closerI+1);
                    if (i >= str.Length)
                        break;
                    str=reduce(str);
                }
                if (IsExistsIn(str[i], OperatorsValidChars))
                {
                    if (temp.Length > 0)
                    {
                        decimal d = decimal.Parse(temp);
                        if(Chain.NeedANumber)
                        Chain.AddNumber(d);
                    }
                    temp = "";
                    var c = str[i];
                    switch (c)
                    {
                        case '+':
                            Chain.AddAnOperator(Operator.Plus);
                            break;
                        case '-':
                            Chain.AddAnOperator(Operator.Minus);
                            break;
                        case '*':
                            Chain.AddAnOperator(Operator.Multiply);
                            break;
                        case '/':
                            Chain.AddAnOperator(Operator.Division);
                            break;
                    }
                }
                else
                    temp += str[i];
            }
            if(temp.Length>0)
            Chain.AddNumber(decimal.Parse(temp));
            //end adds to chain.
            return Chain.Calculate();
        }
        private string reduce(string str)
        {
            string ret = "";
            if (str[str.Length - 1] == '(')
                str = str.Substring(0, str.Length - 1);
            if (NumberOf('(', str) != NumberOf(')', str))
                throw new FormatException("Math Error");
            if (IsExistsIn(str[0], ASValidChars))
                str = 0 + str;
            for(int i=0;i<str.Length;i++)
            {
                char c = str[i];
                if (c == '(')
                    if (i > 0 && IsExistsIn(str[i - 1], NumberValidChars))
                        ret += '*';
                if (c == 'x' || c == 'X')
                    ret += '*';
                else
                if (c != ' ')
                    ret += c;
                if (c == ')' && i < str.Length - 1 && IsExistsIn(str[i + 1], NumberValidChars))
                    ret += '*';
            }
            return ret;
        }
        private int IndexOfCloser(int opener_index,string str)
        {
            int opens = 0;
            for (int i = opener_index; i < str.Length; i++)
            {
                if (str[i] == ')')
                    opens--;
                if (str[i] == '(')
                    opens++;
                if (opens == 0)
                    return i;
            }
            return -1;
        }
        private int NumberOf(char c, string str)
        {
            int n = 0;
            foreach (char ch in str) 
            {
                n += ch == c ? 1 : 0;
            }
            return n;
        }
        private bool Validate(string str)
        {
            if (!ValidateChars(str))
                return false;
            return true;
        }
        private bool ValidateChars(string str)
        {
            foreach (char c in str)
                if (!IsExistsIn(c, ValidChars))
                    return false;
            return true;
        }
        private bool IsExistsIn(char find, char[] inCollection)
        {
            foreach (char c in inCollection)
                if (find == c)
                    return true;
            return false;
        }
    }
    public enum Operator { Minus, Plus, Multiply, Division };
    public class CalculateChain
    {
        List<string> Chain;
        public bool NeedANumber
        {
            get
            {
                if (Chain.Count == 0)
                    return true;
                return Chain.Last()[0] == 'E';
            }
        }

        public CalculateChain()
        {
            Chain = new List<string>();
        }
        public void AddNumber(decimal d)
        {
            if (NeedANumber)
                Chain.Add(d + "");
            else
                throw new ArgumentException("Needs an operator!");
        }
        public void AddAnOperator(Operator @operator)
        {
            if (!NeedANumber)
                Chain.Add("E" + (int)@operator);
            else
                throw new ArgumentException("Needs a number!");
        }
        public decimal Calculate()
        {
            CalculateMD();
            CalculateAS();
            if (Chain.Count > 1)
                throw new Exception("The chain count is not 1!");
            return decimal.Parse(Chain[0]);
        }
        private void CalculateAS()
        {
            bool made = false;
            for (int i = 0; i < Chain.Count; i++)
            {
                if (Chain[i][0] == 'E')
                {
                    if (Chain[i][1] == (((int)Operator.Plus) + "")[0])
                    {
                        decimal left = decimal.Parse(Chain[i - 1]);
                        decimal right = decimal.Parse(Chain[i + 1]);
                        decimal answer = left + right;
                        Chain.RemoveRange(i - 1, 3);
                        Chain.Insert(i - 1, answer + "");
                        made = true;
                    }
                    else
                    if (Chain[i][1] == (((int)Operator.Minus) + "")[0])
                    {
                        decimal left = decimal.Parse(Chain[i - 1]);
                        decimal right = decimal.Parse(Chain[i + 1]);
                        decimal answer = left - right;
                        Chain.RemoveRange(i - 1, 3);
                        Chain.Insert(i - 1, answer + "");
                        made = true;
                    }
                }
            }
            if (made)
                CalculateAS();
        }
        private void CalculateMD()
        {
            bool made = false;
            for (int i = 0; i < Chain.Count; i++)
            {
                if (Chain[i][0] == 'E')
                {
                    if (Chain[i][1] == (((int)Operator.Multiply) + "")[0])
                    {
                        decimal left = decimal.Parse(Chain[i - 1]);
                        decimal right = decimal.Parse(Chain[i + 1]);
                        decimal answer = left * right;
                        Chain.RemoveRange(i - 1, 3);
                        Chain.Insert(i - 1, answer + "");
                        made = true;
                    }
                    else
                    if (Chain[i][1] == (((int)Operator.Division) + "")[0])
                    {
                        decimal left = decimal.Parse(Chain[i - 1]);
                        decimal right = decimal.Parse(Chain[i + 1]);
                        decimal answer = left / right;
                        Chain.RemoveRange(i - 1, 3);
                        Chain.Insert(i - 1, answer + "");
                        made = true;
                    }
                }
            }
            if (made)
                CalculateMD();
        }
    }
}
