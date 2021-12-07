using System;

namespace UnityEngine
{
	// Token: 0x020000D2 RID: 210
	public class AndroidJavaClass : AndroidJavaObject
	{
		// Token: 0x06000D4B RID: 3403 RVA: 0x00011C00 File Offset: 0x0000FE00
		public AndroidJavaClass(string className)
		{
			this._AndroidJavaClass(className);
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x00011C10 File Offset: 0x0000FE10
		internal AndroidJavaClass(IntPtr jclass)
		{
			if (jclass == IntPtr.Zero)
			{
				throw new Exception("JNI: Init'd AndroidJavaClass with null ptr!");
			}
			this.m_jclass = AndroidJNI.NewGlobalRef(jclass);
			this.m_jobject = IntPtr.Zero;
		}

		// Token: 0x06000D4D RID: 3405 RVA: 0x00011C58 File Offset: 0x0000FE58
		private void _AndroidJavaClass(string className)
		{
			base.DebugPrint("Creating AndroidJavaClass from " + className);
			using (AndroidJavaObject androidJavaObject = AndroidJavaObject.FindClass(className))
			{
				this.m_jclass = AndroidJNI.NewGlobalRef(androidJavaObject.GetRawObject());
				this.m_jobject = IntPtr.Zero;
			}
		}
	}
}
