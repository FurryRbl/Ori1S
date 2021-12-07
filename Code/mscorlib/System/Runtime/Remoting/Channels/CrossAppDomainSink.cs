using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Remoting.Messaging;
using System.Threading;

namespace System.Runtime.Remoting.Channels
{
	// Token: 0x0200046A RID: 1130
	[MonoTODO("Handle domain unloading?")]
	internal class CrossAppDomainSink : IMessageSink
	{
		// Token: 0x06002EC5 RID: 11973 RVA: 0x0009AD78 File Offset: 0x00098F78
		internal CrossAppDomainSink(int domainID)
		{
			this._domainID = domainID;
		}

		// Token: 0x06002EC7 RID: 11975 RVA: 0x0009ADB0 File Offset: 0x00098FB0
		internal static CrossAppDomainSink GetSink(int domainID)
		{
			object syncRoot = CrossAppDomainSink.s_sinks.SyncRoot;
			CrossAppDomainSink result;
			lock (syncRoot)
			{
				if (CrossAppDomainSink.s_sinks.ContainsKey(domainID))
				{
					result = (CrossAppDomainSink)CrossAppDomainSink.s_sinks[domainID];
				}
				else
				{
					CrossAppDomainSink crossAppDomainSink = new CrossAppDomainSink(domainID);
					CrossAppDomainSink.s_sinks[domainID] = crossAppDomainSink;
					result = crossAppDomainSink;
				}
			}
			return result;
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x06002EC8 RID: 11976 RVA: 0x0009AE48 File Offset: 0x00099048
		internal int TargetDomainId
		{
			get
			{
				return this._domainID;
			}
		}

		// Token: 0x06002EC9 RID: 11977 RVA: 0x0009AE50 File Offset: 0x00099050
		private static CrossAppDomainSink.ProcessMessageRes ProcessMessageInDomain(byte[] arrRequest, CADMethodCallMessage cadMsg)
		{
			CrossAppDomainSink.ProcessMessageRes result = default(CrossAppDomainSink.ProcessMessageRes);
			try
			{
				AppDomain.CurrentDomain.ProcessMessageInDomain(arrRequest, cadMsg, out result.arrResponse, out result.cadMrm);
			}
			catch (Exception e)
			{
				IMessage msg = new MethodResponse(e, new ErrorMessage());
				result.arrResponse = CADSerializer.SerializeMessage(msg).GetBuffer();
			}
			return result;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x0009AEC8 File Offset: 0x000990C8
		public virtual IMessage SyncProcessMessage(IMessage msgRequest)
		{
			IMessage result = null;
			try
			{
				byte[] array = null;
				byte[] array2 = null;
				CADMethodReturnMessage retmsg = null;
				CADMethodCallMessage cadmethodCallMessage = CADMethodCallMessage.Create(msgRequest);
				if (cadmethodCallMessage == null)
				{
					MemoryStream memoryStream = CADSerializer.SerializeMessage(msgRequest);
					array2 = memoryStream.GetBuffer();
				}
				Context currentContext = Thread.CurrentContext;
				try
				{
					CrossAppDomainSink.ProcessMessageRes processMessageRes = (CrossAppDomainSink.ProcessMessageRes)AppDomain.InvokeInDomainByID(this._domainID, CrossAppDomainSink.processMessageMethod, null, new object[]
					{
						array2,
						cadmethodCallMessage
					});
					array = processMessageRes.arrResponse;
					retmsg = processMessageRes.cadMrm;
				}
				finally
				{
					AppDomain.InternalSetContext(currentContext);
				}
				if (array != null)
				{
					MemoryStream mem = new MemoryStream(array);
					result = CADSerializer.DeserializeMessage(mem, msgRequest as IMethodCallMessage);
				}
				else
				{
					result = new MethodResponse(msgRequest as IMethodCallMessage, retmsg);
				}
			}
			catch (Exception e)
			{
				try
				{
					result = new ReturnMessage(e, msgRequest as IMethodCallMessage);
				}
				catch (Exception)
				{
				}
			}
			return result;
		}

		// Token: 0x06002ECB RID: 11979 RVA: 0x0009AFE8 File Offset: 0x000991E8
		public virtual IMessageCtrl AsyncProcessMessage(IMessage reqMsg, IMessageSink replySink)
		{
			AsyncRequest state = new AsyncRequest(reqMsg, replySink);
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.SendAsyncMessage), state);
			return null;
		}

		// Token: 0x06002ECC RID: 11980 RVA: 0x0009B014 File Offset: 0x00099214
		public void SendAsyncMessage(object data)
		{
			AsyncRequest asyncRequest = (AsyncRequest)data;
			IMessage msg = this.SyncProcessMessage(asyncRequest.MsgRequest);
			asyncRequest.ReplySink.SyncProcessMessage(msg);
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x0009B044 File Offset: 0x00099244
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040013EB RID: 5099
		private static Hashtable s_sinks = new Hashtable();

		// Token: 0x040013EC RID: 5100
		private static MethodInfo processMessageMethod = typeof(CrossAppDomainSink).GetMethod("ProcessMessageInDomain", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x040013ED RID: 5101
		private int _domainID;

		// Token: 0x0200046B RID: 1131
		private struct ProcessMessageRes
		{
			// Token: 0x040013EE RID: 5102
			public byte[] arrResponse;

			// Token: 0x040013EF RID: 5103
			public CADMethodReturnMessage cadMrm;
		}
	}
}
