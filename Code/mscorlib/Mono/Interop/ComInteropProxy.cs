using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Remoting;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Proxies;
using System.Threading;

namespace Mono.Interop
{
	// Token: 0x02000090 RID: 144
	internal class ComInteropProxy : RealProxy, IRemotingTypeInfo
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x0001FB54 File Offset: 0x0001DD54
		private ComInteropProxy(Type t) : base(t)
		{
			this.com_object = __ComObject.CreateRCW(t);
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0001FB70 File Offset: 0x0001DD70
		private ComInteropProxy(IntPtr pUnk) : this(pUnk, typeof(__ComObject))
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0001FB84 File Offset: 0x0001DD84
		internal ComInteropProxy(IntPtr pUnk, Type t) : base(t)
		{
			this.com_object = new __ComObject(pUnk);
			this.CacheProxy();
		}

		// Token: 0x06000879 RID: 2169
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void AddProxy(IntPtr pItf, ComInteropProxy proxy);

		// Token: 0x0600087A RID: 2170
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern ComInteropProxy FindProxy(IntPtr pItf);

		// Token: 0x0600087B RID: 2171 RVA: 0x0001FBB4 File Offset: 0x0001DDB4
		private void CacheProxy()
		{
			ComInteropProxy.AddProxy(this.com_object.IUnknown, this);
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0001FBC8 File Offset: 0x0001DDC8
		internal static ComInteropProxy GetProxy(IntPtr pItf, Type t)
		{
			Guid iid_IUnknown = __ComObject.IID_IUnknown;
			IntPtr intPtr;
			int errorCode = Marshal.QueryInterface(pItf, ref iid_IUnknown, out intPtr);
			Marshal.ThrowExceptionForHR(errorCode);
			ComInteropProxy comInteropProxy = ComInteropProxy.FindProxy(intPtr);
			if (comInteropProxy == null)
			{
				Marshal.Release(pItf);
				return new ComInteropProxy(intPtr);
			}
			Marshal.Release(pItf);
			Interlocked.Increment(ref comInteropProxy.ref_count);
			return comInteropProxy;
		}

		// Token: 0x0600087D RID: 2173 RVA: 0x0001FC1C File Offset: 0x0001DE1C
		internal static ComInteropProxy CreateProxy(Type t)
		{
			ComInteropProxy comInteropProxy = new ComInteropProxy(t);
			comInteropProxy.com_object.Initialize(t);
			ComInteropProxy comInteropProxy2 = ComInteropProxy.FindProxy(comInteropProxy.com_object.IUnknown);
			if (comInteropProxy2 == null)
			{
				return comInteropProxy;
			}
			Type type = comInteropProxy2.com_object.GetType();
			if (type != t)
			{
				throw new InvalidCastException(string.Format("Unable to cast object of type '{0}' to type '{1}'.", type, t));
			}
			return comInteropProxy2;
		}

		// Token: 0x0600087E RID: 2174 RVA: 0x0001FC7C File Offset: 0x0001DE7C
		public override IMessage Invoke(IMessage msg)
		{
			Console.WriteLine("Invoke");
			Console.WriteLine(Environment.StackTrace);
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600087F RID: 2175 RVA: 0x0001FC9C File Offset: 0x0001DE9C
		// (set) Token: 0x06000880 RID: 2176 RVA: 0x0001FCA4 File Offset: 0x0001DEA4
		public string TypeName
		{
			get
			{
				return this.type_name;
			}
			set
			{
				this.type_name = value;
			}
		}

		// Token: 0x06000881 RID: 2177 RVA: 0x0001FCB0 File Offset: 0x0001DEB0
		public bool CanCastTo(Type fromType, object o)
		{
			__ComObject _ComObject = o as __ComObject;
			if (_ComObject == null)
			{
				throw new NotSupportedException("Only RCWs are currently supported");
			}
			return (fromType.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic && !(_ComObject.GetInterface(fromType, false) == IntPtr.Zero);
		}

		// Token: 0x040001B1 RID: 433
		private __ComObject com_object;

		// Token: 0x040001B2 RID: 434
		private int ref_count = 1;

		// Token: 0x040001B3 RID: 435
		private string type_name;
	}
}
