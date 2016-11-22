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
            //string mathstring = null;
            Value value = new Value();

            //mathstring = Console.ReadLine();
            //mathstring = "1/2+3/4";//for debug
            //double ans = value.devided(mathstring);
            //Console.WriteLine(ans);
            Console.ReadLine();

            ValFrac valfra = new ValFrac();
            CalcFrac Cfra = new CalcFrac();
            Frac flac1 = new Frac();
            //flac1.SetFrac(4, 9);
            //Frac flac2 = new Frac(3, 7);
            //FracWrite fracout = new FracWrite(Cfra.FracAdder(flac1, flac2));
            //FracWrite fracout2 = new FracWrite(Cfra.TimesFrac(flac1, flac2));
            //FracWrite outfrac3 = new FracWrite(Cfra.Euclidean(Cfra.TimesFrac(flac1,flac2)));
            Console.ReadLine();
        }
    }
    public class Value
    {

        Value Left;
        Value Right;
        int num;

        /*括弧がなくなった時、呼び出す*/
        internal Frac devided(string eq)
        {
            Frac frac = new Frac();
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
            frac.SetFrac(1, num);
            return frac;
        }
        private int calc(string eq)
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
            Frac a = new Frac();
            Frac b = new Frac();
            a = Left.devided(eq.Substring(0, pair.Second));
            b = Right.devided(eq.Substring(pair.Second + 1));
            CalcFrac calcfrac = new CalcFrac();
            switch (pair.First)
            {
                case '+':
                    calcfrac.FracAdder(a, b);
                    break;
                case '-':
                    calcfrac.FracAdder(a, b);
                    break;
                case '*':
                    break;
                case '/':
                    break;
            }
            return num;
        }
    }

    internal class ValFrac
    {
        int num = 0;
        void Eval(string eq)
        {
            bool num_check = false;
            num_check = int.TryParse(eq, out num);
            if (num_check == false)
            {
                Console.WriteLine("Serious Error has happened.");
                Environment.Exit(-1);
            }
        }

        internal ValFrac()
        {

        }
    }
    class Frac
    {
        private int Denominator;
        private int Molecular;
        internal void SetFrac(int Denominator, int Molecular)
        {
            this.Denominator = Denominator;//分母
            this.Molecular = Molecular;//分子
        }
        internal Frac()
        {

        }
        internal Frac(int Denominator, int Molecular)
        {
            this.Denominator = Denominator;//分母
            this.Molecular = Molecular;//分子
        }
        internal Frac(Frac frac)
        {
            this.Denominator = frac.GetDenominator();
            this.Molecular = frac.GetMolecular();
        }

        internal int GetDenominator()
        {
            return Denominator;
        }
        internal int GetMolecular()
        {
            return Molecular;
        }
        internal void Increase(int x, Frac frac)// x is Denominator
        {
            Denominator = frac.GetDenominator() * x;
            Molecular = frac.GetMolecular() * x;
        }
    }
    internal class CalcFrac
    {
        Frac ans = new Frac();
        internal Frac FracAdder(Frac frac1, Frac frac2)
        {
            Frac tempfrac1 = new Frac();
            Frac tempfrac2 = new Frac();
            tempfrac2.Increase(frac1.GetDenominator(), frac2);
            tempfrac1.Increase(frac2.GetDenominator(), frac1);
            ans.SetFrac(tempfrac2.GetDenominator(),
                tempfrac1.GetMolecular() + tempfrac2.GetMolecular());
            return ans;
        }
        internal Frac TimesFrac(Frac frac1, Frac frac2)
        {
            ans.SetFrac(frac1.GetDenominator() * frac2.GetDenominator(),
                frac1.GetMolecular() * frac2.GetMolecular());
            return ans;
        }
        internal Frac DeviedFrac(Frac frac1, Frac frac2)
        {
            ans.SetFrac(frac1.GetDenominator() * frac2.GetMolecular(),
                frac1.GetMolecular() * frac2.GetDenominator());
            return ans;
        }
        Frac Euclidean(Frac frac1)
        {
            int[] temp = new int[20];
            int lastnum = 0;
            temp[0] = frac1.GetDenominator();
            temp[1] = frac1.GetMolecular();
            for (int i = 2; i < temp.Length; i++)
            {
                temp[i] = temp[i - 2] % temp[i - 1];
                if (temp[i] == 0)
                {
                    lastnum = i - 1;
                    break;
                }
            }
            Frac ans = new Frac(temp[0] / temp[lastnum], temp[1] / temp[lastnum]);
            return ans;

        }

        internal CalcFrac()
        {
        }
    }
    internal class FracWrite
    {
        internal FracWrite(Frac frac)
        {
            Console.WriteLine(frac.GetMolecular());
            Console.WriteLine("---------------------");
            Console.WriteLine(frac.GetDenominator());
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

    public class Numeral
    {
        Numeral numeral;
        private bool imagenaly = false;
        private double num_d;
        private int num_i;
        private Frac num_f;
        //get
        Numeral GetNumeral()
        {
            return numeral;
        }
        //set
        void setRealNumeral(int num_i)
        {
            this.num_i = num_i;
        }
        void setRealNumeral(double num_d)
        {
            this.num_d = num_d;
        }
        void setRealNumeral(Frac num_f)
        {
            this.num_f = num_f;
        }
        Numeral()
        {
        }

    }
    class Exponential
    {
        Numeral numeral;


        Exponential()
        {

        }
    }

}