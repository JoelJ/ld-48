using Godot;
using System;

public class Player : Node2D {
	private AnimationPlayer _animationPlayer;

	private Direction _direction = Direction.Down;

	private HeartBeat _heartBeat;
	
	public override void _Ready() {
		_heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
		_heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));
		
		_animationPlayer = this.GetNode<AnimationPlayer>();
		_animationPlayer.Play("WalkDown");
	}

	public override void _PhysicsProcess(float delta) {
		var direction = ReadInput();
		if (direction != Direction.None) {
			_direction = direction;
			ApplyAnimation(_direction);
		}
	}

	public void OnHeartBeat(int _) {
		_direction = Direction.None;
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
