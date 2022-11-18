//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Runtime.InteropServices;
using RDR2.Math;

namespace RDR2.Native
{
	public interface INativeValue
	{
		ulong NativeValue
		{
			get; set;
		}
	}
	[StructLayout(LayoutKind.Explicit, Size = 0x18)]
	internal struct scrVector3
	{
		[FieldOffset(0x00)]
		internal float X;
		[FieldOffset(0x08)]
		internal float Y;
		[FieldOffset(0x10)]
		internal float Z;

		internal scrVector3(float x, float y, float z)
		{
			X = x;
			Y = y;
			Z = z;
		}

		public static implicit operator Vector3(scrVector3 value) => new Vector3(value.X, value.Y, value.Z);
		public static implicit operator scrVector3(Vector3 value) => new scrVector3(value.X, value.Y, value.Z);
	}

	internal unsafe static class NativeHelper<T>
	{
		static class CastCache<TFrom>
		{
			internal static readonly Func<TFrom, T> Convert;

			static CastCache()
			{
				var paramExp = Expression.Parameter(typeof(TFrom));
				var convertExp = Expression.Convert(paramExp, typeof(T));
				Convert = Expression.Lambda<Func<TFrom, T>>(convertExp, paramExp).Compile();
			}
		}

		static readonly Func<IntPtr, T> _ptrToStrFunc;

		static NativeHelper()
		{
			var ptrToStrMethod = new DynamicMethod("PtrToStructure<" + typeof(T) + ">", typeof(T),
				new Type[] { typeof(IntPtr) }, typeof(NativeHelper<T>), true);

			ILGenerator generator = ptrToStrMethod.GetILGenerator();
			generator.Emit(OpCodes.Ldarg_0);
			generator.Emit(OpCodes.Ldobj, typeof(T));
			generator.Emit(OpCodes.Ret);

			_ptrToStrFunc = (Func<IntPtr, T>)ptrToStrMethod.CreateDelegate(typeof(Func<IntPtr, T>));
		}

		internal static T Convert<TFrom>(TFrom from)
		{
			return CastCache<TFrom>.Convert(from);
		}

		internal static T PtrToStructure(IntPtr ptr)
		{
			return _ptrToStrFunc(ptr);
		}
	}

	public class InputArgument
	{
		internal ulong data;

		public InputArgument(ulong value)	{ data = value; }
		public InputArgument(object value)	{ data = Function.ObjectToNative(value); }

		// Types - ctor
		public InputArgument([MarshalAs(UnmanagedType.U1)] bool value)	: this(value ? 1UL : 0UL) { }
		public InputArgument(int value)		: this((uint)value) { }
		public InputArgument(uint value)	: this((ulong)value) { }
		public InputArgument(double value)	: this((float)value) { }
		public InputArgument(string value)	: this((object)value) { }
		public InputArgument(float value)	{ unsafe { data = *(uint*)&value; } }

		// Script Types - ctor
		public InputArgument(Model value)	: this((uint)value.Hash) { }
		public InputArgument(Blip value)	: this((object)value) { }
		public InputArgument(Camera value)	: this((object)value) { }
		public InputArgument(Entity value)	: this((object)value) { }
		public InputArgument(Ped value)		: this((object)value) { }
		public InputArgument(Player value)	: this((object)value) { }
		public InputArgument(Prop value)	: this((object)value) { }
		public InputArgument(Vehicle value)	: this((object)value) { }
		public InputArgument(Rope value)	: this((object)value) { }
		public InputArgument(Vector3 value)	: this((object)value) { }

		// Types - Operator
		public static implicit operator InputArgument([MarshalAs(UnmanagedType.U1)] bool value) { return value ? new InputArgument(1UL) : new InputArgument(0UL); }
		public static implicit operator InputArgument(byte value) { return new InputArgument((ulong)value); }
		public static implicit operator InputArgument(sbyte value) { return new InputArgument((ulong)value); }
		public static implicit operator InputArgument(short value) { return new InputArgument((ulong)value); }
		public static implicit operator InputArgument(ushort value) { return new InputArgument((ulong)value); }
		public static implicit operator InputArgument(int value) { return new InputArgument((ulong)value); }
		public static implicit operator InputArgument(uint value) { return new InputArgument((ulong)value); }
		public static implicit operator InputArgument(float value) { return new InputArgument(value); }
		public static implicit operator InputArgument(string value) { return new InputArgument(value); }
		public static implicit operator InputArgument(double value) { return new InputArgument((float)value); }
		
