using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Provides data for the <see cref="E:System.AppDomain.AssemblyLoad" /> event.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000105 RID: 261
	[ComVisible(true)]
	public class AssemblyLoadEventArgs : EventArgs
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.AssemblyLoadEventArgs" /> class using the specified <see cref="T:System.Reflection.Assembly" />.</summary>
		/// <param name="loadedAssembly">An instance that represents the currently loaded assembly. </param>
		// Token: 0x06000D8A RID: 3466 RVA: 0x0003AF38 File Offset: 0x00039138
		public AssemblyLoadEventArgs(Assembly loadedAssembly)
		{
			this.m_loadedAssembly = loadedAssembly;
		}

		/// <summary>Gets an <see cref="T:System.Reflection.Assembly" /> that represents the currently loaded assembly.</summary>
		/// <returns>An instance of <see cref="T:System.Reflection.Assembly" /> that represents the currently loaded assembly.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000D8B RID: 3467 RVA: 0x0003AF48 File Offset: 0x00039148
		public Assembly LoadedAssembly
		{
			get
			{
				return this.m_loadedAssembly;
			}
		}

		// Token: 0x040003A5 RID: 933
		private Assembly m_loadedAssembly;
	}
}
