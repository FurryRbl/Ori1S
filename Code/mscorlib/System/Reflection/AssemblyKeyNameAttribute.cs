using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the name of a key container within the CSP containing the key pair used to generate a strong name.</summary>
	// Token: 0x0200027B RID: 635
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyKeyNameAttribute" /> class with the name of the container holding the key pair used to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyName">The name of the container containing the key pair. </param>
		// Token: 0x060020D1 RID: 8401 RVA: 0x00077DAC File Offset: 0x00075FAC
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.name = keyName;
		}

		/// <summary>Gets the name of the container having the key pair that is used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the container that has the relevant key pair.</returns>
		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060020D2 RID: 8402 RVA: 0x00077DBC File Offset: 0x00075FBC
		public string KeyName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C09 RID: 3081
		private string name;
	}
}
