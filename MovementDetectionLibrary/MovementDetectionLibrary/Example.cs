using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementDetectionLibrary
{
    public class Example
    {

        public static void Main()
        {
            int m = 1;
            FullBody exm = new FullBody();
            while (m != 0)
            {                
                m = Console.Read();
                Console.WriteLine("Posicion"+exm.returnPosition("Head"));
                Console.Write("hola");
                m = Console.Read();
            }
            
        }
    }
}
