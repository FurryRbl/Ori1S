using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Assemblies
{
	/// <summary>Represents a hash of an assembly manifest's contents.</summary>
	// Token: 0x020001D5 RID: 469
	[ComVisible(true)]
	[Obsolete]
	[Serializable]
	public struct AssemblyHash : ICloneable
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> structure with the specified hash algorithm and the hash value.</summary>
		/// <param name="algorithm">The algorithm used to generate the hash. Values for this parameter come from the <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> enumeration. </param>
		/// <param name="value">The hash value. </param>
		// Token: 0x06001842 RID: 6210 RVA: 0x0005D3F0 File Offset: 0x0005B5F0
		[Obsolete]
		public AssemblyHash(AssemblyHashAlgorithm algorithm, byte[] value)
		{
			this._algorithm = algorithm;
			if (value != null)
			{
				this._value = (byte[])value.Clone();
			}
			else
			{
				this._value = null;
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> structure with the specified hash value. The hash algorithm defaults to <see cref="F:System.Configuration.Assemblies.AssemblyHashAlgorithm.SHA1" />.</summary>
		/// <param name="value">The hash value. </param>
		// Token: 0x06001843 RID: 6211 RVA: 0x0005D428 File Offset: 0x0005B628
		[Obsolete]
		public AssemblyHash(byte[] value)
		{
			this = new AssemblyHash(AssemblyHashAlgorithm.SHA1, value);
		}

		/// <summary>Gets or sets the hash algorithm.</summary>
		/// <returns>An assembly hash algorithm.</returns>
		// Token: 0x170003D5 RID: 981
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x0005D448 File Offset: 0x0005B648
		// (set) Token: 0x06001846 RID: 6214 RVA: 0x0005D450 File Offset: 0x0005B650
		[Obsolete]
		public AssemblyHashAlgorithm Algorithm
		{
			get
			{
				return this._algorithm;
			}
			set
			{
				this._algorithm = value;
			}
		}

		/// <summary>Clones this object.</summary>
		/// <returns>An exact copy of this object.</returns>
		// Token: 0x06001847 RID: 6215 RVA: 0x0005D45C File Offset: 0x0005B65C
		[Obsolete]
		public object Clone()
		{
			return new AssemblyHash(this._algorithm, this._value);
		}

		/// <summary>Gets the hash value.</summary>
		/// <returns>The hash value.</returns>
		// Token: 0x06001848 RID: 6216 RVA: 0x0005D474 File Offset: 0x0005B674
		[Obsolete]
		public byte[] GetValue()
		{
			return this._value;
		}

		/// <summary>Sets the hash value.</summary>
		/// <param name="value">The hash value. </param>
		// Token: 0x06001849 RID: 6217 RVA: 0x0005D47C File Offset: 0x0005B67C
		[Obsolete]
		public void SetValue(byte[] value)
		{
			this._value = value;
		}

		// Token: 0x040008CA RID: 2250
		private AssemblyHashAlgorithm _algorithm;

		// Token: 0x040008CB RID: 2251
		private byte[] _value;

		/// <summary>An empty <see cref="T:System.Configuration.Assemblies.AssemblyHash" /> object.</summary>
		// Token: 0x040008CC RID: 2252
		[Obsolete]
		public static readonly AssemblyHash Empty = new AssemblyHash(AssemblyHashAlgorithm.None, null);
	}
}
