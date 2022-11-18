//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Math;
using RDR2.Native;

namespace RDR2
{
	public static class GameplayCamera
	{
		public static float FieldOfView => CAM.GET_GAMEPLAY_CAM_FOV();
		public static bool IsRendering => CAM.IS_GAMEPLAY_CAM_RENDERING();
		public static bool IsFirstPersonAimCamActive => CAM.IS_FIRST_PERSON_AIM_CAM_ACTIVE();
		public static bool IsLookingBehind => CAM.IS_GAMEPLAY_CAM_LOOKING_BEHIND();
		public static bool IsShaking => CAM.IS_GAMEPLAY_CAM_SHAKING();
		public static bool IsInFirstPerson => CAM._IS_IN_FULL_FIRST_PERSON_MODE();
		public static bool IsInThirdPerson => !IsInFirstPerson;
		public static float Zoom { set => CAM.SET_THIRD_PERSON_CAM_ORBIT_DISTANCE_LIMITS_THIS_UPDATE(1.0f, value); }

		public static Vector3 Position => CAM.GET_GAMEPLAY_CAM_COORD();
		public static Vector3 Rotation => CAM.GET_GAMEPLAY_CAM_ROT(2);
		public static Vector3 Direction
		{
			get
			{
				Vector3 rot = Rotation;
				double rotX = rot.X / 57.295779513082320876798154814105;
				double rotZ = rot.Z / 57.295779513082320876798154814105;
				double multXY = System.Math.Abs(System.Math.Cos(rotX));
				
				return new Vector3((float)(-System.Math.Sin(rotZ) * multXY), (float)(System.Math.Cos(rotZ) * multXY), (float)System.Math.Sin(rotX));
			}
		}
		public static Vector3 Forward => Direction;

		public static Vector3 GetOffsetInWorldCoords(Vector3 offset)
		{
			Vector3 Forward = Direction;
			const double D2R = 0.01745329251994329576923690768489;
			double num1 = System.Math.Cos(Rotation.Y * D2R);
			double x = num1 * System.Math.Cos(-Rotation.Z * D2R);
			double y = num1 * System.Math.Sin(Rotation.Z * D2R);
			double z = System.Math.Sin(-Rotation.Y * D2R);
			Vector3 Right = new Vector3((float)x, (float)y, (float)z);
			Vector3 Up = Vector3.Cross(Right, Forward);
			return Position + (Right * offset.X) + (Forward * offset.Y) + (Up * offset.Z);
		}

		public static Vector3 GetOffsetFromWorldCoords(Vector3 worldCoords)
		{
			Vector3 Forward = Direction;
			const double D2R = 0.01745329251994329576923690768489;
			double num1 = System.Math.Cos(Rotation.Y * D2R);
			double x = num1 * System.Math.Cos(-Rotation.Z * D2R);
			double y = num1 * System.Math.Sin(Rotation.Z * D2R);
			double z = System.Math.Sin(-Rotation.Y * D2R);
			Vector3 Right = new Vector3((float)x, (float)y, (float)z);
			Vector3 Up = Vector3.Cross(Right, Forward);
			Vector3 Delta = worldCoords - Position;
			return new Vector3(Vector3.Dot(Right, Delta), Vector3.Dot(Forward, Delta), Vector3.Dot(Up, Delta));
		}

		public static void ClampYaw(float min, float max)
		{
			CAM.SET_THIRD_PERSON_CAM_RELATIVE_HEADING_LIMITS_THIS_UPDATE(min, max);
		}
		public static void ClampPitch(float min, float max)
		{
			CAM.SET_THIRD_PERSON_CAM_RELATIVE_PITCH_LIMITS_THIS_UPDATE(min, max);
		}

		public static float RelativePitch
		{
			get => CAM.GET_GAMEPLAY_CAM_RELATIVE_PITCH();
			set => CAM.SET_GAMEPLAY_CAM_RELATIVE_PITCH(value, 1.0f);
		}

		public static float RelativeHeading
		{
			get => CAM.GET_GAMEPLAY_CAM_RELATIVE_HEADING();
			set => CAM.SET_GAMEPLAY_CAM_RELATIVE_HEADING(value, 1.0f);
		}
	}
}
