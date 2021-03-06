﻿using System;

namespace NumMethods
{
    public class Array
    {

        private double[,] MtrxOfCoefs; //A

        public bool AFactorized { get; set; }

        public int Znak { get; set; }

        int oper_f = 0; //Фактическое число операций

        int oper_t = 0; //Теоретическое число операций

        double time_f = 0; //Время работы программы
        public double TIME { get { return time_f; } }
        public int OPER_F { get { return oper_f; } }
        public int OPER_T { get { return oper_t; } }

        //b
        private double[] VctrOfFreeMembers;

        //x
        private double[] VctrOfVars;

        //первая обращенная матрица
        private double[,] InvertMatrix1;

        //вторая обращенная матрица
        private double[,] InvertMatrix2;

        //матрица перестановок
        private int[] TranspositionJ;

        private int[] TranspositionI;
        public int Dimension { get; set; }

        public void InsertData()
        {
            Console.Clear();

            Console.WriteLine("Введите размерность матрицы А:");
            Dimension = int.Parse(Console.ReadLine());
            while (Dimension <= 0)
            {
                Console.WriteLine("Недопустимое значение! Введите размерность матрицы А заново:\n");
                Dimension = int.Parse(Console.ReadLine());
                Console.Clear();
            }

            this.MtrxOfCoefs = new double[Dimension, Dimension];
            TranspositionJ = new int[Dimension];
            TranspositionI = new int[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                TranspositionJ[i] = i;
                TranspositionI[i] = i;
            }

            Console.WriteLine("Введите элементы матрицы А:\n");
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                {

                    try
                    {
                        MtrxOfCoefs[i, j] = double.Parse(Console.ReadLine());
                    }

                    catch
                    {
                        Console.WriteLine("Неправильно введен элемент. Введите заново!");
                        MtrxOfCoefs[i, j] = double.Parse(Console.ReadLine());
                    }

                }

            Console.Clear();
            this.VctrOfVars = new double[Dimension];
            AFactorized = false;
            PrintData();
            Console.Clear();
        }

        public void PrintData()
        {
            Console.Clear();

            Console.WriteLine("Матрица А:\n");
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    Console.Write(MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] + " \t");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        public void PrintData(double[,] x)
        {
            Console.Clear();

            Console.WriteLine("Матрица А:\n");
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    Console.Write(x[TranspositionI[i], TranspositionJ[j]] + " \t");
                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void PrintDataTrans(double[,] x)
        {
            Console.Clear();

            Console.WriteLine("Матрица А:\n");
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    Console.Write(x[i, j].ToString("f2") + " \t");
                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void MatrixFactorization()
        {

            oper_f = 0;                         //Фактическое число оперций.
            oper_t = 0;                         //Теоретическое число операций.
            DateTime date = DateTime.Now;
            TranspositionJ = new int[Dimension];
            TranspositionI = new int[Dimension];
            Znak = 1;
            for (int i = 0; i < Dimension; i++)
            {
                TranspositionJ[i] = i;
                TranspositionI[i] = i;
            }

            for (int k = 0; k < Dimension; k++)
            {
                int iMax = k;
                int jMax = k;

                for (int i = k; i < Dimension; i++)
                {
                    for (int j = k; j < Dimension; j++)
                    {
                        if (Math.Abs(MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]]) > Math.Abs(MtrxOfCoefs[TranspositionI[k], TranspositionJ[k]]))
                        {
                            iMax = i;
                            jMax = j;
                            //oper_f++;
                        }
                    }
                    //if (Math.Abs(MtrxOfCoefs[k, Transposition[k]]) < 2 * Double.Epsilon)
                    //{
                    //    Console.WriteLine("Ошибка разложения! ");
                    //    return;
                    //}
                }
                if (iMax != k)
                {
                    int buf = TranspositionI[k];
                    TranspositionI[k] = TranspositionI[iMax];
                    TranspositionI[iMax] = buf;
                    Znak *= -1;
                    //oper_f++;
                }
                if (jMax != k)
                {
                    int buf = TranspositionJ[k];
                    TranspositionJ[k] = TranspositionJ[jMax];
                    TranspositionJ[jMax] = buf;
                    Znak *= -1;
                    //oper_f++;
                }
                //////////////////////////////////////////////
                for (int j = k + 1; j < Dimension; j++)
                {
                    MtrxOfCoefs[TranspositionI[k], TranspositionJ[j]] /= MtrxOfCoefs[TranspositionI[k], TranspositionJ[k]];
                    oper_f++;
                }
                for (int i = k + 1; i < Dimension; i++)
                {
                    for (int j = k + 1; j < Dimension; j++)
                    {
                        MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] -= MtrxOfCoefs[TranspositionI[i], TranspositionJ[k]] * MtrxOfCoefs[TranspositionI[k], TranspositionJ[j]];
                        oper_f++;
                    }
                }
            }
            TimeSpan sp = DateTime.Now - date;      //Фиксируем время окончания LU разложения
            time_f = sp.TotalMilliseconds;

