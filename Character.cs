namespace EasterEgg
{
    public class Character
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public Character(string name, int level, int health, int attack, int defense)
        {
            Name = name;
            Level = level;
            Health = health;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"Stats of the character:\n" +
                   $"- Name: {Name}" +
                   $"- Level: {Level}" +
                   $"- Health: {Health}" +
                   $"- Attack: {Attack}" +
                   $"- Defense: {Defense}";
        }

        public void TakeDamage(int damage)
        {
            this.Health -= damage - damage * Defense / 100;
        }

        public bool IsDead()
        {
            return Health <= 0;
        }
    }
}