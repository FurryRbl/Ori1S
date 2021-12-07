using System;
using System.Collections;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace System.Diagnostics
{
	/// <summary>Represents a stack trace, which is an ordered collection of one or more stack frames.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E6 RID: 486
	[MonoTODO("Serialized objects are not compatible with .NET")]
	[ComVisible(true)]
	[Serializable]
	public class StackTrace
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame.</summary>
		// Token: 0x06001894 RID: 6292 RVA: 0x0005DC08 File Offset: 0x0005BE08
		public StackTrace()
		{
			this.init_frames(0, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, optionally capturing source information.</summary>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		// Token: 0x06001895 RID: 6293 RVA: 0x0005DC18 File Offset: 0x0005BE18
		public StackTrace(bool fNeedFileInfo)
		{
			this.init_frames(0, fNeedFileInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x06001896 RID: 6294 RVA: 0x0005DC28 File Offset: 0x0005BE28
		public StackTrace(int skipFrames)
		{
			this.init_frames(skipFrames, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class from the caller's frame, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x06001897 RID: 6295 RVA: 0x0005DC38 File Offset: 0x0005BE38
		public StackTrace(int skipFrames, bool fNeedFileInfo)
		{
			this.init_frames(skipFrames, fNeedFileInfo);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		// Token: 0x06001898 RID: 6296 RVA: 0x0005DC48 File Offset: 0x0005BE48
		public StackTrace(Exception e) : this(e, 0, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class, using the provided exception object and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		// Token: 0x06001899 RID: 6297 RVA: 0x0005DC54 File Offset: 0x0005BE54
		public StackTrace(Exception e, bool fNeedFileInfo) : this(e, 0, fNeedFileInfo)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object and skipping the specified number of frames.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x0600189A RID: 6298 RVA: 0x0005DC60 File Offset: 0x0005BE60
		public StackTrace(Exception e, int skipFrames) : this(e, skipFrames, false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class using the provided exception object, skipping the specified number of frames and optionally capturing source information.</summary>
		/// <param name="e">The exception object from which to construct the stack trace. </param>
		/// <param name="skipFrames">The number of frames up the stack from which to start the trace. </param>
		/// <param name="fNeedFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.ArgumentNullException">The parameter <paramref name="e" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="skipFrames" /> parameter is negative. </exception>
		// Token: 0x0600189B RID: 6299 RVA: 0x0005DC6C File Offset: 0x0005BE6C
		public StackTrace(Exception e, int skipFrames, bool fNeedFileInfo) : this(e, skipFrames, fNeedFileInfo, false)
		{
		}

		// Token: 0x0600189C RID: 6300 RVA: 0x0005DC78 File Offset: 0x0005BE78
		internal StackTrace(Exception e, int skipFrames, bool fNeedFileInfo, bool returnNativeFrames)
		{
			if (e == null)
			{
				throw new ArgumentNullException("e");
			}
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			this.frames = StackTrace.get_trace(e, skipFrames, fNeedFileInfo);
			if (!returnNativeFrames)
			{
				bool flag = false;
				for (int i = 0; i < this.frames.Length; i++)
				{
					if (this.frames[i].GetMethod() == null)
					{
						flag = true;
					}
				}
				if (flag)
				{
					ArrayList arrayList = new ArrayList();
					for (int j = 0; j < this.frames.Length; j++)
					{
						if (this.frames[j].GetMethod() != null)
						{
							arrayList.Add(this.frames[j]);
						}
					}
					this.frames = (StackFrame[])arrayList.ToArray(typeof(StackFrame));
				}
			}
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class that contains a single frame.</summary>
		/// <param name="frame">The frame that the <see cref="T:System.Diagnostics.StackTrace" /> object should contain. </param>
		// Token: 0x0600189D RID: 6301 RVA: 0x0005DD58 File Offset: 0x0005BF58
		public StackTrace(StackFrame frame)
		{
			this.frames = new StackFrame[1];
			this.frames[0] = frame;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.StackTrace" /> class for a specific thread, optionally capturing source information.</summary>
		/// <param name="targetThread">The thread whose stack trace is requested. </param>
		/// <param name="needFileInfo">true to capture the file name, line number, and column number; otherwise, false. </param>
		/// <exception cref="T:System.Threading.ThreadStateException">The thread <paramref name="targetThread" /> is not suspended. </exception>
		// Token: 0x0600189E RID: 6302 RVA: 0x0005DD78 File Offset: 0x0005BF78
		[MonoTODO("Not possible to create StackTraces from other threads")]
		public StackTrace(Thread targetThread, bool needFileInfo)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600189F RID: 6303 RVA: 0x0005DD88 File Offset: 0x0005BF88
		private void init_frames(int skipFrames, bool fNeedFileInfo)
		{
			if (skipFrames < 0)
			{
				throw new ArgumentOutOfRangeException("< 0", "skipFrames");
			}
			ArrayList arrayList = new ArrayList();
			skipFrames += 2;
			StackFrame stackFrame;
			while ((stackFrame = new StackFrame(skipFrames, fNeedFileInfo)) != null && stackFrame.GetMethod() != null)
			{
				arrayList.Add(stackFrame);
				skipFrames++;
			}
			this.debug_info = fNeedFileInfo;
			this.frames = (StackFrame[])arrayList.ToArray(typeof(StackFrame));
		}

		// Token: 0x060018A0 RID: 6304
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern StackFrame[] get_trace(Exception e, int skipFrames, bool fNeedFileInfo);

		/// <summary>Gets the number of frames in the stack trace.</summary>
		/// <returns>The number of frames in the stack trace. </returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x060018A1 RID: 6305 RVA: 0x0005DE04 File Offset: 0x0005C004
		public virtual int FrameCount
		{
			get
			{
				return (this.frames != null) ? this.frames.Length : 0;
			}
		}

		/// <summary>Gets the specified stack frame.</summary>
		/// <returns>The specified stack frame.</returns>
		/// <param name="index">The index of the stack frame requested. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060018A2 RID: 6306 RVA: 0x0005DE20 File Offset: 0x0005C020
		public virtual StackFrame GetFrame(int index)
		{
			if (index < 0 || index >= this.FrameCount)
			{
				return null;
			}
			return this.frames[index];
		}

		/// <summary>Returns a copy of all stack frames in the current stack trace.</summary>
		/// <returns>An array of type <see cref="T:System.Diagnostics.StackFrame" /> representing the function calls in the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060018A3 RID: 6307 RVA: 0x0005DE40 File Offset: 0x0005C040
		[ComVisible(false)]
		public virtual StackFrame[] GetFrames()
		{
			return this.frames;
		}

		/// <summary>Builds a readable representation of the stack trace.</summary>
		/// <returns>A readable representation of the stack trace.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060018A4 RID: 6308 RVA: 0x0005DE48 File Offset: 0x0005C048
		public override string ToString()
		{
			string value = string.Format("{0}   {1} ", Environment.NewLine, Locale.GetText("at"));
			string text = Locale.GetText("<unknown method>");
			string text2 = Locale.GetText(" in {0}:line {1}");
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < this.FrameCount; i++)
			{
				StackFrame frame = this.GetFrame(i);
				if (i > 0)
				{
					stringBuilder.Append(value);
				}
				else
				{
					stringBuilder.AppendFormat("   {0} ", Locale.GetText("at"));
				}
				MethodBase method = frame.GetMethod();
				if (method != null)
				{
					stringBuilder.AppendFormat("{0}.{1}", method.DeclaringType.FullName, method.Name);
					stringBuilder.Append("(");
					ParameterInfo[] parameters = method.GetParameters();
					for (int j = 0; j < parameters.Length; j++)
					{
						if (j > 0)
						{
							stringBuilder.Append(", ");
						}
						Type type = parameters[j].ParameterType;
						bool isByRef = type.IsByRef;
						if (isByRef)
						{
							type = type.GetElementType();
						}
						if (type.IsClass && type.Namespace != string.Empty)
						{
							stringBuilder.Append(type.Namespace);
							stringBuilder.Append(".");
						}
						stringBuilder.Append(type.Name);
						if (isByRef)
						{
							stringBuilder.Append(" ByRef");
						}
						stringBuilder.AppendFormat(" {0}", parameters[j].Name);
					}
					stringBuilder.Append(")");
				}
				else
				{
					stringBuilder.Append(text);
				}
				if (this.debug_info)
				{
					string secureFileName = frame.GetSecureFileName();
					if (secureFileName != "<filename unknown>")
					{
						stringBuilder.AppendFormat(text2, secureFileName, frame.GetFileLineNumber());
					}
				}
			}
			return stringBuilder.ToString();
		}

		/// <summary>Defines the default for the number of methods to omit from the stack trace. This field is constant.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x040008F9 RID: 2297
		public const int METHODS_TO_SKIP = 0;

		// Token: 0x040008FA RID: 2298
		private StackFrame[] frames;

		// Token: 0x040008FB RID: 2299
		private bool debug_info;
	}
}
