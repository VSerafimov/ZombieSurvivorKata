using System.Linq;
using Xunit;
using ZombieSurvivorKata.Models;

namespace ZombieSurvivorKata.Tests
{
    public class GameShould
    {
        private readonly Game _game;
        private readonly Survivor _survivor;
        public GameShould()
        {
            _game = new Game();
            _survivor = new Survivor("Vlad");
        }

        [Fact]
        public void Start_with_0_survivors()
        {
            Assert.Equal(0, _game.Survivors.Count);
        }

        [Fact]
        public void Be_able_to_add_survivors()
        {
            _game.AddSurvivor(_survivor);

            Assert.True(_game.Survivors.Contains(_survivor));
        }

        [Fact]
        public void Have_unique_survivor_names()
        {
            _game.AddSurvivor(_survivor);
            _game.AddSurvivor(_survivor);

            Assert.Equal(1, _game.Survivors.Count);
        }

        [Fact]
        public void Start_out_not_over()
        {
            Assert.False(_game.IsOver);
        }

        [Fact]
        public void End_when_last_survivor_dies()
        {
            _game.AddSurvivor(_survivor);

            _survivor.ReceiveWound();
            _survivor.ReceiveWound();

            Assert.False(_survivor.IsAlive);
            Assert.True(_game.IsOver);
        }

        [Fact]
        public void Starts_with_level_blue()
        {
            Assert.Equal(Level.Blue, _game.Level);
        }

        [Fact]
        public void Level_up_to_blue_when_survivor_levels_up_to_blue()
        {
            _game.AddSurvivor(_survivor);

            for (var i = 0; i < 7; i++)
            {
                _survivor.KillZombie();
            }

            Assert.Equal(Level.Yellow, _survivor.Level);
            Assert.Equal(_game.Level, _survivor.Level);
        }

        [Fact]
        public void Level_up_to_yellow_when_survivor_levels_up_to_yellow()
        {
            _game.AddSurvivor(_survivor);

            for (var i = 0; i < 19; i++)
            {
                _survivor.KillZombie();
            }

            Assert.Equal(Level.Orange, _survivor.Level);
            Assert.Equal(_game.Level, _survivor.Level);
        }

        [Fact]
        public void Level_up_to_yellow_when_survivor_levels_up_to_orange()
        {
            _game.AddSurvivor(_survivor);

            for (var i = 0; i < 43; i++)
            {
                _survivor.KillZombie();
            }

            Assert.Equal(Level.Red, _survivor.Level);
            Assert.Equal(_game.Level, _survivor.Level);
        }

        [Fact]
        public void Track_in_history_that_the_game_started()
        {
            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.GameStarted));
        }

        [Fact]
        public void Track_in_history_that_survivor_was_added()
        {
            _game.AddSurvivor(_survivor);

            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.SurvivorAdded));
        }

        [Fact]
        public void Track_in_history_that_survivor_acquired_equipment()
        {
            _game.AddSurvivor(_survivor);
            var equipmentItem = new Equipment(WeaponType.BaseballBat);
            _survivor.Equip(equipmentItem);

            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.SurvivorAcquiredEquipment));
        }

        [Fact]
        public void Track_in_history_that_survivor_is_wounded()
        {
            _game.AddSurvivor(_survivor);
            _survivor.ReceiveWound();

            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.SurvivorWounded));
        }

        [Fact]
        public void Track_in_history_that_survivor_dies()
        {
            _game.AddSurvivor(_survivor);
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();

            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.SurvivorDied));
        }

        [Fact]
        public void Track_in_history_that_survivor_leveled_up()
        {
            _game.AddSurvivor(_survivor);

            for (var i = 0; i < 7; i++)
            {
                _survivor.KillZombie();
            }

            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.SurvivorLeveledUp));
        }

        [Fact]
        public void Track_in_history_that_game_leveled_up()
        {
            _game.AddSurvivor(_survivor);

            for (var i = 0; i < 7; i++)
            {
                _survivor.KillZombie();
            }

            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.GameLeveledUp));
        }

        [Fact]
        public void Track_in_history_that_game_ended()
        {
            _game.AddSurvivor(_survivor);
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();

            Assert.Equal(1, _game.History.Count(x => x.EventType == EventType.GameEnded));
        }
    }
}
