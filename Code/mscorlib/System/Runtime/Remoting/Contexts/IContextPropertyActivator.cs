using System;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Contexts
{
	/// <summary>Indicates that the implementing property is interested in participating in activation and might not have provided a message sink.</summary>
	// Token: 0x02000478 RID: 1144
	[ComVisible(true)]
	public interface IContextPropertyActivator
	{
		/// <summary>Called on each client context property that has this interface, before the construction request leaves the client.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />. </param>
		// Token: 0x06002F16 RID: 12054
		void CollectFromClientContext(IConstructionCallMessage msg);

		/// <summary>Called on each server context property that has this interface, before the construction response leaves the server for the client.</summary>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />. </param>
		// Token: 0x06002F17 RID: 12055
		void CollectFromServerContext(IConstructionReturnMessage msg);

		/// <summary>Called on each client context property that has this interface, when the construction request returns to the client from the server.</summary>
		/// <returns>true if successful; otherwise, false.</returns>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />. </param>
		// Token: 0x06002F18 RID: 12056
		bool DeliverClientContextToServerContext(IConstructionCallMessage msg);

		/// <summary>Called on each client context property that has this interface, when the construction request returns to the client from the server.</summary>
		/// <returns>true if successful; otherwise, false.</returns>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionReturnMessage" />. </param>
		// Token: 0x06002F19 RID: 12057
		bool DeliverServerContextToClientContext(IConstructionReturnMessage msg);

		/// <summary>Indicates whether it is all right to activate the object type indicated in the <paramref name="msg" /> parameter.</summary>
		/// <returns>A Boolean value indicating whether the requested type can be activated.</returns>
		/// <param name="msg">An <see cref="T:System.Runtime.Remoting.Activation.IConstructionCallMessage" />. </param>
		// Token: 0x06002F1A RID: 12058
		bool IsOKToActivate(IConstructionCallMessage msg);
	}
}
