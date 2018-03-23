using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Strategy
{
    public class CapitalStrategy
    {
        public double Calc(Loan loan)
        {
            return loan.RiskAmount() * loan.Duration();
        }


    }
}
