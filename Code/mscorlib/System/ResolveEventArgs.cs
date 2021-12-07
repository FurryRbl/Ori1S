using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, and <see cref="E:System.AppDomain.AssemblyResolve" /> events.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200016E RID: 366
	[ComVisible(true)]
	public class ResolveEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ResolveEventArgs" /> class.</summary>
		/// <param name="name">The name of an item to resolve. </param>
		// Token: 0x06001365 RID: 4965 RVA: 0x0004D958 File Offset: 0x0004BB58
		public ResolveEventArgs(string name)
		{
			this.m_Name = name;
		}

		/// <summary>Gets the name of the item to resolve.</summary>
		/// <returns>The name of the item to resolve.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x06001366 RID: 4966 RVA: 0x0004D968 File Offset: 0x0004BB68
		public string Name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x040005A5 RID: 1445
		private string m_Name;
	}
}
