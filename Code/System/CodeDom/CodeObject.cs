using System;
using System.Collections;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

namespace System.CodeDom
{
	/// <summary>Provides a common base class for most Code Document Object Model (CodeDOM) objects.</summary>
	// Token: 0x02000054 RID: 84
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Serializable]
	public class CodeObject
	{
		/// <summary>Gets the user-definable data for the current object.</summary>
		/// <returns>An <see cref="T:System.Collections.IDictionary" /> containing user data for the current object.</returns>
		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x0000C730 File Offset: 0x0000A930
		public IDictionary UserData
		{
			get
			{
				if (this.userData == null)
				{
					this.userData = new System.Collections.Specialized.ListDictionary();
				}
				return this.userData;
			}
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0000C750 File Offset: 0x0000A950
		internal virtual void Accept(ICodeDomVisitor visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040000DE RID: 222
		private IDictionary userData;
	}
}
