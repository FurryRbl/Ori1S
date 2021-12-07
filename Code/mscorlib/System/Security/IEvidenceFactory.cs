using System;
using System.Runtime.InteropServices;
using System.Security.Policy;

namespace System.Security
{
	/// <summary>Gets an object's <see cref="T:System.Security.Policy.Evidence" />.</summary>
	// Token: 0x02000536 RID: 1334
	[ComVisible(true)]
	public interface IEvidenceFactory
	{
		/// <summary>Gets <see cref="T:System.Security.Policy.Evidence" /> that verifies the current object's identity.</summary>
		/// <returns>
		///   <see cref="T:System.Security.Policy.Evidence" /> of the current object's identity.</returns>
		// Token: 0x170009D5 RID: 2517
		// (get) Token: 0x0600348B RID: 13451
		Evidence Evidence { get; }
	}
}
