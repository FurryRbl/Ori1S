using System;
using System.Reflection;
using System.Runtime.Remoting;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000126 RID: 294
	[Serializable]
	internal class DelegateSerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x00044DFC File Offset: 0x00042FFC
		private DelegateSerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			DelegateSerializationHolder.DelegateEntry delegateEntry = (DelegateSerializationHolder.DelegateEntry)info.GetValue("Delegate", typeof(DelegateSerializationHolder.DelegateEntry));
			int num = 0;
			DelegateSerializationHolder.DelegateEntry delegateEntry2 = delegateEntry;
			while (delegateEntry2 != null)
			{
				delegateEntry2 = delegateEntry2.delegateEntry;
				num++;
			}
			if (num == 1)
			{
				this._delegate = delegateEntry.DeserializeDelegate(info);
			}
			else
			{
				Delegate[] array = new Delegate[num];
				delegateEntry2 = delegateEntry;
				for (int i = 0; i < num; i++)
				{
					array[i] = delegateEntry2.DeserializeDelegate(info);
					delegateEntry2 = delegateEntry2.delegateEntry;
				}
				this._delegate = Delegate.Combine(array);
			}
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x00044E9C File Offset: 0x0004309C
		public static void GetDelegateData(Delegate instance, SerializationInfo info, StreamingContext ctx)
		{
			Delegate[] invocationList = instance.GetInvocationList();
			DelegateSerializationHolder.DelegateEntry delegateEntry = null;
			for (int i = 0; i < invocationList.Length; i++)
			{
				Delegate @delegate = invocationList[i];
				string text = (@delegate.Target == null) ? null : ("target" + i);
				DelegateSerializationHolder.DelegateEntry delegateEntry2 = new DelegateSerializationHolder.DelegateEntry(@delegate, text);
				if (delegateEntry == null)
				{
					info.AddValue("Delegate", delegateEntry2);
				}
				else
				{
					delegateEntry.delegateEntry = delegateEntry2;
				}
				delegateEntry = delegateEntry2;
				if (@delegate.Target != null)
				{
					info.AddValue(text, @delegate.Target);
				}
			}
			info.SetType(typeof(DelegateSerializationHolder));
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x00044F44 File Offset: 0x00043144
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x00044F4C File Offset: 0x0004314C
		public object GetRealObject(StreamingContext context)
		{
			return this._delegate;
		}

		// Token: 0x040004C5 RID: 1221
		private Delegate _delegate;

		// Token: 0x02000127 RID: 295
		[Serializable]
		private class DelegateEntry
		{
			// Token: 0x060010D4 RID: 4308 RVA: 0x00044F54 File Offset: 0x00043154
			public DelegateEntry(Delegate del, string targetLabel)
			{
				this.type = del.GetType().FullName;
				this.assembly = del.GetType().Assembly.FullName;
				this.target = targetLabel;
				this.targetTypeAssembly = del.Method.DeclaringType.Assembly.FullName;
				this.targetTypeName = del.Method.DeclaringType.FullName;
				this.methodName = del.Method.Name;
			}

			// Token: 0x060010D5 RID: 4309 RVA: 0x00044FD8 File Offset: 0x000431D8
			public Delegate DeserializeDelegate(SerializationInfo info)
			{
				object obj = null;
				if (this.target != null)
				{
					obj = info.GetValue(this.target.ToString(), typeof(object));
				}
				Assembly assembly = Assembly.Load(this.assembly);
				Type type = assembly.GetType(this.type);
				Delegate result;
				if (obj != null)
				{
					if (RemotingServices.IsTransparentProxy(obj))
					{
						Assembly assembly2 = Assembly.Load(this.targetTypeAssembly);
						Type type2 = assembly2.GetType(this.targetTypeName);
						if (!type2.IsInstanceOfType(obj))
						{
							throw new RemotingException("Unexpected proxy type.");
						}
					}
					result = Delegate.CreateDelegate(type, obj, this.methodName);
				}
				else
				{
					Assembly assembly3 = Assembly.Load(this.targetTypeAssembly);
					Type type3 = assembly3.GetType(this.targetTypeName);
					result = Delegate.CreateDelegate(type, type3, this.methodName);
				}
				return result;
			}

			// Token: 0x040004C6 RID: 1222
			private string type;

			// Token: 0x040004C7 RID: 1223
			private string assembly;

			// Token: 0x040004C8 RID: 1224
			public object target;

			// Token: 0x040004C9 RID: 1225
			private string targetTypeAssembly;

			// Token: 0x040004CA RID: 1226
			private string targetTypeName;

			// Token: 0x040004CB RID: 1227
			private string methodName;

			// Token: 0x040004CC RID: 1228
			public DelegateSerializationHolder.DelegateEntry delegateEntry;
		}
	}
}
