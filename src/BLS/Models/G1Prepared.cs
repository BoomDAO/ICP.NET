using System.Collections.Generic;

namespace EdjCase.ICP.BLS.Models
{
	internal class G1Prepared
	{
		public bool IsInfinity { get; }
		public List<(Fp2, Fp2, Fp2)> Coefficients { get; }

		public G1Prepared(bool isInfinity, List<(Fp2, Fp2, Fp2)> coefficients)
		{
			this.IsInfinity = isInfinity;
			this.Coefficients = coefficients;
		}
	}
}
