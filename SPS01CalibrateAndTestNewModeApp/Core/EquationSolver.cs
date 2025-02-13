using System;
using System.Collections.Generic;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace SPS01CalibrateAndTestNewModeApp.Core
{
    public class EquationSolver
    {
        public static void SolveSymbolic(double a, double b, double c)
        {
            var roots = new QuadraticRoots();
            if (a == 0)
            {
                throw new ArgumentException("系数a不能为零。");
            }

            double discriminant = b * b - 4 * a * c;
            double sqrtDiscriminant = Math.Sqrt(Math.Abs(discriminant));

            if (discriminant > 0)
            {
                double root1 = (-b + sqrtDiscriminant) / (2 * a);
                double root2 = (-b - sqrtDiscriminant) / (2 * a);
                roots.Root_1[0] = root1;
                roots.Root_2[0] = root2;
                roots.Root_1[1] = 0;
                roots.Root_2[1] = 0;
                Console.WriteLine($"实根：x1 = {root1:F2}, x2 = {root2:F2}");
            }
            else if (discriminant == 0)
            {
                double root = -b / (2 * a);
                roots.Root_1[0] = root;
                roots.Root_2[0] = root;
                roots.Root_1[1] = 0;
                roots.Root_2[1] = 0;
                Console.WriteLine($"重根：x = {root:F2}");
            }
            else
            {
                double real = -b / (2 * a);
                double imaginary = sqrtDiscriminant / (2 * a);
                roots.Root_1[0] = real;
                roots.Root_2[0] = real;
                roots.Root_1[1] = imaginary;
                roots.Root_2[1] = -imaginary;
                Console.WriteLine($"复根：x1 = {real:F2} + {imaginary:F2}i, x2 = {real:F2} - {imaginary:F2}i");
            }
        }
        
        public static void SolveMatrixMethod(double a, double b, double c)
        {
            var roots = new QuadraticRoots();
            
            if (a == 0)
            {
                throw new ArgumentException("系数a不能为零。");
            }

            // 构造伴随矩阵：[[-b/a, -c/a], [1, 0]]
            double trace = -b / a;  // 矩阵的迹（对角线之和）
            double determinant = c / a; // 矩阵的行列式

            // 特征方程：λ² - trace*λ + determinant = 0
            double discriminant = trace * trace - 4 * determinant;

            if (discriminant > 0)
            {
                double sqrtD = Math.Sqrt(discriminant);
                double lambda1 = (trace + sqrtD) / 2;
                double lambda2 = (trace - sqrtD) / 2;
                roots.Root_1[0] = lambda1;
                roots.Root_2[0] = lambda2;
                roots.Root_1[1] = 0;
                roots.Root_2[1] = 0;
                Console.WriteLine($"矩阵特征根：λ1 = {lambda1:F2}, λ2 = {lambda2:F2}");
            }
            else if (discriminant == 0)
            {
                double lambda = trace / 2;
                roots.Root_1[0] = lambda;
                roots.Root_2[0] = lambda;
                roots.Root_1[1] = 0;
                roots.Root_2[1] = 0;
                Console.WriteLine($"矩阵重根：λ = {lambda:F2}");
            }
            else
            {
                double real = trace / 2;
                double imaginary = Math.Sqrt(-discriminant) / 2;
                roots.Root_1[0] = real;
                roots.Root_2[0] = real;
                roots.Root_1[1] = imaginary;
                roots.Root_2[1] = -imaginary;
                Console.WriteLine($"矩阵复根：λ1 = {real:F2} + {imaginary:F2}i, λ2 = {real:F2} - {imaginary:F2}i");
            }
        }
        
        public static void SolveCubicEquationMatrix(double a, double b, double c, double d)
        {
            if (a == 0)
            {
                throw new ArgumentException("Coefficient 'a' cannot be zero for a cubic equation.");
            }

            // 构造伴随矩阵
            var matrix = DenseMatrix.OfArray(new double[,]
            {
                { -b / a, -c / a, -d / a },
                { 1, 0, 0 },
                { 0, 1, 0 }
            });

            // 进行特征分解
            var eigenDecomposition = matrix.Evd();

            // 获取特征值
            var eigenValues = eigenDecomposition.EigenValues;

            // 输出特征值（即方程的根）
            for (int i = 0; i < eigenValues.Count; i++)
            {
                Console.WriteLine($"根 {i + 1}: {eigenValues[i]}");
            }
        }
    }

    
    public class  QuadraticRoots
    {
        public List<double> Root_1 { get; }  = new List<double>(){0,0};
        public List<double> Root_2 { get; }  = new List<double>(){0,0};
    }
}