using System;
using System.Runtime.InteropServices;

namespace System.Security.Cryptography
{
	/// <summary>Represents the abstract base class from which all classes that derive byte sequences of a specified length inherit.</summary>
	// Token: 0x020005A5 RID: 1445
	[ComVisible(true)]
	public abstract class DeriveBytes
	{
		/// <summary>When overridden in a derived class, returns pseudo-random key bytes.</summary>
		/// <returns>A byte array filled with pseudo-random key bytes.</returns>
		/// <param name="cb">The number of pseudo-random key bytes to generate. </param>
		// Token: 0x060037AB RID: 14251
		public abstract byte[] GetBytes(int cb);

		/// <summary>When overridden in a derived class, resets the state of the operation.</summary>
		// Token: 0x060037AC RID: 14252
		public abstract void Reset();
	}
}
