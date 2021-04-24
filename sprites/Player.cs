using Godot;
using System;

public class Player : Node2D {
	private AnimationPlayer _animationPlayer;

	private Direction _direction = Direction.Down;
	
	public override void _Ready() {
		_animationPlayer = this.GetNode<AnimationPlayer>();
		_animationPlayer.Play("WalkDown");
	}

	public override void _PhysicsProcess(float delta) {
		var direction = ReadInput();
		ApplyAnimation(direction);

		_direction = direction;
	}

	private Direction ReadInput() {
		if (Input.IsActionJustPressed("up")) {
			return Direction.Up;
		}

		if (Input.IsActionPressed("down")) {
			return Direction.Down;
		}

		if (Input.IsActionPressed("left")) {
			return Direction.Left;
		}

		if (Input.IsActionPressed("right")) {
			return Direction.Right;
		}

		return Direction.None;
	}

	private void ApplyAnimation(Direction direction) {
		switch (direction) {
			case Direction.None:
				break;
			case Direction.Up:
			case Direction.Down:
			case Direction.Left:
			case Direction.Right:
				_animationPlayer.Play("Walk" + direction);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
		}
	}
}
