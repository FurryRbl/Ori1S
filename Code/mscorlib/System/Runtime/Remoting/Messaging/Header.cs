using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Messaging
{
	/// <summary>Defines the out-of-band data for a call.</summary>
	// Token: 0x02000497 RID: 1175
	[ComVisible(true)]
	[Serializable]
	public class Header
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name and value.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		// Token: 0x06002FCC RID: 12236 RVA: 0x0009DDF4 File Offset: 0x0009BFF4
		public Header(string _Name, object _Value) : this(_Name, _Value, true)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class with the given name, value, and additional configuration information.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_Value">The object that contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand the out-of-band data. </param>
		// Token: 0x06002FCD RID: 12237 RVA: 0x0009DE00 File Offset: 0x0009C000
		public Header(string _Name, object _Value, bool _MustUnderstand) : this(_Name, _Value, _MustUnderstand, null)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Runtime.Remoting.Messaging.Header" /> class.</summary>
		/// <param name="_Name">The name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_Value">The object that contains the value of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />. </param>
		/// <param name="_MustUnderstand">Indicates whether the receiving end must understand out-of-band data. </param>
		/// <param name="_HeaderNamespace">The <see cref="T:System.Runtime.Remoting.Messaging.Header" /> XML namespace. </param>
		// Token: 0x06002FCE RID: 12238 RVA: 0x0009DE0C File Offset: 0x0009C00C
		public Header(string _Name, object _Value, bool _MustUnderstand, string _HeaderNamespace)
		{
			this.Name = _Name;
			this.Value = _Value;
			this.MustUnderstand = _MustUnderstand;
			this.HeaderNamespace = _HeaderNamespace;
		}

		/// <summary>Indicates the XML namespace that the current <see cref="T:System.Runtime.Remoting.Messaging.Header" /> belongs to.</summary>
		// Token: 0x04001458 RID: 5208
		public string HeaderNamespace;

		/// <summary>Indicates whether the receiving end must understand the out-of-band data.</summary>
		// Token: 0x04001459 RID: 5209
		public bool MustUnderstand;

		/// <summary>Contains the name of the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x0400145A RID: 5210
		public string Name;

		/// <summary>Contains the value for the <see cref="T:System.Runtime.Remoting.Messaging.Header" />.</summary>
		// Token: 0x0400145B RID: 5211
		public object Value;
	}
}
