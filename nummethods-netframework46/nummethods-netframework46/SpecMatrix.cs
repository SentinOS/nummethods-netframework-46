using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nummethods_netframework46
{
    public class SpecMatrix
    {
        double[,] A;
        int N;
        int Type;
        double arg;
        public double[,] Matrix { get { return A; } }
        public SpecMatrix(int n, int t)
        {
            N = n;
            Type = t;
            genMatrix();
        }
        public SpecMatrix(int n, int t, double a)
        {
            N = n;
            Type = t;
            arg = a;
            genMatrix();
        }
        public SpecMatrix(double a)
        {
            Type = 6;
            arg = a;
            genMatrix();
        }
        public SpecMatrix(int t)
        {
            Type = t;
            genMatrix();
        }
        private void genMatrix()
        {
            switch (Type)
            {
                case 1:
                    Matrix1();
                    break;
                case 2:
                    Matrix2();
                    break;
                case 3:
                    Matrix3();
                    break;
                case 4:
                    Matrix4();
                    break;
                case 5:
                    Matrix5();
                    break;
                case 6:
                    Matrix6();
                    break;
                case 7:
                    Matrix7();
                    break;
                case 8:
                    Matrix8();
                    break;
                case 9:
                    Matrix9();
                    break;
                case 10:
                    Matrix10();
                    break;
                case 11:
                    Matrix11();
                    break;

            }
        }
        private void Matrix1()
        {
            A = new double[N, N];
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    A[i - 1, j - 1] = 1 / ((double)i + (double)j - 1);
                }
            }
        }
        private void Matrix2()
        {
            N = 20;
            A = new double[N, N];
            for (int i = 0; i < 20; i++)
            {
                if (i != 19)
                {
                    A[i, i + 1] = 1;
                }
                A[i, i] = 1;
            }
        }
        private void Matrix3()
        {
            N = 7;
            A = new double[,] {{5,4,7,5,6,7,5},
                               {4,12,8,7,8,8,6},
                               {7,8,10,9,8,7,7},
                               {5,7,9,11,9,7,5},
                               {6,8,8,9,10,8,9},
                               {7,8,7,7,8,10,10},
                               {5,6,7,5,9,10,10}};
        }
        private void Matrix4()
        {
            A = new double[N, N];
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    if (i == j)
                    {
                        A[i - 1, j - 1] = 0.01 / (((double)N - (double)i + 1) / ((double)i + 1));
                    }
                    else if (i < j)
                    {
                        A[i - 1, j - 1] = 0;
                    }
                    else
                    {
                        A[i - 1, j - 1] = i * (N - j);
                    }
                }
            }
        }
        private void Matrix5()
        {
            A = new double[N, N];
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    if (i == j)
                    {
                        A[i - 1, j - 1] = 0.01 / (((double)N - (double)i + 1) / ((double)i + 1));
                    }
                    else if (i < j)
                    {
                        A[i - 1, j - 1] = j * (N - i);
                    }
                    else
                    {
                        A[i - 1, j - 1] = i * (N - j);
                    }
                }
            }
        }
        double cosec(double arg)
        {
            try
            {
                return 1 / Math.Sin(arg);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        double ctg(double arg)
        {
            try
            {
                return Math.Sin(arg) / Math.Cos(arg);
            }
            catch (Exception e)
            {
                return 0;
            }
        }
        private void Matrix6()
        {
            N = 8;
            A = new double[N, N];
            double[,] T = new double[,] { { 1, 1 }, { 1, 1 } };
            double[,] R = new double[,] { { ctg(arg), cosec(arg) }, { -cosec(arg), ctg(arg) } };
            double[,] S = new double[,] { { 1 - ctg(arg), cosec(arg) }, { 1 - cosec(arg), 1 + ctg(arg) } };
            for (int i = 0; i < N; i += 2)
            {
                for (int j = 0; j < N; j += 2)
                {
                    double[,] V = null;
                    if (i == j)
                    {
                        V = R;
                    }
                    else if (i == j + 2 || i + 2 == j)
                    {
                        V = S;
                    }
                    else
                    {
                        V = T;
                    }
                    for (int k = 0; k < 2; k++)
                    {
                        for (int t = 0; t < 2; t++)
                        {
                            Matrix[i + k, j + t] = V[k, t];
                        }
                    }
                }

            }
        }
        private void Matrix7()
        {
            A = new double[N, N];
            for (int i = 1; i <= N; i++)
            {
                A[i - 1, i - 1] = Math.Pow(arg, Math.Abs((double)N - (double)i * 2) / 2);
            }
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    if (i != j)
                    {
                        if (i == 1 || j == 1)
                        {
                            A[i - 1, j - 1] = Matrix[0, 0] / Math.Pow(arg, (double)j);
                        }
                        else if (i == N || j == N)
                        {
                            A[i - 1, j - 1] = A[N - 1, N - 1] / Math.Pow(arg, (double)j);
                        }
                        else
                        {
                            A[i - 1, j - 1] = 0;
                        }
                    }
                }
            }
        }
        private void Matrix8()
        {
            A = new double[N, N];
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    A[i - 1, j - 1] = Math.Exp((double)i * (double)j * arg);
                }
            }
        }
        private void Matrix9()
        {
            A = new double[N, N];
            for (int i = 1; i <= N; i++)
            {
                for (int j = 1; j <= N; j++)
                {
                    A[i - 1, j - 1] = arg + Math.Log((double)i * (double)j, 2);
                }
            }
        }
        private void Matrix10()
        {
            N = 4;
            A = new double[,] {{0.9143*Math.Pow(10,-4),0,0,0},
                                     {0.8762,0.756*Math.Pow(10,-4),0,0},
                                     {0.794,0.8143,0.9504*Math.Pow(10,-4),0},
                                     {0.8017,0.6123,0.7165,0.7123*Math.Pow(10,-4)} };
        }
        private void Matrix11()
        {
            Random rnd = new Random();
            A = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    A[i, j] = rnd.NextDouble() * 100 - 50;
                }
            }
        }

    }

}
