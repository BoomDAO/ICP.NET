using System.Numerics;

namespace EdjCase.ICP.BLS.Models
{

	public class Scalar
	{
		private BigInteger value;
		private static readonly BigInteger Modulus = BigInteger.Parse("52435875175126190479447740508185965837690552500527637822603658699938581184512", System.Globalization.NumberStyles.AllowHexSpecifier);

		public Scalar(BigInteger value)
		{
			this.value = value % Modulus;
		}

		public static Scalar Zero => new Scalar(BigInteger.Zero);
		public static Scalar One => new Scalar(BigInteger.One);

		// Implement the addition operation
		public static Scalar operator +(Scalar a, Scalar b)
		{
			return new Scalar(a.value + b.value);
		}

		// Implement the subtraction operation
		public static Scalar operator -(Scalar a, Scalar b)
		{
			return new Scalar(a.value - b.value + Modulus); // Ensure result is positive
		}

		// Implement the multiplication operation
		public static Scalar operator *(Scalar a, Scalar b)
		{
			return new Scalar(a.value * b.value);
		}

		// Implement the negation operation
		public static Scalar operator -(Scalar a)
		{
			return new Scalar(-a.value + Modulus);
		}

		// Implement the equality check
		public override bool Equals(object obj)
		{
			return obj is Scalar other && this.value.Equals(other.value);
		}

		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Convert to and from byte arrays, considering endianness
		public byte[] ToByteArray()
		{
			return this.value.ToByteArray();
		}

		public static Scalar FromByteArray(byte[] bytes)
		{
			return new Scalar(new BigInteger(bytes));
		}

		// Implement other methods as needed, such as inversion, square, etc.
	}

}
