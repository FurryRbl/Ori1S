using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Channels
{
	/// <summary>Stores sink provider data for sink providers.</summary>
	// Token: 0x02000466 RID: 1126
	[ComVisible(true)]
	public class SinkProviderData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> class.</summary>
		/// <param name="name">The name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with. </param>
		// Token: 0x06002EAF RID: 11951 RVA: 0x0009ABA4 File Offset: 0x00098DA4
		public SinkProviderData(string name)
		{
			this.sinkName = name;
			this.children = new ArrayList();
			this.properties = new Hashtable();
		}

		/// <summary>Gets a list of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</summary>
		/// <returns>A <see cref="T:System.Collections.IList" /> of the child <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> nodes.</returns>
		// Token: 0x1700085B RID: 2139
		// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x0009ABCC File Offset: 0x00098DCC
		public IList Children
		{
			get
			{
				return this.children;
			}
		}

		/// <summary>Gets the name of the sink provider that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</summary>
		/// <returns>A <see cref="T:System.String" /> with the name of the XML node that the data in the current <see cref="T:System.Runtime.Remoting.Channels.SinkProviderData" /> object is associated with.</returns>
		// Token: 0x1700085C RID: 2140
		// (get) Token: 0x06002EB1 RID: 11953 RVA: 0x0009ABD4 File Offset: 0x00098DD4
		public string Name
		{
			get
			{
				return this.sinkName;
			}
		}

		/// <summary>Gets a dictionary through which properties on the sink provider can be accessed.</summary>
		/// <returns>A dictionary through which properties on the sink provider can be accessed.</returns>
		// Token: 0x1700085D RID: 2141
		// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x0009ABDC File Offset: 0x00098DDC
		public IDictionary Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x040013E1 RID: 5089
		private string sinkName;

		// Token: 0x040013E2 RID: 5090
		private ArrayList children;

		// Token: 0x040013E3 RID: 5091
		private Hashtable properties;
	}
}