		// Pointers - Operator
		public static unsafe implicit operator InputArgument(bool* value) { return new InputArgument((ulong)new IntPtr(value).ToInt64()); }
		public static unsafe implicit operator InputArgument(int* value) { return new InputArgument((ulong)new IntPtr(value).ToInt64()); }
		public static unsafe implicit operator InputArgument(uint* value) { return new InputArgument((ulong)new IntPtr(value).ToInt64()); }
		public static unsafe implicit operator InputArgument(ulong* value) { return new InputArgument((ulong)new IntPtr(value).ToInt64()); }
		public static unsafe implicit operator InputArgument(float* value) { return new InputArgument((ulong)new IntPtr(value).ToInt64()); }
		public static unsafe implicit operator InputArgument(Vector3* value) { return new InputArgument((ulong)new IntPtr(value).ToInt64()); }
		public static unsafe implicit operator InputArgument(sbyte* value) { return new InputArgument(new string(value)); }

		// Script Types - Operator
		public static implicit operator InputArgument(Model value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Blip value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Camera value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Entity value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Ped value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Player value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Prop value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Vehicle value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Rope value) { return new InputArgument(value); }
		public static implicit operator InputArgument(Vector3 value) { return new InputArgument(value); }

		public override string ToString() { return data.ToString(); }
	}


	public static class Function
	{
		public static T Call<T>(ulong hash, params InputArgument[] arguments)
		{
			ulong[] args = new ulong[arguments.Length];
			for (int i = 0; i < arguments.Length; ++i) {
				args[i] = arguments[i].data;
			}

			unsafe {
				var res = RDR2DN.NativeFunc.Invoke(hash, args);

				// The result will be null when this method is called from a thread other than the main thread
				if (res == null) {
					throw new InvalidOperationException("Native.Function.Call can only be called from the main thread.");
				}

				if (typeof(T).IsEnum || typeof(T).IsPrimitive || typeof(T) == typeof(Vector3) || typeof(T) == typeof(Vector2)) {
					return ObjectFromNative<T>(res);
				}
				else {
					return (T)ObjectFromNative(typeof(T), res);
				}
			}
		}

		// Hack for VAR_STRING
		public static T _Call<T>(ulong hash, int flags, params InputArgument[] arguments)
		{
			// dumbass hack. just get it to fucking work.
			ulong[] args = new ulong[arguments.Length + 1];
			args[0] = (ulong)flags;
			for (int i = 1; i <= arguments.Length; ++i) {
				args[i] = arguments[i - 1].data;
			}

			unsafe {
				var res = RDR2DN.NativeFunc.Invoke(hash, args);

				// The result will be null when this method is called from a thread other than the main thread
				if (res == null) {
					throw new InvalidOperationException("Native.Function.Call can only be called from the main thread.");
				}

				if (typeof(T).IsEnum || typeof(T).IsPrimitive || typeof(T) == typeof(Vector3) || typeof(T) == typeof(Vector2)) {
					return ObjectFromNative<T>(res);
				}
				else {
					return (T)ObjectFromNative(typeof(T), res);
				}
			}
		}

		// Hack for _DATABINDING_WRITE_DATA_SCRIPT_VARIABLES 
		public static void _Call(ulong hash, int p0, int p1, params InputArgument[] arguments)
		{
			// dumbass hack. just get it to fucking work.
			ulong[] args = new ulong[arguments.Length + 2];
			args[0] = (ulong)p0;
			args[1] = (ulong)p1;
			for (int i = 2; i <= arguments.Length; ++i) {
				args[i] = arguments[i - 1].data;
			}

			unsafe {
				RDR2DN.NativeFunc.Invoke(hash, args);
			}
		}

