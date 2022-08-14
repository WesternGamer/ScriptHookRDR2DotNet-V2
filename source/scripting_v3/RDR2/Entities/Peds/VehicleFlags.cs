using System;

namespace RDR2
{
	[Flags]
	public enum eExitConfigFlag
	{
		ECF_NONE = 0,
		ECF_RESUME_IF_INTERRUPTED = (1 << 0),
		ECF_WARP_ENTRY_POINT = (1 << 1),
		ECF_INTERRUPT_DURING_GET_IN = (1 << 2),
		ECF_JACK_ANYONE = (1 << 3),
		ECF_WARP_PED = (1 << 4),
		ECF_INTERRUPT_DURING_GET_OUT = (1 << 5),
		ECF_DONT_WAIT_FOR_VEHICLE_TO_STOP = (1 << 6),
		ECF_ALLOW_EXTERIOR_ENTRY = (1 << 7),
		ECF_DONT_CLOSE_DOOR = (1 << 8),
		ECF_WARP_IF_DOOR_IS_BLOCKED = (1 << 9),
		ECF_ENTER_USING_NAVMESH = (1 << 10),
		ECF_JUMP_OUT = (1 << 12),
		ECF_PREFER_DISMOUNT_SIDE_WITH_FEWER_PEDS = (1 << 14),
		ECF_DONT_DEFAULT_WARP_IF_DOOR_BLOCKED = (1 << 16),
		ECF_USE_LEFT_ENTRY = (1 << 17),
		ECF_USE_RIGHT_ENTRY = (1 << 18),
		ECF_JUST_PULL_PED_OUT = (1 << 19),
		ECF_BLOCK_SEAT_SHUFFLING = (1 << 20),
		ECF_WARP_IF_SHUFFLE_LINK_IS_BLOCKED = (1 << 22),
		ECF_DONT_JACK_ANYONE = (1 << 23),
		ECF_WAIT_FOR_ENTRY_POINT_TO_BE_CLEAR = (1 << 24),
		ECF_USE_HITCH_DISMOUNT_VARIANT = (1 << 25),
		ECF_EXIT_SEAT_ON_TO_VEHICLE = (1 << 26),
		ECF_ALLOW_SCRIPTED_TASK_ABORT = (1 << 27),
		ECF_WILL_SHOOT_AT_TARGET_PEDS = (1 << 28),
		ECF_INTERRUPT_ALWAYS = (1 << 29),
		ECF_IGNORE_ENTRY_FROM_CLOSEST_POINT = (1 << 30),
		ECF_ALLOW_JACK_PLAYER_PED_ONLY = (1 << 31),
	}

	[Flags]
	public enum VehicleDrivingFlags
	{
		Default = AvoidVehicles | AvoidEmptyVehicles | StopBeforePeds | StopBeforeVehicles | AvoidPeds | AvoidObjects,
		StopBeforeVehicles = 1 << 0,
		StopBeforePeds = 1 << 1,
		AvoidVehicles = 1 << 2,
		AvoidEmptyVehicles = 1 << 3,
		AvoidPeds = 1 << 4,
		AvoidObjects = 1 << 5,
		Unk6 = 1 << 6,
		ObeyTrafficStops = 1 << 7,
		UseBlinkers = 1 << 8,
		AllowCuttingTraffic = 1 << 9,
		Reverse = 1 << 10,
		Unk11 = 1 << 11,
		Unk12 = 1 << 12,
		Unk13 = 1 << 13,
		Unk14 = 1 << 14,
		Unk15 = 1 << 15,
		Unk16 = 1 << 16,
		Unk17 = 1 << 17,
		TakeShortestPath = 1 << 18,
		AvoidOffroad = 1 << 19,
		Unk20 = 1 << 20,
		Unk21 = 1 << 21,
		IgnoreRoads = 1 << 22,
		Unk23 = 1 << 23,
		IgnoreAllPathing = 1 << 24,
		Unk25 = 1 << 25,
		Unk26 = 1 << 26,
		Unk27 = 1 << 27,
		Unk28 = 1 << 28,
		AvoidHighways = 1 << 29,
		Unk30 = 1 << 30
	}
}
