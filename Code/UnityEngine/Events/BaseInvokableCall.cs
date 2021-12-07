using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000310 RID: 784
	internal abstract class BaseInvokableCall
	{
		// Token: 0x06002729 RID: 10025 RVA: 0x00037100 File Offset: 0x00035300
		protected BaseInvokableCall()
		{
		}

		// Token: 0x0600272A RID: 10026 RVA: 0x00037108 File Offset: 0x00035308
		protected BaseInvokableCall(object target, MethodInfo function)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			if (function == null)
			{
				throw new ArgumentNullException("function");
			}
		}

		// Token: 0x0600272B RID: 10027
		public abstract void Invoke(object[] args);

		// Token: 0x0600272C RID: 10028 RVA: 0x00037140 File Offset: 0x00035340
		protected static void ThrowOnInvalidArg<T>(object arg)
		{
			if (arg != null && !(arg is T))
			{
				throw new ArgumentException(UnityString.Format("Passed argument 'args[0]' is of the wrong type. Type:{0} Expected:{1}", new object[]
				{
					arg.GetType(),
					typeof(T)
				}));
			}
		}

		// Token: 0x0600272D RID: 10029 RVA: 0x00037180 File Offset: 0x00035380
		protected static bool AllowInvoke(Delegate @delegate)
		{
			object target = @delegate.Target;
			if (target == null)
			{
				return true;
			}
			Object @object = target as Object;
			return object.ReferenceEquals(@object, null) || @object != null;
		}

		// Token: 0x0600272E RID: 10030
		public abstract bool Find(object targetObj, MethodInfo method);
	}
}
