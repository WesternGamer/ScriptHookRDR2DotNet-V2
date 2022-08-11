//
// Copyright (C) 2015 crosire & contributors
// License: https://github.com/crosire/scripthookvdotnet#license
//

using RDR2.Native;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace RDR2
{
	public static class Game
	{
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		private static extern System.IntPtr GetForegroundWindow();
		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(System.IntPtr hWnd, Rectangle rect);


		public static float LastFrameTime => MISC.GET_FRAME_TIME();
		public static float FPS => 1.0f / LastFrameTime;

		public static Size ScreenResolution
		{
			get
			{
				Rectangle rect = new Rectangle();
				GetWindowRect(GetForegroundWindow(), rect);
				return new Size(rect.Width, rect.Height);
			}
		}

		static Player _cachedPlayer;
		public static Player Player
		{
			get {
				int handle = PLAYER.PLAYER_ID();

				if (_cachedPlayer == null || handle != _cachedPlayer.Handle)
				{
					_cachedPlayer = new Player(handle);
				}

				return _cachedPlayer;
			}
		}

		public static int Version => (RDR2DN.NativeMemory.GetGameVersion() + 1);

		public static GlobalCollection Globals { get; private set; } = new GlobalCollection();

		public static void Pause(bool toggle)
		{
			MISC.SET_GAME_PAUSED(toggle);
		}
		public static void PauseClock(bool toggle)
		{
			CLOCK.PAUSE_CLOCK(toggle, 0);
		}

		public static bool IsPaused
		{
			get => HUD.IS_PAUSE_MENU_ACTIVE();
			set => MISC.SET_GAME_PAUSED(value);
		}


		public static bool IsLoading => DLC.GET_IS_LOADING_SCREEN_ACTIVE();
		public static bool IsScreenFadedIn => CAM.IS_SCREEN_FADED_IN();
		public static bool IsScreenFadedOut => CAM.IS_SCREEN_FADED_OUT();
		public static bool IsScreenFadingIn => CAM.IS_SCREEN_FADING_IN();
		public static bool IsScreenFadingOut => CAM.IS_SCREEN_FADING_OUT();

		public static void FadeScreenIn(int duration)
		{
			CAM.DO_SCREEN_FADE_IN(duration);
		}
		public static void FadeScreenOut(int duration)
		{
			CAM.DO_SCREEN_FADE_OUT(duration);
		}

		public static bool IsWaypointActive => MAP.IS_WAYPOINT_ACTIVE();




		public static int GameTime => MISC.GET_GAME_TIMER();
		public static int FrameCount => MISC.GET_FRAME_COUNT();

		public static float TimeScale
		{
			set => MISC.SET_TIME_SCALE(value);
		}

		public static int RadarZoom
		{
			set => MAP.SET_RADAR_ZOOM(value);
		}

		public static bool InMission
		{
			get => MISC.GET_MISSION_FLAG();
			set => MISC.SET_MISSION_FLAG(value);
		}

		public static bool ShowsPoliceBlipsOnRadar
		{
			set => PLAYER.SET_POLICE_RADAR_BLIPS(value);
		}

		public static string GetUserInput(int maxLength)
		{
			return GetUserInput("", "", maxLength);
		}
		public static string GetUserInput(string windowTitle, int maxLength)
		{
			return GetUserInput(windowTitle, "", maxLength);
		}
		public static string GetUserInput(string windowTitle, string defaultText, int maxLength)
		{
			RDR2DN.ScriptDomain.CurrentDomain.PauseKeyEvents(true);

			MISC.DISPLAY_ONSCREEN_KEYBOARD(0, windowTitle, "", defaultText, "", "", "", maxLength + 1);

			while (MISC.UPDATE_ONSCREEN_KEYBOARD() == 0)
			{
				Script.Yield();
			}

			RDR2DN.ScriptDomain.CurrentDomain.PauseKeyEvents(false);

			return MISC.GET_ONSCREEN_KEYBOARD_RESULT();
		}

		public static void PlayMusicEvent(string eventName)
		{
			AUDIO.TRIGGER_MUSIC_EVENT(eventName);
		}
		public static void StopMusicEvent(string eventName)
		{
			AUDIO.CANCEL_MUSIC_EVENT(eventName);
		}

		public static uint Joaat(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				return 0;
			}

			return MISC.GET_HASH_KEY(key);
		}

		public static string GetGXTEntry(string entry)
		{
			return HUD._GET_LABEL_TEXT(entry);
		}

		public static bool DoesGXTEntryExist(string entry)
		{
			return HUD.DOES_TEXT_LABEL_EXIST(entry);
		}
	}
}
