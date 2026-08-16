using Godot;

namespace Utils;

public abstract partial class UIScreen : Control
{
	public virtual void OnPushed() { }

	public virtual void OnPopped() { }

	public virtual void OnCovered()
	{
		Hide();
	}

	public virtual void OnRevealed()
	{
		Show();
	}
}

