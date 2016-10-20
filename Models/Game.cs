using System;
using System.Collections.Generic;
using System.Linq;

namespace ZombieSurvivorKata.Models
{
    //public delegate void GameStartedEventHandler(object sender, EventArgs e);
    //public delegate void GameLeveledUpEventHandler(object sender, EventArgs e);
    public delegate void GameEndedEventHandler(object sender, EventArgs e);
    //public delegate void SurvivorAddedEventHandler(object sender, EventArgs e);

    public class Game
    {
        public List<Survivor> Survivors = new List<Survivor>();
        public bool IsOver { get; private set; }
        public Level Level { get; private set; } = Level.Blue;
        public List<Event> History = new List<Event>();

        public Game()
        {
            AddHistoryEvent(EventType.GameStarted);
        }

        public void AddSurvivor(Survivor survivor)
        {
            if (SurvivorAlreadyExists(survivor)) { return; }
            Survivors.Add(survivor);
            survivor.SurvivorDied += HandleSurvivorDied;
            survivor.SurvivorLeveledUp += HandleGameLevelUpEvent;
            survivor.SurvivorAcquiredEquipment += HandleSurvivorAcquiredEquipment;
            survivor.SurvivorWounded += HandleSurvivorWounded;

            AddHistoryEvent(EventType.SurvivorAdded);
        }

        private void HandleSurvivorWounded(object sender, EventArgs e)
        {
            AddHistoryEvent(EventType.SurvivorWounded);
        }

        private void HandleSurvivorAcquiredEquipment(object sender, EventArgs e)
        {
            AddHistoryEvent(EventType.SurvivorAcquiredEquipment);
        }

        private void HandleSurvivorDied(object sender, EventArgs e)
        {
            // Over when all survivors are not alive
            IsOver = Survivors.All(s => !s.IsAlive);

            AddHistoryEvent(EventType.SurvivorDied);

            if (IsOver)
            {
                AddHistoryEvent(EventType.GameEnded);
            }
        }

        private void AddHistoryEvent(string eventType)
        {
            var newEvent = new Event
            {
                EventType = eventType,
                EventDateTime = DateTime.UtcNow
            };
            History.Add(newEvent);
        }

        private bool SurvivorAlreadyExists(Survivor survivor)
        {
            return Survivors.Count(x => x.Name == survivor.Name) > 0;
        }

        private void HandleGameLevelUpEvent(object sender, EventArgs e)
        {
            AddHistoryEvent(EventType.SurvivorLeveledUp);
            AddHistoryEvent(EventType.GameLeveledUp);

            if (Level == Level.Blue) { Level = Level.Yellow; return; }
            if (Level == Level.Yellow) { Level = Level.Orange; return; }
            if (Level == Level.Orange) { Level = Level.Red; }
        }
    }
}
