using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Activation;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000494 RID: 1172
	internal class ConstructionCallDictionary : MethodDictionary
	{
		// Token: 0x06002FBE RID: 12222 RVA: 0x0009DB3C File Offset: 0x0009BD3C
		public ConstructionCallDictionary(IConstructionCallMessage message) : base(message)
		{
			base.MethodKeys = ConstructionCallDictionary.InternalKeys;
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x0009DBC4 File Offset: 0x0009BDC4
		protected override object GetMethodProperty(string key)
		{
			switch (key)
			{
			case "__Activator":
				return ((IConstructionCallMessage)this._message).Activator;
			case "__CallSiteActivationAttributes":
				return ((IConstructionCallMessage)this._message).CallSiteActivationAttributes;
			case "__ActivationType":
				return ((IConstructionCallMessage)this._message).ActivationType;
			case "__ContextProperties":
				return ((IConstructionCallMessage)this._message).ContextProperties;
			case "__ActivationTypeName":
				return ((IConstructionCallMessage)this._message).ActivationTypeName;
			}
			return base.GetMethodProperty(key);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x0009DCBC File Offset: 0x0009BEBC
		protected override void SetMethodProperty(string key, object value)
		{
			if (key != null)
			{
				if (ConstructionCallDictionary.<>f__switch$map29 == null)
				{
					ConstructionCallDictionary.<>f__switch$map29 = new Dictionary<string, int>(5)
					{
						{
							"__Activator",
							0
						},
						{
							"__CallSiteActivationAttributes",
							1
						},
						{
							"__ActivationType",
							1
						},
						{
							"__ContextProperties",
							1
						},
						{
							"__ActivationTypeName",
							1
						}
					};
				}
				int num;
				if (ConstructionCallDictionary.<>f__switch$map29.TryGetValue(key, out num))
				{
					if (num == 0)
					{
						((IConstructionCallMessage)this._message).Activator = (IActivator)value;
						return;
					}
					if (num == 1)
					{
						throw new ArgumentException("key was invalid");
					}
				}
			}
			base.SetMethodProperty(key, value);
		}

		// Token: 0x04001454 RID: 5204
		public static string[] InternalKeys = new string[]
		{
			"__Uri",
			"__MethodName",
			"__TypeName",
			"__MethodSignature",
			"__Args",
			"__CallContext",
			"__CallSiteActivationAttributes",
			"__ActivationType",
			"__ContextProperties",
			"__Activator",
			"__ActivationTypeName"
		};
	}
}
