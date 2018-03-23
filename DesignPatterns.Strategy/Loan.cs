using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPatterns.Strategy
{
    public class Loan
    {
        private double unusedPercentage;
        private double notional;
        private double outstanding;
        private DateTime expire = DateTime.MinValue;
        private DateTime start = DateTime.MinValue;
        private DateTime maturity = DateTime.MinValue;
        private int rating;
        private static readonly int MILLIS_PER_DAY = 86400000;

        public double CalcCapital()
        {
            return RiskAmount() * Duration();

        }

        public double RiskAmount()
        {
            if (unusedPercentage != 1.00)
            {
                return outstanding + CalcUnusedRiskAmount();
            }
            else
                return outstanding;
        }

        private double CalcUnusedRiskAmount()
        {
            return (notional - outstanding) * unusedPercentage;
        }

        public double Duration()
        {
            if (expire == DateTime.MinValue)
            {
                return (maturity - start).Seconds / MILLIS_PER_DAY / 365;
            }
            else if (maturity == DateTime.MinValue)
            {
                return (expire - start).Seconds / MILLIS_PER_DAY / 365;
            }
            else
            {
                long millsToExpiry = (expire - start).Seconds;
                long millsFromExpiryToMaturity = (maturity - expire).Seconds;
                double revolverDuration = (millsToExpiry / MILLIS_PER_DAY) / 365;
                double termDuration = millsFromExpiryToMaturity / MILLIS_PER_DAY / 365;
                return revolverDuration + termDuration;
            }
        }

        private void SetUnusedPercentage()
        {
            if(expire!= DateTime.MinValue && maturity != DateTime.MinValue)
            {
                if (rating > 4) unusedPercentage = 0.95;
                else unusedPercentage = 0.50;
            }else if (maturity != DateTime.MinValue)
            {
                unusedPercentage = 1.00;
            }else if (expire != DateTime.MinValue)
            {
                if (rating > 4) unusedPercentage = 0.75;
                else unusedPercentage = 0.25;
            }
        }
    }
}
