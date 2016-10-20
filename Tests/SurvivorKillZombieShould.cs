using Xunit;
using ZombieSurvivorKata.Models;

namespace ZombieSurvivorKata.Tests
{
    public class SurvivorKillZombieShould
    {
        private const string SurvivorName = "Vlad";
        private readonly Survivor _survivor;

        public SurvivorKillZombieShould()
        {
            _survivor = new Survivor(SurvivorName);
        }

        [Fact]
        public void Increment_experience_by_1()
        {
            _survivor.KillZombie();

            Assert.Equal(1, _survivor.Experience);
        }

        [Fact]
        public void Level_up_to_yellow_given_7_experience()
        {
            for (var i = 0; i < 7; i++)
            {
                _survivor.KillZombie();
            }
            Assert.Equal(7, _survivor.Experience);
            Assert.Equal(Level.Yellow, _survivor.Level);
        }

        [Fact]
        public void Level_up_to_orange_given_19_experience()
        {
            for (var i = 0; i < 19; i++)
            {
                _survivor.KillZombie();
            }
            Assert.Equal(19, _survivor.Experience);
            Assert.Equal(Level.Orange, _survivor.Level);
        }

        [Fact]
        public void Level_up_to_red_given_42_experience()
        {
            for (var i = 0; i < 43; i++)
            {
                _survivor.KillZombie();
            }
            Assert.Equal(43, _survivor.Experience);
            Assert.Equal(Level.Red, _survivor.Level);
        }
    }
}
