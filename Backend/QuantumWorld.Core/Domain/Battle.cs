
namespace QuantumWorld.Core.Domain;
public class Battle : IBattle
{
    private List<String> BattleRaport { get; set; }
    public int Attack(int totalAP, int totalHP)
    {
        int result;
        result = totalHP - totalAP;
        return result;
    }

    public List<Resource> CollectRewards(Enemy enemy)
    {
        List<Resource> rewards = new List<Resource>();
        rewards = enemy.GetRewards();
        return rewards;
    }

    public void CalculateDestroyedShips(List<Ship> ships, int damage, out int remainingDamage)
    {
        int result = 0;
        remainingDamage = 0;

        while (damage > 0)
        {
            foreach (var ship in ships)
            {
                result = (int)MathF.Round(damage / ship.HealthPoints);

                if (ship.Count > 0)
                {
                    {
                        if (result >= ship.Count)
                        {
                            damage -= ship.Count * ship.HealthPoints;
                            ship.SetCount(0);
                            remainingDamage = damage;
                        }
                        else if (result < ship.Count)
                        {
                            ship.CalculateCount(-result);
                            damage = 0;
                        }
                        else if (result <= 0)
                        {
                            damage = 0;
                        }
                    }
                }
            }
            damage = 0;
        }
    }
    public int GetTotalAP(List<Ship> ships)
    {
        int totalAP = 0;
        foreach (var ship in ships)
        {
            totalAP += ship.Count * ship.AttackPower;
        }
        return totalAP;
    }

    public int GetTotalHP(List<Ship> ships)
    {
        int totalHP = 0;
        foreach (var ship in ships)
        {
            totalHP += ship.Count * ship.HealthPoints;
        }
        return totalHP;
    }

    public List<string> StartBattle(List<Ship> playerShips, List<Resource> playerResources, Enemy enemy)
    {
        BattleRaport = new();

        bool continiueFight = true;
        AddToRaport($"Player has gathered forces around {enemy.Name.Replace("Enemy", "")} planet.");
        AddToRaport("Player's fleet:");

        foreach (var ship in playerShips)
        {
            if (ship.Count > 0)
            {
                AddToRaport($"{ship.Type}: {ship.Count}");
            }
        }

        AddToRaport($"{enemy.Name.Replace("Enemy", "")} fleet:");

        foreach (var ship in enemy.Ships)
        {
            AddToRaport($"{ship.Type}: {ship.Count}");
        }

        var playerTotalAP = GetTotalAP(playerShips);
        AddToRaport($"Player's attack power is {playerTotalAP} AP.");
        var playerTotalHP = GetTotalHP(playerShips);
        AddToRaport($"Player's health point's are {playerTotalHP} HP.");
        var enemyTotalAP = GetTotalAP(enemy.Ships);
        AddToRaport($"Enemy's attack power is {enemyTotalAP} AP.");
        var enemyTotalHP = GetTotalHP(enemy.Ships);
        AddToRaport($"Enemy's health point's are {enemyTotalHP} HP.");

        while (continiueFight)
        {
            enemyTotalHP = Attack(playerTotalAP, enemyTotalHP);
            AddToRaport($"Player's fleet attacked.");
            CalculateDestroyedShips(enemy.Ships, playerTotalAP, out int remainingDamage);
            enemyTotalAP = GetTotalAP(enemy.Ships);
            AddToRaport($"Enemy's attack power is now equal to: {enemyTotalAP} AP.");
            if (enemyTotalHP <= 0)
            {
                var enemyRewards = CollectRewards(enemy);
                AssignRewards(playerResources, enemyRewards);
                enemy.Defeat();
                AddToRaport("Player defeated enemy.");
                continiueFight = false;
                return BattleRaport;
            }
            AddToRaport($"Enemy's health point's are now equal to: {enemyTotalHP} HP.");
            playerTotalHP = Attack(enemyTotalAP, playerTotalHP);
            AddToRaport("Enemy's fleet attacked.");
            if (playerTotalHP <= 0)
            {
                CalculateDestroyedShips(playerShips, enemyTotalAP, out remainingDamage);
                AddToRaport("Player was defeated.");
                continiueFight = false;
                return BattleRaport;
            }
            else if (playerTotalHP > 0)
            {
                CalculateDestroyedShips(playerShips, enemyTotalAP, out remainingDamage);
                playerTotalAP = GetTotalAP(playerShips);
            }
        }
        return BattleRaport;
    }

    public void AssignRewards(List<Resource> playerResources, List<Resource> rewards)
    {
        var carbonReward = rewards.Find(r => r.Name == "CarbonFiberResource");
        var quantumReward = rewards.Find(r => r.Name == "QuantumGlassResource");
        if (carbonReward is null || quantumReward is null)
        {
            throw new Exception("Rewards are no longer availible.");
        }
        playerResources.Where(r => r.Name == "CarbonFiberResource").ToList().ForEach(r => r.Value += carbonReward.Value);

        playerResources.Where(r => r.Name == "QuantumGlassResource").ToList().ForEach(r => r.Value += quantumReward.Value);
    }

    public List<string> GetRaport()
    {
        return BattleRaport;
    }

    private void AddToRaport(string message)
    {
        BattleRaport.Add(message);
    }
}

