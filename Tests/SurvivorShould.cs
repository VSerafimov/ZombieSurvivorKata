using Xunit;
using ZombieSurvivorKata.Models;

namespace ZombieSurvivorKata.Tests
{
    public class SurvivorShould
    {
        private const string SurvivorName = "Vlad";
        private readonly Survivor _survivor;
        public SurvivorShould()
        {
            _survivor = new Survivor(SurvivorName);
        }

        [Fact]
        public void Have_name_when_created()
        {
            Assert.Equal(SurvivorName, _survivor.Name);
        }

        [Fact]
        public void Start_with_zero_wounds()
        {
            Assert.Equal(0, _survivor.Wounds);
        }

        [Fact]
        public void Start_alive()
        {
            Assert.True(_survivor.IsAlive);
        }
        [Fact]
        public void Start_zero_experience()
        {
            Assert.Equal(0, _survivor.Experience);
        }
        [Fact]
        public void Start_level_blue()
        {
            Assert.Equal(Level.Blue, _survivor.Level);
        }

    }
}
