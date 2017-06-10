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
            Value_Fra value = new Value_Fra();
            mathstring = Console.ReadLine();
            //mathstring = "1+2^2/6";//for debug
            Fraction ans = value.devided(mathstring);
            Console.WriteLine(ans.numerator);
            Console.WriteLine("-----");
            Console.WriteLine(ans.denominator);
            Console.ReadLine();

        }
    }

    #region Value class oldversion
    public class Value
    {

        Value Left;
        Value Right;
        double num;


        /*括弧がなくなった時、呼び出す*/
        public double devided(string eq)
        {
            if (eq == "")
            {
                Console.WriteLine("入力されていない");
                return 0;
            }
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
            Left = new Value();
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
    #endregion
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


    #region olderversion
    public class Value_Fra
    {

        Value_Fra Left;
        Value_Fra Right;
        Fraction num;


        /*括弧がなくなった時、呼び出す*/
        public Fraction devided(string eq)
        {
            int temp;
            if (eq == "")
            {
                Console.WriteLine("入力されていない");
                Environment.Exit(-1);
            }
            try
            {
                //if ((Math.Ceiling(double.Parse(eq)) - Math.Floor(double.Parse(eq))) == 0)
                //{
                //    Console.WriteLine("整数");
                //}
                temp = int.Parse(eq);//式が数字だけかどうかを判断する
                this.num.numerator = temp;
                num.denominator = 1;
            }
            catch (FormatException)//string eqが式である時
            {
                num = calc(eq);
                return num;
            }
            catch (OverflowException)
            {
                Console.WriteLine("intの最大値を超えてしまいました");
                Environment.Exit(-1);
            }
            return this.num;
        }
        private Fraction calc(string eq)
        {
            //括弧をみつけたら中を無視する。
            //一番外に括弧があるときは、最初に取り除く。
            Pair pair = new Pair();
            FraCalc fraCal = new FraCalc();
            Right = new Value_Fra();
            Left = new Value_Fra();
            char op;
            //一番外側の括弧の位置を把握する必要がある。
            //一番外側の括弧の左右に演算子がない場合もあるので、ちょっと困る。
            //一番外側の括弧を取り除くことが必要
            bool Bra_rm = true;
            char[] operater = new char[] { '+', '-', '*', '/', '^' };
            do
            {
                Bra_rm = false;
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
            Fraction a, b;
            a = Left.devided(eq.Substring(0, pair.Second));
            b = Right.devided(eq.Substring(pair.Second + 1));
            switch (pair.First)
            {
                case '+':
                    num = fraCal.Plus(a, b);
                    break;
                case '-':
                    num = fraCal.Minus(a, b);
                    break;
                case '*':
                    num = fraCal.Times(a, b);
                    break;
                case '/':
                    num = fraCal.Devided(a, b);
                    break;
                case '^':
                    num = fraCal.Power(a, b);
                    break;
            }
            return num;
        }
    }
    #endregion
    public class FraCalc
    {
        public Fraction Plus(Fraction fra1, Fraction fra2)
        {
            Fraction ans;
            ans.numerator = fra1.denominator * fra2.numerator + fra2.denominator * fra1.numerator;
            ans.denominator = fra1.denominator * fra2.denominator;
            ans = Redction(ans);
            return ans;
        }
        /// <summary>
        /// 分数の引き算メソッド
        /// </summary>
        /// <param name="fra1">引かれる数(a)：a-b</param>
        /// <param name="fra2">引く数(b):a-b</param>
        /// <returns></returns>
        public Fraction Minus(Fraction fra1, Fraction fra2)
        {
            Fraction ans;
            ans.numerator = fra2.denominator * fra1.numerator - fra1.denominator * fra2.numerator;
            ans.denominator = fra1.denominator * fra2.denominator;
            ans = Redction(ans);
            return ans;
        }
        public Fraction Times(Fraction fra1, Fraction fra2)
        {
            Fraction ans;
            ans.denominator = fra1.denominator * fra2.denominator;
            ans.numerator = fra1.numerator * fra2.numerator;
            ans = Redction(ans);
            return ans;
        }
        /// <summary>
        /// frac1/frac2で想定
        /// </summary>
        /// <param name="fra1">割られる数(a):a/b　</param>
        /// <param name="fra2">割る数(b):a/b</param>
        /// <returns></returns>
        public Fraction Devided(Fraction fra1, Fraction fra2)
        {
            Fraction ans;
            ans.denominator = fra1.denominator * fra2.numerator;
            ans.numerator = fra1.numerator * fra2.denominator;
            ans = Redction(ans);
            return ans;
        }
        public Fraction Power(Fraction fra1, Fraction fra2)
        {
            Fraction ans;
            ans.denominator = (int)Math.Round(Math.Pow(fra1.denominator, fra2.numerator / fra2.denominator), 0);
            ans.numerator = (int)Math.Round(Math.Pow(fra1.numerator, fra2.numerator / fra2.denominator), 0);
            ans = Redction(ans);
            return ans;
        }
        /// <summary>
        /// 通分するメソッド：ユークリッド互除法で実装
        /// </summary>
        /// <param name="frac">分数</param>
        /// <returns></returns>
        private Fraction Redction(Fraction frac)
        {
            int max = Math.Max(frac.denominator, frac.numerator);
            int min = Math.Min(frac.denominator, frac.numerator);
            int surplus;
            while (true)
            {
                surplus = max % min;
                if (surplus == 0)
                {
                    break;
                }
                max = min;
                min = surplus;
            }
            frac.denominator = frac.denominator / min;
            frac.numerator = frac.numerator / min;
            return frac;
        }
    }
    public struct Fraction
    {
        public int denominator, numerator;
    }



    public class value_stack
    {
        Stack<object> Fragment = new Stack<object>();
        public void hoge(string eq)
        {
            for (int i = 0; i < eq.Length; i++)
            {
                char[] operater = new char[] { '+', '-', '*', '/' };

            }
        }

    }
}
