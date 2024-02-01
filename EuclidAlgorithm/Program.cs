using System;
using System.Linq;

namespace EuclidAlgorithm
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите a: ");
            var a = int.Parse(Console.ReadLine());
            Console.Write("Введите b: ");
            var b = int.Parse(Console.ReadLine());

            Console.WriteLine($"| q | r | x | y | a | b | x2 | x1 | y2 | y1 |");
            Console.WriteLine($"| - | - | - | - | {a} | {b} | 1 | 0 | 0 | 1 |");
            var result = EuclidAlgorithm(0, 0, 0, 0, a, b, 1, 0, 0, 1);
            Console.WriteLine("d = {0}; x = {1}; y = {2}", result.d, result.x, result.y);
            Console.ReadKey();
        }

        static EuclidAlgorithmResult EuclidAlgorithm(int q, int r, int x, int y, int a, int b, int x2, int x1, int y2, int y1)
        {
            q = Math.Abs(a / b);
            r = a % b;
            if (r != 0)
            {
                x = x2 - (q * x1);
                y = y2 - (q * y1);
                a = b;
                b = r;
                x2 = x1;
                y2 = y1;
                x1 = x;
                y1 = y;
                Console.WriteLine($"| {q} | {r} | {x} | {y} | {a} | {b} | {x2} | {x1} | {y2} | {y1} |");
                return EuclidAlgorithm(q, r, x, y, a, b, x2, x1, y2, y1);
            }
            else
            {
                a = b;
                b = r;
                x2 = x1;
                y2 = y1;
                Console.WriteLine($"| {q} | {r} | X | X | {a} | {b} | {x2} | X | {y2} | X |");
                return new EuclidAlgorithmResult(a, x2, y2);
            }
        }

        struct EuclidAlgorithmResult
        {
            public int d;
            public int x;
            public int y;
            
            public EuclidAlgorithmResult(int d, int x, int y)
            {
                this.d = d;
                this.x = x;
                this.y = y;
            }
        }
    }
}