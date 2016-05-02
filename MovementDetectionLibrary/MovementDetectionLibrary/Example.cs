using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovementDetectionLibrary
{
    public class Example
    {
        public int a = 0;

        private void Cambio(FullBody f, List<BodyPointPosition> b)
        {
            a++;
            Console.WriteLine("Cambio: " + a.ToString());
            Console.WriteLine("Joints changed: " + b.Count.ToString());
        }

        void Subscribe(FullBody f)
        {
            f.BodyChanged += new FullBody.BodyChangedHandler(Cambio);
        }

        public static void Main()
        {
            int m = 1;
            FullBody exm = new FullBody();

            Example a = new Example();
            a.Subscribe(exm);

            while (m != 0)
            {
                BodyPointPosition pos = exm.returnPosition(BodyParts.KneeLeft);
            }
            
        }
    }
}
