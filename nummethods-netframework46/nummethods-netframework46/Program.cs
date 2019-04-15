using System;

namespace NumMethods
{
    class Program
    {

        static void Menu(Array array)
        {
            short caseSwitch = 0;
            while (true)
            {
                Console.WriteLine(
                    "1. Ввод данных \n2. Факторизация матрицы \n" +
                    "3. Решение СЛАУ \n4. Вычисление определителя \n" +
                    "5. Первый способ обращения матрицы \n6. Второй способ обращения матрицы \n" +
                    "7. Первый эксперимент \n8. Второй эксперимент \n9. Третий эксперимент \n" +
                    "10. Выход"
                    );

                caseSwitch = 0;

                try
                {
                    caseSwitch = short.Parse(Console.ReadLine());
                }

                catch
                {
                    Console.Clear();
                    Console.WriteLine("Неправильно набран номер. Нажмите какую-нибудь кнопку, чтобы повторить ввод");
                    Console.ReadKey();
                    Console.Clear();
                }



                switch (caseSwitch)
                {
                    case 1:
                        array.InsertData();
                        
                        break;

                    case 2:
                        array.MatrixFactorization();
                        
                        break;

                    case 3:
                        array.SolutionSLAE();
                        
                        break;

                    case 4:
                        array.FindDeterminant();
                        
                        break;

                    case 5:
                        array.FirstMatrixInversion();
                        
                        break;

                    case 6:
                        array.SecondMatrixInversion();
                        
                        break;

                    case 7:
                        array.Experiment();
                        
                        break;

                    case 8:
                        array.Experiment();
                        
                        break;

                    case 9:
                        array.Experiment();
                        
                        break;

                    case 10:
                        Environment.Exit(1);
                        break;

                    case 11:
                        array.PrintData();
                        Console.Clear();
                        break;

                    default:
                        Console.Clear();
                        break;
                }
            }
        }

        static void Main(string[] args)
        {
            Array array = new Array();
            Menu(array);
        }
    }
}
