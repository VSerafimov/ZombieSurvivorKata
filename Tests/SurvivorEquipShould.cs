using Xunit;
using ZombieSurvivorKata.Models;

namespace ZombieSurvivorKata.Tests
{
    public class SurvivorEquipShould
    {
        private const string SurvivorName = "Vlad";
        private readonly Survivor _survivor;

        public SurvivorEquipShould()
        {
            _survivor = new Survivor(SurvivorName);
        }

        [Fact]
        public void Add_item_equipment_list()
        {
            var equipmentItem = new Equipment(WeaponType.BaseballBat);
            _survivor.Equip(equipmentItem);

            Assert.True(_survivor.Equipment.Contains(equipmentItem));
        }

        [Fact]
        public void Ignore_items_exceeding_capacity()
        {
            var equipmentItem = new Equipment(WeaponType.BaseballBat);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);

            Assert.Equal(5, _survivor.Equipment.Count);
        }

        [Fact]
        public void Ignore_items_exceeding_capacity_when_wounded()
        {
            var equipmentItem = new Equipment(WeaponType.BaseballBat);
            _survivor.ReceiveWound();
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);
            _survivor.Equip(equipmentItem);

            Assert.Equal(4, _survivor.Equipment.Count);
        }
    }
}