		public static void Call(ulong hash, params InputArgument[] arguments)
		{
			ulong[] args = new ulong[arguments.Length];
			for (int i = 0; i < arguments.Length; ++i) {
				args[i] = arguments[i].data;
			}

			unsafe {
				RDR2DN.NativeFunc.Invoke(hash, args);
			}
		}


		internal static unsafe ulong ObjectToNative(object value)
		{
			if (value is null)
			{
				return 0;
			}

			if (value is bool valueBool)
			{
				return valueBool ? 1UL : 0UL;
			}
			if (value is int valueInt32)
			{
				// Prevent value from changing memory expression, in case the type is incorrect
				return (uint)valueInt32;
			}
			if (value is uint valueUInt32)
			{
				return valueUInt32;
			}
			if (value is float valueFloat)
			{
				return *(uint*)&valueFloat;
			}
			if (value is double valueDouble)
			{
				valueFloat = (float)valueDouble;
				return *(uint*)&valueFloat;
			}
			if (value is IntPtr valueIntPtr)
			{
				return (ulong)valueIntPtr.ToInt64();
			}
			if (value is string valueString)
			{
				return (ulong)RDR2DN.ScriptDomain.CurrentDomain.PinString(valueString).ToInt64();
			}

			// Scripting types
			if (value is Model valueModel)
			{
				return (ulong)valueModel.Hash;
			}
			if (typeof(INativeValue).IsAssignableFrom(value.GetType()))
			{
				return ((INativeValue)value).NativeValue;
			}

			throw new InvalidCastException(string.Concat("Unable to cast object of type '", value.GetType(), "' to native value"));
		}

		internal static unsafe T ObjectFromNative<T>(ulong* value)
		{
			if (typeof(T).IsEnum)
			{
				return NativeHelper<T>.Convert(*value);
			}

			if (typeof(T) == typeof(bool))
			{
				// Return proper boolean values (true if non-zero and false if zero)
				bool valueBool = *value != 0;
				return NativeHelper<T>.PtrToStructure(new IntPtr(&valueBool));
			}

			if (typeof(T) == typeof(int) || typeof(T) == typeof(uint) || typeof(T) == typeof(long) || typeof(T) == typeof(ulong) || typeof(T) == typeof(float))
			{
				return NativeHelper<T>.PtrToStructure(new IntPtr(value));
			}

			if (typeof(T) == typeof(double))
			{
				return NativeHelper<T>.Convert(NativeHelper<T>.PtrToStructure(new IntPtr(value)));
			}

			if (typeof(T) == typeof(Vector2) || typeof(T) == typeof(Vector3))
			{
				return NativeHelper<T>.Convert(*(scrVector3*)value);
			}

			throw new InvalidCastException(string.Concat("Unable to cast native value to object of type '", typeof(T), "'"));
		}

		internal static unsafe object ObjectFromNative(Type type, ulong* value)
		{
			if (type == typeof(string))
			{
				return RDR2DN.NativeMemory.PtrToStringUTF8(new IntPtr((byte*)*value));
			}

			// Scripting types
			if (type == typeof(Blip))
			{
				return new Blip(*(int*)value);
			}
			if (type == typeof(Camera))
			{
				return new Camera(*(int*)value);
			}
			if (type == typeof(Entity))
			{
				return Entity.FromHandle(*(int*)value);
			}
			if (type == typeof(Ped))
			{
				return new Ped(*(int*)value);
			}
			if (type == typeof(Player))
			{
				return new Player(*(int*)value);
			}
			if (type == typeof(Prop))
			{
				return new Prop(*(int*)value);
			}
			if (type == typeof(Rope))
			{
				return new Rope(*(int*)value);
			}
			if (type == typeof(Vehicle))
			{
				return new Vehicle(*(int*)value);
			}

			throw new InvalidCastException(string.Concat("Unable to cast native value to object of type '", type, "'"));
		}
	}
}
