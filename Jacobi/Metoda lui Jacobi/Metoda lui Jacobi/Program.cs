using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Metoda_lui_Jacobi
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int n;
            Console.WriteLine("Introduceti n");
            n = int.Parse(Console.ReadLine());

            Console.WriteLine("Introduceti epsilon");
            float epsilon = float.Parse(Console.ReadLine());

            float[,] A = new float[n + 1, n + 1];
            float[] B = new float[n + 1];
            float[] X = new float[n + 1];
            float[] Xprev = new float[n + 1];



            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= n; j++)
                {
                    Console.Write($"Introduceti A[{i},{j}]: ");
                    A[i, j] = int.Parse(Console.ReadLine());
                }
            }

            // cond. de convergenta

            for (int i = 1; i <= n; i++)
            {
                float s = 0;
                for (int j = 1; j <= n; j++)
                {
                    if(i != j) s += A[i, j];
                }

                if(s > A[i, i])
                {
                    Console.WriteLine("Sistemul nu se poate rezolva cu algoritmul lui Jacobi");
                    Console.ReadLine();
                    return;
                }
            }


            for (int i = 1; i <= n; i++)
            {
                Console.Write($"Introduceti b[{i}]: ");
                B[i] = int.Parse(Console.ReadLine());
            }

            for (int i = 1; i <= n; i++) Xprev[i] = B[i] / A[i, i];
            Array.Copy(Xprev, X, n + 1);

            int k = 0;

            do
            {
                Array.Copy(X, Xprev, n+1);
                for (int i = 1; i <= n; i++)
                {
                    float S = 0;
                    for(int j = 1;j <= n; j++)
                    {
                        if (i != j) S += A[i, j] * Xprev[j];
                    }
                    X[i] = (B[i] -  S) / A[i, i];
                }
                
                k++;
            } while (Conditie(epsilon, n, X, Xprev));
            k--;

            Console.Write($"La pasul {k} Vectorul X: ");
            for (int i = 1; i <= n; i++) Console.Write($"{X[i]} ");

            Console.ReadLine();

        }

        static bool Conditie(float epsilon, int n, float[] X, float[] Xprev)
        {
            float max = Math.Abs(X[1] - Xprev[1]);
            for (int i = 1; i <= n; i++) { 
                max = Math.Max(max, Math.Abs(X[i] - Xprev[i]));
            }

            return max >= epsilon;
        }
    }
}
/*
n = 3
eps = 0,001
5 1 1
1 6 4
1 1 10

10 4 -7


 * */