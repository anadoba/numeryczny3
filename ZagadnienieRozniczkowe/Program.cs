using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZagadnienieRozniczkowe
{
    class Program
    {
        /*
         * Dokumentacja projektu
         * https://docs.google.com/document/d/1A61DvJzAxQP9eW005EGn1y-gEQfOpIMC9BIlK6hZf48/
         */

        private static double _a; // wieksza od 1
        private static int _n; // wieksza od 1
        private static double _h; // dokladnosc przyblizenia

        private static double[] _x;
        private static double[] _y;
        static void Main(string[] args)
        {
            Powitanie();
            InicjalizacjaZmiennych();
            Console.WriteLine("\n\n-----------------------------\n-----\t METODA EULERA \t-----\n-----------------------------");
            Console.WriteLine(ObliczWynik() + "\n");


            Console.ReadLine();
        }

        static void Powitanie()
        {
            Console.WriteLine("Zagadnienie różniczkowe y'(x)' = f(x,y(x)),\t y(1)=2,\t gdzie f(x,y)=sqrt(y-x)+x+1" +
                              "\nrozwiązać na przedziale [1,a] z krokiem h=(a-1)/1) metodą Eulera oraz metodą Heuna." +
                              "\nWyniki porównać z rowiązaniem dokładnym y(x)=x^2 +x przez wyznaczenie błędu maksymalnego każdej z metod.");

            Console.Write("\nPodaj liczbę rzeczywistą a > 1 :  ");
            _a = Double.Parse(Console.ReadLine());

            Console.Write("Podaj liczbę naturalną n > 1 :  ");
            _n = Int32.Parse(Console.ReadLine());
        }

        static void InicjalizacjaZmiennych()
        {
            _h = (_a - 1) / _n;
            _x = new double[100]; // TODO: ograniczyc wymiar
            _y = new double[100];
            _y[0] = 2;
        }

        // y[k+1] = y[k] + h*f(x[k],y[k])
        static void MetodaEulera(int iteracja)
        {
            _x[iteracja] = 1 + iteracja*_h;
            _y[iteracja + 1] = _y[iteracja] + _h*F(_x[iteracja], _y[iteracja]);

            Console.WriteLine("x[{0}] = {1}\ty[{0}] = {2}", iteracja, _x[iteracja], _y[iteracja]);
        }

        static void MetodaHeuna(int iteracja)
        {
            _x[iteracja] = 1 + iteracja * _h;
            _y[iteracja + 1] = _y[iteracja] + _h / 2 * (F(_x[iteracja], _y[iteracja]) + F(_x[iteracja] + _h, _y[iteracja] + _h * F(_x[iteracja], _y[iteracja])));

            Console.WriteLine("x[{0}] = {1}\ty[{0}] = {2}", iteracja, _x[iteracja], _y[iteracja]);
        }

        static double F(double a, double b)
        {
            return Math.Sqrt(b - a) + a + 1;
        }

        static double RozwiazanieDokladne(double x)
        {
            return x*x + x;
        }

        // |y[k] - y(x[k])|
        static double ObliczWynik()
        {
            int k;

            for (k= 0; k <= _a; k++)
            {
                MetodaEulera(k);
            }

            k--;

            double wynik = _y[k] - RozwiazanieDokladne(_x[k]);
            Console.Write("\n| y[{0}] - y(x[{0}]) | = ", k);
            return Math.Abs(wynik);
        }
    }
}
