using System;
using System.Collections;
using System.Diagnostics;
using System.Reflection;

namespace System.Runtime.InteropServices
{
	/// <summary>Enables customization of managed objects that extend from unmanaged objects during creation.</summary>
	// Token: 0x0200038D RID: 909
	[ComVisible(true)]
	public sealed class ExtensibleClassFactory
	{
		// Token: 0x06002A50 RID: 10832 RVA: 0x000925F4 File Offset: 0x000907F4
		private ExtensibleClassFactory()
		{
		}

		// Token: 0x06002A52 RID: 10834 RVA: 0x00092608 File Offset: 0x00090808
		internal static ObjectCreationDelegate GetObjectCreationCallback(Type t)
		{
			return ExtensibleClassFactory.hashtable[t] as ObjectCreationDelegate;
		}

		/// <summary>Registers a delegate that is called when an instance of a managed type, that extends from an unmanaged type, needs to allocate the aggregated unmanaged object.</summary>
		/// <param name="callback">A delegate that is called in place of CoCreateInstance. </param>
		// Token: 0x06002A53 RID: 10835 RVA: 0x0009261C File Offset: 0x0009081C
		public static void RegisterObjectCreationCallback(ObjectCreationDelegate callback)
		{
			int i = 1;
			StackTrace stackTrace = new StackTrace(false);
			while (i < stackTrace.FrameCount)
			{
				StackFrame frame = stackTrace.GetFrame(i);
				MethodBase method = frame.GetMethod();
				if (method.MemberType == MemberTypes.Constructor && method.IsStatic)
				{
					ExtensibleClassFactory.hashtable.Add(method.DeclaringType, callback);
					return;
				}
				i++;
			}
			throw new InvalidOperationException("RegisterObjectCreationCallback must be called from .cctor of class derived from ComImport type.");
		}

		// Token: 0x0400110A RID: 4362
		private static Hashtable hashtable = new Hashtable();
	}
}
