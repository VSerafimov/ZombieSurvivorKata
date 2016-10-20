using Xunit;
using ZombieSurvivorKata.Models;

namespace ZombieSurvivorKata.Tests
{
    public class SurvivorReceiveWoundShould
    {
        private const string SurvivorName = "Vlad";
        private readonly Survivor _survivor;

        public SurvivorReceiveWoundShould()
        {
            _survivor = new Survivor(SurvivorName);
        }

        [Fact]
        public void Increment_wounds()
        {
            _survivor.ReceiveWound();

            Assert.Equal(1, _survivor.Wounds);
        }

        [Fact]
        public void Dead_if_2_wounds()
        {
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();

            Assert.False(_survivor.IsAlive);
        }

        [Fact]
        public void Alive_if_1_wound()
        {
            _survivor.ReceiveWound();

            Assert.True(_survivor.IsAlive);
        }

        [Fact]
        public void Ignore_more_than_2_wounds()
        {
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();
            _survivor.ReceiveWound();

            Assert.Equal(2, _survivor.Wounds);
        }
    }
}
