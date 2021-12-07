using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Specifies the name of a file containing the key pair used to generate a strong name.</summary>
	// Token: 0x0200027A RID: 634
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[ComVisible(true)]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the AssemblyKeyFileAttribute class with the name of the file containing the key pair to generate a strong name for the assembly being attributed.</summary>
		/// <param name="keyFile">The name of the file containing the key pair. </param>
		// Token: 0x060020CF RID: 8399 RVA: 0x00077D94 File Offset: 0x00075F94
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.name = keyFile;
		}

		/// <summary>Gets the name of the file containing the key pair used to generate a strong name for the attributed assembly.</summary>
		/// <returns>A string containing the name of the file that contains the key pair.</returns>
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060020D0 RID: 8400 RVA: 0x00077DA4 File Offset: 0x00075FA4
		public string KeyFile
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C08 RID: 3080
		private string name;
	}
}
