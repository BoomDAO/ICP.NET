namespace Sample.Shared.Governance.Models
{
	public enum ChangeType
	{
		ToRemove,
		ToAdd,
	}
	public class Change
	{
		public ChangeType Type { get; }
		private readonly object? value;
		
		public Change(ChangeType type, object? value)
		{
			this.Type = type;
			this.value = value;
		}
		
		public static Change ToRemove(NodeProvider info)
		{
			return new Change(ChangeType.ToRemove, info);
		}
		
		public NodeProvider AsToRemove()
		{
			this.ValidateType(ChangeType.ToRemove);
			return (NodeProvider)this.value!;
		}
		
		public static Change ToAdd(NodeProvider info)
		{
			return new Change(ChangeType.ToAdd, info);
		}
		
		public NodeProvider AsToAdd()
		{
			this.ValidateType(ChangeType.ToAdd);
			return (NodeProvider)this.value!;
		}
		
		private void ValidateType(ChangeType type)
		{
			if (this.Type != type)
			{
				throw new InvalidOperationException($"Cannot cast '{this.Type}' to type '{type}'");
			}
		}
	}
}
