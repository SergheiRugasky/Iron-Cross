using System;

namespace IronCross
{
    public partial class Unit
    {
        private Random _random = new Random(Guid.NewGuid().GetHashCode());
        private double _experience;
        public double[] Injuried = new double[168];
        public string Name { get; set; }
        public int Strength { get; set; } = 10000;
        public double Organization { get; set; } = 70;
        public double Morale { get; set; } = 50;
        public int SoftAttack { get; set; } = 12;
        public int Defensive { get; set; } = 8;
        public int Offensive { get; set; } = 12;
        public double DefensiveEffictiveness { get; set; } = 100;
        public double OffensiveEffictivenees { get; set; } = 100;
        public Action Action { get; set; }

        public void Attack(int roundNumber, Unit defender, int duration)
        {
            if (roundNumber > SoftAttack)
            {
                Console.WriteLine($"Unit {Name} can not attack this round");
            }
            var randomChance = (double)_random.Next(1, 101) / 100;
            Console.WriteLine($"Unit {Name} gets a {randomChance} random chance");
            var finalChance = randomChance * 1/10 + GetHitChanceBasedOnExperience(defender) * 1/4 + GetHitChangeBasedOnDivisionAttributes(Offensive) * 1/4 + GetHitChanceBasedOnModifiers() * 4/10;
            Console.WriteLine($"Final chance is {finalChance}");
            var isHit = finalChance >= 0.5;
            if (isHit)
            {
                TakeHit(defender,duration);
            }
            else
            {
                Experience += 1 / Math.Pow(10, 5);
                Console.WriteLine($"Unit {Name} misses");
            }
        }

        private double GetHitChanceBasedOnModifiers()
        {
            return 0.8;
        }

        private void TakeHit(Unit defender, int duration)
        {
            var damage = (int)((double)SoftAttack * Strength / 1000 * Organization / 100);
            defender.Strength = defender.Strength - damage;
            defender.Organization = defender.Organization - (double)damage / 100;
            Experience += damage / Math.Pow(10, 4);
            ComputeInjuries(defender,damage, duration);
            Console.WriteLine($"unit {Name} has done {damage} damage");
        }

        private void ComputeInjuries(Unit defender, int damage, int roundNumber)
        {
            //45% of the hit are either dead or irrelevant to the battle
            //the relevence of the battle is set to a day,24 hours
            var injuried = 0.45 * damage;
            var coef = 4.2; //~100/24 
            for (int j = roundNumber + 1; j < roundNumber + 24; j++)
            {
                var value = injuried *(5.4 - (0.1 * j)) /100;
                defender.Injuried[j] += value;
            }
            
        }

        private double GetHitChangeBasedOnDivisionAttributes(int defensiveValue)
        {
            var chance = (double)_random.Next(1, 101) / 100; 
            Console.WriteLine($"Unit {Name} gets a {chance} based on attributes");
            return chance;
        }

        private double GetHitChanceBasedOnExperience(Unit defender)
        {
            //function is 0.9x^2+9.5x+30 where x is the difference between experience between the units
            var experienceDiff = Experience - defender.Experience;
            var chance = (0.9 * Math.Pow(experienceDiff,2) + 9.5 * experienceDiff + 30)/100;
            Console.WriteLine($"Unit {Name} gets a {chance} based on experience");
            return chance;
        }

        public double Experience { get; set; }
        

        public void Defend(int roundNumber, Unit defender, int duration)
        {
            if (roundNumber > SoftAttack)
            {
                Console.WriteLine($"Unit {Name} can not attack this round");
            }
            var randomChance = (double)_random.Next(1, 101) / 100;
            Console.WriteLine($"Unit {Name} gets a {randomChance}");
            var finalChance = randomChance * 1 / 10 + GetHitChanceBasedOnExperience(defender) * 1 / 4 + GetHitChangeBasedOnDivisionAttributes(Offensive) * 1 / 4 + GetHitChanceBasedOnModifiers() * 4 / 10;
            Console.WriteLine($"Final chance is {finalChance}");
            var isHit = finalChance >= 0.5;
            if (isHit)
            {
                TakeHit(defender, duration);
            }
            else
            {
                Experience += 1 / Math.Pow(10, 5);
                Console.WriteLine($"Unit {Name} misses");
            }
        }

        public void UpdateFromInjuries(int roundNumber)
        {
            var strength = (int)Math.Round(Injuried[roundNumber], MidpointRounding.AwayFromZero);
            TotalInjuriesReturned += strength;
            this.Strength += strength; 
            Console.WriteLine($"{strength} injured return to the unit {Name}");
        }

        public int TotalInjuriesReturned { get; set; }
    }
}
