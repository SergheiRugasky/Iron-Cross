using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IronCross;
using NUnit.Framework;
using Action = IronCross.Action;

namespace UnitTests
{
    [TestFixture]
    public class Class1
    {
        [Test]
        public void ShouldTest()
        {
            Unit A = new Unit() { Action = Action.Defend, Name = "A", Defensive = 15, Offensive = 15, SoftAttack = 10 , Experience = 2 };
            Unit B = new Unit() { Action = Action.Attack, Name = "B", Defensive = 15, Offensive = 15, SoftAttack = 10, };

            new Gameplay().Battle(B,A);

            Console.WriteLine($"Unit {A.Name} has a {A.Experience} experience");
            Console.WriteLine($"Unit {B.Name} has a {B.Experience} experience");

            
            Console.WriteLine($"Unit {A.Name} has {A.TotalInjuriesReturned} injured returned");
            Console.WriteLine($"Unit {B.Name} has {B.TotalInjuriesReturned} injured returned");
        }
    }
}
