namespace Sample.Shared.Governance.Models
{
	public enum NeuronIdOrSubaccountType
	{
		Subaccount,
		NeuronId,
	}
	public class NeuronIdOrSubaccount
	{
		public NeuronIdOrSubaccountType Type { get; }
		private readonly object? value;
		
		public NeuronIdOrSubaccount(NeuronIdOrSubaccountType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static NeuronIdOrSubaccount Subaccount(List<byte> info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountType.Subaccount, info);
		}
		
		public List<byte> AsSubaccount()
		{
			this.ValidateType(NeuronIdOrSubaccountType.Subaccount);
			return (List<byte>)this.value!;
		}
		
		public static NeuronIdOrSubaccount NeuronId(NeuronId info)
		{
			return new NeuronIdOrSubaccount(NeuronIdOrSubaccountType.NeuronId, info);
		}
		
		public NeuronId AsNeuronId()
		{
			this.ValidateType(NeuronIdOrSubaccountType.NeuronId);
			return (NeuronId)this.value!;
		}
		
		private void ValidateType(NeuronIdOrSubaccountType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
