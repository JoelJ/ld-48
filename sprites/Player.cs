using Godot;

public class Player : Node2D, IRoot {
	[Export(PropertyHint.Range, "1,3")]
	public int Speed { get; set; } = 1; // lower is faster

	private AnimationPlayer _animationPlayer;
	private HeartBeat _heartBeat;

	private Stats _stats;
	public IStats Stats => _stats;

	public override void _Ready() {
		_animationPlayer = this.GetNode<AnimationPlayer>();
		
		_heartBeat = GetNode<HeartBeat>("/root/HeartBeat");
		_heartBeat.SafeConnect(nameof(HeartBeat.OnHeartBeat), this, nameof(OnHeartBeat));

		_stats = this.GetNode<Stats>();
	}
	
	public void OnHeartBeat(int beat) {
		var frame = (beat / Speed) % 4;
		_animationPlayer.Play($"Beat{frame}");
	}

	public void Die() {
		QueueFree();
	}
}
