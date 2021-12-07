using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Serialization.Formatters
{
	/// <summary>Provides an interface for an object that contains the names and types of parameters required during serialization of a SOAP RPC (Remote Procedure Call).</summary>
	// Token: 0x02000515 RID: 1301
	[ComVisible(true)]
	public interface ISoapMessage
	{
		/// <summary>Gets or sets the out-of-band data of the method call.</summary>
		/// <returns>The out-of-band data of the method call.</returns>
		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x0600339D RID: 13213
		// (set) Token: 0x0600339E RID: 13214
		Header[] Headers { get; set; }

		/// <summary>Gets or sets the name of the called method.</summary>
		/// <returns>The name of the called method.</returns>
		// Token: 0x170009B1 RID: 2481
		// (get) Token: 0x0600339F RID: 13215
		// (set) Token: 0x060033A0 RID: 13216
		string MethodName { get; set; }

		/// <summary>Gets or sets the parameter names of the method call.</summary>
		/// <returns>The parameter names of the method call.</returns>
		// Token: 0x170009B2 RID: 2482
		// (get) Token: 0x060033A1 RID: 13217
		// (set) Token: 0x060033A2 RID: 13218
		string[] ParamNames { get; set; }

		/// <summary>Gets or sets the parameter types of a method call.</summary>
		/// <returns>The parameter types of a method call.</returns>
		// Token: 0x170009B3 RID: 2483
		// (get) Token: 0x060033A3 RID: 13219
		// (set) Token: 0x060033A4 RID: 13220
		Type[] ParamTypes { get; set; }

		/// <summary>Gets or sets the parameter values of a method call.</summary>
		/// <returns>The parameter values of a method call.</returns>
		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x060033A5 RID: 13221
		// (set) Token: 0x060033A6 RID: 13222
		object[] ParamValues { get; set; }

		/// <summary>Gets or sets the XML namespace of the SOAP RPC (Remote Procedure Call) <see cref="P:System.Runtime.Serialization.Formatters.ISoapMessage.MethodName" /> element.</summary>
		/// <returns>The XML namespace name where the object that contains the called method is located.</returns>
		// Token: 0x170009B5 RID: 2485
		// (get) Token: 0x060033A7 RID: 13223
		// (set) Token: 0x060033A8 RID: 13224
		string XmlNameSpace { get; set; }
	}
}
