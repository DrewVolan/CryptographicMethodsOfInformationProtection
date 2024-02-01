using System;
using System.Collections.Generic;

namespace CalculatingTheLegendreSymbol
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите a: ");
            var a = int.Parse(Console.ReadLine());
            Console.Write("Введите p: ");
            var p = int.Parse(Console.ReadLine());
            CalculatingTheLegendreSymbol(a, p);
            //Console.WriteLine(CalculatingTheLegendreSymbol(561, 757));
            //Console.WriteLine(CalculatingTheLegendreSymbol(426, 557));
            Console.ReadKey();
        }

        static sbyte CalculatingTheLegendreSymbol(int a, int p)
        {
            var primeFactors = PrimeFactorization(a);

            Console.WriteLine($"Разложим число {a} на простые множители" +
                $"\r\nи получим {a} = {StringBuilder.ProductOfPrimeNumbers(primeFactors)}. Следовательно, данный символ Лежандра" +
                $"\r\nможно представить в виде произведения следующих символов Лежандра: {StringBuilder.LegendresNewView(primeFactors, p)}");

            var localLegendreSymbols = new List<sbyte>();
            foreach (var factor in primeFactors)
            {
                localLegendreSymbols.AddRange(CalculateLocalLegendreSymbol(factor, p));
            }

            var result = 1;
            foreach (var symbol in localLegendreSymbols)
            {
                result *= symbol;
            }
            Console.WriteLine($"{StringBuilder.ProductResult(localLegendreSymbols)} = {result}");
            return (sbyte)result;
        }

        static List<sbyte> CalculateLocalLegendreSymbol(int factor, int p)
        {
            Console.WriteLine($"({factor}/{p})");
            if (factor == 2)
            {
                return new List<sbyte>() { GetLocalLegendreSymbolWhenFactor2(p) };
            }
            else
            {
                var newP = factor % 4 == 1 || p % 4 == 1 ? p % factor : factor - (p % factor);
                if (factor % 4 == 1 || p % 4 == 1)
                {
                    Console.WriteLine($"{{{factor} mod 4 = {factor % 4}}} и {{{p} mod 4 = {p % 4}}} значит переворачиваем без изменения знака");
                }
                else
                {
                    Console.WriteLine($"{{{factor} mod 4 = {factor % 4}}} и {{{p} mod 4 = {p % 4}}} значит переворачиваем со знаком минус");
                }
                Console.WriteLine($"({p}/{factor}) = ({newP}/{factor})");
                if (newP == 2) return new List<sbyte>() { GetLocalLegendreSymbolWhenFactor2(p) };
                else
                {
                    if (IsPrime(newP))
                    {
                        if (Math.Sqrt(newP) == (int)Math.Sqrt(newP))
                        {
                            Console.WriteLine($"({newP}/{factor}) = ({Math.Sqrt(newP)}^2/{factor}) = 1");
                            Console.WriteLine("----------------");
                            return new List<sbyte>() { 1 };
                        }
                        else
                        {
                            Console.WriteLine($"({newP}/{factor}) = -1");
                            Console.WriteLine("----------------");
                            return new List<sbyte>() { -1 };
                        }
                    }
                    else
                    {
                        var primeFactors = PrimeFactorization(newP);

                        Console.WriteLine($"Разложим число {newP} на простые множители" +
                            $"\r\nи получим {newP} = {StringBuilder.ProductOfPrimeNumbers(primeFactors)}. Следовательно, данный символ Лежандра" +
                            $"\r\nможно представить в виде произведения следующих символов Лежандра: {StringBuilder.LegendresNewView(primeFactors, factor)}");

                        var result = new List<sbyte>();
                        foreach (var newFactor in primeFactors)
                        {
                            result.AddRange(CalculateLocalLegendreSymbol(newFactor, factor));
                        }
                        return result;
                    }
                }
            }
        }

        private static sbyte GetLocalLegendreSymbolWhenFactor2(int p)
        {
            Console.Write($"{{{p} mod 8 = {p % 8}}} ");
            if (p % 8 == 1 || p % 8 == 7)
            {
                Console.WriteLine("= 1");
                Console.WriteLine("----------------");
                return 1;
            }
            else
            {
                Console.WriteLine("= -1");
                Console.WriteLine("----------------");
                return -1;
            }
        }

        static List<int> PrimeFactorization(int x)
        {
            var result = new List<int>();

            var divider = 2;
            while (x > 1)
            {
                if (x % divider == 0)
                {
                    result.Add(divider);
                    x /= divider;
                    divider = 2;
                }
                else
                {
                    divider++;
                }
            }

            return result;
        }

        static bool IsPrime(int x)
        {
            var result = true;

            for (int i = 2; i <= Math.Sqrt(x); i++)
            {
                if (x % i == 0)
                {
                    result = false;
                    break;
                }
            }

            return result;
        }

        static class StringBuilder
        {
            internal static string ProductOfPrimeNumbers(List<int> primeFactors)
            {
                var result = "";
                for(int i = 0; i < primeFactors.Count; i++)
                {
                    result += primeFactors[i].ToString();
                    if (i + 1 != primeFactors.Count)
                    {
                        result += " ∙ ";
                    }
                }
                return result;
            }

            internal static string LegendresNewView(List<int> primeFactors, int p)
            {
                var result = "";
                foreach (var factor in primeFactors)
                {
                    result += $"({factor}/{p})";
                }
                return result;
            }

            internal static string ProductResult(List<sbyte> resultList)
            {
                var result = "";
                for (int i = 0; i < resultList.Count; i++)
                {
                    result += resultList[i].ToString();
                    if (i + 1 != resultList.Count)
                    {
                        result += " ∙ ";
                    }
                }
                return result;
            }
        }
    }
}
