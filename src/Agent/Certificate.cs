using EdjCase.ICP.Agent.Identity;
using EdjCase.ICP.Candid.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace EdjCase.ICP.Agent
{

	public class Certificate
	{
		public Certificate(HashTree tree, byte[] signature, CertificateDelegation? delegation)
		{
			this.Tree = tree ?? throw new ArgumentNullException(nameof(tree));
			this.Signature = signature ?? throw new ArgumentNullException(nameof(signature));
			this.Delegation = delegation;
		}

		public HashTree Tree { get; }
		public byte[] Signature { get; }
		public CertificateDelegation? Delegation { get; }
	}

	public class CertificateDelegation
	{
		public Principal SubnetId { get; }
		public Certificate Certificate { get; }
		public CertificateDelegation(Principal subnetId, Certificate certificate)
		{
			this.SubnetId = subnetId ?? throw new ArgumentNullException(nameof(subnetId));
			this.Certificate = certificate ?? throw new ArgumentNullException(nameof(certificate));
		}
	}
}
