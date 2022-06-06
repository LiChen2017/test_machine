using System;
using System.Diagnostics;

namespace ShockAnalyze
{
    public class Spline
    {
        private int n, last_interval;
        private double[] x, f, b, c, d;
        private bool isUniform, isDebug;

        public Spline(double[] xx, double[] ff)
        {
            // Calculate coefficients defining a smooth cubic interpolatory spline.
            //
            // Input parameters:
            // xx = vector of values of the independent variable ordered
            // so that x[i] < x[i+1] for all i.
            // ff = vector of values of the dependent variable.
            // class values constructed:
            // n = number of data points.
            // b = vector of S'(x[i]) values.
            // c = vector of S"(x[i])/2 values.
            // d = vector of S'''(x[i]+)/6 values (i < n).
            // x = xx
            // f = ff
            // Local variables:
            double fp1 = 0, fpn = 0, h = 0, p = 0;
            double zero = 0.0, two = 2.0, three = 3.0;
            bool sorted = true;

            isUniform = true;
            isDebug = false;
            last_interval = 0;
            n = xx.Length;
            if (n <= 3)
            {
                Debug.WriteLine("not enough points to build spline, n=" + n);
                return;
            }
            if (n != ff.Length)
            {
                Debug.WriteLine("not same number of x and f(x)");
                return;
            }

            x = new double[n];
            f = new double[n];
            b = new double[n];
            c = new double[n];
            d = new double[n];
            for (int i = 0; i < n; i++)
            {
                x[i] = xx[i];
                f[i] = ff[i];
                if (isDebug)
                    Debug.WriteLine("Spline data x[" + i + "]=" + x[i]
                            + ", f[]=" + f[i]);
                if (i >= 1 && x[i] < x[i - 1])
                    sorted = false;
            }
            // sort if necessary
            if (!sorted)
            {
                if (isDebug)
                    Debug.WriteLine("sorting");
                dHeapSort(x, f);
            }

            // Calculate coefficients for the tri-diagonal system: store
            // sub-diagonal in b, diagonal in d, difference quotient in c.
            b[0] = x[1] - x[0];
            c[0] = (f[1] - f[0]) / b[0];
            if (n == 2)
            {
                b[0] = c[0];
                c[0] = zero;
                d[0] = zero;
                b[1] = b[0];
                c[1] = zero;
                return;
            }
            d[0] = two * b[0];
            for (int i = 1; i < n - 1; i++)
            {
                b[i] = x[i + 1] - x[i];
                if (Math.Abs(b[i] - b[0]) / b[0] > 1.0E-5)
                    isUniform = false;
                c[i] = (f[i + 1] - f[i]) / b[i];
                d[i] = two * (b[i] + b[i - 1]);
            }
            d[n - 1] = two * b[n - 2];

            // Calculate estimates for the end slopes. Use polynomials
            // interpolating data nearest the end.
            fp1 = c[0] - b[0] * (c[1] - c[0]) / (b[0] + b[1]);
            if (n > 3)
                fp1 = fp1
                        + b[0]
                        * ((b[0] + b[1]) * (c[2] - c[1]) / (b[1] + b[2]) - c[1] + c[0])
                        / (x[3] - x[0]);
            fpn = c[n - 2] + b[n - 2] * (c[n - 2] - c[n - 3])
                    / (b[n - 3] + b[n - 2]);
            if (n > 3)
                fpn = fpn
                        + b[n - 2]
                        * (c[n - 2] - c[n - 3] - (b[n - 3] + b[n - 2])
                                * (c[n - 3] - c[n - 4]) / (b[n - 3] + b[n - 4]))
                        / (x[n - 1] - x[n - 4]);

            // Calculate the right-hand-side and store it in c.
            c[n - 1] = three * (fpn - c[n - 2]);
            for (int i = n - 2; i > 0; i--)
                c[i] = three * (c[i] - c[i - 1]);
            c[0] = three * (c[0] - fp1);

            // Solve the tridiagonal system.
            for (int k = 1; k < n; k++)
            {
                p = b[k - 1] / d[k - 1];
                d[k] = d[k] - p * b[k - 1];
                c[k] = c[k] - p * c[k - 1];
            }
            c[n - 1] = c[n - 1] / d[n - 1];
            for (int k = n - 2; k >= 0; k--)
                c[k] = (c[k] - b[k] * c[k + 1]) / d[k];

            // Calculate the coefficients defining the spline.
            h = x[1] - x[0];
            for (int i = 0; i < n - 1; i++)
            {
                h = x[i + 1] - x[i];
                d[i] = (c[i + 1] - c[i]) / (three * h);
                b[i] = (f[i + 1] - f[i]) / h - h * (c[i] + h * d[i]);
            }
            b[n - 1] = b[n - 2] + h * (two * c[n - 2] + h * three * d[n - 2]);
            if (isDebug)
                Debug.WriteLine("spline coefficients");
            for (int i = 0; i < n; i++)
            {
                if (isDebug)
                    Debug.WriteLine("i=" + i + ", b[i]=" + b[i].ToString("F5")
                            + ", c[i]=" + c[i].ToString("F5") + ", d[i]="
                            + d[i].ToString("F5"));
            }
        } // end Spline

