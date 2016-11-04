using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LBFA_test
{
    class Program
    {
        static void Main(string[] args)
        {
            input_A input_a = new input_A();
            input_B input_b = new input_B();
            bool q_1 = true, q_2 = false;
            string inputstring = null;
        LOOP:
            q_1 = true;
            q_2 = false;
            inputstring = Console.ReadLine();
            for (int i = 0; i < inputstring.Length; i++)
            {
                if (inputstring[i] == 'A')
                {
                    input_a.set(q_1, q_2);
                    q_1 = input_a.q_1;
                    q_2 = input_a.q_2;
                }
                else if (inputstring[i] == 'B')
                {
                    input_b.set(q_1, q_2);
                    q_1 = input_b.q_1;
                    q_2 = input_b.q_2;
                }
            }
            bool ans = q_1 & q_2;
            Console.WriteLine(ans);
            goto LOOP;
            //Console.ReadLine();

        }
    }

    public class input_A
    {
        public bool q_1, q_2;
        public void set(bool q_1, bool q_2)
        {
            this.q_1 = q_1 | q_2;
            this.q_2 = q_2;

        }

        public input_A()
        {

        }
    }
    class input_B
    {
        public bool q_1, q_2;
        public void set(bool q_1, bool q_2)
        {
            this.q_1 = true;
            this.q_2 = q_1 & (!q_2);

        }
        public input_B()
        {

        }
    }
}
