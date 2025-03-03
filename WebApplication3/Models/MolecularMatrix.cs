using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApplication3.Models
{
    public class MolecularMatrix
    {
        private static readonly Dictionary<string, double> AtomicMasses = new()
        {
    {"H", 1}, {"He", 4}, {"Li", 7}, {"Be", 9}, {"B", 11}, {"C", 12}, {"N", 14}, {"O", 16},
    {"F", 19}, {"Ne", 20}, {"Na", 23}, {"Mg", 24}, {"Al", 27}, {"Si", 28}, {"P", 31}, {"S", 32},
    {"Cl", 35}, {"Ar", 40}, {"K", 39}, {"Ca", 40}, {"Sc", 45}, {"Ti", 48}, {"V", 51}, {"Cr", 52},
    {"Mn", 55}, {"Fe", 56}, {"Co", 59}, {"Ni", 59}, {"Cu", 64}, {"Zn", 65}, {"Ga", 70}, {"Ge", 73},
    {"As", 75}, {"Se", 79}, {"Br", 80}, {"Kr", 84}, {"Rb", 85}, {"Sr", 88}, {"Y", 89}, {"Zr", 91},
    {"Nb", 93}, {"Mo", 96}, {"Tc", 98}, {"Ru", 101}, {"Rh", 103}, {"Pd", 106}, {"Ag", 108}, {"Cd", 112},
    {"In", 115}, {"Sn", 119}, {"Sb", 122}, {"Te", 128}, {"I", 127}, {"Xe", 131}, {"Cs", 133}, {"Ba", 137},
    {"La", 139}, {"Ce", 140}, {"Pr", 141}, {"Nd", 144}, {"Pm", 145}, {"Sm", 150}, {"Eu", 152}, {"Gd", 157},
    {"Tb", 159}, {"Dy", 163}, {"Ho", 165}, {"Er", 167}, {"Tm", 169}, {"Yb", 173}, {"Lu", 175}, {"Hf", 178},
    {"Ta", 181}, {"W", 184}, {"Re", 186}, {"Os", 190}, {"Ir", 192}, {"Pt", 195}, {"Au", 197}, {"Hg", 201},
    {"Tl", 204}, {"Pb", 207}, {"Bi", 209}, {"Po", 209}, {"At", 210}, {"Rn", 222}, {"Fr", 223}, {"Ra", 226},
    {"Ac", 227}, {"Th", 232}, {"Pa", 231}, {"U", 238}, {"Np", 237}, {"Pu", 244}, {"Am", 243}, {"Cm", 247},
    {"Bk", 247}, {"Cf", 251}, {"Es", 252}, {"Fm", 257}, {"Md", 258}, {"No", 259}, {"Lr", 262}, {"Rf", 267},
    {"Db", 270}, {"Sg", 271}, {"Bh", 274}, {"Hs", 277}, {"Mt", 278}, {"Ds", 281}, {"Rg", 282}, {"Cn", 285},
    {"Nh", 286}, {"Fl", 289}, {"Mc", 289}, {"Lv", 293}, {"Ts", 294}, {"Og", 294}
        };

        public List<string> Elements { get; set; } = new();
        public List<int> Coefficients { get; set; } = new();
        public double TotalMass { get; set; }

        public double CalculateMolarMass()
        {
            double totalMass = 0;
            for (int i = 0; i < Elements.Count; i++)
            {
                if (AtomicMasses.TryGetValue(Elements[i], out double atomicMass))
                {
                    totalMass += atomicMass * Coefficients[i];
                }
                else
                {
                    throw new ArgumentException($"❌ Invalid element: {Elements[i]}");
                }
            }
            return totalMass;
        }

        public Dictionary<string, double> CalculatePercentComposition()
        {
            double molarMass = CalculateMolarMass();
            if (molarMass == 0) return new Dictionary<string, double>(); 

            var composition = new Dictionary<string, double>();

            for (int i = 0; i < Elements.Count; i++)
            {
                if (AtomicMasses.TryGetValue(Elements[i], out double atomicMass))
                {
                    double elementMass = atomicMass * Coefficients[i];
                    composition[Elements[i]] = Math.Round((elementMass / molarMass) * 100, 2);
                }
            }

            return composition;
        }

        public string CalculateEmpiricalFormula()
        {
            if (Coefficients.Count == 0) return "N/A";

            int minCoef = Coefficients.Min();
            if (minCoef == 0) return "N/A";

            return string.Join("", Elements.Select((e, i) => $"{e}{Coefficients[i] / minCoef}"));
        }

        public string CalculateMolecularFormula()
        {
            double molarMass = CalculateMolarMass();
            if (molarMass == 0 || TotalMass == 0) return "N/A";

            double ratio = TotalMass / molarMass;
            return string.Join("", Elements.Select((e, i) => $"{e}{Math.Round(Coefficients[i] * ratio)}"));
        }
    }
}
