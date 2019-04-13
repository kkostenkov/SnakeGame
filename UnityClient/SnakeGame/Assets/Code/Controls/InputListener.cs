namespace InputListener
{
	public interface IInputListener
	{
		void CustomUpdate(float deltaTime);
		MoveDirection GetLastDirection();
	}

	public enum MoveDirection
	{
		Up,
		Right,
		Down,
		Left,
	}
}