using System;

namespace System.Configuration.Internal
{
	/// <summary>Represents a method for hosts to call when a monitored stream has changed.</summary>
	/// <param name="streamName">The name of the <see cref="T:System.IO.Stream" /> object performing I/O tasks on the configuration file.</param>
	// Token: 0x02000083 RID: 131
	// (Invoke) Token: 0x0600043F RID: 1087
	public delegate void StreamChangeCallback(string streamName);
}
