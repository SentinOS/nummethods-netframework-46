using System;

namespace NumMethods
{
    public class Array
    {

        //A
        private double[,] MtrxOfCoefs;

        public bool AFactorized { get; set; }

        //b
        private double[] VctrOfFreeMembers;

        //x
        private double[] VctrOfVars;

        private double[,] InvertMatrix1;

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
                    Console.Write(MtrxOfCoefs[i, j] + " \t");
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
                    Console.Write(x[i, j] + " \t");
                Console.WriteLine();
            }

            Console.ReadKey();
        }

        public void MatrixFactorization()
        {
            for (int k = 0; k < Dimension; k++)
            {
                for (int j = k + 1; j < Dimension; j++)
                    MtrxOfCoefs[k, j] /= MtrxOfCoefs[k, k];
                for (int i = k + 1; i < Dimension; i++)
                    for (int j = k + 1; j < Dimension; j++)
                        MtrxOfCoefs[i, j] -= MtrxOfCoefs[i, k] * MtrxOfCoefs[k, j];
            }


            Console.Clear();

            AFactorized = true;
            PrintData();

        }

        public void SolutionSLAE()
        {
            Console.Clear();
            //Проверка на факторизованность матрицы
            if (AFactorized)
            {
                Console.WriteLine("Введите вектор свободных членов: \n");
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
                    for (int j = 0; j < Dimension; j++)
                    {
                        TempSum += MtrxOfCoefs[i, j] * y[j];
                    }
                    //Присваивание i-ой неизвестной 
                    y[i] = (VctrOfFreeMembers[i] - TempSum) / MtrxOfCoefs[i, i];
                }


                for (int i = Dimension - 1; i >= 0; i--)
                {
                    TempSum = 0;
                    //Высчитывание суммы всех известных членов и коэффициентов при них до I-ого столбца
                    for (int j = Dimension - 1; j > i; j--)
                    {
                        TempSum += MtrxOfCoefs[i, j] * x[j];
                    }
                    //Присваивание i-ой неизвестной 
                    x[i] = y[i] - TempSum;
                }

                //Вывод вектора с неизвестными
                Console.WriteLine("Вектор неизвестных X:\n");
                for (uint i = 0; i < Dimension; i++)
                    Console.WriteLine(x[i] + "\n");
            }

            //Если не факторизована
            else
            {
                Console.WriteLine("Матрица не факторизована!");
            }

            Console.ReadKey();
            Console.Clear();
        }

        public void SolutionSLAE(double[] b, ref double[] x)
        {
            Console.Clear();
            //Проверка на факторизованность матрицы
            if (AFactorized)
            {
                double[] y = new double[Dimension];


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
                        TempSum += MtrxOfCoefs[i, j] * y[j];
                    }
                    //Присваивание i-ой неизвестной 
                    y[i] = (b[i] - TempSum) / MtrxOfCoefs[i, i];
                }


                for (int i = Dimension - 1; i >= 0; i--)
                {
                    TempSum = 0;
                    //Высчитывание суммы всех известных членов и коэффициентов при них до I-ого столбца
                    for (int j = Dimension - 1; j > i; j--)
                    {
                        TempSum += MtrxOfCoefs[i, j] * x[j];
                    }
                    //Присваивание i-ой неизвестной 
                    x[i] = y[i] - TempSum;
                }
            }
        }


        public void FindDeterminant()
        {
            Console.Clear();
            if (AFactorized)
            {
                double determinant = MtrxOfCoefs[0, 0];
                for (int i = 1; i < Dimension; i++)
                    determinant *= MtrxOfCoefs[i, i];

                Console.WriteLine("Определитель матрицы равен {0}", determinant);
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

            double[] x = new double[Dimension];

            double[] e = new double[Dimension];

            this.InvertMatrix1 = new double[Dimension, Dimension];

            e.Initialize();

            for (int i = 0; i < Dimension; i++)
            {
                if (i > 0)
                    e[i - 1] = 0;
                e[i] = 1;
                SolutionSLAE(e, ref x);
                for (int j = 0; j < Dimension; j++)
                    InvertMatrix1[j, i] = x[j];
            }


            PrintData(InvertMatrix1);
            Console.Clear();
        }

        public void SecondMatrixInversion()
        {
            
            //Подготовка всей матрицы
           // Подготовка |U
            for (int i = 0; i < Dimension; i++)
            {
                for (int j = i + 1; j < Dimension; j++)
                {
                    MtrxOfCoefs[i, j] = -MtrxOfCoefs[i, j];
                }
            }

            //Подготовка L
            for (int j = 0; j < Dimension; j++)
            {
                MtrxOfCoefs[j, j] = 1 / MtrxOfCoefs[j, j];
                for (int i = j+1; i < Dimension; i++)
                {
                    MtrxOfCoefs[i,j] = -MtrxOfCoefs[i, j] * MtrxOfCoefs[j, j];
                }
            }

            //Ищем |U^-1
            for(int k=Dimension-1;k>1;k--)
            {
                for(int i=0; i<k-1; i++)
                { 
                    for(int j=k;j<Dimension;j++)
                    {
                        MtrxOfCoefs[i, j] += MtrxOfCoefs[i, k-1] * MtrxOfCoefs[k-1, j];
                    }
                }
            }
            PrintData(MtrxOfCoefs);

            //Ищем L^-1 Что то тут не так
            for (int k=0;k<Dimension-1;k++)
            {
                for (int i=k+1;i<Dimension;i++)
                {
                    for (int j = 0; j < k; j++) { 
                        MtrxOfCoefs[i, j] += MtrxOfCoefs[i, k + 1] * MtrxOfCoefs[k + 1, j];
                    }
                }
                for (int j = 0; j < k; j++)
                {
                    MtrxOfCoefs[k + 1, j] = MtrxOfCoefs[k + 1, j] * MtrxOfCoefs[k + 1, k + 1];
                }
            }
            PrintData(MtrxOfCoefs);

            for(int i=0;i<Dimension;i++)
            {
                for(int j=0;j<Dimension;j++)
                {
                    double sum=0;
                    if (i<j)
                    {
                        sum = 0;
                        for(int k=j;k<Dimension;k++)
                        {
                            sum += MtrxOfCoefs[i, k] * MtrxOfCoefs[k, j];
                        }
                    }
                    if(i>=j)
                    {
                        sum = MtrxOfCoefs[i,j];
                        for (int k = i+1; k < Dimension; k++)
                        {
                            sum += MtrxOfCoefs[i, k] * MtrxOfCoefs[k, j];
                        }
                    }
                    MtrxOfCoefs[i, j] = sum;
                }
            }
            PrintData(MtrxOfCoefs);
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