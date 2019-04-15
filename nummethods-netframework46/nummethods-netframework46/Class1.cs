using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nummethods_netframework46
{
    //Класс с LU разложением, решением СЛАУ, вычислением обратной матрицы
    public class NumMeth
    {
        public double[,] A;                             //Матрица А (над которой выполняется LU разложение)
        int N;                                          //Размер матрицы
        public int[] p_c;                               //Массив перестановок
        int znak = 1;                                   //Знак фиксирующий чётное(нечётной) количество перестановок
        int oper_f = 0;                                 //Фактическое число операций
        int oper_t = 0;                                 //Теоретическое число операций
        double time_f = 0;                              //Время работы программы
        double EPS = 0;                                 //Машинный эпсилон
        public bool flagError = false;                  //Флаг ошибки
        public double TIME { get { return time_f; } }
        public int OPER_F { get { return oper_f; } }
        public int OPER_T { get { return oper_t; } }
        public NumMeth() { }
        //Конструктор
        public NumMeth(double e) { EPS = e; }    //Аргументы: e - машинный эпсилон.

        //Метод выполняет LU разложение
        public void setA(double[,] a, int n)    //Аргументы: a - матрица, n - размер матрицы.
        {
            flagError = false;                  //Сбрасываем флаг ошибки, которая может возникнуть в результате LU разложения.
            oper_f = 0;                         //Файктическое число оперций.
            oper_t = 0;                         //Теоретическое число операций.
            DateTime date = DateTime.Now;       //Сохраняем время перед LU разложением.
            N = n;                              //Сохраняем размер матрицы в глобальную переменную (так он нужен будет в других методах).
            A = new double[n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    A[i, j] = a[i, j];
                }
            }
            oper_t = n * n * n;                 //Теоретическая сложность LU разложения имеет ценку O(N^3).
            p_c = new int[n];                   //Создаём массив перестановок.
            for (int i = 0; i < n; i++)         //Заполняем массив перестановок начальными значениями.
            {
                p_c[i] = i;
            }
            for (int k = 0; k < n; k++)         //Начало LU разложения.
            {
                int imax = k;                   //Сохраняем индекс текущего элемента.
                double max = Math.Abs(A[k, p_c[k]]);    //Сохраняем значение текущего элемента (с учётом перестановок).
                for (int i = k + 1; i < n; i++)         //В этом цикле выполняется поиск максимального по 
                {                                       //абсолютному значению элемента, активной подматрицы.
                    if (Math.Abs(A[i, p_c[k]]) > max)
                    {
                        max = Math.Abs(A[i, p_c[k]]);   //В случае нахождения элемента большего по абсолютному значению
                        imax = i;                       //чем текущий элемент, запоминаем его значение и номер столбца.
                        oper_f++;                       //Так же увеличиваем число фактических операций.
                    }
                }
                if (imax != k)                          //Выполняем перестановку и изменяем знак в переменной
                {                                       //на которую будет умножаться определитель матрицы.
                    int buf = p_c[k];
                    p_c[k] = p_c[imax];
                    p_c[imax] = buf;
                    znak *= -1;
                    oper_f++;
                }
                if (Math.Abs(A[k, p_c[k]]) < 2 * EPS)     //Сравниваем значение максимального значения в активной подматрице
                {                                       //с двумя машинными эпсилонами,
                    flagError = true;                   //если значение меньше, то устанавливаем флаг ошибки
                    return;                             //и завершаем LU разложение.
                }
                for (int j = k + 1; j < n; j++)         //В этом цикле выполняеятся нормировка строки
                {
                    A[k, p_c[j]] /= A[k, p_c[k]];
                    oper_f++;
                }
                for (int i = k + 1; i < n; i++)         //Вычитание из всех строк активной подматрицы, текущей
                {
                    for (int j = k + 1; j < n; j++)
                    {
                        A[i, p_c[j]] -= A[i, p_c[k]] * A[k, p_c[j]];
                        oper_f++;
                    }
                }
            }
            TimeSpan sp = DateTime.Now - date;      //Фиксируем время окончания LU разложения
            time_f = sp.TotalMilliseconds;          //и сохраняем его в пременную.
        }

        //Метод решения СЛАУ 
        public double[] getX(double[] B)            //Аргументы: B - вектор (известная, правая часть СЛАУ)
        {
            double[] X = new double[B.Length];      //Создаём вектор X (неизвестная часть СЛАУ)
            DateTime date = DateTime.Now;           //Сохраняем время начала решения СЛАУ
            oper_t += N * N;                        //Увеличиваем теоретическое число операций на N^2 (такова теоретическая сложность этого алгоритма)
            for (int i = 0; i < B.Length; i++)        //Вычисляем вектор Y
            {
                X[i] = B[i];
                for (int k = 0; k <= i - 1; k++)
                {
                    X[i] -= A[i, p_c[k]] * X[k];
                    oper_f++;
                }
                X[i] /= A[i, p_c[i]];
                oper_f++;
            }
            for (int i = N - 1; i >= 0; i--)        //Вычисляем вектор X
            {
                for (int k = i + 1; k < N; k++)
                {
                    X[i] -= A[i, p_c[k]] * X[k];
                    oper_f++;
                }
            }
            TimeSpan sp = DateTime.Now - date;      //Вычисляем разницу во времени до и после решения СЛАУ
            time_f += sp.TotalMilliseconds;         //Увеличиваем время на эту разницу
            return Preobraz(X);                     //Возврашаем вектор X с учётом перестановок
        }

        //Вспомогательный метод, возвращающий вектор X, с учётом перестановок
        private double[] Preobraz(double[] x)       //Аргументы: X - вектор (неизвестная часть СЛАУ)
        {
            double[] X = new double[N];
            for (int i = 0; i < N; i++)
            {
                X[p_c[i]] = x[i];
            }
            return X;
        }

        //Метод возвращает правую часть СЛАУ, вычисленную по известному вектору X
        public double[] getB(double[,] matrix, double[] X, int n)   //Аргументы: matrix - матрицы, X - точное решение СЛАУ, n - размер матрицы
        {
            double[] B = new double[n];
            for (int i = 0; i < n; i++) B[i] = 0;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    B[i] += matrix[i, j] * X[j];
                }
            }
            return B;
        }

        //Метод вычисления определителя. Определитель равен произведению элементов, стоящих на главной диагонали в матрице
        //после LU разложения и умноженое на переменную, которая равна 1, если число перестановок было чётным и -1, если нечётным
        //det(A) = det(L)*det(U) = det(L) = znak*П(a[i,i])
        public double getDet()
        {
            double r = 1;
            for (int i = 0; i < N; i++)
            {
                r *= A[i, p_c[i]];
            }
            return znak * r;
        }

        //Метод возвращает погрешность вычисления обратной матрицы
        public double PogreshInv(double[,] A, double[,] invA, int n)//Аргументы: A - исходная матрица
                                                                    //invA - обратная матрица
                                                                    //n - размер матрицы
        {
            double result = 0;              //Переменная будет хранить значение максимальной погрешности
            double[,] E = new double[n, n]; //Создаём единичную матрицу 
            for (int i = 0; i < n; i++)     //и заполняем её значениями
            {
                E[i, i] = 1;
            }
            //Умножение матрицы на обратную должно дать единичную матрицу
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    double rr = 0;
                    for (int k = 0; k < n; k++)
                    {
                        rr += A[i, k] * invA[k, j];

                    }
                    if (Math.Abs(E[i, j] - rr) > result) result = Math.Abs(E[i, j] - rr);   //Если разница
                                                                                            //между соответствующими элементами больше по абсолютному значению
                                                                                            //текущей максимальной погрешности, то обновляем значение погрешности.
                }
            }
            return result;
        }

        //Метод вычисления обратной матрицы, через решение СЛАУ
        public double[,] Inv1(double[,] A, int n)   //Аргументы: A - матрица, n - размер матрицы.
        {
            setA(A, n);                     //Выполняем LU разложение
            if (flagError) return null;     //Если в результате LU разложения произошло деление на ноль, то выходим из метода
            double[][] X = new double[n][]; //Создаём вложенные массивы (X - обратная матрица, E - единичная матрица), 
            double[][] E = new double[n][]; //для решения СЛАУ нужно передавать вектор, а это возможно только с вложенными массивами
            for (int i = 0; i < N; i++)     //Заполняем единичную матрицу значениями
            {
                E[i] = new double[N];
                E[i][i] = 1;

            }
            for (int i = 0; i < N; i++)     //Решение СЛАУ
            {
                X[i] = new double[n];
                X[i] = getX(E[i]);
                oper_f++;
            }
            DateTime date = DateTime.Now;   //В решение СЛАУ уже ведётся учёт времени, следовательно в этом методе его 
                                            //необходимо начинать только сейчас
            for (int i = 0; i < n; i++)     //Выполняем транспонирование матрицы, так как решение записывалось в строки,
            {                               //записывать значения сразу в столбцы не представляется возможным
                for (int j = i; j < n; j++) //так как работали с вложенными массивами. Работать с обычнимы массивами
                {                           //так же не совсем удобно, так как пришлось бы записывать значения каждый раз
                    double s = X[i][j];     //после решения СЛАУ, в предыдущем цыкле. Таким образом оценка сложности осталась бы
                    X[i][j] = X[j][i];      //той же, а кода было бы больше.
                    X[j][i] = s;
                    oper_f++;
                }
            }
            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;
            return toMatrix(X);             //Переводим вложенный массив в обычный двухмерный и возвращаем
        }

        //Метод преобразует вложенный квадратный массив, в квадратный двухмерный массив
        private double[,] toMatrix(double[][] x)    //Аргументы: x - вложенный массив
        {
            double[,] X = new double[N, N];
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    X[i, j] = x[i][j];
                }
            }
            return X;
        }

        //Метод вычисления обратной матрицы через элементарый преобразования (A^(-1) = L^(-1)*U^(-1))
        public double[,] Inv2(double[,] a, int n)
        {
            setA(a, n);
            double sum = 0;
            DateTime date = DateTime.Now;
            oper_t += N * N * N;
            //Первый этап - подготовка (обращение элементарных матриц)
            for (int i = 0; i < N; i++)
            {
                for (int j = i + 1; j < N; j++)
                {
                    A[i, p_c[j]] *= -1;
                }
            }
            for (int j = 0; j < N; j++)
            {
                A[j, p_c[j]] = 1 / A[j, p_c[j]];
                oper_f++;
                for (int i = j + 1; i < N; i++)
                {
                    oper_f++;
                    A[i, p_c[j]] = -A[i, p_c[j]] * A[j, p_c[j]];
                }
            }
            //Считаем матрицу U^(-1)
            for (int k = n - 1; k > 0; k--)
            {
                for (int i = 0; i < k - 1; i++)
                {
                    for (int j = k; j < N; j++)
                    {
                        oper_f++;
                        A[i, p_c[j]] += A[i, p_c[k - 1]] * A[k - 1, p_c[j]];
                    }
                }
            }
            //Считаем матрицу L^(-1)
            for (int k = 0; k < N - 1; k++)
            {
                for (int i = k + 2; i < N; i++)
                {
                    for (int j = 0; j <= k; j++)
                    {
                        oper_f++;
                        A[i, p_c[j]] += A[i, p_c[k + 1]] * A[k + 1, p_c[j]];
                    }
                }
                for (int j = 0; j <= k; j++)
                {
                    oper_f++;
                    A[k + 1, p_c[j]] = A[k + 1, p_c[j]] * A[k + 1, p_c[k + 1]];
                }
            }
            //Перемножение матриц U^(-1) и L^(-1)
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (i < j)
                    {
                        sum = 0;
                        for (int k = j; k < N; k++)
                        {
                            sum += A[i, p_c[k]] * A[k, p_c[j]];
                            oper_f++;
                        }
                    }
                    else if (i >= j)
                    {
                        sum = A[i, p_c[j]];
                        for (int k = i + 1; k < N; k++)
                        {
                            sum += A[i, p_c[k]] * A[k, p_c[j]];
                            oper_f++;
                        }
                    }
                    A[i, p_c[j]] = sum;
                    oper_f++;
                }
            }
            //Выполняем обратную перестановку
            double[,] R = new double[N, N];
            int[] p2 = new int[N];
            for (int i = 0; i < N; i++)
            {
                p2[p_c[i]] = i;
                oper_f++;
            }
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    R[i, j] = A[p2[i], p_c[j]];
                    oper_f++;
                }
            }
            TimeSpan sp = DateTime.Now - date;
            time_f += sp.TotalMilliseconds;
            return R;
        }

        //Метод вычислнея погрешности решения СЛАУ
        public double VectorPogr(double[] X1, double[] X2)//Аргументы: два вектора, один из которых представляет собой точное решение СЛАУ,
        {                                                 //другой приближённой (найденной через LU разложение)
            double result = 0;
            for (int i = 0; i < X1.Length; i++)
            {
                if (Math.Abs(X1[i] - X2[i]) > result)
                {
                    result = Math.Abs(X2[i] - X1[i]);
                }
            }
            return result;
        }
    }



}
