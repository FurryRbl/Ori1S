using System;
using System.Reflection;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents the method that handles the <see cref="E:System.AppDomain.TypeResolve" />, <see cref="E:System.AppDomain.ResourceResolve" />, and <see cref="E:System.AppDomain.AssemblyResolve" /> events of an <see cref="T:System.AppDomain" />.</summary>
	/// <returns>The <see cref="T:System.Reflection.Assembly" /> that resolves the type, assembly, or resource; or null if the assembly cannot be resolved.</returns>
	/// <param name="sender">The source of the event. </param>
	/// <param name="args">A <see cref="T:System.ResolveEventArgs" /> that contains the event data. </param>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006ED RID: 1773
	// (Invoke) Token: 0x060043A8 RID: 17320
	[ComVisible(true)]
	[Serializable]
	public delegate Assembly ResolveEventHandler(object sender, ResolveEventArgs args);
}
