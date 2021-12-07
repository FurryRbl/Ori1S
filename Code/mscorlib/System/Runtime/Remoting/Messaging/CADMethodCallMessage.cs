using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting.Channels;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x020004BD RID: 1213
	internal class CADMethodCallMessage : CADMessageBase
	{
		// Token: 0x0600311B RID: 12571 RVA: 0x000A1880 File Offset: 0x0009FA80
		internal CADMethodCallMessage(IMethodCallMessage callMsg)
		{
			this._uri = callMsg.Uri;
			this.MethodHandle = callMsg.MethodBase.MethodHandle;
			this.FullTypeName = callMsg.MethodBase.DeclaringType.AssemblyQualifiedName;
			ArrayList arrayList = null;
			this._propertyCount = CADMessageBase.MarshalProperties(callMsg.Properties, ref arrayList);
			this._args = base.MarshalArguments(callMsg.Args, ref arrayList);
			base.SaveLogicalCallContext(callMsg, ref arrayList);
			if (arrayList != null)
			{
				MemoryStream memoryStream = CADSerializer.SerializeObject(arrayList.ToArray());
				this._serializedArgs = memoryStream.GetBuffer();
			}
		}

		// Token: 0x17000936 RID: 2358
		// (get) Token: 0x0600311C RID: 12572 RVA: 0x000A1918 File Offset: 0x0009FB18
		internal string Uri
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x0600311D RID: 12573 RVA: 0x000A1920 File Offset: 0x0009FB20
		internal static CADMethodCallMessage Create(IMessage callMsg)
		{
			IMethodCallMessage methodCallMessage = callMsg as IMethodCallMessage;
			if (methodCallMessage == null)
			{
				return null;
			}
			return new CADMethodCallMessage(methodCallMessage);
		}

		// Token: 0x0600311E RID: 12574 RVA: 0x000A1944 File Offset: 0x0009FB44
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

		// Token: 0x0600311F RID: 12575 RVA: 0x000A1984 File Offset: 0x0009FB84
		internal object[] GetArgs(ArrayList args)
		{
			return base.UnmarshalArguments(this._args, args);
		}

		// Token: 0x17000937 RID: 2359
		// (get) Token: 0x06003120 RID: 12576 RVA: 0x000A1994 File Offset: 0x0009FB94
		internal int PropertiesCount
		{
			get
			{
				return this._propertyCount;
			}
		}

		// Token: 0x06003121 RID: 12577 RVA: 0x000A199C File Offset: 0x0009FB9C
		private static Type[] GetSignature(MethodBase methodBase, bool load)
		{
			ParameterInfo[] parameters = methodBase.GetParameters();
			Type[] array = new Type[parameters.Length];
			for (int i = 0; i < parameters.Length; i++)
			{
				if (load)
				{
					array[i] = Type.GetType(parameters[i].ParameterType.AssemblyQualifiedName, true);
				}
				else
				{
					array[i] = parameters[i].ParameterType;
				}
			}
			return array;
		}

		// Token: 0x06003122 RID: 12578 RVA: 0x000A19FC File Offset: 0x0009FBFC
		internal MethodBase GetMethod()
		{
			Type type = Type.GetType(this.FullTypeName);
			MethodBase methodBase;
			if (type.IsGenericType || type.IsGenericTypeDefinition)
			{
				methodBase = MethodBase.GetMethodFromHandleNoGenericCheck(this.MethodHandle);
			}
			else
			{
				methodBase = MethodBase.GetMethodFromHandle(this.MethodHandle);
			}
			if (type == methodBase.DeclaringType)
			{
				return methodBase;
			}
			Type[] signature = CADMethodCallMessage.GetSignature(methodBase, true);
			if (methodBase.IsGenericMethod)
			{
				MethodBase[] methods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				Type[] genericArguments = methodBase.GetGenericArguments();
				foreach (MethodBase methodBase2 in methods)
				{
					if (methodBase2.IsGenericMethod && !(methodBase2.Name != methodBase.Name))
					{
						Type[] genericArguments2 = methodBase2.GetGenericArguments();
						if (genericArguments.Length == genericArguments2.Length)
						{
							MethodInfo methodInfo = ((MethodInfo)methodBase2).MakeGenericMethod(genericArguments);
							Type[] signature2 = CADMethodCallMessage.GetSignature(methodInfo, false);
							if (signature2.Length == signature.Length)
							{
								bool flag = false;
								for (int j = signature2.Length - 1; j >= 0; j--)
								{
									if (signature2[j] != signature[j])
									{
										flag = true;
										break;
									}
								}
								if (!flag)
								{
									return methodInfo;
								}
							}
						}
					}
				}
				return methodBase;
			}
			MethodBase method = type.GetMethod(methodBase.Name, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, signature, null);
			if (method == null)
			{
				throw new RemotingException(string.Concat(new object[]
				{
					"Method '",
					methodBase.Name,
					"' not found in type '",
					type,
					"'"
				}));
			}
			return method;
		}

		// Token: 0x040014C8 RID: 5320
		private string _uri;

		// Token: 0x040014C9 RID: 5321
		internal RuntimeMethodHandle MethodHandle;

		// Token: 0x040014CA RID: 5322
		internal string FullTypeName;
	}
}
