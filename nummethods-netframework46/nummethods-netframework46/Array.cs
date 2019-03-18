using System;

namespace NumMethods
{
    public class Array
    {

        private double[,] MtrxOfCoefs;


        public bool AFactorized { get; set; }

        private double[] VctrOfFreeMembers;

        private double[] VctrOfVars;

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
                ///////////////////////////////////////////////
                //Создаем дополнительную переменную для суммирования известных членов в строках матрицы
                    double TempSum = 0;
                //Цикл по всей матрице, двигаясь обратным ходом по верхней треугольной матрице
                    for (int i = Dimension-1; i>=0; i--)
                    {
                        TempSum = 0;
                        //Высчитывание суммы всех известных членов и коэффициентов при них до I-ого столбца
                        for (int j=Dimension-1; j> i;j--)
                        {
                            TempSum += MtrxOfCoefs[i, j] * VctrOfVars[j];
                        }
                        //Присваивание i-ой неизвестной 
                        VctrOfVars[i] = VctrOfFreeMembers[i]/MtrxOfCoefs[i,i] - TempSum;
                    }

                    //Вывод вектора с неизвестными
                    Console.WriteLine("Вектор неизвестных X:\n");
                    for (uint i = 0; i < Dimension; i++)
                        Console.WriteLine(VctrOfVars[i] + "\n");
            }   

            //Если не факторизована
            else
            {
                    Console.WriteLine("Матрица не факторизована!");
            }

            Console.ReadKey();
            Console.Clear();
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