using System;

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
            for (int i=0;i<Dimension;i++)
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
                    Console.Write(x[i, j] + " \t");
                Console.WriteLine();
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void MatrixFactorization()
        {

            oper_f = 0;                         //Файктическое число оперций.
            oper_t = 0;                         //Теоретическое число операций.
            DateTime date = DateTime.Now;

            Znak = 1;
            for (int i = 0; i < Dimension; i++) { 
                TranspositionJ[i] = i;
                TranspositionI[i] = i;
            }

            oper_t = Dimension * Dimension * Dimension;

            for (int k = 0; k < Dimension; k++)
            {
                int iMax = k;
                int jMax = k;
                double max = Math.Abs(MtrxOfCoefs[TranspositionI[k], TranspositionJ[k]]);

                for (int i = k; i < Dimension; i++)
                {
                    for(int j=k;j<Dimension;j++)
                    { 
                        if (Math.Abs(MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]]) > max)
                        {
                            max = Math.Abs(MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]]);
                            iMax = i;
                            jMax = j;
                            oper_f++;
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
                    oper_f++;
                }
                if (jMax != k)
                {
                    int buf = TranspositionJ[k];
                    TranspositionJ[k] = TranspositionJ[jMax];
                    TranspositionJ[jMax] = buf;
                    Znak *= -1;
                    oper_f++;
                }
                //////////////////////////////////////////////
                for (int j = k + 1; j < Dimension; j++) {                    
                    MtrxOfCoefs[TranspositionI[k], TranspositionJ[j]] /= MtrxOfCoefs[TranspositionI[k], TranspositionJ[k]];
                    oper_f++;
                }
                for (int i = k + 1; i < Dimension; i++) { 
                    for (int j = k + 1; j < Dimension; j++) { 
                        MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] -= MtrxOfCoefs[TranspositionI[i], TranspositionJ[k]] * MtrxOfCoefs[TranspositionI[k], TranspositionJ[j]];
                        oper_f++;
                    }
                }
            }
            TimeSpan sp = DateTime.Now - date;      //Фиксируем время окончания LU разложения
            time_f = sp.TotalMilliseconds;

            Console.Clear();

            AFactorized = true;
            Console.WriteLine("Матрица факторизирована.\n");
            Console.ReadKey();
            Console.Clear();

        }

        public void SolutionSLAE()
        {
            Console.Clear();
            //Проверка на факторизованность матрицы
            if (!AFactorized)
            {
                Console.Clear();
                MatrixFactorization();
                Console.WriteLine("Матрица была не факторизирована, факторизировали.\n");
            }
            Console.WriteLine("Введите вектор свободных членов: \n");

            DateTime date = DateTime.Now;           //Сохраняем время начала решения СЛАУ
            oper_t += Dimension * Dimension;

            //Заполняем вектор свободных членов данными
            this.VctrOfFreeMembers = new double[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                try
                {
                    VctrOfFreeMembers[i] = double.Parse(Console.ReadLine());
                }

                catch
                {
                    Console.WriteLine("Неправильно введен элемент. Введите заново!");
                    VctrOfFreeMembers[i] = double.Parse(Console.ReadLine());
                }

            }

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
                for (int j = 0; j < i; j++)
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
                for (int j = i+1; j < Dimension; j++)
                {
                    TempSum += MtrxOfCoefs[TranspositionI[i], TranspositionJ[j]] * x[TranspositionJ[j]];
                    oper_f++;
                }
                //Присваивание i-ой неизвестной 
                x[TranspositionJ[i]] = y[i] - TempSum;
                oper_f++;
            }

            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;
            //Вывод вектора с неизвестными
            Console.WriteLine("Вектор неизвестных X:\n");
            for (uint i = 0; i < Dimension; i++)
                Console.WriteLine(x[i] + "\n");


            Console.ReadKey();
            Console.Clear();
        }

        public void SolutionSLAE(double[] b, ref double[] x)
        {
            //Проверка на факторизованность матрицы
            if (!AFactorized)
            {
                Console.Clear();
                MatrixFactorization();
                Console.WriteLine("Матрица была не факторизирована, факторизировали.\n");
            }

            double[] y = new double[Dimension];


            DateTime date = DateTime.Now;           //Сохраняем время начала решения СЛАУ
            oper_t += Dimension * Dimension;

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
                oper_f++;
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
            Console.Clear();

            double[] x = new double[Dimension];

            double[] e = new double[Dimension];

            InvertMatrix1 = new double[Dimension, Dimension];

            e.Initialize();
            



            for (int i = 0; i < Dimension; i++)
            {
                if (i > 0)
                    e[i - 1] = 0;
                e[i] = 1;
                SolutionSLAE(e, ref x);
                oper_f++;
                DateTime date = DateTime.Now;
                for (int j = 0; j < Dimension; j++)
                    InvertMatrix1[j, i] = x[j];
                oper_f++;
                TimeSpan sp = DateTime.Now - date;
                time_f += sp.TotalMilliseconds;
            }

            

            PrintDataTrans(InvertMatrix1);
        }

        public void SecondMatrixInversion()
        {

            if (!AFactorized)
            {
                Console.Clear();
                MatrixFactorization();
                Console.WriteLine("Матрица была не факторизирована, факторизировали.\n");
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
                    oper_f++;
                }
            }

            //Выполняем обратную перестановку
            
            int[] I = new int[Dimension];
            int[] J = new int[Dimension];
            for (int i = 0; i < Dimension; i++)
            {
                I[TranspositionI[i]] = i;
                J[TranspositionJ[i]] = i;
                oper_f++;
            }
            Console.WriteLine("Матрица А:\n");
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = 0; j < Dimension; j++)
                    Console.Write(InvertMatrix2[TranspositionI[J[i]], TranspositionJ[I[j]]] + " \t");
                oper_f++;
                Console.WriteLine();
            }
            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;


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
