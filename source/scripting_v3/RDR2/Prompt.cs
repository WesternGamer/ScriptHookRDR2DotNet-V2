using System;
using RDR2.Native;

namespace RDR2.UI
{
	public sealed class Prompt : PoolObject, IEquatable<Prompt>
	{
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

		public bool IsVisible
		{
			get => HUD._UI_PROMPT_IS_ACTIVE(Handle);
			set => HUD._UI_PROMPT_SET_VISIBLE(Handle, value);
		}

		public int HoldTime
		{
			set => HUD._UI_PROMPT_SET_PRESSED_TIMED_MODE(Handle, value);
		}

		public bool IsPressed => HUD._UI_PROMPT_IS_PRESSED(Handle);
		public bool IsJustPressed => HUD._UI_PROMPT_IS_JUST_PRESSED(Handle);
		public bool IsReleased => HUD._UI_PROMPT_IS_RELEASED(Handle);
		public bool IsJustReleased => HUD._UI_PROMPT_IS_JUST_RELEASED(Handle);

		public Control Control
		{
			set => HUD._UI_PROMPT_SET_CONTROL_ACTION(Handle, (uint)value);
		}

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
}
