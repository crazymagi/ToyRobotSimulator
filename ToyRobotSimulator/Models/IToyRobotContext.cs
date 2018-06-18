namespace ToyRobotSimulator.Models
{
    public interface IToyRobotContext
    {
        Position Position { get; set; }
        Facing? Facing { get; set; }

        bool IsValid { get; }
    }
}