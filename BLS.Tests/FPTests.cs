using EdjCase.ICP.BLS.Models;

public class FpTests
{
	[Fact]
	public void FromBytes_SquareEqual()
	{
		Fp a = new(
			0x085d_bea8_4ed4_7f79,
			0xf575_54f3_a297_4f3f,
			0x9b43_821f_849e_2284,
			0xcff1_a7f4_e958_3ab3,
			0x8755_caf7_4596_91a1,
			0xdc90_6d9b_e3f9_5dc8
		);

		for (int i = 0; i < 100; i++)
		{
			a = a.Square(); // Ensure you have implemented Square method in Fp
			byte[] tmp = a.ToBytes(); // Ensure you have implemented ToBytes method in Fp
			Fp b = Fp.FromBytes(tmp);
			Assert.Equal(a, b);
		}
	}
	[Fact]
	public void FromBytes_NegativeOne()
	{

		var negativeOneBytes = new byte[]
		{
			26, 1, 17, 234, 57, 127, 230, 154, 75, 27, 167, 182, 67, 75, 172, 215, 100, 119, 75,
			132, 243, 133, 18, 191, 103, 48, 210, 160, 246, 176, 246, 36, 30, 171, 255, 254, 177,
			83, 255, 255, 185, 254, 255, 255, 255, 255, 170, 170
		};
		Assert.Equal(-Fp.One(), Fp.FromBytes(negativeOneBytes)); // Assuming you have a static property for Fp.One()
	}
	[Fact]
	public void FromBytes_InvalidBytes_Error()
	{
		var invalidBytes = new byte[]
		{
			27, 1, 17, 234, 57, 127, 230, 154, 75, 27, 167, 182, 67, 75, 172, 215, 100, 119, 75,
			132, 243, 133, 18, 191, 103, 48, 210, 160, 246, 176, 246, 36, 30, 171, 255, 254, 177,
			83, 255, 255, 185, 254, 255, 255, 255, 255, 170, 170
		};
		Assert.Throws<ArgumentException>(() => Fp.FromBytes(invalidBytes));
	}

	[Fact]
	public void FromBytes_OverflowBytes_Error()
	{
		var overflowBytes = new byte[48];
		Array.Fill(overflowBytes, (byte)0xff);
		Assert.Throws<ArgumentException>(() => Fp.FromBytes(overflowBytes));
	}
}
