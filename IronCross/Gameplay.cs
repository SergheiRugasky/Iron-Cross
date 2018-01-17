using System;
using System.Threading;

namespace IronCross
{
    public class Gameplay
    {
        public void Battle(Unit attacker,Unit defender)
        {
            var duration = 0;
            while (CheckUnitsState(attacker,defender))
            {
                attacker.UpdateFromInjuries(duration);
                defender.UpdateFromInjuries(duration);
                ExchangeShots(attacker, defender,duration);
                Thread.Sleep(TimeSpan.FromSeconds(3));
                duration++;
            }

            Console.WriteLine($"Duration of the battle {duration} hours");
            var sum = 0;
            var returned = 0;
            for (int i = duration+1 ; i<attacker.Injuried.Length;i++)
            {
                if (attacker.Injuried[i] != 0)
                {
                    sum += (int)Math.Round(attacker.Injuried[i], MidpointRounding.AwayFromZero);
                    returned++;
                }
               
            }

            Console.WriteLine($"Unit {attacker.Name} still has {sum} injured that will return over {returned}");
        }

        private void ExchangeShots(Unit attacker, Unit defender, int duration)
        {
            var numberOfRounds = Math.Max(attacker.SoftAttack, defender.SoftAttack);
            for (int i = 0; i < numberOfRounds; i++)
            {
                attacker.Attack(i,defender,duration);
                defender.Defend(i,attacker,duration);
            }
        }

        private bool CheckUnitsState(Unit attacker, Unit defender)
        {
            Console.WriteLine($"unit {attacker.Name} has a strength of {attacker.Strength} and organization of {attacker.Organization}");
            Console.WriteLine($"unit {defender.Name} has a strength of {defender.Strength} and organization of {defender.Organization}");

            if (attacker.Organization < 35)
            {
                Console.WriteLine($"Unit {defender.Name} wins");
                return false;
            }

            if (defender.Organization < 20)
            {
                Console.WriteLine($"Unit {attacker.Name} wins");
                return false;
            }

            return true;
        }
    }
}
