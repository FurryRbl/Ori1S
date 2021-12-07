using System;
using System.Collections;
using System.Runtime.Remoting.Contexts;

namespace System.Runtime.Remoting.Activation
{
	// Token: 0x02000442 RID: 1090
	internal class RemoteActivationAttribute : Attribute, IContextAttribute
	{
		// Token: 0x06002E08 RID: 11784 RVA: 0x00099498 File Offset: 0x00097698
		public RemoteActivationAttribute()
		{
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x000994A0 File Offset: 0x000976A0
		public RemoteActivationAttribute(IList contextProperties)
		{
			this._contextProperties = contextProperties;
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x000994B0 File Offset: 0x000976B0
		public bool IsContextOK(Context ctx, IConstructionCallMessage ctor)
		{
			return false;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x000994B4 File Offset: 0x000976B4
		public void GetPropertiesForNewContext(IConstructionCallMessage ctor)
		{
			if (this._contextProperties != null)
			{
				foreach (object value in this._contextProperties)
				{
					ctor.ContextProperties.Add(value);
				}
			}
		}

		// Token: 0x040013C3 RID: 5059
		private IList _contextProperties;
	}
}
