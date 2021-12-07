using System;
using System.Configuration.Assemblies;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies an algorithm to hash all files in an assembly. This class cannot be inherited.</summary>
	// Token: 0x02000270 RID: 624
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyAlgorithmIdAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using one of the members of <see cref="T:System.Configuration.Assemblies.AssemblyHashAlgorithm" /> to represent the hash algorithm.</summary>
		/// <param name="algorithmId">A member of AssemblyHashAlgorithm that represents the hash algorithm. </param>
		// Token: 0x060020B7 RID: 8375 RVA: 0x00077C5C File Offset: 0x00075E5C
		public AssemblyAlgorithmIdAttribute(AssemblyHashAlgorithm algorithmId)
		{
			this.id = (uint)algorithmId;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyAlgorithmIdAttribute" /> class with the specified hash algorithm, using an unsigned integer to represent the hash algorithm.</summary>
		/// <param name="algorithmId">An unsigned integer representing the hash algorithm. </param>
		// Token: 0x060020B8 RID: 8376 RVA: 0x00077C6C File Offset: 0x00075E6C
		[CLSCompliant(false)]
		public AssemblyAlgorithmIdAttribute(uint algorithmId)
		{
			this.id = algorithmId;
		}

		/// <summary>Gets the hash algorithm of an assembly manifest's contents.</summary>
		/// <returns>An unsigned integer representing the assembly hash algorithm.</returns>
		// Token: 0x17000571 RID: 1393
		// (get) Token: 0x060020B9 RID: 8377 RVA: 0x00077C7C File Offset: 0x00075E7C
		[CLSCompliant(false)]
		public uint AlgorithmId
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x04000BFE RID: 3070
		private uint id;
	}
}
