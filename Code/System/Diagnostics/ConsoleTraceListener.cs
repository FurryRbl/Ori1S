using System;

namespace System.Diagnostics
{
	/// <summary>Directs tracing or debugging output to either the standard output or the standard error stream.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200020E RID: 526
	public class ConsoleTraceListener : TextWriterTraceListener
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with trace output written to the standard output stream.</summary>
		// Token: 0x06001194 RID: 4500 RVA: 0x0002EC24 File Offset: 0x0002CE24
		public ConsoleTraceListener() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.ConsoleTraceListener" /> class with an option to write trace output to the standard output stream or the standard error stream.</summary>
		/// <param name="useErrorStream">true to write tracing and debugging output to the standard error stream; false to write tracing and debugging output to the standard output stream.</param>
		// Token: 0x06001195 RID: 4501 RVA: 0x0002EC30 File Offset: 0x0002CE30
		public ConsoleTraceListener(bool useErrorStream) : base((!useErrorStream) ? Console.Out : Console.Error)
		{
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x0002EC50 File Offset: 0x0002CE50
		internal ConsoleTraceListener(string data) : this(Convert.ToBoolean(data))
		{
		}
	}
}
