
namespace QuantumWorld.Core.Domain;
public class Battle : IBattle
{
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

    public void StartBattle(List<Ship> playerShips, List<Resource> playerResources, Enemy enemy)
    {
        bool continiueFight = true;

        var playerTotalAP = GetTotalAP(playerShips);
        var playerTotalHP = GetTotalHP(playerShips);
        var enemyTotalAP = GetTotalAP(enemy.Ships);
        var enemyTotalHP = GetTotalHP(enemy.Ships);

        while (continiueFight)
        {
            enemyTotalHP = Attack(playerTotalAP, enemyTotalHP);
            CalculateDestroyedShips(enemy.Ships, playerTotalAP, out int remainingDamage);
            enemyTotalAP = GetTotalAP(enemy.Ships);
            if (enemyTotalHP <= 0)
            {
                var enemyRewards = CollectRewards(enemy);
                AssignRewards(playerResources, enemyRewards);
                enemy.Defeat();
                continiueFight = false; ;
                break;
            }
            playerTotalHP = Attack(enemyTotalAP, playerTotalHP);
            if (playerTotalHP <= 0)
            {
                CalculateDestroyedShips(playerShips, enemyTotalAP, out remainingDamage);
                continiueFight = false;
                break;
            }
            else if (playerTotalHP > 0)
            {
                CalculateDestroyedShips(playerShips, enemyTotalAP, out remainingDamage);
                playerTotalAP = GetTotalAP(playerShips);
            }
        }
    }

    public void AssignRewards(List<Resource> playerResources, List<Resource> rewards)
    {
        var carbonReward = rewards.Find(r => r.Name == "CarbonFiberResource");
        var quantumReward = rewards.Find(r => r.Name == "QuantumGlassResource");
        playerResources.Where(r => r.Name == "CarbonFiberResource").ToList().ForEach(r => r.Value += carbonReward.Value);
        playerResources.Where(r => r.Name == "QuantumGlassResource").ToList().ForEach(r => r.Value += quantumReward.Value);
    }
}
