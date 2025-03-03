using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System;
using System.Collections.Generic;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();  // This is for the landing page
        }
        [HttpPost("Calculate")]
        public IActionResult Calculate([FromBody] MolecularRequest request)
        {
            try
            {

                if (request == null || request.elem == null || request.coeff == null || request.elem.Length != request.coeff.Length)
                {
                    return Json(new { error = "Invalid input: Elements and coefficients must match in length." });
                }

                var calculator = new MolecularMatrix
                {
                    Elements = request.elem.ToList(),
                    Coefficients = request.coeff.ToList(),
                    TotalMass = request.totalMass ?? 0
                };

                double molarMass = calculator.CalculateMolarMass();
                var percentComposition = calculator.CalculatePercentComposition() ?? new Dictionary<string, double>();
                string empiricalFormula = calculator.CalculateEmpiricalFormula();
                string molecularFormula = (request.totalMass.HasValue && request.totalMass > 0)
                    ? calculator.CalculateMolecularFormula()
                    : "N/A";


                return Json(new
                {
                    MolarMass = molarMass,
                    PercentComposition = percentComposition,
                    EmpiricalFormula = empiricalFormula,
                    MolecularFormula = molecularFormula
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Server Error: {ex.Message}");
                return Json(new { error = $"Server error: {ex.Message}" });
            }
        }

        public class MolecularRequest
        {
            public string[] elem { get; set; }
            public int[] coeff { get; set; }
            public double? totalMass { get; set; }
        }
    }
}
