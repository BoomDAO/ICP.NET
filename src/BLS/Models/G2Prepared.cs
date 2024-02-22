using System.Collections.Generic;

namespace EdjCase.ICP.BLS.Models
{
	internal class G2Prepared
	{
		public bool IsInfinity { get; }
		public List<(Fp2, Fp2, Fp2)> Coefficients { get; }

		public G2Prepared(bool isInfinity, List<(Fp2, Fp2, Fp2)> coefficients)
		{
			this.IsInfinity = isInfinity;
			this.Coefficients = coefficients;
		}
	}
}
