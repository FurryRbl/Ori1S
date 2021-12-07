using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	/// <summary>Defines a copyright custom attribute for an assembly manifest.</summary>
	// Token: 0x02000273 RID: 627
	[ComVisible(true)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Reflection.AssemblyCopyrightAttribute" /> class.</summary>
		/// <param name="copyright">The copyright information. </param>
		// Token: 0x060020BE RID: 8382 RVA: 0x00077CB4 File Offset: 0x00075EB4
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.name = copyright;
		}

		/// <summary>Gets copyright information.</summary>
		/// <returns>A string containing the copyright information.</returns>
		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x00077CC4 File Offset: 0x00075EC4
		public string Copyright
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x04000C01 RID: 3073
		private string name;
	}
}
