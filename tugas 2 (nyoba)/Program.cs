using System;

namespace RobotSimulator
{
    // Abstract class Robot
    public abstract class Robot
    {
        public string Nama { get; set; }
        public int Energi { get; set; }
        public int Armor { get; set; }
        public int Serangan { get; set; }

        public Robot(string nama, int energi, int armor, int serangan)
        {
            Nama = nama;
            Energi = energi;
            Armor = armor;
            Serangan = serangan;
        }

        public abstract void Serang(Robot target);
        public abstract void GunakanKemampuan(Kemampuan kemampuan);

        public void CetakInformasi()
        {
            Console.WriteLine($"Nama: {Nama}");
            Console.WriteLine($"Energi: {Energi}");
            Console.WriteLine($"Armor: {Armor}");
            Console.WriteLine($"Serangan: {Serangan}");
            Console.WriteLine();
        }

        // Metode untuk menerima serangan dengan mengurangi energi berdasarkan armor
        public void TerimaSerangan(int kekuatanSerangan)
        {
            int damage = Math.Max(0, kekuatanSerangan - Armor);
            Energi -= damage;
            Console.WriteLine($"{Nama} menerima serangan dan mengalami kerusakan: {damage}. Sisa energi: {Energi}");
            if (Energi <= 0)
            {
                Console.WriteLine($"{Nama} telah dikalahkan!");
            }
        }
    }

    // Interface Kemampuan
    public interface Kemampuan
    {
        void GunakanKemampuan(Robot robot);
        int GetCooldown();
    }

    // Class Perbaikan
    public class Perbaikan : Kemampuan
    {
        private int cooldown;
        private int healingAmount = 20; // Jumlah energi yang dipulihkan

        public Perbaikan(int cooldown)
        {
            this.cooldown = cooldown;
        }

        public void GunakanKemampuan(Robot robot)
        {
            robot.Energi += healingAmount;
            Console.WriteLine($"Robot {robot.Nama} memulihkan energi sebesar {healingAmount}!");
        }

        public int GetCooldown()
        {
            return cooldown;
        }
    }

    // Class Serangan Listrik
    public class SeranganListrik : Kemampuan
    {
        private int cooldown;
        private int damage = 15; // Besarnya kerusakan dari serangan listrik

        public SeranganListrik(int cooldown)
        {
            this.cooldown = cooldown;
        }

        public void GunakanKemampuan(Robot robot)
        {
            robot.Energi -= damage;
            Console.WriteLine($"Robot {robot.Nama} terkena serangan listrik dan mengalami kerusakan {damage}!");
        }

        public int GetCooldown()
        {
            return cooldown;
        }
    }

    // Class Serangan Plasma
    public class SeranganPlasma : Kemampuan
    {
        private int cooldown;
        private int damage = 25; // Besarnya kerusakan dari serangan plasma

        public SeranganPlasma(int cooldown)
        {
            this.cooldown = cooldown;
        }

        public void GunakanKemampuan(Robot robot)
        {
            robot.Energi -= damage;
            Console.WriteLine($"Robot {robot.Nama} terkena serangan plasma dan mengalami kerusakan {damage}!");
        }

        public int GetCooldown()
        {
            return cooldown;
        }
    }

    // Class Pertahanan Super
    public class PertahananSuper : Kemampuan
    {
        private int cooldown;
        private int extraArmor = 10; // Tambahan armor sementara

        public PertahananSuper(int cooldown)
        {
            this.cooldown = cooldown;
        }

        public void GunakanKemampuan(Robot robot)
        {
            robot.Armor += extraArmor;
            Console.WriteLine($"Robot {robot.Nama} meningkatkan pertahanan sebesar {extraArmor} poin!");
        }

        public int GetCooldown()
        {
            return cooldown;
        }
    }

    // Class RobotBiasa
    public class RobotBiasa : Robot
    {
        private Kemampuan kemampuan;

        public RobotBiasa(string nama, int energi, int armor, int serangan, Kemampuan kemampuan)
            : base(nama, energi, armor, serangan)
        {
            this.kemampuan = kemampuan;
        }

        public override void Serang(Robot target)
        {
            Console.WriteLine($"{Nama} menyerang {target.Nama} dengan kekuatan {Serangan}!");
            target.TerimaSerangan(Serangan);
        }

        public override void GunakanKemampuan(Kemampuan kemampuan)
        {
            kemampuan.GunakanKemampuan(this);
        }
    }

    // Class BosRobot
    public class BosRobot : Robot
    {
        public BosRobot(string nama, int energi, int armor)
            : base(nama, energi, armor, 15) // Serangan default 15
        {
        }

        public override void Serang(Robot target)
        {
            Console.WriteLine($"Bos {Nama} menyerang robot {target.Nama} dengan kekuatan {Serangan}!");
            target.TerimaSerangan(Serangan);
        }

        public override void GunakanKemampuan(Kemampuan kemampuan)
        {
            kemampuan.GunakanKemampuan(this);
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Robot robot1 = new RobotBiasa("Robot 1", 100, 20, 25, new Perbaikan(2));
            Robot bossRobot = new BosRobot("Boss Robot", 150, 30);

            robot1.CetakInformasi();
            bossRobot.CetakInformasi();

            // Robot 1 menyerang Boss Robot
            robot1.Serang(bossRobot);
            bossRobot.CetakInformasi();

            // Boss Robot menyerang Robot 1
            bossRobot.Serang(robot1);
            robot1.CetakInformasi();

            // Robot 1 menggunakan kemampuannya
            robot1.GunakanKemampuan(new Perbaikan(2));
            robot1.CetakInformasi();
        }
    }
}
