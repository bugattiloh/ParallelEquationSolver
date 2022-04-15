using System;
using System.Diagnostics;
using System.Threading;

namespace PP3
{
    class Program
    {
        static void Main(string[] args)
        {
            var equationSolver = new EquationSolver(0, 10, 0.01f);

            Stopwatch stopwatchP = Stopwatch.StartNew();
            equationSolver.Start();
            Thread.Sleep(100);

            long time = stopwatchP.ElapsedMilliseconds;
            Console.WriteLine(
                $"В результате вычисления интеграла в многопоточном режиме функции f=x*x*x*x на отрезке [0;10] с минимальным шагом 0.000001,мы получили результаты время работы:{time}мс,результат:{equationSolver.Res}");


            Stopwatch stopwatchS = Stopwatch.StartNew();
            var singleResult = equationSolver.SolveEquationSingle();
            time = stopwatchS.ElapsedMilliseconds;
            Console.WriteLine(
                $"В результате вычисления интеграла в однопоточном режиме функции f=x*x*x*x на отрезке [0;10] с минимальным шагом 0.000001,мы получили результаты время работы:{time}мс,результат:{singleResult}");
        }
    }
}