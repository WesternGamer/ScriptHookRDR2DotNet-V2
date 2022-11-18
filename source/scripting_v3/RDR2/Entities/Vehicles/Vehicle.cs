//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace RDR2
{
    public sealed class Vehicle : Entity
    {
        public Vehicle(int handle) : base(handle)
        {
        }

        public void Repair()
        {
            VEHICLE.SET_VEHICLE_FIXED(Handle);
        }


        #region Styling

        public bool IsExtraOn(int extra)
        {
            return VEHICLE.IS_VEHICLE_EXTRA_TURNED_ON(Handle, extra);
        }

        public bool ExtraExists(int extra)
        {
            return VEHICLE.DOES_EXTRA_EXIST(Handle, extra);
        }

        public void ToggleExtra(int extra, bool toggle)
        {
            VEHICLE.SET_VEHICLE_EXTRA(Handle, extra, !toggle);
        }

        #endregion

        #region Configuration

        public bool IsStolen
        {
            set => VEHICLE.SET_VEHICLE_IS_STOLEN(Handle, value);
        }

        /*public bool IsWanted
		{
			set => VEHICLE.SET_VEHICLE_IS_WANTED(Handle, value);
		}*/


        #endregion

        #region Health

        public float BodyHealth
        {
            get => VEHICLE.GET_VEHICLE_BODY_HEALTH(Handle);
            set => VEHICLE.SET_VEHICLE_BODY_HEALTH(Handle, value);
        }

        #endregion

        #region Performance & Driving


        public float Speed
        {
            get => ENTITY.GET_ENTITY_SPEED(Handle);
            set
            {
                if (Model.IsTrain)
                {
                    VEHICLE.SET_TRAIN_SPEED(Handle, value);
                    VEHICLE.SET_TRAIN_CRUISE_SPEED(Handle, value);
                }
                else
                {
                    VEHICLE.SET_VEHICLE_FORWARD_SPEED(Handle, value);
                }
            }
        }

        #endregion

        #region Damaging

        public bool IsDamaged
        {
            get
            {
                int health = ENTITY.GET_ENTITY_HEALTH(Handle);
				int maxHealth = ENTITY.GET_ENTITY_MAX_HEALTH(Handle, false);

				if (health < maxHealth)
                {
                    return true;
                }
				if (health > maxHealth)
                {
                    return false;
                }
                return false;
            }
		}
		public bool IsDriveable
		{
			get => VEHICLE.IS_VEHICLE_DRIVEABLE(Handle, true, false);
			set => VEHICLE.SET_VEHICLE_UNDRIVEABLE(Handle, !value);
		}

		public bool IsAxlesStrong
		{
			set => VEHICLE.SET_VEHICLE_HAS_STRONG_AXLES(Handle, value);
		}

		public bool CanWheelsBreak
		{
			set => VEHICLE.SET_VEHICLE_WHEELS_CAN_BREAK(Handle, value);
		}

		public bool CanBeVisiblyDamaged
		{
			set => VEHICLE.SET_VEHICLE_CAN_BE_VISIBLY_DAMAGED(Handle, value);
		}


		public void ApplyDamage(Vector3 loc, float damageAmount, float radius)
		{
			VEHICLE.SET_VEHICLE_DAMAGE(Handle, loc.X, loc.Y, loc.Z, damageAmount, radius, true);
		}

		#endregion

		#region Occupants

		public Ped Driver => GetPedInSeat(-1);

		public Ped GetPedInSeat(int seat)
		{
			return (Ped)FromHandle(VEHICLE.GET_PED_IN_VEHICLE_SEAT(Handle, seat));
		}

		public Ped[] Occupants
		{
			get
			{
				Ped driver = Driver;

				int arraySize = Entity.Exists(driver) ? PassengerCount + 1 : PassengerCount;
				Ped[] occupantsArray = new Ped[arraySize];
				int occupantIndex = 0;

				if (arraySize == 0)
				{
					return occupantsArray;
				}

				if (Entity.Exists(driver))
				{
					occupantsArray[0] = driver;
					++occupantIndex;
				}

				for (int i = 0, seats = PassengerSeats; i < seats; i++)
				{
					Ped ped = GetPedInSeat((int)i);

					if (!Entity.Exists(ped))
					{
						continue;
					}

					occupantsArray[occupantIndex] = ped;
					++occupantIndex;

					if (occupantIndex >= arraySize)
					{
						return occupantsArray;
					}
				}

				return occupantsArray;
			}
		}

		public Ped[] Passengers
		{
			get
			{
				var passengersArray = new Ped[PassengerCount];
				int passengerIndex = 0;

				if (passengersArray.Length == 0)
				{
					return passengersArray;
				}

				for (int i = 0, seats = PassengerSeats; i < seats; i++)
				{
					Ped ped = GetPedInSeat((int)i);

					if (!Entity.Exists(ped))
					{
						continue;
					}

					passengersArray[passengerIndex] = ped;
					++passengerIndex;

					if (passengerIndex >= passengersArray.Length)
					{
						return passengersArray;
					}
				}

				return passengersArray;
			}
		}

		public int PassengerCount => VEHICLE.GET_VEHICLE_NUMBER_OF_PASSENGERS(Handle);

		public int PassengerSeats => VEHICLE.GET_VEHICLE_MAX_NUMBER_OF_PASSENGERS(Handle);

		public Ped CreatePedOnSeat(int seat, RDR2.Model model)
		{
			if (!model.IsPed || !model.Request(1000))
			{
				return null;
			}

			return (Ped)FromHandle(PED.CREATE_PED_INSIDE_VEHICLE(Handle, (uint)model.Hash, seat, true, true, false));
		}

		
		public bool IsSeatFree(int seat)
		{
			return VEHICLE.IS_VEHICLE_SEAT_FREE(Handle, (int)seat);
		}

		#endregion

		#region Positioning

		public bool IsStopped => VEHICLE.IS_VEHICLE_STOPPED(Handle);

		public bool IsOnAllWheels => VEHICLE.IS_VEHICLE_ON_ALL_WHEELS(Handle);

		public bool PlaceOnGround()
		{
			return VEHICLE.SET_VEHICLE_ON_GROUND_PROPERLY(Handle, false);
		}

		public void PlaceOnNextStreet()
		{
			Vector3 pos = Position;

			for (int i = 1; i < 40; i++)
			{
				float heading;
				ulong unk;
				Vector3 outPos = Vector3.Zero;
				Vector3 newPos = outPos;
				unsafe
				{
					if (PATHFIND.GET_NTH_CLOSEST_VEHICLE_NODE_WITH_HEADING(pos.X, pos.Y, pos.Z, i, &outPos, &heading, &unk, 1, 3.0f, 0.0f)) {
						newPos = outPos;
					}
				}

				if (true)
				{
					Position = newPos;
					PlaceOnGround();
					Heading = heading;
					break;
				}
			}
		}

		public bool ProvidesCover
		{
			set => VEHICLE.SET_VEHICLE_PROVIDES_COVER(Handle, value);
		}

		#endregion

	}
}
