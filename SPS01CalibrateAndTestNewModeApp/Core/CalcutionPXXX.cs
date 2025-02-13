using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Symbolics;
// using MathNet.Symbolics.Algebra;


namespace SPS01CalibrateAndTestNewModeApp.Core
{
    public class CalcutionPxxx
{
    private List<double> press;
    private List<double> temp;
    private List<double> target;
    private string calimode;
    private string source_data_format;
    private string asic;
    public List<double> abcd;
    private double? s0;
    public double? f0;
    private double? k2;
    private double? k3;
    private double? stc1;
    private double? stc2;
    private double? ftc1;
    private double? ftc2;
    private double baseT;
    private List<double> btemp;
    private Dictionary<string, double> coefficient;
    private List<double> coefficient_up;
    private List<double> coefficient_down;
    private int p_num;
    private int t_num;
    private List<double> x;
    private List<double> y;

    public CalcutionPxxx(List<double> press1, List<double> temp1, List<double> target1, string calimode1, string source_data_format1, string asic1)
    {
        this.press = NormalizePress(press1, asic1);
        this.temp = temp1;
        this.target = target1;
        this.calimode = calimode1;
        this.source_data_format = source_data_format1;
        this.asic = asic1;

        this.p_num = int.Parse(calimode.Substring(2,1));
        this.t_num = int.Parse(calimode.Substring(0, 1));
        this.x = this.press.GetRange(0, int.Parse(calimode.Substring(2,1)));
        this.y = this.target.GetRange(0, int.Parse(calimode.Substring(2, 1)));

        this.abcd = null;
        this.s0 = null;
        this.f0 = null;
        this.k2 = null;
        this.k3 = null;
        this.stc1 = null;
        this.stc2 = null;
        this.ftc1 = null;
        this.ftc2 = null;
        this.baseT = 0;
        this.btemp = new List<double>();
        this.coefficient = new Dictionary<string, double>();

        SetCoefficientValues(asic);
    }

    private List<double> NormalizePress(List<double> press, string asic)
    {
        if (asic == "p100")
        {
            for (int i = 0; i < press.Count; i++)
            {
                press[i] /= 8388608;
                Console.WriteLine(press[i]);
            }
        }
        else
        {
            for (int i = 0; i < press.Count; i++)
            {
                press[i] /= (double)(1 << 15);
            }
        }
        return press;
    }

    private void SetCoefficientValues(string asic)
    {
        if (asic == "p100")
        {
            coefficient_up = new List<double> { 8, 1, 0.5, 0.25, 0.006, 1.5e-5, 0.006, 1.5e-5, 125 };
            coefficient_down = new List<double> { 0.8, -1, -0.5, -0.25, -0.006, -1.5e-5, -0.006, -1.5e-5, -40 };
        }
        else
        {
            coefficient_up = new List<double> { 8, 1, 0.5, 0.25, 0.006, 3.1e-5, 0.006, 3.1e-5, 125 };
            coefficient_down = new List<double> { 0.8, -1, -0.5, -0.25, -0.006, -3.1e-5, -0.006, -3.1e-5, -40 };
        }
    }
    
    public void SolveAbc()
    {
        var f = new List<List<double>>();
        for (var j = 0; j < x.Count; j++)
        {
            var xarr = new List<double>();
            for (var i = 0; i < x.Count; i++)
            {
                xarr.Add(Math.Pow(x[j], i));
            }
            xarr.Reverse();
            f.Add(xarr);
        }

        var farr = Matrix<double>.Build.DenseOfRows(f);
        Console.WriteLine(farr);
        var yarr = MathNet.Numerics.LinearAlgebra.Vector<double>.Build.DenseOfArray(y.ToArray());
        Console.WriteLine(yarr);
        var abc_ = farr.Solve(yarr);
        abcd = abc_.ToList();
    }
    
    public void AbcdFour()
    {
        if (abcd.Count == 2)
        {
            abcd.Insert(0, 0);
            abcd.Insert(0, 0);
        }
        else if (abcd.Count == 3)
        {
            abcd.Insert(0, 0);
        }
        // 其他情况可按需求添加相应逻辑
    }

    public void SolveF0()
    {
        abcd.Reverse();
        
        var solutions = FindRoots.Polynomial(abcd.ToArray()); 
        // Console.WriteLine(solutions);
        foreach (var solution in solutions)
        {
            Console.WriteLine(solution);
        }
        foreach (var solution in solutions.Where(solution => solution.Imaginary == 0).Where(solution => solution.Real >= 0 && solution.Real <= 1))
        {
            f0 = solution.Real;
        }
   
    }

    public void SolveK2()
    {
        
    }
}
}