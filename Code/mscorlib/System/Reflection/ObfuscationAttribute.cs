using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Instructs obfuscation tools to take the specified actions for an assembly, type, or member.</summary>
	// Token: 0x020002AE RID: 686
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Parameter | AttributeTargets.Delegate, AllowMultiple = true, Inherited = false)]
	public sealed class ObfuscationAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.ObfuscationAttribute" /> class.</summary>
		// Token: 0x060022ED RID: 8941 RVA: 0x0007DA28 File Offset: 0x0007BC28
		public ObfuscationAttribute()
		{
			this.exclude = true;
			this.strip = true;
			this.applyToMembers = true;
			this.feature = "all";
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should exclude the type or member from obfuscation.</summary>
		/// <returns>true if the type or member to which this attribute is applied should be excluded from obfuscation; otherwise, false. The default is true.</returns>
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x0007DA5C File Offset: 0x0007BC5C
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x0007DA64 File Offset: 0x0007BC64
		public bool Exclude
		{
			get
			{
				return this.exclude;
			}
			set
			{
				this.exclude = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the obfuscation tool should remove this attribute after processing.</summary>
		/// <returns>true if an obfuscation tool should remove the attribute after processing; otherwise, false. The default is true.</returns>
		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x0007DA70 File Offset: 0x0007BC70
		// (set) Token: 0x060022F1 RID: 8945 RVA: 0x0007DA78 File Offset: 0x0007BC78
		public bool StripAfterObfuscation
		{
			get
			{
				return this.strip;
			}
			set
			{
				this.strip = value;
			}
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value indicating whether the attribute of a type is to apply to the members of the type.</summary>
		/// <returns>true if the attribute is to apply to the members of the type; otherwise, false. The default is true.</returns>
		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x060022F2 RID: 8946 RVA: 0x0007DA84 File Offset: 0x0007BC84
		// (set) Token: 0x060022F3 RID: 8947 RVA: 0x0007DA8C File Offset: 0x0007BC8C
		public bool ApplyToMembers
		{
			get
			{
				return this.applyToMembers;
			}
			set
			{
				this.applyToMembers = value;
			}
		}

		/// <summary>Gets or sets a string value that is recognized by the obfuscation tool, and which specifies processing options. </summary>
		/// <returns>A string value that is recognized by the obfuscation tool, and which specifies processing options. The default is "all".</returns>
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0007DA98 File Offset: 0x0007BC98
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x0007DAA0 File Offset: 0x0007BCA0
		public string Feature
		{
			get
			{
				return this.feature;
			}
			set
			{
				this.feature = value;
			}
		}

		// Token: 0x04000D06 RID: 3334
		private bool exclude;

		// Token: 0x04000D07 RID: 3335
		private bool strip;

		// Token: 0x04000D08 RID: 3336
		private bool applyToMembers;

		// Token: 0x04000D09 RID: 3337
		private string feature;
	}
}
