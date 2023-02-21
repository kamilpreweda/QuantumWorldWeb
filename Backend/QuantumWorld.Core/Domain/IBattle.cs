namespace QuantumWorld.Core.Domain;
    public interface IBattle
    {
        int GetTotalHP(List<Ship> ships);
        int GetTotalAP(List<Ship> ships);
        int Attack(int totalAP, int totalHP);
        void CalculateDestroyedShips(List<Ship> ships, int damage, out int remainingDamage);
        List<string> StartBattle(List<Ship> playerShips, List<Resource> playerResources, Enemy enemy);
        void AssignRewards(List<Resource> playerResources, List<Resource> rewards);
        List<Resource> CollectRewards(Enemy enemy);

        List<string> GetRaport();
    }
