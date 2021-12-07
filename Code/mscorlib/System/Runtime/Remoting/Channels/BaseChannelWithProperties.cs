using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Provides a base implementation for channels that want to expose a dictionary interface to their properties.</summary>
	// Token: 0x02000448 RID: 1096
	[ComVisible(true)]
	public abstract class BaseChannelWithProperties : BaseChannelObjectWithProperties
	{
		/// <summary>Gets a <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</summary>
		/// <returns>A <see cref="T:System.Collections.IDictionary" /> of the channel properties associated with the current channel object.</returns>
		/// <exception cref="T:System.Security.SecurityException">The immediate caller does not have infrastructure permission. </exception>
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x0009999C File Offset: 0x00097B9C
		public override IDictionary Properties
		{
			get
			{
				if (this.SinksWithProperties == null || this.SinksWithProperties.Properties == null)
				{
					return base.Properties;
				}
				IDictionary[] dics = new IDictionary[]
				{
					base.Properties,
					this.SinksWithProperties.Properties
				};
				return new AggregateDictionary(dics);
			}
		}

		/// <summary>Indicates the top channel sink in the channel sink stack.</summary>
		// Token: 0x040013CC RID: 5068
		protected IChannelSinkBase SinksWithProperties;
	}
}
