using System;
using System.Reflection;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200048D RID: 1165
	internal class ArgInfo
	{
		// Token: 0x06002F7E RID: 12158 RVA: 0x0009D1DC File Offset: 0x0009B3DC
		public ArgInfo(MethodBase method, ArgInfoType type)
		{
			this._method = method;
			ParameterInfo[] parameters = this._method.GetParameters();
			this._paramMap = new int[parameters.Length];
			this._inoutArgCount = 0;
			if (type == ArgInfoType.In)
			{
				for (int i = 0; i < parameters.Length; i++)
				{
					if (!parameters[i].ParameterType.IsByRef)
					{
						this._paramMap[this._inoutArgCount++] = i;
					}
				}
			}
			else
			{
				for (int j = 0; j < parameters.Length; j++)
				{
					if (parameters[j].ParameterType.IsByRef || parameters[j].IsOut)
					{
						this._paramMap[this._inoutArgCount++] = j;
					}
				}
			}
		}

		// Token: 0x06002F7F RID: 12159 RVA: 0x0009D2B0 File Offset: 0x0009B4B0
		public int GetInOutArgIndex(int inoutArgNum)
		{
			return this._paramMap[inoutArgNum];
		}

		// Token: 0x06002F80 RID: 12160 RVA: 0x0009D2BC File Offset: 0x0009B4BC
		public virtual string GetInOutArgName(int index)
		{
			return this._method.GetParameters()[this._paramMap[index]].Name;
		}

		// Token: 0x06002F81 RID: 12161 RVA: 0x0009D2D8 File Offset: 0x0009B4D8
		public int GetInOutArgCount()
		{
			return this._inoutArgCount;
		}

		// Token: 0x06002F82 RID: 12162 RVA: 0x0009D2E0 File Offset: 0x0009B4E0
		public object[] GetInOutArgs(object[] args)
		{
			object[] array = new object[this._inoutArgCount];
			for (int i = 0; i < this._inoutArgCount; i++)
			{
				array[i] = args[this._paramMap[i]];
			}
			return array;
		}

		// Token: 0x04001435 RID: 5173
		private int[] _paramMap;

		// Token: 0x04001436 RID: 5174
		private int _inoutArgCount;

		// Token: 0x04001437 RID: 5175
		private MethodBase _method;
	}
}
