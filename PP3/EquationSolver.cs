using System.Threading;

namespace PP3
{
    public class EquationSolver
    {
        public double A { get; set; }
        public double B { get; set; }

        public double Eps { get; set; }
        public double Res { get; set; }
        
        private Semaphore _semaphore;


        public EquationSolver(float a, float b, float eps)
        {
            A = a;
            B = b;
            Eps = eps;
            _semaphore = new Semaphore(8, 8);
        }

        public void Start()
        {
            Thread t = new Thread(SolveEquationParallel);
            t.Start(new object[] {A, B});
        }

        public double SolveEquationSingle()
        {
            double res = 0;
            for (double left = A; left < B; left += Eps)
            {
                double right = left + Eps;
                res += (right - left) * (F(left) + F(right)) / 2;
            }

            return res;
        }

        private void SolveEquationParallel(object data)
        {
            _semaphore.WaitOne();
            double[] doubles = (double[]) data;
            double left = doubles[0];
            double right = doubles[1];

            double distance = right - left;
            double h = distance / 2;
            double center = left + h;

            if (right - left < Eps)
            {
                Res += F(center) * distance;
            }
            else
            {
                var threadLeft = new Thread(SolveEquationParallel);
                threadLeft.Start(new object[] {left, center});

                var threadRight = new Thread(SolveEquationParallel);
                threadRight.Start(new object[] {center, right});
            }

            _semaphore.Release();
        }

        private static double F(double x)
        {
            return x * x * x * x;
        }
    }
}