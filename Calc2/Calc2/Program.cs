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
            Frac frac = new Frac();
            Integer integer = new Integer();
            if (frac.GetType() == integer.GetType())
                Console.WriteLine(frac.GetType());
            else
                Console.WriteLine("型が不一致です.\n");
            Console.ReadLine();
        }
    }
    public abstract class Numeral
    {

    }
    public class Frac :Numeral 
    {
        private long Denominator;
        private long Molecular;
        public Frac()
        {

        }
        public Frac(long Denominator, long Molecular)
        {
            this.Denominator = Denominator;//分母
            this.Molecular = Molecular;//分子
        }
        public void SetFrac(long Denominator, long Molecular)
        {
            this.Denominator = Denominator;//分母
            this.Molecular = Molecular;//分子
        }
        public long GetDenominator()
        {
            return Denominator;
        }
        public long GetMolecular()
        {
            return Molecular;
        }
    }
    public class Natural : Numeral
    {

    }
    public class Integer :Numeral
    {
        public Integer()
        {

        }
    }
    public class  Decimal : Numeral
    {

    }
    public class Expnential : Numeral
    {

    }
    public class Imaginary : Numeral
    {

    }
    internal class CalcFrac
    {
        private Frac ans = new Frac();
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
}