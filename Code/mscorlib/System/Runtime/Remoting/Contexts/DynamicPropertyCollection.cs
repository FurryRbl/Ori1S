using System;
using System.Collections;
using System.Runtime.Remoting.Messaging;

namespace System.Runtime.Remoting.Contexts
{
	// Token: 0x0200046F RID: 1135
	internal class DynamicPropertyCollection
	{
		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x0009BBC4 File Offset: 0x00099DC4
		public bool HasProperties
		{
			get
			{
				return this._properties.Count > 0;
			}
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0009BBD4 File Offset: 0x00099DD4
		public bool RegisterDynamicProperty(IDynamicProperty prop)
		{
			bool result;
			lock (this)
			{
				if (this.FindProperty(prop.Name) != -1)
				{
					throw new InvalidOperationException("Another property by this name already exists");
				}
				ArrayList arrayList = new ArrayList(this._properties);
				DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = new DynamicPropertyCollection.DynamicPropertyReg();
				dynamicPropertyReg.Property = prop;
				IContributeDynamicSink contributeDynamicSink = prop as IContributeDynamicSink;
				if (contributeDynamicSink != null)
				{
					dynamicPropertyReg.Sink = contributeDynamicSink.GetDynamicSink();
				}
				arrayList.Add(dynamicPropertyReg);
				this._properties = arrayList;
				result = true;
			}
			return result;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0009BC7C File Offset: 0x00099E7C
		public bool UnregisterDynamicProperty(string name)
		{
			bool result;
			lock (this)
			{
				int num = this.FindProperty(name);
				if (num == -1)
				{
					throw new RemotingException("A property with the name " + name + " was not found");
				}
				this._properties.RemoveAt(num);
				result = true;
			}
			return result;
		}

		// Token: 0x06002EF9 RID: 12025 RVA: 0x0009BCF4 File Offset: 0x00099EF4
		public void NotifyMessage(bool start, IMessage msg, bool client_site, bool async)
		{
			ArrayList properties = this._properties;
			if (start)
			{
				foreach (object obj in properties)
				{
					DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg = (DynamicPropertyCollection.DynamicPropertyReg)obj;
					if (dynamicPropertyReg.Sink != null)
					{
						dynamicPropertyReg.Sink.ProcessMessageStart(msg, client_site, async);
					}
				}
			}
			else
			{
				foreach (object obj2 in properties)
				{
					DynamicPropertyCollection.DynamicPropertyReg dynamicPropertyReg2 = (DynamicPropertyCollection.DynamicPropertyReg)obj2;
					if (dynamicPropertyReg2.Sink != null)
					{
						dynamicPropertyReg2.Sink.ProcessMessageFinish(msg, client_site, async);
					}
				}
			}
		}

		// Token: 0x06002EFA RID: 12026 RVA: 0x0009BDFC File Offset: 0x00099FFC
		private int FindProperty(string name)
		{
			for (int i = 0; i < this._properties.Count; i++)
			{
				if (((DynamicPropertyCollection.DynamicPropertyReg)this._properties[i]).Property.Name == name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x04001400 RID: 5120
		private ArrayList _properties = new ArrayList();

		// Token: 0x02000470 RID: 1136
		private class DynamicPropertyReg
		{
			// Token: 0x04001401 RID: 5121
			public IDynamicProperty Property;

			// Token: 0x04001402 RID: 5122
			public IDynamicMessageSink Sink;
		}
	}
}
