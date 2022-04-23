using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using System.Collections.Generic;
using System.Linq;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights = new List<IHero>();
            List<IHero> barbarians = new List<IHero>();
            foreach (IHero player in players)
            {
                if (player.GetType().Name == nameof(Barbarian))
                {
                    barbarians.Add(player);
                }
                else if (player.GetType().Name == nameof(Knight))
                {
                    knights.Add(player);
                }
            }

            List<IHero> aliveKnights = knights.Where(x => x.IsAlive).ToList();
            int initialCountofAliveKnights = aliveKnights.Count;
            List<IHero> aliveBarbarians = barbarians.Where(x => x.IsAlive).ToList();
            int initialCountofAliveBarbarians = aliveBarbarians.Count;

            while (aliveKnights.Count > 0 && aliveBarbarians.Count > 0)
            {
                bool isFlag = false;
                for (int i = 0; i < aliveKnights.Count; i++)
                {
                    for (int j = 0; j < aliveBarbarians.Count; j++)
                    {
                        aliveBarbarians[j].TakeDamage(aliveKnights[i].Weapon.DoDamage());
                        if (aliveBarbarians[j].Health <= 0)
                        {
                            aliveBarbarians.Remove(aliveBarbarians[j]);
                            j--;
                            if (aliveBarbarians.Count == 0)
                            {
                                isFlag = true;

                                break;
                            }


                        }
                    }
                    if (isFlag)
                    {
                        break;
                    }
                }
                if (isFlag)
                {
                    break;
                }



                for (int i = 0; i < aliveBarbarians.Count; i++)
                {
                    for (int j = 0; j < aliveKnights.Count; j++)
                    {
                        aliveKnights[j].TakeDamage(aliveBarbarians[i].Weapon.DoDamage());
                        if (aliveKnights[j].Health <= 0)
                        {
                            aliveKnights.Remove(aliveKnights[j]);
                            j--;
                            if (aliveKnights.Count == 0)
                            {
                                isFlag = true;
                                break;
                            }
                            
                        }
                    }
                    if (isFlag)
                    {
                        break;
                    }
                }
            }

            if (aliveBarbarians.Count == 0)
            {
                return $"The knights took {initialCountofAliveKnights - aliveKnights.Count} casualties but won the battle.";
            }
            return $"The barbarians took {initialCountofAliveBarbarians - aliveBarbarians.Count} casualties but won the battle.";

        }
    }
}
