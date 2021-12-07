using System;
using System.Collections;
using System.IO;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004BE RID: 1214
	internal class CADMethodReturnMessage : CADMessageBase
	{
		// Token: 0x06003123 RID: 12579 RVA: 0x000A1BA4 File Offset: 0x0009FDA4
		internal CADMethodReturnMessage(IMethodReturnMessage retMsg)
		{
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(retMsg.Properties, ref arrayList);
			this._returnValue = base.MarshalArgument(retMsg.ReturnValue, ref arrayList);
			this._args = base.MarshalArguments(retMsg.Args, ref arrayList);
			if (retMsg.Exception != null)
			{
				if (arrayList == null)
				{
					arrayList = new ArrayList();
				}
				this._exception = new CADArgHolder(arrayList.Count);
				arrayList.Add(retMsg.Exception);
			}
			base.SaveLogicalCallContext(retMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x06003124 RID: 12580 RVA: 0x000A1C50 File Offset: 0x0009FE50
		internal static CADMethodReturnMessage Create(IMessage callMsg)
		{
			IMethodReturnMessage methodReturnMessage = callMsg as IMethodReturnMessage;
			if (methodReturnMessage == null)
			{
				return null;
			}
			return new CADMethodReturnMessage(methodReturnMessage);
		}

		// Token: 0x06003125 RID: 12581 RVA: 0x000A1C74 File Offset: 0x0009FE74
		internal ArrayList GetArguments()
		{
			ArrayList result = null;
			if (this._serializedArgs != null)
			{
				object[] c = (object[])CADSerializer.DeserializeObject(new MemoryStream(this._serializedArgs));
				result = new ArrayList(c);
				this._serializedArgs = null;
			}
			return result;
		}

		// Token: 0x06003126 RID: 12582 RVA: 0x000A1CB4 File Offset: 0x0009FEB4
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x06003127 RID: 12583 RVA: 0x000A1CC4 File Offset: 0x0009FEC4
		internal object GetReturnValue(ArrayList args)
		{
			return base.UnmarshalArgument(this._returnValue, args);
		}

		// Token: 0x06003128 RID: 12584 RVA: 0x000A1CD4 File Offset: 0x0009FED4
		internal Exception GetException(ArrayList args)
		{
			if (this._exception == null)
			{
				return null;
			}
			return (Exception)args[this._exception.index];
		}

		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06003129 RID: 12585 RVA: 0x000A1CFC File Offset: 0x0009FEFC
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x040014CB RID: 5323
		private object _returnValue;

		// Token: 0x040014CC RID: 5324
		private CADArgHolder _exception;
	}
}
