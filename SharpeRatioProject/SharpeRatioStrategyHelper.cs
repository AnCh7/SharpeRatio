using System;
using WealthLab;

namespace SharpeRatioProject
{
	class SharpeRatioStrategyHelper : StrategyHelper
	{
		public override string Name
		{
			get { return "SharpeRatio Strategy"; }
		}

		public override Guid ID
		{
			get
			{
				return new Guid("de25ad80-21c3-4677-90b3-c4cd787618b0");
			}
		}

		public override string Author
		{
			get { return "AnCh Software"; }
		}

		public override Type WealthScriptType
		{
			get { return typeof(SharpeRatioStrategyScript); }
		}

		public override string Description
		{
			get { return "SharpeRatio Strategy Description"; }
		}

		public override DateTime CreationDate
		{
			get { return new DateTime(2012, 05, 13); }
		}

		public override DateTime LastModifiedDate
		{
			get { return DateTime.Now; }
		}
	}
}
