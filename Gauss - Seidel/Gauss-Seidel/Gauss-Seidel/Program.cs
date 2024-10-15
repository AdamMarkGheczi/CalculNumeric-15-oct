using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gauss_Seidel
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
            float[,] X = new float[n + 1, 50];

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
                    if (i != j) s += A[i, j];
                }

                if (s > A[i, i])
                {
                    Console.WriteLine("Sistemul nu se poate rezolva cu algoritmul lui Gauss - Seidel");
                    Console.ReadLine();
                    return;
                }
            }


            for (int i = 1; i <= n; i++)
            {
                Console.Write($"Introduceti b[{i}]: ");
                B[i] = int.Parse(Console.ReadLine());
            }

            for (int i = 1; i <= n; i++) X[i, 0] = B[i] / A[i, i];

            int k = 1;

            do
            {
                for (int i = 1; i <= n; i++)
                {
                    float[] S1 = new float[n+1], S2 = new float[n+1];
                    for (int j = 1; j <= i - 1; j++)
                    {
                        S1[i] += A[i, j]*X[j, k]; 
                    }

                    for (int j = i + 1; j <= n; j++)
                    {
                        if (j != i) S2[i] += A[i, j] * X[j, k - 1];
                    }

                    X[i, k] = (B[i] - S1[i] - S2[i]) / A[i, i];
                }

                k++;
            } while (Conditie(epsilon, n, X, k-1));
            k--;

            Console.Write($"La pasul {k} Vectorul X: ");
            for (int i = 1; i <= n; i++) Console.Write($"{X[i, k]} ");

            Console.ReadLine();
        }

        static bool Conditie(float epsilon, int n, float[,] X, int k)
        {
            float max = Math.Abs(X[1, k] - X[1, k-1]);
            for (int i = 1; i <= n; i++)
            {
                max = Math.Max(max, Math.Abs(X[i, k] - X[i, k-1]));
            }

            return max >= epsilon;
        }
    }
}