            //Console.Clear();

            AFactorized = true;
            //Console.WriteLine("Матрица факторизирована.\n");
            //Console.ReadKey();
            //Console.Clear();

        }

        public void SolutionSLAE(bool accuracy)
        {
            //Console.Clear();
            //Проверка на факторизованность матрицы
            if (!AFactorized)
            {
                //Console.Clear();
                MatrixFactorization();
                //Console.WriteLine("Матрица была не факторизирована, факторизировали.\n");
            }
            //Console.WriteLine("Введите вектор свободных членов: \n");

            oper_t += (Dimension * Dimension * Dimension) / 3;
            //Заполняем вектор свободных членов данными
            VctrOfVars = new double[Dimension];
            //for (int i = 0; i < Dimension; i++)
            //{
            //    try
            //    {
            //        VctrOfFreeMembers[i] = double.Parse(Console.ReadLine());
            //    }

            //    catch
            //    {
            //        Console.WriteLine("Неправильно введен элемент. Введите заново!");
            //        VctrOfFreeMembers[i] = double.Parse(Console.ReadLine());
            //    }

            //}

            if (accuracy)
            {
                var rnd = new Random();

                for (int j = 0; j < Dimension; j++)
                {
                    VctrOfFreeMembers[j] = rnd.NextDouble() * 100 - 50;
                }
            }
            DateTime date = DateTime.Now;           //Сохраняем время начала решения СЛАУ


            double[] x = new double[Dimension];
            double[] y = new double[Dimension];



            ///////////////////////////////////////////////
            //Создаем дополнительную переменную для суммирования известных членов в строках матрицы
            double TempSum = 0;
            //Цикл по всей матрице, двигаясь обратным ходом по верхней треугольной матрице

            for (int i = 0; i < Dimension; i++)
            {
                TempSum = 0;

                //Высчитывание суммы всех известных членов и коэффициентов при них до I-ого столбца
                for (int j = 0; j <= i; j++)
                {
                    TempSum += MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] * y[j];
                    oper_f++;
                }
                //Присваивание i-ой неизвестной 
                y[i] = (VctrOfFreeMembers[TranspositionI[i]] - TempSum) / MtrxOfCoefs[TranspositionI[i], TranspositionJ[i]];
                oper_f++;
            }


