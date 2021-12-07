using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace System
{
	// Token: 0x02000193 RID: 403
	[Serializable]
	internal class UnitySerializationHolder : ISerializable, IObjectReference
	{
		// Token: 0x06001473 RID: 5235 RVA: 0x00052248 File Offset: 0x00050448
		private UnitySerializationHolder(SerializationInfo info, StreamingContext ctx)
		{
			this._data = info.GetString("Data");
			this._unityType = (UnitySerializationHolder.UnityType)info.GetInt32("UnityType");
			this._assemblyName = info.GetString("AssemblyName");
		}

		// Token: 0x06001474 RID: 5236 RVA: 0x00052290 File Offset: 0x00050490
		public static void GetTypeData(Type instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", instance.FullName);
			info.AddValue("UnityType", 4);
			info.AddValue("AssemblyName", instance.Assembly.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001475 RID: 5237 RVA: 0x000522E0 File Offset: 0x000504E0
		public static void GetDBNullData(DBNull instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", null);
			info.AddValue("UnityType", 2);
			info.AddValue("AssemblyName", instance.GetType().Assembly.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001476 RID: 5238 RVA: 0x00052330 File Offset: 0x00050530
		public static void GetAssemblyData(Assembly instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", instance.FullName);
			info.AddValue("UnityType", 6);
			info.AddValue("AssemblyName", instance.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001477 RID: 5239 RVA: 0x0005237C File Offset: 0x0005057C
		public static void GetModuleData(Module instance, SerializationInfo info, StreamingContext ctx)
		{
			info.AddValue("Data", instance.ScopeName);
			info.AddValue("UnityType", 5);
			info.AddValue("AssemblyName", instance.Assembly.FullName);
			info.SetType(typeof(UnitySerializationHolder));
		}

		// Token: 0x06001478 RID: 5240 RVA: 0x000523CC File Offset: 0x000505CC
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06001479 RID: 5241 RVA: 0x000523D4 File Offset: 0x000505D4
		public virtual object GetRealObject(StreamingContext context)
		{
			switch (this._unityType)
			{
			case UnitySerializationHolder.UnityType.DBNull:
				return DBNull.Value;
			case UnitySerializationHolder.UnityType.Type:
			{
				Assembly assembly = Assembly.Load(this._assemblyName);
				return assembly.GetType(this._data);
			}
			case UnitySerializationHolder.UnityType.Module:
			{
				Assembly assembly2 = Assembly.Load(this._assemblyName);
				return assembly2.GetModule(this._data);
			}
			case UnitySerializationHolder.UnityType.Assembly:
				return Assembly.Load(this._data);
			}
			throw new NotSupportedException(Locale.GetText("UnitySerializationHolder does not support this type."));
		}

		// Token: 0x04000803 RID: 2051
		private string _data;

		// Token: 0x04000804 RID: 2052
		private UnitySerializationHolder.UnityType _unityType;

		// Token: 0x04000805 RID: 2053
		private string _assemblyName;

		// Token: 0x02000194 RID: 404
		private enum UnityType : byte
		{
			// Token: 0x04000807 RID: 2055
			DBNull = 2,
			// Token: 0x04000808 RID: 2056
			Type = 4,
			// Token: 0x04000809 RID: 2057
			Module,
			// Token: 0x0400080A RID: 2058
			Assembly
		}
	}
}