        public void dHeapSort(double[] key, double[] trail)
        {
            int nkey = key.Length;
            int last_parent_pos = (nkey - 2) / 2;
            int last_parent_index = last_parent_pos;
            double tkey, ttrail;
            int index_val;

            if (nkey <= 1)
                return;
            for (index_val = last_parent_index; index_val >= 0; index_val--)
                DremakeHeap(key, trail, index_val, nkey - 1);

            tkey = key[0];
            key[0] = key[nkey - 1];
            key[nkey - 1] = tkey;
            ttrail = trail[0];
            trail[0] = trail[nkey - 1];
            trail[nkey - 1] = ttrail;

            for (index_val = nkey - 2; index_val > 0; index_val--)
            {
                DremakeHeap(key, trail, 0, index_val);
                tkey = key[0];
                key[0] = key[index_val];
                key[index_val] = tkey;
                ttrail = trail[0];
                trail[0] = trail[index_val];
                trail[index_val] = ttrail;
            }
        } // end dHeapSort

        static void DremakeHeap(double[] key, double[] trail, int parent_index,
            int last_index)
        {
            int last_parent_pos = (last_index - 1) / 2;
            int last_parent_index = last_parent_pos;
            int l_child;
            int r_child;
            int max_child_index;
            int parent_temp = parent_index;
            double tkey, ttrail;

            while (true)
            {
                if (parent_temp > last_parent_index)
                    break;
                l_child = parent_temp * 2 + 1;
                if (l_child == last_index)
                {
                    max_child_index = l_child;
                }
                else
                {
                    r_child = l_child + 1;
                    if (key[l_child] > key[r_child])
                    {
                        max_child_index = l_child;
                    }
                    else
                    {
                        max_child_index = r_child;
                    }
                }
                if (key[max_child_index] > key[parent_temp])
                {
                    tkey = key[max_child_index];
                    key[max_child_index] = key[parent_temp];
                    key[parent_temp] = tkey;
                    ttrail = trail[max_child_index];
                    trail[max_child_index] = trail[parent_temp];
                    trail[parent_temp] = ttrail;
                    parent_temp = max_child_index;
                }
                else
                {
                    break;
                }
            }
        } // end dremake_heap

        public double SplineValue(double t)
        {
            // Evaluate the spline s at t using coefficients from Spline constructor
            //
            // Input parameters
            // class variables
            // t = point where spline is to be evaluated.
            // Output:
            // s = value of spline at t.
            // Local variables:
            double dt, s;
            int interval; // index such that t>=x[interval] and t<x[interval+1]

            if (n <= 1)
            {
                Debug.WriteLine("not enough points to compute value");
                return 0.0; // should throw exception
            }
            // Search for correct interval for t.
            interval = last_interval; // heuristic
            if (t < x[0])
            {
                Debug.WriteLine("requested point below Spline region");
                return 0.0; // should throw exception
            }
            if (t > x[n - 1])
            {
                Debug.WriteLine("requested point above Spline region");
                return 0.0; // should throw exception
            }
            if (t > x[n - 2])
                interval = n - 2;
            else if (t >= x[last_interval])
                for (int j = last_interval; j < n && t >= x[j]; j++)
                    interval = j;
            else
                for (int j = last_interval; t < x[j]; j--)
                    interval = j - 1;
            last_interval = interval; // class variable for next call
            if (isDebug)
                Debug.WriteLine("interval=" + interval + ", x[interval]="
                        + x[interval] + ", t=" + t);
            // Evaluate cubic polynomial on [x[interval] , x[interval+1]].
            dt = t - x[interval];
            s = f[interval] + dt
                    * (b[interval] + dt * (c[interval] + dt * d[interval]));
            return s;
        } // end spline_value

        public double Integrate()
        {
            double suma, sumb, sumc, sumd;
            double dx, t;
            if (n <= 3)
            {
                Debug.WriteLine("not enough data to integrate");
                return 0.0;
            }
            if (!isUniform)
            {
                if (isDebug)
                    Debug.WriteLine("non uniform spacing integration");
                t = 0.0;
                for (int i = 0; i < n - 1; i++)
                {
                    dx = x[i + 1] - x[i];
                    t = t
                            + (f[i] + (b[i] / 2.0 + (c[i] / 3.0 + dx * d[i] / 4.0)
                                    * dx)
                                    * dx) * dx;
                }
                return t;
            }
            // compute uniform integral of spline fit
            suma = 0.0;
            sumb = 0.0;
            sumc = 0.0;
            sumd = 0.0;
            for (int i = 0; i < n; i++)
            {
                suma = suma + d[i];
                sumb = sumb + c[i];
                sumc = sumc + b[i];
                sumd = sumd + f[i];
            }
            dx = x[1] - x[0]; // assumes equally spaced points
            t = (sumd + (sumc / 2.0 + (sumb / 3.0 + dx * suma / 4.0) * dx) * dx)
                    * dx;
            if (isDebug)
                Debug.WriteLine("suma=" + suma + ", sumb=" + sumb + ",\n sumc="
                        + sumc + ", sumd=" + sumd);
            return t;
        } // end integral

    }
}
