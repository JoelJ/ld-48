public interface IStats {
    void Hurt(int amount);
    void Miss();
    
    int MaxHealth { get; }
    int RemainingHealth { get; }
    
    int Strength { get; }
    int Defense { get; }
    
    int Accuracy { get; }
    int Evasion { get; }
    
    IRoot Root { get; }
}
