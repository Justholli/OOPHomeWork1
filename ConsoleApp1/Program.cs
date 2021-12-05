using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Person me = new Person("Misha", Gender.male);
            Person father = new Person("Andry", Gender.male);
            Person mother = new Person("Sveta", Gender.female);
            Person grandfather_by_father_side = new Person("Pety", Gender.male);
            Person grandmother_by_father_side = new Person("Nady", Gender.female);
            Person uncle_by_father_side_1 = new Person("Yra", Gender.male);
            Person aunt_by_mother_side_2 = new Person("Vera", Gender.female);
            Person grandmother_by_mother_side = new Person("Toma", Gender.female);
            Person grandfather_by_mother_side = new Person("Vity", Gender.male);
            Person uncle_by_mother_side_1 = new Person("Andry", Gender.male);
            Person cusin_by_mother_side_1 = new Person("Anton", Gender.male);


            father.MarrriedOn(mother);
            father.SetChild(me);
            grandfather_by_father_side.MarrriedOn(grandmother_by_father_side);
            grandmother_by_mother_side.MarrriedOn(grandfather_by_mother_side);
            grandfather_by_father_side.SetChild(father);
            grandfather_by_father_side.SetChild(uncle_by_father_side_1);
            grandmother_by_mother_side.SetChild(mother);
            grandmother_by_mother_side.SetChild(uncle_by_mother_side_1);
            grandmother_by_mother_side.SetChild(aunt_by_mother_side_2);
            aunt_by_mother_side_2.SetChild(cusin_by_mother_side_1);


            me.ColnsolePrintParents();
            me.ColnsolePrintUnclesAndAunts();
            me.ColnsolePrintCousins();
        }
    }
    enum Gender
    {
        male,
        female
    };

    class Person
    {
        private Gender gender;
        private string name;
        private Person dad;
        private Person mom;
        private Person partner;
        private List<Person> childs;

        public Person(string name, Gender gender)
        {
            this.name = name;
            this.gender = gender;
        }

        public void MarrriedOn(Person married)
        {
            if (this.partner == null)
            {
                this.partner = married;
                married.partner = this;
            }

            else
            {
                this.partner.partner = null;
                this.partner = married;
                married.partner = this;
            }
        }

        public void SetChild(Person child)
        {
            if (this.childs != null)
            {
                this.childs.Add(child);
            }

            if (this.childs == null)
            {
                List<Person> childs = new List<Person>();
                childs.Add(child);
                this.childs = childs;
            }

            if (this.partner != null)
            {
                if (this.partner.childs != null)
                {
                    this.partner.childs.Add(child);
                }

                if (this.partner.childs == null)
                {
                    List<Person> childs = new List<Person>();
                    childs.Add(child);
                    this.partner.childs = childs;
                }

                if (this.partner.gender == Gender.male)
                {
                    child.dad = this.partner;
                }

                if (this.partner.gender == Gender.female)
                {
                    child.mom = this.partner;
                }
            }

            if (this.gender == Gender.male)
            {
                child.dad = this;
            }

            if (this.gender == Gender.female)
            {
                child.mom = this;
            }
        }

        private List<Person> GetSibling()
        {
            List<Person> Cousins = new List<Person>();
            if (this.dad != null)
            {
                Cousins.AddRange(this.dad.childs);
            }
            else
            {
                if (this.mom != null)
                {
                    Cousins.AddRange(this.mom.childs);
                }
            }
            

            Cousins.Remove(this);
            return Cousins;
        }

        private List<Person> GetUnclesAndAunts()
        {
            List<Person> UnclesAndAunts = new List<Person>();
            if(this.dad != null)
            {
                UnclesAndAunts.AddRange(this.dad.GetSibling());
            }
            if (this.mom != null)
            {
                UnclesAndAunts.AddRange(this.mom.GetSibling());
            }

            return UnclesAndAunts;
        }

        private List<Person> GetCousins()
        {
            List<Person> Cousins = new List<Person>();
            if(this.GetUnclesAndAunts() != null)
            {
                foreach(Person uncleOrAunt in this.GetUnclesAndAunts())
                {
                    if(uncleOrAunt.childs != null)
                    {
                        Cousins.AddRange(uncleOrAunt.childs);
                    }
                }
            }
            return Cousins;
        }

        public void ColnsolePrintSibling()
        {
            if (this.GetSibling() == null || this.GetSibling().Count == 0)
            {
                Console.WriteLine("You have not sibling");
            }
            else
            {
                foreach (Person person in this.GetSibling())
                {
                    Console.WriteLine(person.name);
                }
            }
        }

        public void ColnsolePrintParents()
        {
            if (this.dad != null)
            {
                Console.WriteLine("Dad: "+this.dad.name);
            }
            if (this.mom != null)
            {
                Console.WriteLine("Mom: "+this.mom.name);
            }
        }

        public void ColnsolePrintUnclesAndAunts()
        {
            if (this.GetUnclesAndAunts() != null)
            {
                Console.WriteLine("UnclesAndAunts:");
                foreach (Person person in this.GetUnclesAndAunts())
                {
                    if(person.gender == Gender.female)
                    {
                        Console.WriteLine("Aunt:: "+person.name);
                    }
                    else
                    {
                        Console.WriteLine("Uncle: "+person.name);
                    }
                   
                }
            }
        }

        public void ColnsolePrintCousins()
        {
            if (this.GetCousins() != null)
            {
                Console.WriteLine("Cousins:");
                foreach (Person person in this.GetCousins())
                {
                    if (person.gender == Gender.female)
                    {
                        Console.WriteLine("Sister:: " + person.name);
                    }
                    else
                    {
                        Console.WriteLine("Brother: " + person.name);
                    }
                }
            }
        }
    };
}
