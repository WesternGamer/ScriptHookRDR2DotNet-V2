//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;
using System;

namespace RDR2
{
	public struct Model : IEquatable<Model>, INativeValue
	{
		public Model(int hash)
		{
			Hash = hash;
		}
		public Model(string name) : this(Game.Joaat(name))
		{
		}
		public Model(uint hash) : this((int)hash)
		{
		}
		public Model(PedHash hash) : this((int)hash)
		{
		}

		public ulong NativeValue
		{
			get => (ulong)Hash;
			set => Hash = unchecked((int)value);
		}

		public int Hash
		{
			get; private set;
		}

		public bool IsValid => STREAMING.IS_MODEL_VALID((uint)Hash);
		public bool IsInCdImage => STREAMING.IS_MODEL_IN_CDIMAGE((uint)Hash);

		public bool IsLoaded => STREAMING.HAS_MODEL_LOADED((uint)Hash);
		public bool IsCollisionLoaded => STREAMING.HAS_COLLISION_FOR_MODEL_LOADED((uint)Hash);

		public bool IsBoat => VEHICLE.IS_THIS_MODEL_A_BOAT((uint)Hash);
        public bool IsPed => STREAMING.IS_MODEL_A_PED((uint)Hash);
		public bool IsTrain => VEHICLE.IS_THIS_MODEL_A_TRAIN((uint)Hash);
		public bool IsVehicle => STREAMING.IS_MODEL_A_VEHICLE((uint)Hash);

		public void Request()
		{
            if (!STREAMING.IS_MODEL_VALID((uint)Hash))
            {
                return;
            }
            STREAMING.REQUEST_MODEL((uint)Hash, false);
		}
		public bool Request(int timeout)
		{
			Request();

			DateTime endtime = timeout >= 0 ? DateTime.UtcNow + new TimeSpan(0, 0, 0, 0, timeout) : DateTime.MaxValue;

			while (!IsLoaded)
			{
				Script.Yield();

				if (DateTime.UtcNow >= endtime)
					return false;
			}

			return true;
		}

		public void MarkAsNoLongerNeeded() // Release
		{
			STREAMING.SET_MODEL_AS_NO_LONGER_NEEDED((uint)Hash);
		}

		public bool Equals(Model obj)
		{
			return Hash == obj.Hash;
		}
		public override bool Equals(object obj)
		{
			return obj != null && obj.GetType() == GetType() && Equals((Model)obj);
		}

		public static bool operator ==(Model left, Model right)
		{
			return left.Equals(right);
		}
		public static bool operator !=(Model left, Model right)
		{
			return !left.Equals(right);
		}

		public static implicit operator int(Model source)
		{
			return source.Hash;
		}
		public static implicit operator PedHash(Model source)
		{
			return (PedHash)source.Hash;
		}
		public static implicit operator uint(Model source)
		{
			return (uint)source.Hash;
		}

		public static implicit operator Model(int source)
		{
			return new Model(source);
		}
		public static implicit operator Model(string source)
		{
			return new Model(source);
		}
		public static implicit operator Model(PedHash source)
		{
			return new Model(source);
		}
		public static implicit operator Model(uint source)
		{
			return new Model(source);
		}

		public override int GetHashCode()
		{
			return Hash;
		}

		public override string ToString()
		{
			return "0x" + Hash.ToString("X");
		}
	}
}
