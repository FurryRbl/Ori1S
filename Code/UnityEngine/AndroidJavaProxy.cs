using System;
using System.Reflection;

namespace UnityEngine
{
	// Token: 0x0200026E RID: 622
	public class AndroidJavaProxy
	{
		// Token: 0x060024CE RID: 9422 RVA: 0x000301B4 File Offset: 0x0002E3B4
		public AndroidJavaProxy(string javaInterface) : this(new AndroidJavaClass(javaInterface))
		{
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000301C4 File Offset: 0x0002E3C4
		public AndroidJavaProxy(AndroidJavaClass javaInterface)
		{
			this.javaInterface = javaInterface;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000301D4 File Offset: 0x0002E3D4
		public virtual AndroidJavaObject Invoke(string methodName, object[] args)
		{
			Exception ex = null;
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic;
			Type[] array = new Type[args.Length];
			for (int i = 0; i < args.Length; i++)
			{
				array[i] = ((args[i] != null) ? args[i].GetType() : typeof(AndroidJavaObject));
			}
			try
			{
				MethodInfo method = base.GetType().GetMethod(methodName, bindingAttr, null, array, null);
				if (method != null)
				{
					return _AndroidJNIHelper.Box(method.Invoke(this, args));
				}
			}
			catch (TargetInvocationException ex2)
			{
				ex = ex2.InnerException;
			}
			catch (Exception ex3)
			{
				ex = ex3;
			}
			string[] array2 = new string[args.Length];
			for (int j = 0; j < array.Length; j++)
			{
				array2[j] = array[j].ToString();
			}
			if (ex != null)
			{
				throw new TargetInvocationException(string.Concat(new object[]
				{
					base.GetType(),
					".",
					methodName,
					"(",
					string.Join(",", array2),
					")"
				}), ex);
			}
			throw new Exception(string.Concat(new object[]
			{
				"No such proxy method: ",
				base.GetType(),
				".",
				methodName,
				"(",
				string.Join(",", array2),
				")"
			}));
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x00030370 File Offset: 0x0002E570
		public virtual AndroidJavaObject Invoke(string methodName, AndroidJavaObject[] javaArgs)
		{
			object[] array = new object[javaArgs.Length];
			for (int i = 0; i < javaArgs.Length; i++)
			{
				array[i] = _AndroidJNIHelper.Unbox(javaArgs[i]);
			}
			return this.Invoke(methodName, array);
		}

		// Token: 0x040009CB RID: 2507
		public readonly AndroidJavaClass javaInterface;
	}
}
