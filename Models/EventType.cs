namespace ZombieSurvivorKata.Models
{
    public class EventType
    {
        public static string GameStarted = nameof(GameStarted);
        public static string SurvivorAdded = nameof(SurvivorAdded);
        public static string SurvivorAcquiredEquipment = nameof(SurvivorAcquiredEquipment);
        public static string SurvivorWounded = nameof(SurvivorWounded);
        public static string SurvivorDied = nameof(SurvivorDied);
        public static string SurvivorLeveledUp = nameof(SurvivorLeveledUp);
        public static string GameLeveledUp = nameof(GameLeveledUp);
        public static string GameEnded = nameof(GameEnded);
    }
}
