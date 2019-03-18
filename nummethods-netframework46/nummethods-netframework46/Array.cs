using System;

namespace NumMethods
{
    public class Array
    {

        private double[,] A;

        public bool AFactorized { get; set; }

        private double[] B;

        private double[] y;

        public uint Dimension { get; set; }


        public void InsertData()
        {
            Console.Clear();

            Console.WriteLine("Введите размерность матрицы A:");
            Dimension = uint.Parse(Console.ReadLine());
            this.A = new double[Dimension, Dimension];
            Console.WriteLine("Введите элементы матрицы A:\n");
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                {
                    try
                    {
                        A[i, j] = double.Parse(Console.ReadLine());
                    }

                    catch
                    {
                        Console.WriteLine("Неправильно введен элемент. Введите заново!");
                        A[i, j] = double.Parse(Console.ReadLine());
                    }

                }

            Console.Clear();
            AFactorized = false;
            PrintData();
            Console.Clear();
        }

        public void PrintData()
        {
            Console.Clear();

            Console.WriteLine("Матрица A:\n");
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    Console.Write(A[i, j] + " \t");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        public void MatrixFactorization()
        {

            //this.L = new double[Dimension, Dimension];
            //this.U = new double[Dimension, Dimension];
            for (int k = 0; k < Dimension; k++)
            {
                for (int j = k + 1; j < Dimension; j++)
                    A[k, j] /= A[k, k];
                for (int i = k + 1; i < Dimension; i++)
                    for (int j = k + 1; j < Dimension; j++)
                        A[i, j] -= A[i, k] * A[k, j];
            }


            Console.Clear();

            AFactorized = true;
            PrintData();


            //Console.WriteLine("Ìàòðèöà L:\n");
            //for (int i = 0; i < Dimension; i++)
            //{
            //    for (int j = 0; j < Dimension; j++)
            //        Console.Write((Math.Round(L[i, j], 2) + " \t"));
            //    Console.WriteLine();
            //}

            //Console.WriteLine("Ìàòðèöà U:\n");
            //for (int i = 0; i < Dimension; i++)
            //{
            //    for (int j = 0; j < Dimension; j++)
            //        Console.Write((Math.Round(U[i, j], 2) + " \t"));
            //    Console.WriteLine();
            //}

            Console.ReadKey();
        }

        public void SolutionSLAE()
        {
            Console.Clear();
            Console.WriteLine("Введите матрицу B: \n");
            this.B = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                try
                {
                    B[i] = double.Parse(Console.ReadLine());
                }

                catch
                {
                    Console.WriteLine("Неправильно введен элемент. Введите заново!");
                    B[i] = double.Parse(Console.ReadLine());
                }

            }

            for (int i = 0; i < Dimension; i++)
            {
                double sum = 0;
                for (int j = 0; j < i; j++)
                    sum += A[i, j] * y[j];
                y[i] = (B[i] - sum) / A[i, i];
            }

            for (int i = (int)Dimension - 1; i > -1; i--)
            {
                double sum = 0;
            }
            Console.ReadKey();
            Console.Clear();
        }

        public void FindDeterminant()
        {
            Console.Clear();
            if (AFactorized)
            {
                double determinant = A[0, 0];
                for (int i = 1; i < Dimension; i++)
                    determinant *= A[i, i];

                Console.WriteLine("Детерминант матрицы равен {0}", determinant);
            }

            else
            {
                Console.WriteLine("Матрица не факторизована!");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void FirstMatrixInversion() 
        {
            Console.Clear();
            Console.WriteLine("Заглушка. Когда-нибудь здесь что-нибудь да будет.");
            Console.ReadKey();
            Console.Clear();
        }

        public void SecondMatrixInversion() 
        {
            Console.Clear();
            Console.WriteLine("Заглушка. Когда-нибудь здесь что-нибудь да будет.");
            Console.ReadKey();
            Console.Clear();
        }

        public void Experiment() 
        {
            Console.Clear();
            Console.WriteLine("Заглушка. Когда-нибудь здесь что-нибудь да будет.");
            Console.ReadKey();
            Console.Clear();
        }
    }
}   