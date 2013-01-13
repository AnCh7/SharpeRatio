using System;
using System.IO;
using WealthLab;
using System.Collections.Generic;

namespace SharpeRatioProject
{
	public class SharpeRatioStrategyScript : WealthScript
	{
		#region Helpers

		private void SaveToDisk(string str)
		{
			using (StreamWriter file = new StreamWriter(@"d:\Sharpe.csv", true))
			{
				file.WriteLine(str);
			}
		}

		private double StandardDeviation(IEnumerable<double> valueList)
		{
			double m = 0.0;
			double s = 0.0;
			int k = 1;

			foreach (double value in valueList)
			{
				double tmpM = m;
				m += (value - tmpM) / k;
				s += (value - tmpM) * (value - m);
				k++;
			}
			return (Math.Sqrt(s / (k - 1))) / 100;
		}

		private double AverageReturn(ICollection<double> list)
		{
			double counter = 0;
			foreach (double l in list)
			{
				counter += l;
			}
			return (counter / list.Count) / 100;
		}

		private double SharpeRatio(double average, double stdev, int numberOfTradeSessions)
		{
			return (Math.Sqrt(numberOfTradeSessions) * (average / stdev));
		}

		private double DailyReturn(double current, double prev)
		{
			return ((100 * current) / prev) - 100;
		}

		private double AnnualReturn(double first, double last)
		{
			return ((last * 100) / first) - 100;
		}

		#endregion Helpers

		protected override void Execute()
		{
			List<double> dailyReturnList = new List<double>();

			for (int i = 1; i < Bars.Count; i++)
			{
				double dailyReturn = DailyReturn(Close[i], Close[i - 1]);
				dailyReturnList.Add(dailyReturn);
			}

			double average = AverageReturn(dailyReturnList);
			double stdev = StandardDeviation(dailyReturnList);
			double sharpeRatio = SharpeRatio(average, stdev, Bars.Count);
			double annualReturn = AnnualReturn(Close[0], Close[Bars.Count - 1]);

			string str = Bars.Symbol + ";" + Bars.Count + ";" + annualReturn + ";" + average + ";" + stdev + ";" + sharpeRatio;
			SaveToDisk(str);
		}
	}
}
