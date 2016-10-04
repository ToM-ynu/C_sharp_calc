using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calc2
{
    class Program
    {
        static void Main(string[] args)
        {
            string mathstring = null;
            Value value = new Value();
            //mathstring = Console.ReadLine();
            mathstring = "1+2+3*2+4-1";//for debug
            long ans = value.devided(mathstring);
            Console.WriteLine(ans);
            Console.ReadLine();

        }
    }
    public class Value
    {

        Value Left;
        Value Right;
        long num;


        public long devided(string eq)
        {

            try
            {
                num = int.Parse(eq);//式が数字だけかどうかを判断する
            }
            catch (FormatException)//string eqが式である時
            {
                num = calc(eq);
            }
            catch (OverflowException)
            {
                Console.WriteLine("intの最大値を超えてしまいました");
                Environment.Exit(-1);
            }
            return num;
        }
        private long calc(string eq)
        {
            Pair pair = new Pair();
            Plus plus = new Plus();
            Devided devided = new Devided();
            Minus minus = new Minus();
            Times times = new Times();
            Right = new Value();
            Left = new Calc2.Value();
            char op;
            char[] operater = new char[] { '+', '-', '*', '/' };
            for (int i = 0; i < operater.Length; i++)
            {

                for (int j = 0; j < eq.Length; j++)
                {
                    op = eq[j];
                    if (op == operater[i])
                    {
                        pair.First = operater[i];
                        pair.Second = j;
                        goto LOOPOUT;
                    }
                }
            }
        LOOPOUT:
            //文字列を分解する。
            long a = 0, b = 0;
            a = Left.devided(eq.Substring(0, pair.Second));
            b = Right.devided(eq.Substring(pair.Second + 1));
            switch (pair.First)
            {
                case '+':
                    num = plus.calc(a, b);
                    break;
                case '-':
                    num = minus.calc(a, b);
                    break;
                case '*':
                    num = times.calc(a, b);
                    break;
                case '/':
                    num = devided.calc(a, b);
                    break;
            }
            return num;
        }
    }

    public abstract class ToM_Math
    {
        public abstract long calc(long a, long b);
        public abstract double calc(double a, double b);
        public abstract double calc(double a, long b);
    }


    class Plus : ToM_Math
    {
        public override double calc(double a, long b)
        {
            return a + b;
        }

        public override double calc(double a, double b)
        {
            return a + b;
        }

        public override long calc(long a, long b)
        {
            return a + b;
        }
    }

    class Minus : ToM_Math
    {
        public override double calc(double a, long b)
        {
            return a - b;
        }

        public override double calc(double a, double b)
        {
            return a - b;
        }

        public override long calc(long a, long b)
        {
            return a - b;
        }
    }

    class Times : ToM_Math
    {
        public override double calc(double a, long b)
        {
            return a * b;
        }

        public override double calc(double a, double b)
        {
            return a * b;
        }

        public override long calc(long a, long b)
        {
            return a * b;
        }
    }

    class Devided : ToM_Math
    {
        public override double calc(double a, long b)
        {
            return a / b;
        }

        public override double calc(double a, double b)
        {
            return a / b;
        }

        public override long calc(long a, long b)
        {
            return a - b;

        }
    }

    public class Pair
    {
        public char First;
        public int Second;
        public Pair(char x, int y)
        {
            First = x;
            Second = y;
        }
        public Pair()
        {

        }

    }
}
