//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;
using System;
using System.Drawing;

namespace RDR2
{
	public sealed class Player : INativeValue
	{
		#region Fields
		Ped ped;

        /*struct StatIDArgs
        {
            long BaseId;
            long PermutationId;
        }*/ // for money, will fix later
		
        #endregion

        public Player(int handle)
		{
			Handle = handle;
		}

		public int Handle
		{
			get;
			private set;
		}

		public ulong NativeValue
		{
			get => (ulong)Handle;
			set => Handle = unchecked((int)value);
		}

		public Ped Character
		{
			get
			{
				int handle = PLAYER.GET_PLAYER_PED(Handle);

				if (ped == null || handle != ped.Handle)
				{
					ped = new Ped(handle);
				}

				return ped;
			}
		}

		public string Name => PLAYER.GET_PLAYER_NAME(Handle);

		public static int Money
		{
			get => MONEY._MONEY_GET_CASH_BALANCE();
			set {
				var source = Money;
				var target = value;
				if (target < source)
				{
					MONEY._MONEY_DECREMENT_CASH_BALANCE(source - target);
				}
				else
				{
					MONEY._MONEY_INCREMENT_CASH_BALANCE(target - source, 752097756); // ADD_REASON_DEFAULT
				}
			}
		}

		public int WantedLevel
		{
			get => PLAYER.GET_PLAYER_WANTED_LEVEL(Handle);
			set
			{
				PLAYER.SET_PLAYER_WANTED_LEVEL(Handle, value, false);
			}
		}

		public bool IsDead => PLAYER.IS_PLAYER_DEAD(Handle);

		public bool IsAlive => !IsDead;

		public bool IsAiming => PLAYER.IS_PLAYER_FREE_AIMING(Handle);

		public bool IsClimbing => PLAYER.IS_PLAYER_CLIMBING(Handle);

		public bool IsRidingTrain => PLAYER.IS_PLAYER_RIDING_TRAIN(Handle);


		public bool IsPlaying => PLAYER.IS_PLAYER_PLAYING(Handle);

		public bool IsInvincible
		{
			get => PLAYER.GET_PLAYER_INVINCIBLE(Handle);
			set => PLAYER.SET_PLAYER_INVINCIBLE(Handle, value);
		}

		public bool IgnoredByEveryone
		{
			set => PLAYER.SET_EVERYONE_IGNORE_PLAYER(Handle, value);
		}

		public bool CanUseCover
		{
			set => PLAYER.SET_PLAYER_CAN_USE_COVER(Handle, value);
		}

		public bool CanStartMission
		{
			get => PLAYER.CAN_PLAYER_START_MISSION(Handle);
		}

		public bool CanControlPlayer
		{
			get => PLAYER.IS_PLAYER_CONTROL_ON(Handle);
			set => PLAYER.SET_PLAYER_CONTROL(Handle, value, 0, false);
		}

		public bool ChangeModel(Model model)
		{
			if (!model.IsInCdImage || !model.IsPed || !model.Request(1000))
			{
				return false;
			}

			PLAYER.SET_PLAYER_MODEL(Handle, (uint)model.Hash, false);
			model.MarkAsNoLongerNeeded();
			return true;
		}


		public Vehicle LastVehicle => (Vehicle)Vehicle.FromHandle(PLAYER.GET_PLAYERS_LAST_VEHICLE());

		public bool IsAimingAtEntity(Entity entity)
		{
			return PLAYER.IS_PLAYER_FREE_AIMING_AT_ENTITY(Handle, entity.Handle);
		}

		public bool IsTargettingAnything => PLAYER.IS_PLAYER_TARGETTING_ANYTHING(Handle);


		public void DisableFiringThisFrame(bool toggle)
		{
			PLAYER.DISABLE_PLAYER_FIRING(Handle, toggle);
		}

		public void SetSuperJumpThisFrame()
		{
			MISC.SET_SUPER_JUMP_THIS_FRAME(Handle);
		}

		public void SetMayNotEnterAnyVehicleThisFrame()
		{
			PLAYER.SET_PLAYER_MAY_NOT_ENTER_ANY_VEHICLE(Handle);
		}

		public void SetMayOnlyEnterThisVehicleThisFrame(Vehicle vehicle)
		{
			PLAYER.SET_PLAYER_MAY_ONLY_ENTER_THIS_VEHICLE(Handle, vehicle.Handle);
		}

		public bool Exists()
		{
			// IHandleable forces us to implement this unfortunately,
			// so we'll implement it explicitly and return true
			return true;
		}

		public bool Equals(Player obj)
		{
			return !(obj is null) && Handle == obj.Handle;
		}
		public override bool Equals(object obj)
		{
			return !(obj is null) && obj.GetType() == GetType() && Equals((Player)obj);
		}

		public static bool operator ==(Player left, Player right)
		{
			return left is null ? right is null : left.Equals(right);
		}
		public static bool operator !=(Player left, Player right)
		{
			return !(left == right);
		}

		public sealed override int GetHashCode()
		{
			return Handle.GetHashCode();
		}
	}
}
