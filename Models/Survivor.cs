using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ZombieSurvivorKata.Models
{
    public delegate void SurvivorDiedEventHandler(object sender, EventArgs e);
    public delegate void SurvivorLeveledUpEventHandler(object sender, EventArgs e);
    public delegate void SurvivorAcquiredEquipmentEventHandler(object sender, EventArgs e);
    public delegate void SurvivorWoundedEventHandler(object sender, EventArgs e);

    public class Survivor
    {
        public event SurvivorDiedEventHandler SurvivorDied;
        public event SurvivorLeveledUpEventHandler SurvivorLeveledUp;
        public event SurvivorAcquiredEquipmentEventHandler SurvivorAcquiredEquipment;
        public event SurvivorWoundedEventHandler SurvivorWounded;

        public string Name;
        public ReadOnlyCollection<Equipment> Equipment { get; private set; }
        public int Experience { get; private set; }
        public Level Level { get; private set; } = Level.Blue;

        public int Wounds { get; private set; }
        private readonly List<Equipment> _equipment = new List<Equipment>();
        private int EquipmentCapacity = 5;

        public Survivor(string name)
        {
            Name = name;
            SurvivorLeveledUp += HandleLevelUpEvent;
            Equipment = _equipment.AsReadOnly();
        }

        private void HandleLevelUpEvent(object sender, EventArgs e)
        {
            if (Level == Level.Blue) { Level = Level.Yellow; return; }
            if (Level == Level.Yellow) { Level = Level.Orange; return; }
            if (Level == Level.Orange) { Level = Level.Red; }
        }

        public bool IsAlive
        {
            get { return Wounds < 2; }
        }

        public void Equip(Equipment equipmentItem)
        {
            var remainingCapacity = (EquipmentCapacity - Equipment.Count) - Wounds;
            if (remainingCapacity <= 0)
            {
                return;
            }
            _equipment.Add(equipmentItem);
            SurvivorAcquiredEquipment?.Invoke(this, EventArgs.Empty);
        }

        public void ReceiveWound()
        {
            if (Wounds == 2) { return; }

            Wounds++;
            SurvivorWounded?.Invoke(this, EventArgs.Empty);

            if (Wounds == 2)
            {
                SurvivorDied?.Invoke(this, EventArgs.Empty);
            }
        }

        public void KillZombie()
        {
            Experience++;
            if (Experience == 7)
            {
                SurvivorLeveledUp?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (Experience == 19)
            {
                SurvivorLeveledUp?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (Experience == 43)
            {
                SurvivorLeveledUp?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
