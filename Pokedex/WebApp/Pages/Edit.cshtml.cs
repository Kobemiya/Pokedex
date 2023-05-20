using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages;

public class EditModel : PageModel
{
    public long Id;
        [BindProperty]
        public string Name { set; get; }
        public string ImagePath;
        public string TypeOne;
        public string? TypeTwo;
        [BindProperty]
        public string Description { set; get; }
        public int HP;
        public int Speed;
        public int Attack;
        public int SpeAttack;
        public int Defense;
        public int SpeDefense;
        public List<int> AttackIds;

        public class AttackInfo
        {
            public long Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public string Description { get; set; }
            public int Damage { get; set; }
            public int Accuracy { get; set; }

            public AttackInfo(long id, string name, string type, string description, int damage, int accuracy)
            {
                Id = id;
                Name = name;
                Type = type;
                Description = description;
                Damage = damage;
                Accuracy = accuracy;
            }
        }

        public List<AttackInfo> AllAttacks;
        public IEnumerable<AttackInfo> Attacks;

        private async Task<List<AttackInfo>> GetAllAttacks()
        {
            AttackInfo Charge = new AttackInfo(0, "Charge", "Normal", "Charge le Pokémon adverse",
                20, 100);
            AttackInfo Wolfgang = new AttackInfo(1, "Wolfgang", "Combat", "Woof woof",
                25, 80);
            AttackInfo Ddubaddu = new AttackInfo(2, "Ddubaddu", "Fée", "Wari wari",
                19, 99);
            List<AttackInfo> Attacks = new List<AttackInfo>();
            Attacks.Add(Charge);
            Attacks.Add(Wolfgang);
            Attacks.Add(Ddubaddu);
            return Attacks;
        }

        private async Task<List<AttackInfo>> GetAttacks(List<int> Ids)
        {
            List<AttackInfo> attacks = new List<AttackInfo>();
            foreach (int id in Ids)
            {
                attacks.Add(AllAttacks[id]);
            }
            return attacks;
        }
        public async Task<IActionResult> OnGet()
        {
            Name = "Hyunjin";
            ImagePath = "hyunjin.jpg";
            TypeOne = "Fée";
            Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.";
            AllAttacks = await GetAllAttacks();
            AttackIds = new List<int> { 1, 2 };
            Attacks = await GetAttacks(AttackIds);
            return Page();
        }

        public async Task<IActionResult>  OnPost(string name, string description, string typeOne, string typeTwo,
            int hp, int speed, int attack, int speAtt, int defense, int speDef)
        {
            Console.WriteLine("New values :" + name + " " + description + " " + typeOne + " " + typeTwo
            + " " + hp + " " + speed + " " + attack + " " + speAtt + " " + defense + " " + speDef);
            return await OnGet();
        }
}