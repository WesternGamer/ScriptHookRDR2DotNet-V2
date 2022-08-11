using System;
using RDR2.Native;

namespace RDR2.UI
{
	public sealed class Prompt : PoolObject, IEquatable<Prompt>
	{

		Prompt CreatePrompt(eInputType control, eUseContextMode mode, string text, int numMashes = 0, int holdTime = 1000, int depletionTime = 1000, int fillTime = 1000, uint timedEvent = 0)
		{
			int prompt = HUD._UI_PROMPT_REGISTER_BEGIN();
			HUD._UI_PROMPT_SET_CONTROL_ACTION(prompt, (uint)control);
			Text = text;

			switch (mode)
			{
				case eUseContextMode.UCM_PRESS:
					HUD._UI_PROMPT_SET_STANDARD_MODE(prompt, false);
					break;
				case eUseContextMode.UCM_TIMED_PRESS:
					HUD._UI_PROMPT_SET_PRESSED_TIMED_MODE(prompt, depletionTime);
					break;
				case eUseContextMode.UCM_RELEASE:
					HUD._UI_PROMPT_SET_STANDARD_MODE(prompt, true);
					break;
				case eUseContextMode.UCM_HOLD:
					HUD._UI_PROMPT_SET_HOLD_INDEFINITELY_MODE(prompt);
					break;
				case eUseContextMode.UCM_METERED:
					HUD._UI_PROMPT_SET_STANDARDIZED_HOLD_MODE(prompt, timedEvent);
					break;
				case eUseContextMode.UCM_METER_STANDARD_TIME:
					HUD._UI_PROMPT_SET_STANDARDIZED_HOLD_MODE(prompt, timedEvent);
					break;
				case eUseContextMode.UCM_AUTO_FILL:
					HUD._UI_PROMPT_SET_HOLD_AUTO_FILL_MODE(prompt, fillTime, holdTime);
					break;
				case eUseContextMode.UCM_AUTO_FILL_WITH_DECAY:
					HUD._UI_PROMPT_SET_HOLD_AUTO_FILL_WITH_DECAY_MODE(prompt, fillTime, holdTime);
					break;
				case eUseContextMode.UCM_MASH:
					HUD._UI_PROMPT_SET_MASH_MODE(prompt, numMashes);
					break;
				case eUseContextMode.UCM_MASH_AUTO_FILL:
					HUD._UI_PROMPT_SET_MASH_AUTO_FILL_MODE(prompt, fillTime, numMashes);
					break;
				case eUseContextMode.UCM_MASH_RESISTANCE:
					HUD._UI_PROMPT_SET_MASH_WITH_RESISTANCE_MODE(prompt, numMashes, 0.0f, 0.0f); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.UCM_MASH_RESISTANCE_CAN_FAIL:
					HUD._UI_PROMPT_SET_MASH_WITH_RESISTANCE_CAN_FAIL_MODE(prompt, numMashes, 0.0f, 0.0f); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.UCM_MASH_RESISTANCE_DYNAMIC:
					HUD._UI_PROMPT_SET_MASH_MANUAL_MODE(prompt, (1.0f / 10.0f), 0.0f, 0.0f, 0); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.UCM_MASH_RESISTANCE_DYNAMIC_CAN_FAIL:
					HUD._UI_PROMPT_SET_MASH_MANUAL_CAN_FAIL_MODE(prompt, (1.0f / 10.0f), 0.0f, 0.0f, 0); // TODO: Figure out what these floats do
					break;
				case eUseContextMode.UCM_MASH_INDEFINITELY:
					HUD._UI_PROMPT_SET_MASH_INDEFINITELY_MODE(prompt);
					break;
				case eUseContextMode.UCM_ROTATE:
					HUD._UI_PROMPT_SET_ROTATE_MODE(prompt, 1.0f, false);
					HUD._UI_PROMPT_SET_ATTRIBUTE(prompt, 10, true);
					break;
				case eUseContextMode.UCM_TARGET_METER:
					HUD._UI_PROMPT_SET_TARGET_MODE(prompt, 0.5f, 0.1f, 0);
					break;
				default:
					return null;
			}

			HUD._UI_PROMPT_REGISTER_END(prompt);
			Visible = false;
			Enabled = false;

			return new Prompt(prompt);
		}

		private string _text;
		public string Text
		{
			get => _text;
			set {
				_text = value;
				string varString = MISC.VAR_STRING(10, "LITERAL_STRING", value);
				HUD._UI_PROMPT_SET_TEXT(Handle, varString);
			}
		}

		public bool Visible
		{
			get => HUD._UI_PROMPT_IS_ACTIVE(Handle);
			set => HUD._UI_PROMPT_SET_VISIBLE(Handle, value);
		}

		public bool Enabled
		{
			get => HUD._UI_PROMPT_IS_ENABLED(Handle);
			set => HUD._UI_PROMPT_SET_ENABLED(Handle, value);
		}

		public bool IsPressed => HUD._UI_PROMPT_IS_PRESSED(Handle);
		public bool IsJustPressed => HUD._UI_PROMPT_IS_JUST_PRESSED(Handle);
		public bool IsReleased => HUD._UI_PROMPT_IS_RELEASED(Handle);
		public bool IsJustReleased => HUD._UI_PROMPT_IS_JUST_RELEASED(Handle);

		public int Priority
		{
			set => HUD._UI_PROMPT_SET_PRIORITY(Handle, value);
		}

		public bool IsPulsing
		{
			get => HUD._UI_PROMPT_GET_URGENT_PULSING_ENABLED(Handle);
			set => HUD._UI_PROMPT_SET_URGENT_PULSING_ENABLED(Handle, value);
		}

		public Prompt(int handle) : base(handle)
		{
		}

		public bool Equals(Prompt other)
		{
			return !ReferenceEquals(null, other) && other.Handle == Handle;
		}

		public override bool Exists()
		{
			return HUD._UI_PROMPT_IS_VALID(Handle);
		}

		public override void Delete()
		{
			HUD._UI_PROMPT_DELETE(Handle);
		}

		public override bool Equals(object obj)
		{
			if (ReferenceEquals(null, obj)) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj.GetType() == GetType() && Equals((Prompt)obj);
		}

		public override int GetHashCode()
		{
			return Handle.GetHashCode();
		}
	}

	enum eUseContextMode : int
	{
		UCM_PRESS,
		UCM_TIMED_PRESS,
		UCM_RELEASE,
		UCM_HOLD,
		UCM_METERED,
		UCM_METER_STANDARD_TIME,
		UCM_AUTO_FILL,
		UCM_AUTO_FILL_WITH_DECAY,
		UCM_MASH,
		UCM_MASH_AUTO_FILL,
		UCM_MASH_RESISTANCE,
		UCM_MASH_RESISTANCE_CAN_FAIL,
		UCM_MASH_RESISTANCE_DYNAMIC,
		UCM_MASH_RESISTANCE_DYNAMIC_CAN_FAIL,
		UCM_MASH_INDEFINITELY,
		UCM_ROTATE,
		UCM_TARGET_METER,
	}
}
