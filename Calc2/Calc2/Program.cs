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
        public Frac()
        {

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
}