using System;

namespace System.Configuration.Internal
{
	/// <summary>Defines a class used by the .NET Framework infrastructure to support configuration events.</summary>
	/// <param name="sender">The source object of the event.</param>
	/// <param name="e">A configuration event argument.</param>
	// Token: 0x02000082 RID: 130
	// (Invoke) Token: 0x0600043B RID: 1083
	public delegate void InternalConfigEventHandler(object sender, InternalConfigEventArgs e);
}