            for (int i = Dimension - 1; i >= 0; i--)
            {
                TempSum = 0;
                //Высчитывание суммы всех известных членов и коэффициентов при них до I-ого столбца
                for (int j = i + 1; j < Dimension; j++)
                {
                    TempSum += MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] * x[TranspositionJ[j]];
                    oper_f++;
                }
                //Присваивание i-ой неизвестной 
                x[TranspositionJ[i]] = y[i] - TempSum;
                //oper_f++;
            }

            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;
            //Вывод вектора с неизвестными
            //Console.WriteLine("Вектор неизвестных X:\n");
            //for (uint i = 0; i < Dimension; i++)
            //    Console.WriteLine(x[i] + "\n");

            x.CopyTo(VctrOfVars, 0);
            //Console.ReadKey();
            //Console.Clear();
        }

        public void SolutionSLAE(double[] b, ref double[] x)
        {
            ////Проверка на факторизованность матрицы
            //if (!AFactorized)
            //{
            //    Console.Clear();
            //    MatrixFactorization();
            //    Console.WriteLine("Матрица была не факторизирована, факторизировали.\n");
            //}

            double[] y = new double[Dimension];


            DateTime date = DateTime.Now;           //Сохраняем время начала решения СЛАУ

            ///////////////////////////////////////////////
            //Создаем дополнительную переменную для суммирования известных членов в строках матрицы
            double TempSum = 0;
            //Цикл по всей матрице, двигаясь обратным ходом по верхней треугольной матрице

            for (int i = 0; i < Dimension; i++)
            {
                TempSum = 0;
                //Высчитывание суммы всех известных членов и коэффициентов при них до I-ого столбца
                for (int j = 0; j < Dimension; j++)
                {
                    TempSum += MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] * y[j];
                    oper_f++;
                }
                //Присваивание i-ой неизвестной 
                y[i] = (b[TranspositionI[i]] - TempSum) / MtrxOfCoefs[TranspositionI[i], TranspositionJ[i]];
                oper_f++;
            }


            for (int i = Dimension - 1; i >= 0; i--)
            {
                TempSum = 0;
                //Высчитывание суммы всех известных членов и коэффициентов при них до I-ого столбца
                for (int j = Dimension - 1; j > i; j--)
                {
                    TempSum += MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] * x[TranspositionJ[j]];
                    oper_f++;
                }
                //Присваивание i-ой неизвестной 
                x[TranspositionJ[i]] = y[i] - TempSum;
                //oper_f++;
            }
            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;
        }

        public double FindDeterminant()
        {
            Console.Clear();
            //Проверка на факторизованность матрицы
            if (!AFactorized)
            {
                Console.Clear();
                MatrixFactorization();
                Console.WriteLine("Матрица была не факторизирована, факторизировали.\n");
            }

            double determinant = MtrxOfCoefs[TranspositionI[0], TranspositionJ[0]];
            for (int i = 1; i < Dimension; i++)
                determinant *= MtrxOfCoefs[TranspositionI[i], TranspositionJ[i]];

            determinant *= Znak;
            Console.WriteLine("Определитель матрицы равен {0}", determinant);


            Console.ReadKey();
            Console.Clear();
            return determinant;
        }

        public void FirstMatrixInversion()
        {
            //Console.Clear();

            double[] x = new double[Dimension];

            double[] e = new double[Dimension];

            InvertMatrix1 = new double[Dimension, Dimension];

            e.Initialize();

            oper_t += Dimension * Dimension * Dimension;
            DateTime date = DateTime.Now;

            for (int i = 0; i < Dimension; i++)
            {
                if (i > 0)
                    e[i - 1] = 0;
                e[i] = 1;
                SolutionSLAE(e, ref x);
                //oper_f++;              
                for (int j = 0; j < Dimension; j++)
                    InvertMatrix1[j, i] = x[j];
                oper_f++;
            }

            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;

            //PrintDataTrans(InvertMatrix1);
        }

        public void SecondMatrixInversion()
        {

            if (!AFactorized)
            {
                //Console.Clear();
                MatrixFactorization();
                //Console.WriteLine("Матрица была не факторизирована, факторизировали.\n");
            }

            InvertMatrix2 = new double[Dimension, Dimension];
            DateTime date = DateTime.Now;
            oper_t += Dimension * Dimension * Dimension;


            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    InvertMatrix2[TranspositionI[i], TranspositionJ[j]] = MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]];
                }
            }

            //Подготовка всей матрицы
            // Подготовка |U
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = i + 1; j < Dimension; j++)
                {
                    InvertMatrix2[TranspositionI[i], TranspositionJ[j]] = -InvertMatrix2[TranspositionI[i], TranspositionJ[j]];
                }
            }

            //Подготовка L
            for (int j = 0; j < Dimension; j++)
            {
                InvertMatrix2[TranspositionI[j], TranspositionJ[j]] = 1 / InvertMatrix2[TranspositionI[j], TranspositionJ[j]];
                oper_f++;
                for (int i = j + 1; i < Dimension; i++)
                {
                    InvertMatrix2[TranspositionI[i], TranspositionJ[j]] = -InvertMatrix2[TranspositionI[i], TranspositionJ[j]] * InvertMatrix2[TranspositionI[j], TranspositionJ[j]];
                    oper_f++;
                }
            }

            //Ищем |U^-1
            for (int k = Dimension - 1; k > 0; k--)
            {
                for (int i = 0; i < k - 1; i++)
                {
                    for (int j = k; j < Dimension; j++)
                    {
                        oper_f++;
                        InvertMatrix2[TranspositionI[i], TranspositionJ[j]] += InvertMatrix2[TranspositionI[i], TranspositionJ[k - 1]] * InvertMatrix2[TranspositionI[k - 1], TranspositionJ[j]];
                    }
                }
            }


            //Ищем L^-1 
            for (int k = 0; k < Dimension - 1; k++)
            {
                for (int i = k + 2; i < Dimension; i++)
                {
                    for (int j = 0; j <= k; j++)
                    {
                        oper_f++;
                        InvertMatrix2[TranspositionI[i], TranspositionJ[j]] += InvertMatrix2[TranspositionI[i], TranspositionJ[k + 1]] * InvertMatrix2[TranspositionI[k + 1], TranspositionJ[j]];
                    }
                }
                for (int j = 0; j <= k; j++)
                {
                    oper_f++;
                    InvertMatrix2[TranspositionI[k + 1], TranspositionJ[j]] = InvertMatrix2[TranspositionI[k + 1], TranspositionJ[j]] * InvertMatrix2[TranspositionI[k + 1], TranspositionJ[k + 1]];
                }
            }

            // Перемножение матриц U^(-1) и L^(-1)
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    double sum = 0;
                    if (i < j)
                    {
                        sum = 0;
                        for (int k = j; k < Dimension; k++)
                        {
                            sum += InvertMatrix2[TranspositionI[i], TranspositionJ[k]] * InvertMatrix2[TranspositionI[k], TranspositionJ[j]];
                            oper_f++;
                        }
                    }
                    else if (i >= j)
                    {
                        sum = InvertMatrix2[TranspositionI[i], TranspositionJ[j]];
                        for (int k = i + 1; k < Dimension; k++)
                        {
                            oper_f++;
                            sum += InvertMatrix2[TranspositionI[i], TranspositionJ[k]] * InvertMatrix2[TranspositionI[k], TranspositionJ[j]];
                        }
                    }
                    InvertMatrix2[TranspositionI[i], TranspositionJ[j]] = sum;
                    //oper_f++;
                }
            }

            //Выполняем обратную перестановку

            int[] I = new int[Dimension];
            int[] J = new int[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                I[TranspositionI[i]] = i;
                J[TranspositionJ[i]] = i;
                //oper_f++;
            }
            //Console.WriteLine("Матрица А:\n");
            //for (int i = 0; i < Dimension; i++)
            //{
            //    for (int j = 0; j < Dimension; j++)
            //        Console.Write(InvertMatrix2[TranspositionI[J[i]], TranspositionJ[I[j]]] + " \t");
            //    oper_f++;
            //    Console.WriteLine();
            //}

            double[,] reverseInvert = new double[Dimension, Dimension];
            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                {
                    reverseInvert[i, j] = InvertMatrix2[TranspositionI[J[i]], TranspositionJ[I[j]]];
                    //oper_f++;
                }
            System.Array.Copy(reverseInvert, InvertMatrix2, Dimension * Dimension);
            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;


            //Console.ReadKey();
            //Console.Clear();

        }

        public void Experiment1()
        {
            Console.Clear();
            Console.WriteLine("| Порядок\t| Время\t\t\t| Точность\t\t\t| ТЧО\t\t\t| РЧО\t ");
            genMatrix(11);
            Console.ReadKey();
            Console.Clear();
        }
        public void Experiment3()
        {
            Console.Clear();
            Console.WriteLine("| Порядок\t| Время1\t| Время2\t| Точность1\t\t| Точность2\t\t| ТЧО\t\t| РЧО1\t| РЧО2\t");
            for (int n = 5; n <= 100; n += 5)
            {
                Matrix11(n);
                double[,] savedCopy = new double[Dimension, Dimension];
                System.Array.Copy(MtrxOfCoefs, savedCopy, Dimension * Dimension);
                MatrixFactorization();
                FirstMatrixInversion();
                var oper_f_1 = oper_f;
                var oper_t_1 = oper_t;
                var time_f_1 = time_f;
                var accuracy_1 = AccuracyInvert(savedCopy, 1);
                System.Array.Copy(savedCopy, MtrxOfCoefs, Dimension * Dimension);
                MatrixFactorization();
                SecondMatrixInversion();
                var oper_t_2 = oper_t;
                var oper_f_2 = oper_f;
                var time_f_2 = time_f;
                var accuracy_2 = AccuracyInvert(savedCopy, 2);
                Console.WriteLine("| {0,3}\t\t| {1,6}\t| {2,6}\t| {3,8}\t| {4,8}\t| {5,6}\t| {6,6}\t| {7,6}\t", 
                    Dimension,
                    time_f_1,
                    time_f_2,
                    accuracy_1,
                    accuracy_2,
                    oper_t_1,
                    oper_f_1,
                    oper_f_2
                    );
            }
            Console.ReadKey();
            Console.Clear();
        }

        public void Experiment2()
        {
            Console.Clear();
            for (int type = 1; type < 11; type++)
            {
                Console.WriteLine("| Порядок\t| Время\t\t\t| Точность\t\t\t| ТЧО\t\t\t| РЧО\t ");
                genMatrix(type); 
                Console.WriteLine($"{type}-----------------------------------------{type}");
                Console.WriteLine();
                // PrintDataTrans(MtrxOfCoefs);
            }
            Console.ReadKey();
            Console.Clear();
        }



        private void genMatrix(int type)
        {
            switch (type)
            {
                case 1:
                    for (int n = 4; n <= 40; n += 4)
                    {
                        oper_t = 0;
                        oper_f = 0;
                        AFactorized = true;
                        Matrix1(n);
                        //SolutionSLAE(true);
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 2:
                    {
                        AFactorized = true;
                        Matrix2();
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 3:
                    {
                        AFactorized = true;
                        Matrix3();
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 4:
                    for (int n = 4; n <= 40; n += 4)
                    {
                        AFactorized = true;
                        Matrix4(n);
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 5:
                    for (int n = 4; n <= 40; n += 4)
                    {
                        AFactorized = true;
                        Matrix5(n);
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 6:
                    {
                        AFactorized = true;
                        Matrix6();
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 7:
                    for (int n = 4; n <= 40; n += 4)
                    {
                        AFactorized = true;
                        Matrix7(n);
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 8:
                    for (int n = 4; n <= 40; n += 4)
                    {
                        AFactorized = true;
                        Matrix8(n);
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 9:
                    for (int n = 4; n <= 40; n += 4)
                    {
                        AFactorized = true;
                        Matrix9(n);
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 10:
                    {
                        AFactorized = true;
                        Matrix10();
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;
                case 11:
                    for (int n = 5; n <= 100; n += 5)
                    {
                        AFactorized = true;
                        Matrix11(n);
                        //SolutionSLAE(true);
                        //Console.WriteLine($"Погрешность решения СЛАУ: {AccuracySLAE()}");
                        //Console.WriteLine($"Реальное число операций: {oper_f}, теоретическое число операций: {oper_t}, скорость решения задачи: {time_f}");
                        Console.WriteLine("| {0,3}\t\t| {1,6}\t\t| {2,10}\t\t| {3,6}\t\t| {4,6}\t\t ", Dimension, time_f, AccuracySLAE(), oper_t, oper_f);
                    }
                    break;

            }
        }
        private void Matrix1(int n)
        {
            Dimension = n;
            MtrxOfCoefs = new double[Dimension, Dimension];
            for (int i = 1; i <= Dimension; i++)
            {
                for (int j = 1; j <= Dimension; j++)
                {
                    MtrxOfCoefs[i - 1, j - 1] = 1 / ((double)i + (double)j - 1);
                }
            }
        }
        private void Matrix2()
        {
            Dimension = 20;
            MtrxOfCoefs = new double[Dimension, Dimension];
            for (int i = 0; i < 20; i++)
            {
                if (i != 19)
                {
                    MtrxOfCoefs[i, i + 1] = 1;
                }
                MtrxOfCoefs[i, i] = 1;
            }
        }
        private void Matrix3()
        {
            Dimension = 7;
            MtrxOfCoefs = new double[,] {{5,4,7,5,6,7,5},
                               {4,12,8,7,8,8,6},
                               {7,8,10,9,8,7,7},
                               {5,7,9,11,9,7,5},
                               {6,8,8,9,10,8,9},
                               {7,8,7,7,8,10,10},
                               {5,6,7,5,9,10,10}};
        }
        private void Matrix4(int n)
        {
            Dimension = n;
            MtrxOfCoefs = new double[Dimension, Dimension];
            for (int i = 1; i <= Dimension; i++)
            {
                for (int j = 1; j <= Dimension; j++)
                {
                    if (i == j)
                    {
                        MtrxOfCoefs[i - 1, j - 1] = 0.01 / (((double)Dimension - (double)i + 1) / ((double)i + 1));
                    }
                    else if (i < j)
                    {
                        MtrxOfCoefs[i - 1, j - 1] = 0;
                    }
                    else
                    {
                        MtrxOfCoefs[i - 1, j - 1] = i * (Dimension - j);
                    }
                }
            }
        }
        private void Matrix5(int n)
        {
            Dimension = n;
            MtrxOfCoefs = new double[Dimension, Dimension];
            for (int i = 1; i <= Dimension; i++)
            {
                for (int j = 1; j <= Dimension; j++)
                {
                    if (i == j)
                    {
                        MtrxOfCoefs[i - 1, j - 1] = 0.01 / (((double)Dimension - (double)i + 1) / ((double)i + 1));
                    }
                    else if (i < j)
                    {
                        MtrxOfCoefs[i - 1, j - 1] = j * (Dimension - i);
                    }
                    else
                    {
                        MtrxOfCoefs[i - 1, j - 1] = i * (Dimension - j);
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
            catch
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
            catch
            {
                return 0;
            }
        }
        private void Matrix6()
        {
            double arg = Math.PI - 0.1;
            Dimension = 8;
            MtrxOfCoefs = new double[Dimension, Dimension];
            double[,] T = new double[,] { { 1, 1 }, { 1, 1 } };
            double[,] R = new double[,] { { ctg(arg), cosec(arg) }, { -cosec(arg), ctg(arg) } };
            double[,] S = new double[,] { { 1 - ctg(arg), cosec(arg) }, { 1 - cosec(arg), 1 + ctg(arg) } };
            for (int i = 0; i < Dimension; i += 2)
            {
                for (int j = 0; j < Dimension; j += 2)
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
                            MtrxOfCoefs[i + k, j + t] = V[k, t];
                        }
                    }
                }

            }
        }
        private void Matrix7(int n)
        {
            Dimension = n;
            MtrxOfCoefs = new double[Dimension, Dimension];
            double arg = 2;
            for (int i = 1; i <= Dimension; i++)
            {
                MtrxOfCoefs[i - 1, i - 1] = Math.Pow(arg, Math.Abs((double)Dimension - (double)i * 2) / 2);
            }
            for (int i = 1; i <= Dimension; i++)
            {
                for (int j = 1; j <= Dimension; j++)
                {
                    if (i != j)
                    {
                        if (i == 1 || j == 1)
                        {
                            MtrxOfCoefs[i - 1, j - 1] = MtrxOfCoefs[0, 0] / Math.Pow(arg, (double)j);
                        }
                        else if (i == Dimension || j == Dimension)
                        {
                            MtrxOfCoefs[i - 1, j - 1] = MtrxOfCoefs[Dimension - 1, Dimension - 1] / Math.Pow(arg, (double)j);
                        }
                        else
                        {
                            MtrxOfCoefs[i - 1, j - 1] = 0;
                        }
                    }
                }
            }
        }
        private void Matrix8(int n)
        {
            Dimension = n;
            MtrxOfCoefs = new double[Dimension, Dimension];
            double arg = 0.000000001;
            for (int i = 1; i <= Dimension; i++)
            {
                for (int j = 1; j <= Dimension; j++)
                {
                    MtrxOfCoefs[i - 1, j - 1] = Math.Exp((double)i * (double)j * arg);
                }
            }
        }
        private void Matrix9(int n)
        {
            Dimension = n;
            MtrxOfCoefs = new double[Dimension, Dimension];
            double arg = 99999;
            for (int i = 1; i <= Dimension; i++)
            {
                for (int j = 1; j <= Dimension; j++)
                {
                    MtrxOfCoefs[i - 1, j - 1] = arg + Math.Log((double)i * (double)j, 2);
                }
            }
        }
        private void Matrix10()
        {
            Dimension = 4;
            MtrxOfCoefs = new double[,] {{0.9143*Math.Pow(10,-4),0,0,0},
                                     {0.8762,0.756*Math.Pow(10,-4),0,0},
                                     {0.794,0.8143,0.9504*Math.Pow(10,-4),0},
                                     {0.8017,0.6123,0.7165,0.7123*Math.Pow(10,-4)} };
        }
        private void Matrix11(int n)
        {
            Random rnd = new Random();
            Dimension = n;
            MtrxOfCoefs = new double[Dimension, Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                {
                    MtrxOfCoefs[i, j] = rnd.NextDouble() * 100 - 50;
                }
            }
        }

        private double AccuracySLAE()
        {
            //SolutionSLAE(true);
            double[] savedVars = new double[Dimension];
            VctrOfVars = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                VctrOfVars[i] = (double)i + 1;
            }

            VctrOfVars.CopyTo(savedVars, 0);
            VctrOfFreeMembers = new double[Dimension];

            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                    VctrOfFreeMembers[i] += MtrxOfCoefs[i, j] * VctrOfVars[j];
            AFactorized = false;
            SolutionSLAE(false);
            double maxAccuracy = 0;
            for (int i = 0; i < Dimension; i++)
            {
                if (Math.Abs(savedVars[i] - VctrOfVars[i]) > maxAccuracy)
                    maxAccuracy = Math.Abs(savedVars[i] - VctrOfVars[i]);
            }

            return maxAccuracy;
        }

        private double AccuracyInvert(double[,] copy, int mode)
        {
            double[,] e = new double[Dimension, Dimension];
            double[,] multiplication = new double[Dimension, Dimension];
            e.Initialize();
            for (int i = 0; i < Dimension; i++)
                e[i, i] = 1;
            if (mode == 1)
            {
                for (int i = 0; i < Dimension; i++)
                    for (int j = 0; j < Dimension; j++)
                        for (int k = 0; k < Dimension; k++)
                        {
                            multiplication[i, j] += copy[i, k] * InvertMatrix1[k, j];
                        }
            }
            else
            {
                for (int i = 0; i < Dimension; i++)
                    for (int j = 0; j < Dimension; j++)
                        for (int k = 0; k < Dimension; k++)
                        {
                            multiplication[i, j] += copy[i, k] * InvertMatrix2[k, j];
                        }
            }

            for (int i = 0; i < Dimension; i++)
                for (int j = 0; j < Dimension; j++)
                    e[i, j] -= multiplication[i, j];

            double maxPrev = 0;
            double maxRazn = 0;

            for (int i = 0; i < Dimension; i++)
            {
                double norma1 = 0;
                double norma2 = 0;

                for (int j = 0; j < Dimension; j++)
                {
                    norma1 += copy[i, j];
                    norma2 += e[i, j];
                }

                if (norma1 > maxPrev)
                    maxPrev = norma1;
                if (norma2 > maxRazn)
                    maxRazn = norma2;
            }

            return maxRazn / maxPrev;
        }

    }
}
