﻿using System;
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
            mathstring = Console.ReadLine();
            //mathstring = "(((1+2)*(3+4)))+1";//for debug
            double ans = value.devided(mathstring);
            Console.WriteLine(ans);
            Console.ReadLine();

        }
    }
    public class Value
    {

        Value Left;
        Value Right;
        double num;

        /*括弧がなくなった時、呼び出す*/
        public double devided(string eq)
        {

            try
            {
                num = double.Parse(eq);//式が数字だけかどうかを判断する
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
        private double calc(string eq)
        {
            //括弧をみつけたら中を無視する。
            //一番外に括弧があるときは、最初に取り除く。
            Pair pair = new Pair();
            Plus plus = new Plus();
            Devided devided = new Devided();
            Minus minus = new Minus();
            Times times = new Times();
            Right = new Value();
            Left = new Calc2.Value();
            char op;
            //一番外側の括弧の位置を把握する必要がある。
            //一番外側の括弧の左右に演算子がない場合もあるので、ちょっと困る。
            //一番外側の括弧を取り除くことが必要
            bool Bra_rm = true;
            do
            {
                Bra_rm = false;
                char[] operater = new char[] { '+', '-', '*', '/' };
                int bra_state = 0;
                for (int i = 0; i < operater.Length; i++)
                {

                    for (int j = 0; j < eq.Length; j++)
                    {
                        if (eq[j] == '(')
                        {
                            bra_state++;
                        }
                        else if (eq[j] == ')')
                        {
                            bra_state--;
                        }
                        op = eq[j];
                        if (op == operater[i] && bra_state == 0)//括弧が開いているときはbra_state==0になっている。
                        {
                            pair.First = operater[i];
                            pair.Second = j;
                            goto LOOPOUT;
                        }
                    }
                }
            LOOPOUT:
                if (pair.Second == 0)
                {
                    eq = eq.Substring(1, eq.Length - 2);
                    Bra_rm = true;
                }
            } while (Bra_rm);
            //文字列を分解する。
            double a = 0, b = 0;
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
        public abstract double calc(long a, double b);
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
        public override double calc(long a, double b)
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
        public override double calc(long a, double b)
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
        public override double calc(long a, double b)
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
            return a / b;

        }
        public override double calc(long a, double b)
        {
            return a / b;
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
    /*
    //括弧：bracketの内側をdevidedに送るクラス
    public class RmBracket
    {
        Pair pair = new Pair();
        string eq;
        RmBracket(string eq)
        {
            eq = this.eq;
        }
        public string GetInsideBracket()//括弧の中の数式を探し、それの解を求める。
        {
            //もっとも左端の括弧を探す
            int leftpos = 0, rightpos = 0;
            for (int i = 0; i < eq.Length; i++)
            {
                if (eq[i] == '(')
                {
                    leftpos = i;
                }
            }
            //もっとも左端の括弧に最も近い右端の括弧を探す
            for (int i = leftpos; i < eq.Length; i++)
            {
                if (eq[i] == ')')
                {
                    rightpos = i;
                }
            }
            //とりあえず括弧による数式の順序変更のみを行えるようにする(変数を扱うことは考えない)

            return null;
        }
    }
    public class CheckEqString
    {

        CheckEqString(string eq)
        {
            for (int i = 0; i < eq.Length; i++)
            {
                //括弧の数がおかしい。
                //演算子が重複している。
                //無効な文字列が挿入されている。
            }
        }
    }
    */
}
