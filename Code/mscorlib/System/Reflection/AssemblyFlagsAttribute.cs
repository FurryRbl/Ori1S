using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags for an assembly, describing just-in-time (JIT) compiler options, whether the assembly is retargetable, and whether it has a full or tokenized public key. This class cannot be inherited.</summary>
	// Token: 0x02000278 RID: 632
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFlagsAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an unsigned integer value.</summary>
		/// <param name="flags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an unsigned integer value, representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x060020C8 RID: 8392 RVA: 0x00077D3C File Offset: 0x00075F3C
		[Obsolete("")]
		[CLSCompliant(false)]
		public AssemblyFlagsAttribute(uint flags)
		{
			this.flags = flags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an integer value.</summary>
		/// <param name="assemblyFlags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags, cast as an integer value, representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x060020C9 RID: 8393 RVA: 0x00077D4C File Offset: 0x00075F4C
		[Obsolete("")]
		public AssemblyFlagsAttribute(int assemblyFlags)
		{
			this.flags = (uint)assemblyFlags;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyFlagsAttribute" /> class with the specified combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</summary>
		/// <param name="assemblyFlags">A bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags representing just-in-time (JIT) compiler options, longevity, whether an assembly is retargetable, and whether it has a full or tokenized public key.</param>
		// Token: 0x060020CA RID: 8394 RVA: 0x00077D5C File Offset: 0x00075F5C
		public AssemblyFlagsAttribute(AssemblyNameFlags assemblyFlags)
		{
			this.flags = (uint)assemblyFlags;
		}

		/// <summary>Gets an unsigned integer value representing the combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags specified when this attribute instance was created.</summary>
		/// <returns>An unsigned integer value representing a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</returns>
		// Token: 0x17000579 RID: 1401
		// (get) Token: 0x060020CB RID: 8395 RVA: 0x00077D6C File Offset: 0x00075F6C
		[CLSCompliant(false)]
		[Obsolete("")]
		public uint Flags
		{
			get
			{
				return this.flags;
			}
		}

		/// <summary>Gets an integer value representing the combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags specified when this attribute instance was created.</summary>
		/// <returns>An integer value representing a bitwise combination of <see cref="T:System.Reflection.AssemblyNameFlags" /> flags.</returns>
		// Token: 0x1700057A RID: 1402
		// (get) Token: 0x060020CC RID: 8396 RVA: 0x00077D74 File Offset: 0x00075F74
		public int AssemblyFlags
		{
			get
			{
				return (int)this.flags;
			}
		}

		// Token: 0x04000C06 RID: 3078
		private uint flags;
	}
}
