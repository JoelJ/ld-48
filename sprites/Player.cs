using Godot;
using System;

public class Player : Node2D {
	private ControllerAi _controllerAi;
	private AnimationPlayer _animationPlayer;

	public override void _Ready() {
		_animationPlayer = this.GetNode<AnimationPlayer>();
		_animationPlayer.Play("WalkDown");

		_controllerAi = this.GetNode<ControllerAi>();
	}

	public override void _PhysicsProcess(float delta) {
		if (_controllerAi.Direction != Direction.None) {
			ApplyAnimation(_controllerAi.Direction);
		}
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
