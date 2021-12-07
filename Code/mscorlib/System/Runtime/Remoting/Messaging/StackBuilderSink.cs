using System;
using System.Reflection;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004B9 RID: 1209
	internal class StackBuilderSink : IMessageSink
	{
		// Token: 0x06003107 RID: 12551 RVA: 0x000A0F6C File Offset: 0x0009F16C
		public StackBuilderSink(MarshalByRefObject obj, bool forceInternalExecute)
		{
			this._target = obj;
			if (!forceInternalExecute && RemotingServices.IsTransparentProxy(obj))
			{
				this._rp = RemotingServices.GetRealProxy(obj);
			}
		}

		// Token: 0x06003108 RID: 12552 RVA: 0x000A0FA4 File Offset: 0x0009F1A4
		public IMessage SyncProcessMessage(IMessage msg)
		{
			this.CheckParameters(msg);
			if (this._rp != null)
			{
				return this._rp.Invoke(msg);
			}
			return RemotingServices.InternalExecuteMessage(this._target, (IMethodCallMessage)msg);
		}

		// Token: 0x06003109 RID: 12553 RVA: 0x000A0FE4 File Offset: 0x0009F1E4
		public IMessageCtrl AsyncProcessMessage(IMessage msg, IMessageSink replySink)
		{
			object[] state = new object[]
			{
				msg,
				replySink
			};
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ExecuteAsyncMessage), state);
			return null;
		}

		// Token: 0x0600310A RID: 12554 RVA: 0x000A1014 File Offset: 0x0009F214
		private void ExecuteAsyncMessage(object ob)
		{
			object[] array = (object[])ob;
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)array[0];
			IMessageSink messageSink = (IMessageSink)array[1];
			this.CheckParameters(methodCallMessage);
			IMessage msg;
			if (this._rp != null)
			{
				msg = this._rp.Invoke(methodCallMessage);
			}
			else
			{
				msg = RemotingServices.InternalExecuteMessage(this._target, methodCallMessage);
			}
			messageSink.SyncProcessMessage(msg);
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x0600310B RID: 12555 RVA: 0x000A1074 File Offset: 0x0009F274
		public IMessageSink NextSink
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0600310C RID: 12556 RVA: 0x000A1078 File Offset: 0x0009F278
		private void CheckParameters(IMessage msg)
		{
			IMethodCallMessage methodCallMessage = (IMethodCallMessage)msg;
			ParameterInfo[] parameters = methodCallMessage.MethodBase.GetParameters();
			int num = 0;
			foreach (ParameterInfo parameterInfo in parameters)
			{
				object arg = methodCallMessage.GetArg(num++);
				Type type = parameterInfo.ParameterType;
				if (type.IsByRef)
				{
					type = type.GetElementType();
				}
				if (arg != null && !type.IsInstanceOfType(arg))
				{
					throw new RemotingException(string.Concat(new object[]
					{
						"Cannot cast argument ",
						parameterInfo.Position,
						" of type '",
						arg.GetType().AssemblyQualifiedName,
						"' to type '",
						type.AssemblyQualifiedName,
						"'"
					}));
				}
			}
		}

		// Token: 0x040014BF RID: 5311
		private MarshalByRefObject _target;

		// Token: 0x040014C0 RID: 5312
		private RealProxy _rp;
	}
}
