using Dahomey.Cbor.Serialization;
using Dahomey.Cbor.Serialization.Converters;
using EdjCase.ICP.Agent.Cbor;
using EdjCase.ICP.Agent.Models;
using EdjCase.ICP.Agent.Responses;
using EdjCase.ICP.Candid.Models;
using System;

namespace EdjCase.ICP.Agent.Cbor.Converters
{
	internal class ReadStateResponseCborConverter : CborConverterBase<ReadStateResponse>
	{
		public override ReadStateResponse Read(ref CborReader reader)
		{
			var context = new Context();
			CborUtil.ReadMap(ref reader, ref context, this.SetReadStateContext);
			return new ReadStateResponse(context.Certificate!);
		}

		private void SetReadStateContext(string field, ref CborReader reader, ref Context context)
		{
			switch (field)
			{
				case "certificate":
					var certReader = new CborReader(reader.ReadByteString());
					context.Certificate = ReadCert(ref certReader);
					break;
				default:
					throw new NotImplementedException($"Cannot deserialize read_state response. Unknown field '{field}'");
			}
		}

		public override void Write(ref CborWriter writer, ReadStateResponse value)
		{
			// Never write
			throw new NotImplementedException();
		}



		public class Context
		{
			public Certificate? Certificate { get; set; }
		}


		internal class CertContext
		{
			public HashTree? HashTree { get; set; }
			public byte[]? Signature { get; set; }
			public CertificateDelegation? Delegation { get; set; }

			internal Certificate ToCert()
			{
				return new Certificate(this.HashTree!, this.Signature!, this.Delegation);
			}
		}

		private static Certificate ReadCert(ref CborReader reader)
		{
			var context = new CertContext();
			CborUtil.ReadMap(ref reader, ref context, SetCertValue);
			return new Certificate(context.HashTree!, context.Signature!, context.Delegation);
		}
		private static void SetCertValue(string name, ref CborReader reader, ref CertContext context)
		{
			switch (name)
			{
				case "tree":
					context.HashTree = ReadTree(ref reader);
					break;
				case "signature":
					context.Signature = reader.ReadByteString().ToArray();
					break;
				case "delegation":
					context.Delegation = ReadDelegation(ref reader);
					break;
				default:
					throw new NotImplementedException($"Cannot deserialize certificate response. Unknown field '{name}'");
			}
		}

		private class CertDelContext
		{
			public Principal SubnetId { get; set; }
			public Certificate Certificate { get; set; }
		}

		private static CertificateDelegation ReadDelegation(ref CborReader reader)
		{
			var context = new CertDelContext();
			CborUtil.ReadMap(ref reader, ref context, SetDelValue);

			return new CertificateDelegation(
				context.SubnetId,
				context.Certificate
			);
		}

		private static void SetDelValue(string field, ref CborReader reader, ref CertDelContext context)
		{
			switch (field)
			{
				case "subnet_id":
					var prinBytes = reader.ReadByteString()!;
					context.SubnetId = Principal.FromBytes(prinBytes.ToArray());
					break;
				case "certificate":
					var certBytes = reader.ReadByteString()!;
					var certReader = new CborReader(certBytes);
					context.Certificate = ReadCert(ref certReader);
					break;
			}
		}

		private static HashTree ReadTree(ref CborReader reader)
		{
			_ = reader.ReadSize(); // Array size
			uint nodeType = reader.ReadUInt32(); // Get tree node type
			switch (nodeType)
			{
				case 0:
					return HashTree.Empty();
				case 1:
					HashTree left = ReadTree(ref reader);
					HashTree right = ReadTree(ref reader);
					return HashTree.Fork(left, right);
				case 2:
					byte[] labelBytes = reader.ReadByteString().ToArray();
					HashTree tree = ReadTree(ref reader);
					return HashTree.Labeled(labelBytes, tree);
				case 3:
					{
						byte[] value = reader.ReadByteString().ToArray();
						return HashTree.Leaf(value);
					}
				case 4:
					{
						byte[] value = reader.ReadByteString().ToArray();
						return HashTree.Pruned(value);
					}
				default:
					throw new NotImplementedException($"No hash tree node type of '{nodeType}' is implemented");
			}
		}

		private class HashTreeContext
		{
			public HashTree? Value { get; set; }
			public int ArraySize { get; set; }
		}
	}

}
