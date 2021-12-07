using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System
{
	/// <summary>Represents the standard input, output, and error streams for console applications. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200010F RID: 271
	public static class Console
	{
		// Token: 0x06000DD9 RID: 3545 RVA: 0x0003BFE8 File Offset: 0x0003A1E8
		static Console()
		{
			if (Environment.IsRunningOnWindows)
			{
				try
				{
					Console.inputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetInputCodePage());
					Console.outputEncoding = Encoding.GetEncoding(Console.WindowsConsole.GetOutputCodePage());
				}
				catch
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
				}
			}
			else
			{
				int num = 0;
				Encoding.InternalCodePage(ref num);
				if (num != -1 && ((num & 268435455) == 3 || (num & 268435456) != 0))
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.UTF8Unmarked);
				}
				else
				{
					Console.inputEncoding = (Console.outputEncoding = Encoding.Default);
				}
			}
			Console.SetEncodings(Console.inputEncoding, Console.outputEncoding);
		}

		/// <summary>Occurs when the <see cref="F:System.ConsoleModifiers.Control" /> modifier key (CTRL) and <see cref="F:System.ConsoleKey.C" /> console key (C) are pressed simultaneously (CTRL+C).</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000DDA RID: 3546 RVA: 0x0003C0C8 File Offset: 0x0003A2C8
		// (remove) Token: 0x06000DDB RID: 3547 RVA: 0x0003C0FC File Offset: 0x0003A2FC
		public static event ConsoleCancelEventHandler CancelKeyPress
		{
			add
			{
				if (!ConsoleDriver.Initialized)
				{
					ConsoleDriver.Init();
				}
				Console.cancel_event = (ConsoleCancelEventHandler)Delegate.Combine(Console.cancel_event, value);
			}
			remove
			{
				if (!ConsoleDriver.Initialized)
				{
					ConsoleDriver.Init();
				}
				Console.cancel_event = (ConsoleCancelEventHandler)Delegate.Remove(Console.cancel_event, value);
			}
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x0003C130 File Offset: 0x0003A330
		private static void SetEncodings(Encoding inputEncoding, Encoding outputEncoding)
		{
			Console.stderr = new UnexceptionalStreamWriter(Console.OpenStandardError(0), outputEncoding);
			((StreamWriter)Console.stderr).AutoFlush = true;
			Console.stderr = TextWriter.Synchronized(Console.stderr, true);
			if (!Environment.IsRunningOnWindows && ConsoleDriver.IsConsole)
			{
				Console.stdout = TextWriter.Synchronized(new CStreamWriter(Console.OpenStandardOutput(0), outputEncoding)
				{
					AutoFlush = true
				}, true);
				Console.stdin = new CStreamReader(Console.OpenStandardInput(0), inputEncoding);
			}
			else
			{
				Console.stdout = new UnexceptionalStreamWriter(Console.OpenStandardOutput(0), outputEncoding);
				((StreamWriter)Console.stdout).AutoFlush = true;
				Console.stdout = TextWriter.Synchronized(Console.stdout, true);
				Console.stdin = new UnexceptionalStreamReader(Console.OpenStandardInput(0), inputEncoding);
				Console.stdin = TextReader.Synchronized(Console.stdin);
			}
			GC.SuppressFinalize(Console.stdout);
			GC.SuppressFinalize(Console.stderr);
			GC.SuppressFinalize(Console.stdin);
		}

		/// <summary>Gets the standard error output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard error output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0003C228 File Offset: 0x0003A428
		public static TextWriter Error
		{
			get
			{
				return Console.stderr;
			}
		}

		/// <summary>Gets the standard output stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextWriter" /> that represents the standard output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000DDE RID: 3550 RVA: 0x0003C230 File Offset: 0x0003A430
		public static TextWriter Out
		{
			get
			{
				return Console.stdout;
			}
		}

		/// <summary>Gets the standard input stream.</summary>
		/// <returns>A <see cref="T:System.IO.TextReader" /> that represents the standard input stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000202 RID: 514
		// (get) Token: 0x06000DDF RID: 3551 RVA: 0x0003C238 File Offset: 0x0003A438
		public static TextReader In
		{
			get
			{
				return Console.stdin;
			}
		}

		/// <summary>Acquires the standard error stream.</summary>
		/// <returns>The standard error stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003C240 File Offset: 0x0003A440
		public static Stream OpenStandardError()
		{
			return Console.OpenStandardError(0);
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003C248 File Offset: 0x0003A448
		private static Stream Open(IntPtr handle, FileAccess access, int bufferSize)
		{
			Stream result;
			try
			{
				result = new FileStream(handle, access, false, bufferSize, false, bufferSize == 0);
			}
			catch (IOException)
			{
				result = new NullStream();
			}
			return result;
		}

		/// <summary>Acquires the standard error stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard error stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003C29C File Offset: 0x0003A49C
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static Stream OpenStandardError(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleError, FileAccess.Write, bufferSize);
		}

		/// <summary>Acquires the standard input stream.</summary>
		/// <returns>The standard input stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003C2AC File Offset: 0x0003A4AC
		public static Stream OpenStandardInput()
		{
			return Console.OpenStandardInput(0);
		}

		/// <summary>Acquires the standard input stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard input stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE4 RID: 3556 RVA: 0x0003C2B4 File Offset: 0x0003A4B4
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static Stream OpenStandardInput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleInput, FileAccess.Read, bufferSize);
		}

		/// <summary>Acquires the standard output stream.</summary>
		/// <returns>The standard output stream.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE5 RID: 3557 RVA: 0x0003C2C4 File Offset: 0x0003A4C4
		public static Stream OpenStandardOutput()
		{
			return Console.OpenStandardOutput(0);
		}

		/// <summary>Acquires the standard output stream, which is set to a specified buffer size.</summary>
		/// <returns>The standard output stream.</returns>
		/// <param name="bufferSize">The internal stream buffer size. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="bufferSize" /> is less than or equal to zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DE6 RID: 3558 RVA: 0x0003C2CC File Offset: 0x0003A4CC
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static Stream OpenStandardOutput(int bufferSize)
		{
			return Console.Open(MonoIO.ConsoleOutput, FileAccess.Write, bufferSize);
		}

		/// <summary>Sets the <see cref="P:System.Console.Error" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newError">A <see cref="T:System.IO.TextWriter" /> stream that is the new standard error output. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newError" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000DE7 RID: 3559 RVA: 0x0003C2DC File Offset: 0x0003A4DC
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static void SetError(TextWriter newError)
		{
			if (newError == null)
			{
				throw new ArgumentNullException("newError");
			}
			Console.stderr = newError;
		}

		/// <summary>Sets the <see cref="P:System.Console.In" /> property to the specified <see cref="T:System.IO.TextReader" /> object.</summary>
		/// <param name="newIn">A <see cref="T:System.IO.TextReader" /> stream that is the new standard input. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newIn" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000DE8 RID: 3560 RVA: 0x0003C2F8 File Offset: 0x0003A4F8
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static void SetIn(TextReader newIn)
		{
			if (newIn == null)
			{
				throw new ArgumentNullException("newIn");
			}
			Console.stdin = newIn;
		}

		/// <summary>Sets the <see cref="P:System.Console.Out" /> property to the specified <see cref="T:System.IO.TextWriter" /> object.</summary>
		/// <param name="newOut">A <see cref="T:System.IO.TextWriter" /> stream that is the new standard output. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="newOut" /> is null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have the required permission. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x06000DE9 RID: 3561 RVA: 0x0003C314 File Offset: 0x0003A514
		[PermissionSet(SecurityAction.Demand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\n               version=\"1\">\n   <IPermission class=\"System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\n                version=\"1\"\n                Flags=\"UnmanagedCode\"/>\n</PermissionSet>\n")]
		public static void SetOut(TextWriter newOut)
		{
			if (newOut == null)
			{
				throw new ArgumentNullException("newOut");
			}
			Console.stdout = newOut;
		}

		/// <summary>Writes the text representation of the specified Boolean value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEA RID: 3562 RVA: 0x0003C330 File Offset: 0x0003A530
		public static void Write(bool value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the specified Unicode character value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEB RID: 3563 RVA: 0x0003C340 File Offset: 0x0003A540
		public static void Write(char value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the specified array of Unicode characters to the standard output stream.</summary>
		/// <param name="buffer">A Unicode character array. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEC RID: 3564 RVA: 0x0003C350 File Offset: 0x0003A550
		public static void Write(char[] buffer)
		{
			Console.stdout.Write(buffer);
		}

		/// <summary>Writes the text representation of the specified <see cref="T:System.Decimal" /> value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DED RID: 3565 RVA: 0x0003C360 File Offset: 0x0003A560
		public static void Write(decimal value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified double-precision floating-point value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEE RID: 3566 RVA: 0x0003C370 File Offset: 0x0003A570
		public static void Write(double value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit signed integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DEF RID: 3567 RVA: 0x0003C380 File Offset: 0x0003A580
		public static void Write(int value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit signed integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF0 RID: 3568 RVA: 0x0003C390 File Offset: 0x0003A590
		public static void Write(long value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified object to the standard output stream.</summary>
		/// <param name="value">The value to write, or null. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF1 RID: 3569 RVA: 0x0003C3A0 File Offset: 0x0003A5A0
		public static void Write(object value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified single-precision floating-point value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF2 RID: 3570 RVA: 0x0003C3B0 File Offset: 0x0003A5B0
		public static void Write(float value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the specified string value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF3 RID: 3571 RVA: 0x0003C3C0 File Offset: 0x0003A5C0
		public static void Write(string value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit unsigned integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF4 RID: 3572 RVA: 0x0003C3D0 File Offset: 0x0003A5D0
		[CLSCompliant(false)]
		public static void Write(uint value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit unsigned integer value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF5 RID: 3573 RVA: 0x0003C3E0 File Offset: 0x0003A5E0
		[CLSCompliant(false)]
		public static void Write(ulong value)
		{
			Console.stdout.Write(value);
		}

		/// <summary>Writes the text representation of the specified object to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF6 RID: 3574 RVA: 0x0003C3F0 File Offset: 0x0003A5F0
		public static void Write(string format, object arg0)
		{
			Console.stdout.Write(format, arg0);
		}

		/// <summary>Writes the text representation of the specified array of objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg">An array of objects to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF7 RID: 3575 RVA: 0x0003C400 File Offset: 0x0003A600
		public static void Write(string format, params object[] arg)
		{
			Console.stdout.Write(format, arg);
		}

		/// <summary>Writes the specified subarray of Unicode characters to the standard output stream.</summary>
		/// <param name="buffer">An array of Unicode characters. </param>
		/// <param name="index">The starting position in <paramref name="buffer" />. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus <paramref name="count" /> specify a position that is not within <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003C410 File Offset: 0x0003A610
		public static void Write(char[] buffer, int index, int count)
		{
			Console.stdout.Write(buffer, index, count);
		}

		/// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DF9 RID: 3577 RVA: 0x0003C420 File Offset: 0x0003A620
		public static void Write(string format, object arg0, object arg1)
		{
			Console.stdout.Write(format, arg0, arg1);
		}

		/// <summary>Writes the text representation of the specified objects to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFA RID: 3578 RVA: 0x0003C430 File Offset: 0x0003A630
		public static void Write(string format, object arg0, object arg1, object arg2)
		{
			Console.stdout.Write(format, arg0, arg1, arg2);
		}

		/// <summary>Writes the text representation of the specified objects and variable-length parameter list to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <param name="arg3">The fourth object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFB RID: 3579 RVA: 0x0003C440 File Offset: 0x0003A640
		[CLSCompliant(false)]
		public static void Write(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			object[] array = new object[remainingCount + 4];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				array[i + 4] = TypedReference.ToObject(nextArg);
			}
			Console.stdout.Write(string.Format(format, array));
		}

		/// <summary>Writes the current line terminator to the standard output stream.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFC RID: 3580 RVA: 0x0003C4B0 File Offset: 0x0003A6B0
		public static void WriteLine()
		{
			Console.stdout.WriteLine();
		}

		/// <summary>Writes the text representation of the specified Boolean value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFD RID: 3581 RVA: 0x0003C4BC File Offset: 0x0003A6BC
		public static void WriteLine(bool value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified Unicode character, followed by the current line terminator, value to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFE RID: 3582 RVA: 0x0003C4CC File Offset: 0x0003A6CC
		public static void WriteLine(char value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified array of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">A Unicode character array. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000DFF RID: 3583 RVA: 0x0003C4DC File Offset: 0x0003A6DC
		public static void WriteLine(char[] buffer)
		{
			Console.stdout.WriteLine(buffer);
		}

		/// <summary>Writes the text representation of the specified <see cref="T:System.Decimal" /> value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E00 RID: 3584 RVA: 0x0003C4EC File Offset: 0x0003A6EC
		public static void WriteLine(decimal value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified double-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E01 RID: 3585 RVA: 0x0003C4FC File Offset: 0x0003A6FC
		public static void WriteLine(double value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E02 RID: 3586 RVA: 0x0003C50C File Offset: 0x0003A70C
		public static void WriteLine(int value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit signed integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E03 RID: 3587 RVA: 0x0003C51C File Offset: 0x0003A71C
		public static void WriteLine(long value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E04 RID: 3588 RVA: 0x0003C52C File Offset: 0x0003A72C
		public static void WriteLine(object value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified single-precision floating-point value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E05 RID: 3589 RVA: 0x0003C53C File Offset: 0x0003A73C
		public static void WriteLine(float value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the specified string value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E06 RID: 3590 RVA: 0x0003C54C File Offset: 0x0003A74C
		public static void WriteLine(string value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 32-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E07 RID: 3591 RVA: 0x0003C55C File Offset: 0x0003A75C
		[CLSCompliant(false)]
		public static void WriteLine(uint value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified 64-bit unsigned integer value, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="value">The value to write. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E08 RID: 3592 RVA: 0x0003C56C File Offset: 0x0003A76C
		[CLSCompliant(false)]
		public static void WriteLine(ulong value)
		{
			Console.stdout.WriteLine(value);
		}

		/// <summary>Writes the text representation of the specified object, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">An object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E09 RID: 3593 RVA: 0x0003C57C File Offset: 0x0003A77C
		public static void WriteLine(string format, object arg0)
		{
			Console.stdout.WriteLine(format, arg0);
		}

		/// <summary>Writes the text representation of the specified array of objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg">An array of objects to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> or <paramref name="arg" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0A RID: 3594 RVA: 0x0003C58C File Offset: 0x0003A78C
		public static void WriteLine(string format, params object[] arg)
		{
			Console.stdout.WriteLine(format, arg);
		}

		/// <summary>Writes the specified subarray of Unicode characters, followed by the current line terminator, to the standard output stream.</summary>
		/// <param name="buffer">An array of Unicode characters. </param>
		/// <param name="index">The starting position in <paramref name="buffer" />. </param>
		/// <param name="count">The number of characters to write. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="index" /> plus <paramref name="count" /> specify a position that is not within <paramref name="buffer" />. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0B RID: 3595 RVA: 0x0003C59C File Offset: 0x0003A79C
		public static void WriteLine(char[] buffer, int index, int count)
		{
			Console.stdout.WriteLine(buffer, index, count);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0C RID: 3596 RVA: 0x0003C5AC File Offset: 0x0003A7AC
		public static void WriteLine(string format, object arg0, object arg1)
		{
			Console.stdout.WriteLine(format, arg0, arg1);
		}

		/// <summary>Writes the text representation of the specified objects, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0D RID: 3597 RVA: 0x0003C5BC File Offset: 0x0003A7BC
		public static void WriteLine(string format, object arg0, object arg1, object arg2)
		{
			Console.stdout.WriteLine(format, arg0, arg1, arg2);
		}

		/// <summary>Writes the text representation of the specified objects and variable-length parameter list, followed by the current line terminator, to the standard output stream using the specified format information.</summary>
		/// <param name="format">A composite format string (see Remarks). </param>
		/// <param name="arg0">The first object to write using <paramref name="format" />. </param>
		/// <param name="arg1">The second object to write using <paramref name="format" />. </param>
		/// <param name="arg2">The third object to write using <paramref name="format" />. </param>
		/// <param name="arg3">The fourth object to write using <paramref name="format" />. </param>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="format" /> is null. </exception>
		/// <exception cref="T:System.FormatException">The format specification in <paramref name="format" /> is invalid. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0E RID: 3598 RVA: 0x0003C5CC File Offset: 0x0003A7CC
		[CLSCompliant(false)]
		public static void WriteLine(string format, object arg0, object arg1, object arg2, object arg3, __arglist)
		{
			ArgIterator argIterator = new ArgIterator(__arglist);
			int remainingCount = argIterator.GetRemainingCount();
			object[] array = new object[remainingCount + 4];
			array[0] = arg0;
			array[1] = arg1;
			array[2] = arg2;
			array[3] = arg3;
			for (int i = 0; i < remainingCount; i++)
			{
				TypedReference nextArg = argIterator.GetNextArg();
				array[i + 4] = TypedReference.ToObject(nextArg);
			}
			Console.stdout.WriteLine(string.Format(format, array));
		}

		/// <summary>Reads the next character from the standard input stream.</summary>
		/// <returns>The next character from the input stream, or negative one (-1) if there are currently no more characters to be read.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E0F RID: 3599 RVA: 0x0003C63C File Offset: 0x0003A83C
		public static int Read()
		{
			if (Console.stdin is CStreamReader && ConsoleDriver.IsConsole)
			{
				return ConsoleDriver.Read();
			}
			return Console.stdin.Read();
		}

		/// <summary>Reads the next line of characters from the standard input stream.</summary>
		/// <returns>The next line of characters from the input stream, or null if no more lines are available.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.OutOfMemoryException">There is insufficient memory to allocate a buffer for the returned string. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of characters in the next line of characters is greater than <see cref="F:System.Int32.MaxValue" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E10 RID: 3600 RVA: 0x0003C668 File Offset: 0x0003A868
		public static string ReadLine()
		{
			if (Console.stdin is CStreamReader && ConsoleDriver.IsConsole)
			{
				return ConsoleDriver.ReadLine();
			}
			return Console.stdin.ReadLine();
		}

		/// <summary>Gets or sets the encoding the console uses to read input. </summary>
		/// <returns>The encoding used to read console input.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This property's set operation is not supported on Windows 98, Windows 98 Second Edition, or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000E11 RID: 3601 RVA: 0x0003C694 File Offset: 0x0003A894
		// (set) Token: 0x06000E12 RID: 3602 RVA: 0x0003C69C File Offset: 0x0003A89C
		public static Encoding InputEncoding
		{
			get
			{
				return Console.inputEncoding;
			}
			set
			{
				Console.inputEncoding = value;
				Console.SetEncodings(Console.inputEncoding, Console.outputEncoding);
			}
		}

		/// <summary>Gets or sets the encoding the console uses to write output. </summary>
		/// <returns>The encoding used to write console output.</returns>
		/// <exception cref="T:System.ArgumentNullException">The property value in a set operation is null.</exception>
		/// <exception cref="T:System.PlatformNotSupportedException">This property's set operation is not supported on Windows 98, Windows 98 Second Edition, or Windows Millennium Edition.</exception>
		/// <exception cref="T:System.IO.IOException">An error occurred during the execution of this operation.</exception>
		/// <exception cref="T:System.Security.SecurityException">Your application does not have permission to perform this operation.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000E13 RID: 3603 RVA: 0x0003C6B4 File Offset: 0x0003A8B4
		// (set) Token: 0x06000E14 RID: 3604 RVA: 0x0003C6BC File Offset: 0x0003A8BC
		public static Encoding OutputEncoding
		{
			get
			{
				return Console.outputEncoding;
			}
			set
			{
				Console.outputEncoding = value;
				Console.SetEncodings(Console.inputEncoding, Console.outputEncoding);
			}
		}

		/// <summary>Gets or sets the background color of the console.</summary>
		/// <returns>A <see cref="T:System.ConsoleColor" /> that specifies the background color of the console; that is, the color that appears behind each character. The default is black.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000E15 RID: 3605 RVA: 0x0003C6D4 File Offset: 0x0003A8D4
		// (set) Token: 0x06000E16 RID: 3606 RVA: 0x0003C6DC File Offset: 0x0003A8DC
		public static ConsoleColor BackgroundColor
		{
			get
			{
				return ConsoleDriver.BackgroundColor;
			}
			set
			{
				ConsoleDriver.BackgroundColor = value;
			}
		}

		/// <summary>Gets or sets the height of the buffer area.</summary>
		/// <returns>The current height, in rows, of the buffer area.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.-or- The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or- The value in a set operation is less than <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000E17 RID: 3607 RVA: 0x0003C6E4 File Offset: 0x0003A8E4
		// (set) Token: 0x06000E18 RID: 3608 RVA: 0x0003C6EC File Offset: 0x0003A8EC
		public static int BufferHeight
		{
			get
			{
				return ConsoleDriver.BufferHeight;
			}
			[MonoLimitation("Implemented only on Windows")]
			set
			{
				ConsoleDriver.BufferHeight = value;
			}
		}

		/// <summary>Gets or sets the width of the buffer area.</summary>
		/// <returns>The current width, in columns, of the buffer area.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than or equal to zero.-or- The value in a set operation is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or- The value in a set operation is less than <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x0003C6F4 File Offset: 0x0003A8F4
		// (set) Token: 0x06000E1A RID: 3610 RVA: 0x0003C6FC File Offset: 0x0003A8FC
		public static int BufferWidth
		{
			get
			{
				return ConsoleDriver.BufferWidth;
			}
			[MonoLimitation("Implemented only on Windows")]
			set
			{
				ConsoleDriver.BufferWidth = value;
			}
		}

		/// <summary>Gets a value indicating whether the CAPS LOCK keyboard toggle is turned on or turned off.</summary>
		/// <returns>true if CAPS LOCK is turned on; false if CAPS LOCK is turned off.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x0003C704 File Offset: 0x0003A904
		[MonoLimitation("Implemented only on Windows")]
		public static bool CapsLock
		{
			get
			{
				return ConsoleDriver.CapsLock;
			}
		}

		/// <summary>Gets or sets the column position of the cursor within the buffer area.</summary>
		/// <returns>The current position, in columns, of the cursor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x0003C70C File Offset: 0x0003A90C
		// (set) Token: 0x06000E1D RID: 3613 RVA: 0x0003C714 File Offset: 0x0003A914
		public static int CursorLeft
		{
			get
			{
				return ConsoleDriver.CursorLeft;
			}
			set
			{
				ConsoleDriver.CursorLeft = value;
			}
		}

		/// <summary>Gets or sets the row position of the cursor within the buffer area.</summary>
		/// <returns>The current position, in rows, of the cursor.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value in a set operation is less than zero.-or- The value in a set operation is greater than or equal to <see cref="P:System.Console.BufferHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000E1E RID: 3614 RVA: 0x0003C71C File Offset: 0x0003A91C
		// (set) Token: 0x06000E1F RID: 3615 RVA: 0x0003C724 File Offset: 0x0003A924
		public static int CursorTop
		{
			get
			{
				return ConsoleDriver.CursorTop;
			}
			set
			{
				ConsoleDriver.CursorTop = value;
			}
		}

		/// <summary>Gets or sets the height of the cursor within a character cell.</summary>
		/// <returns>The size of the cursor expressed as a percentage of the height of a character cell. The property value ranges from 1 to 100.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value specified in a set operation is less than 1 or greater than 100. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000E20 RID: 3616 RVA: 0x0003C72C File Offset: 0x0003A92C
		// (set) Token: 0x06000E21 RID: 3617 RVA: 0x0003C734 File Offset: 0x0003A934
		public static int CursorSize
		{
			get
			{
				return ConsoleDriver.CursorSize;
			}
			set
			{
				ConsoleDriver.CursorSize = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the cursor is visible.</summary>
		/// <returns>true if the cursor is visible; otherwise, false.</returns>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000E22 RID: 3618 RVA: 0x0003C73C File Offset: 0x0003A93C
		// (set) Token: 0x06000E23 RID: 3619 RVA: 0x0003C744 File Offset: 0x0003A944
		public static bool CursorVisible
		{
			get
			{
				return ConsoleDriver.CursorVisible;
			}
			set
			{
				ConsoleDriver.CursorVisible = value;
			}
		}

		/// <summary>Gets or sets the foreground color of the console.</summary>
		/// <returns>A <see cref="T:System.ConsoleColor" /> that specifies the foreground color of the console; that is, the color of each character that is displayed. The default is gray.</returns>
		/// <exception cref="T:System.ArgumentException">The color specified in a set operation is not a valid member of <see cref="T:System.ConsoleColor" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x1700020D RID: 525
		// (get) Token: 0x06000E24 RID: 3620 RVA: 0x0003C74C File Offset: 0x0003A94C
		// (set) Token: 0x06000E25 RID: 3621 RVA: 0x0003C754 File Offset: 0x0003A954
		public static ConsoleColor ForegroundColor
		{
			get
			{
				return ConsoleDriver.ForegroundColor;
			}
			set
			{
				ConsoleDriver.ForegroundColor = value;
			}
		}

		/// <summary>Gets a value indicating whether a key press is available in the input stream.</summary>
		/// <returns>true if a key press is available; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <exception cref="T:System.InvalidOperationException">Standard input is redirected to a file instead of the keyboard. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020E RID: 526
		// (get) Token: 0x06000E26 RID: 3622 RVA: 0x0003C75C File Offset: 0x0003A95C
		public static bool KeyAvailable
		{
			get
			{
				return ConsoleDriver.KeyAvailable;
			}
		}

		/// <summary>Gets the largest possible number of console window rows, based on the current font and screen resolution.</summary>
		/// <returns>The height of the largest possible console window measured in rows.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x1700020F RID: 527
		// (get) Token: 0x06000E27 RID: 3623 RVA: 0x0003C764 File Offset: 0x0003A964
		public static int LargestWindowHeight
		{
			get
			{
				return ConsoleDriver.LargestWindowHeight;
			}
		}

		/// <summary>Gets the largest possible number of console window columns, based on the current font and screen resolution.</summary>
		/// <returns>The width of the largest possible console window measured in columns.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000210 RID: 528
		// (get) Token: 0x06000E28 RID: 3624 RVA: 0x0003C76C File Offset: 0x0003A96C
		public static int LargestWindowWidth
		{
			get
			{
				return ConsoleDriver.LargestWindowWidth;
			}
		}

		/// <summary>Gets a value indicating whether the NUM LOCK keyboard toggle is turned on or turned off.</summary>
		/// <returns>true if NUM LOCK is turned on; false if NUM LOCK is turned off.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x06000E29 RID: 3625 RVA: 0x0003C774 File Offset: 0x0003A974
		[MonoLimitation("Only works on windows")]
		public static bool NumberLock
		{
			get
			{
				return ConsoleDriver.NumberLock;
			}
		}

		/// <summary>Gets or sets the title to display in the console title bar.</summary>
		/// <returns>The string to be displayed in the title bar of the console. The maximum length of the title string is 24500 characters.</returns>
		/// <exception cref="T:System.InvalidOperationException">In a get operation, the retrieved title is longer than 24500 characters. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the specified title is longer than 24500 characters. </exception>
		/// <exception cref="T:System.ArgumentNullException">In a set operation, the specified title is null. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000E2A RID: 3626 RVA: 0x0003C77C File Offset: 0x0003A97C
		// (set) Token: 0x06000E2B RID: 3627 RVA: 0x0003C784 File Offset: 0x0003A984
		public static string Title
		{
			get
			{
				return ConsoleDriver.Title;
			}
			set
			{
				ConsoleDriver.Title = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether the combination of the <see cref="F:System.ConsoleModifiers.Control" /> modifier key and <see cref="F:System.ConsoleKey.C" /> console key (CTRL+C) is treated as ordinary input or as an interruption that is handled by the operating system.</summary>
		/// <returns>true if CTRL+C is treated as ordinary input; otherwise, false.</returns>
		/// <exception cref="T:System.IO.IOException">Unable to get or set the input mode of the console input buffer. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000E2C RID: 3628 RVA: 0x0003C78C File Offset: 0x0003A98C
		// (set) Token: 0x06000E2D RID: 3629 RVA: 0x0003C794 File Offset: 0x0003A994
		public static bool TreatControlCAsInput
		{
			get
			{
				return ConsoleDriver.TreatControlCAsInput;
			}
			set
			{
				ConsoleDriver.TreatControlCAsInput = value;
			}
		}

		/// <summary>Gets or sets the height of the console window area.</summary>
		/// <returns>The height of the console window measured in rows.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is less than or equal to 0.-or-The value of the <see cref="P:System.Console.WindowHeight" /> property plus the value of the <see cref="P:System.Console.WindowTop" /> property is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or-The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000E2E RID: 3630 RVA: 0x0003C79C File Offset: 0x0003A99C
		// (set) Token: 0x06000E2F RID: 3631 RVA: 0x0003C7A4 File Offset: 0x0003A9A4
		[MonoLimitation("Only works on windows")]
		public static int WindowHeight
		{
			get
			{
				return ConsoleDriver.WindowHeight;
			}
			set
			{
				ConsoleDriver.WindowHeight = value;
			}
		}

		/// <summary>Gets or sets the leftmost position of the console window area relative to the screen buffer.</summary>
		/// <returns>The leftmost console window position measured in columns.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the value to be assigned is less than zero.-or-As a result of the assignment, <see cref="P:System.Console.WindowLeft" /> plus <see cref="P:System.Console.WindowWidth" /> would exceed <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x0003C7AC File Offset: 0x0003A9AC
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x0003C7B4 File Offset: 0x0003A9B4
		[MonoLimitation("Only works on windows")]
		public static int WindowLeft
		{
			get
			{
				return ConsoleDriver.WindowLeft;
			}
			set
			{
				ConsoleDriver.WindowLeft = value;
			}
		}

		/// <summary>Gets or sets the top position of the console window area relative to the screen buffer.</summary>
		/// <returns>The uppermost console window position measured in rows.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">In a set operation, the value to be assigned is less than zero.-or-As a result of the assignment, <see cref="P:System.Console.WindowTop" /> plus <see cref="P:System.Console.WindowHeight" /> would exceed <see cref="P:System.Console.BufferHeight" />.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000E32 RID: 3634 RVA: 0x0003C7BC File Offset: 0x0003A9BC
		// (set) Token: 0x06000E33 RID: 3635 RVA: 0x0003C7C4 File Offset: 0x0003A9C4
		[MonoLimitation("Only works on windows")]
		public static int WindowTop
		{
			get
			{
				return ConsoleDriver.WindowTop;
			}
			set
			{
				ConsoleDriver.WindowTop = value;
			}
		}

		/// <summary>Gets or sets the width of the console window.</summary>
		/// <returns>The width of the console window measured in columns.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is less than or equal to 0.-or-The value of the <see cref="P:System.Console.WindowHeight" /> property plus the value of the <see cref="P:System.Console.WindowTop" /> property is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or-The value of the <see cref="P:System.Console.WindowWidth" /> property or the value of the <see cref="P:System.Console.WindowHeight" /> property is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.IO.IOException">Error reading or writing information.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x0003C7CC File Offset: 0x0003A9CC
		// (set) Token: 0x06000E35 RID: 3637 RVA: 0x0003C7D4 File Offset: 0x0003A9D4
		[MonoLimitation("Only works on windows")]
		public static int WindowWidth
		{
			get
			{
				return ConsoleDriver.WindowWidth;
			}
			set
			{
				ConsoleDriver.WindowWidth = value;
			}
		}

		/// <summary>Plays the sound of a beep through the console speaker.</summary>
		/// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to a user interface.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E36 RID: 3638 RVA: 0x0003C7DC File Offset: 0x0003A9DC
		public static void Beep()
		{
			Console.Beep(1000, 500);
		}

		/// <summary>Plays the sound of a beep of a specified frequency and duration through the console speaker.</summary>
		/// <param name="frequency">The frequency of the beep, ranging from 37 to 32767 hertz.</param>
		/// <param name="duration">The duration of the beep measured in milliseconds.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="frequency" /> is less than 37 or more than 32767 hertz.-or-<paramref name="duration" /> is less than or equal to zero.</exception>
		/// <exception cref="T:System.Security.HostProtectionException">This method was executed on a server, such as SQL Server, that does not permit access to the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E37 RID: 3639 RVA: 0x0003C7F0 File Offset: 0x0003A9F0
		public static void Beep(int frequency, int duration)
		{
			if (frequency < 37 || frequency > 32767)
			{
				throw new ArgumentOutOfRangeException("frequency");
			}
			if (duration <= 0)
			{
				throw new ArgumentOutOfRangeException("duration");
			}
			ConsoleDriver.Beep(frequency, duration);
		}

		/// <summary>Clears the console buffer and corresponding console window of display information.</summary>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E38 RID: 3640 RVA: 0x0003C82C File Offset: 0x0003AA2C
		public static void Clear()
		{
			ConsoleDriver.Clear();
		}

		/// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
		/// <param name="sourceLeft">The leftmost column of the source area. </param>
		/// <param name="sourceTop">The topmost row of the source area. </param>
		/// <param name="sourceWidth">The number of columns in the source area. </param>
		/// <param name="sourceHeight">The number of rows in the source area. </param>
		/// <param name="targetLeft">The leftmost column of the destination area. </param>
		/// <param name="targetTop">The topmost row of the destination area. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.-or- <paramref name="sourceLeft" /> or <paramref name="targetLeft" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="sourceTop" /> or <paramref name="targetTop" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000E39 RID: 3641 RVA: 0x0003C834 File Offset: 0x0003AA34
		[MonoLimitation("Implemented only on Windows")]
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop);
		}

		/// <summary>Copies a specified source area of the screen buffer to a specified destination area.</summary>
		/// <param name="sourceLeft">The leftmost column of the source area. </param>
		/// <param name="sourceTop">The topmost row of the source area. </param>
		/// <param name="sourceWidth">The number of columns in the source area. </param>
		/// <param name="sourceHeight">The number of rows in the source area. </param>
		/// <param name="targetLeft">The leftmost column of the destination area. </param>
		/// <param name="targetTop">The topmost row of the destination area. </param>
		/// <param name="sourceChar">The character used to fill the source area. </param>
		/// <param name="sourceForeColor">The foreground color used to fill the source area. </param>
		/// <param name="sourceBackColor">The background color used to fill the source area. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">One or more of the parameters is less than zero.-or- <paramref name="sourceLeft" /> or <paramref name="targetLeft" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="sourceTop" /> or <paramref name="targetTop" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceTop" /> + <paramref name="sourceHeight" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />.-or- <paramref name="sourceLeft" /> + <paramref name="sourceWidth" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />. </exception>
		/// <exception cref="T:System.ArgumentException">One or both of the color parameters is not a member of the <see cref="T:System.ConsoleColor" /> enumeration. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000E3A RID: 3642 RVA: 0x0003C844 File Offset: 0x0003AA44
		[MonoLimitation("Implemented only on Windows")]
		public static void MoveBufferArea(int sourceLeft, int sourceTop, int sourceWidth, int sourceHeight, int targetLeft, int targetTop, char sourceChar, ConsoleColor sourceForeColor, ConsoleColor sourceBackColor)
		{
			ConsoleDriver.MoveBufferArea(sourceLeft, sourceTop, sourceWidth, sourceHeight, targetLeft, targetTop, sourceChar, sourceForeColor, sourceBackColor);
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is displayed in the console window.</summary>
		/// <returns>A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more SHIFT, ALT, or CTRL modifier keys was pressed simultaneously with the console key.</returns>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E3B RID: 3643 RVA: 0x0003C864 File Offset: 0x0003AA64
		public static ConsoleKeyInfo ReadKey()
		{
			return Console.ReadKey(false);
		}

		/// <summary>Obtains the next character or function key pressed by the user. The pressed key is optionally displayed in the console window.</summary>
		/// <returns>A <see cref="T:System.ConsoleKeyInfo" /> object that describes the <see cref="T:System.ConsoleKey" /> constant and Unicode character, if any, that correspond to the pressed console key. The <see cref="T:System.ConsoleKeyInfo" /> object also describes, in a bitwise combination of <see cref="T:System.ConsoleModifiers" /> values, whether one or more SHIFT, ALT, or CTRL modifier keys was pressed simultaneously with the console key.</returns>
		/// <param name="intercept">Determines whether to display the pressed key in the console window. true to not display the pressed key; otherwise, false. </param>
		/// <exception cref="T:System.InvalidOperationException">The <see cref="P:System.Console.In" /> property is redirected from some stream other than the console.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06000E3C RID: 3644 RVA: 0x0003C86C File Offset: 0x0003AA6C
		public static ConsoleKeyInfo ReadKey(bool intercept)
		{
			return ConsoleDriver.ReadKey(intercept);
		}

		/// <summary>Sets the foreground and background console colors to their defaults.</summary>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred. </exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000E3D RID: 3645 RVA: 0x0003C874 File Offset: 0x0003AA74
		public static void ResetColor()
		{
			ConsoleDriver.ResetColor();
		}

		/// <summary>Sets the height and width of the screen buffer area to the specified values.</summary>
		/// <param name="width">The width of the buffer area measured in columns. </param>
		/// <param name="height">The height of the buffer area measured in rows. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="height" /> or <paramref name="width" /> is less than or equal to zero.-or- <paramref name="height" /> or <paramref name="width" /> is greater than or equal to <see cref="F:System.Int16.MaxValue" />.-or- <paramref name="width" /> is less than <see cref="P:System.Console.WindowLeft" /> + <see cref="P:System.Console.WindowWidth" />.-or- <paramref name="height" /> is less than <see cref="P:System.Console.WindowTop" /> + <see cref="P:System.Console.WindowHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000E3E RID: 3646 RVA: 0x0003C87C File Offset: 0x0003AA7C
		[MonoLimitation("Only works on windows")]
		public static void SetBufferSize(int width, int height)
		{
			ConsoleDriver.SetBufferSize(width, height);
		}

		/// <summary>Sets the position of the cursor.</summary>
		/// <param name="left">The column position of the cursor. </param>
		/// <param name="top">The row position of the cursor. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="left" /> or <paramref name="top" /> is less than zero.-or- <paramref name="left" /> is greater than or equal to <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="top" /> is greater than or equal to <see cref="P:System.Console.BufferHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000E3F RID: 3647 RVA: 0x0003C888 File Offset: 0x0003AA88
		public static void SetCursorPosition(int left, int top)
		{
			ConsoleDriver.SetCursorPosition(left, top);
		}

		/// <summary>Sets the position of the console window relative to the screen buffer.</summary>
		/// <param name="left">The column position of the upper left  corner of the console window. </param>
		/// <param name="top">The row position of the upper left corner of the console window. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="left" /> or <paramref name="top" /> is less than zero.-or- <paramref name="left" /> + <see cref="P:System.Console.WindowWidth" /> is greater than <see cref="P:System.Console.BufferWidth" />.-or- <paramref name="top" /> + <see cref="P:System.Console.WindowHeight" /> is greater than <see cref="P:System.Console.BufferHeight" />. </exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000E40 RID: 3648 RVA: 0x0003C894 File Offset: 0x0003AA94
		public static void SetWindowPosition(int left, int top)
		{
			ConsoleDriver.SetWindowPosition(left, top);
		}

		/// <summary>Sets the height and width of the console window to the specified values.</summary>
		/// <param name="width">The width of the console window measured in columns. </param>
		/// <param name="height">The height of the console window measured in rows. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="width" /> or <paramref name="height" /> is less than or equal to zero.-or- <paramref name="width" /> plus <see cref="P:System.Console.WindowLeft" /> or <paramref name="height" /> plus <see cref="P:System.Console.WindowTop" /> is greater than or equal to <see cref="F:System.Int16.MaxValue" />. -or-<paramref name="width" /> or <paramref name="height" /> is greater than the largest possible window width or height for the current screen resolution and console font.</exception>
		/// <exception cref="T:System.Security.SecurityException">The user does not have permission to perform this action. </exception>
		/// <exception cref="T:System.IO.IOException">An I/O error occurred.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.UIPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Window="SafeTopLevelWindows" />
		/// </PermissionSet>
		// Token: 0x06000E41 RID: 3649 RVA: 0x0003C8A0 File Offset: 0x0003AAA0
		public static void SetWindowSize(int width, int height)
		{
			ConsoleDriver.SetWindowSize(width, height);
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x0003C8AC File Offset: 0x0003AAAC
		internal static void DoConsoleCancelEvent()
		{
			bool flag = true;
			if (Console.cancel_event != null)
			{
				ConsoleCancelEventArgs consoleCancelEventArgs = new ConsoleCancelEventArgs(ConsoleSpecialKey.ControlC);
				Delegate[] invocationList = Console.cancel_event.GetInvocationList();
				foreach (ConsoleCancelEventHandler consoleCancelEventHandler in invocationList)
				{
					try
					{
						consoleCancelEventHandler(null, consoleCancelEventArgs);
					}
					catch
					{
					}
				}
				flag = !consoleCancelEventArgs.Cancel;
			}
			if (flag)
			{
				Environment.Exit(58);
			}
		}

		// Token: 0x040003C5 RID: 965
		internal static TextWriter stdout;

		// Token: 0x040003C6 RID: 966
		private static TextWriter stderr;

		// Token: 0x040003C7 RID: 967
		private static TextReader stdin;

		// Token: 0x040003C8 RID: 968
		private static Encoding inputEncoding;

		// Token: 0x040003C9 RID: 969
		private static Encoding outputEncoding;

		// Token: 0x040003CA RID: 970
		private static ConsoleCancelEventHandler cancel_event;

		// Token: 0x040003CB RID: 971
		private static readonly Console.InternalCancelHandler cancel_handler = new Console.InternalCancelHandler(Console.DoConsoleCancelEvent);

		// Token: 0x02000110 RID: 272
		private class WindowsConsole
		{
			// Token: 0x06000E44 RID: 3652
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleCP();

			// Token: 0x06000E45 RID: 3653
			[DllImport("kernel32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
			private static extern int GetConsoleOutputCP();

			// Token: 0x06000E46 RID: 3654 RVA: 0x0003C94C File Offset: 0x0003AB4C
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetInputCodePage()
			{
				return Console.WindowsConsole.GetConsoleCP();
			}

			// Token: 0x06000E47 RID: 3655 RVA: 0x0003C954 File Offset: 0x0003AB54
			[MethodImpl(MethodImplOptions.NoInlining)]
			public static int GetOutputCodePage()
			{
				return Console.WindowsConsole.GetConsoleOutputCP();
			}
		}

		// Token: 0x020006D9 RID: 1753
		// (Invoke) Token: 0x06004358 RID: 17240
		private delegate void InternalCancelHandler();
	}
}
