using System;
using System.IO;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

class Program
{
    string path = @"C:\Users\linus\Desktop\Test.exe";
    List<Tickets> totalticket = new List<Tickets>();
    int madeMoney = 0;
    List<Tickets> refunded = new List<Tickets>();
    static void Main()
    {
        Program p = new Program();
        p.Run();
    }
   private void Run()
    {
        Save();
        Load();
    }
    void Load()
    {
        while (true)
        {
            Console.WriteLine("price for\n" + "adult: 100kr\n" + "elder: 75kr\n" + "child: 25kr\n" + "family pack: adults and children");
            string input = Console.ReadLine().ToLower().Trim();
            if (input == "buy")
            {
                Purchase();
            }
            else if (input == "list")
            {
                TicketList();
            }
            else if (input == "refund")
            {
                Refund();
            }
            else if (input == "mademoney")
            {
                MadeMoney();
            }
            else if (input == "seerefund")
            {
                ListRefunds();
            }
        }
    }
    void Purchase()
    {
        int Adult = 0;
        int Child = 0;
        int Elder = 0;

        while (true)
        {
            Console.WriteLine("What type of tickets do you want to buy");
            string input = Console.ReadLine().ToLower().Trim();
            if (input == "adult")
            {
                Console.WriteLine("how many adult tickets would you like?");
                int.TryParse(Console.ReadLine(), out Adult);
            }
            else if (input == "child")
            {
                Console.WriteLine("how many child tickets would you like?");
                int.TryParse(Console.ReadLine(), out Child);

            }
            else if (input == "elder")
            {
                Console.WriteLine("how many elder tickets would you like?");
                int.TryParse(Console.ReadLine(), out Elder);
            }
            else if (input == "familypack")
            {
                Console.WriteLine("How many adults are you?");
                int.TryParse(Console.ReadLine(), out Adult);
                Console.WriteLine($"you are {Adult} adults.\nHow many children are you?");
                int.TryParse(Console.ReadLine(), out Child);
                Console.WriteLine($"You are {Child} children.");
            }
            Console.WriteLine("\nDo you want to buy any more tickets?");
            string o = Console.ReadLine().ToLower().Trim();
            if (o == "no")
            {
                Console.WriteLine(" tickets purchased. You paid:");
                Tickets t = new Tickets(Adult, Child, Elder);
                madeMoney += Adult * 100;
                madeMoney += Child * 25;
                madeMoney += Elder * 75;
                totalticket.Add(t);
                Console.WriteLine(t.ToString());
                Console.WriteLine("Your ordernumber is: " + (totalticket.Count -1));
                Save();
                break;
            }
            continue;
        }
    }
    void TicketList()
    {
        int adult = 0;
        int child = 0;
        int elder = 0;
        foreach (Tickets ticket in totalticket)
        {
            adult += ticket.AdultAmount();
            child += ticket.ChildAmount();
            elder += ticket.ElderAmount();
        }
        Console.WriteLine("Adult tickets: " + adult + "\n" +
            "Child tickets: " + child + "\n" +
            "Elder tickets: " + elder + "\n \n");
    }
    void ListRefunds()
    {
        foreach (Tickets tickets in refunded)
        {
            Console.WriteLine(tickets.ToString());
        }
    }
    void Refund()
    {
        try
        {
            Console.WriteLine("Enter your ordernumber: ");
            int.TryParse(Console.ReadLine(), out int ordernumber);
            refunded.Add(totalticket[ordernumber]);
            totalticket.Remove(totalticket[ordernumber]);
            Console.WriteLine("Your refund has been transfered.");
        }
        catch (Exception)
        {
            Console.WriteLine("opsi wopsi somthing went wrong~ OwO");
            throw;
        }
    }
    void MadeMoney()
    {
        Console.WriteLine("We have earnd this much: " + madeMoney);
    }
    void Save()
    {

        StreamWriter sr = new StreamWriter(path);
        foreach (Tickets tickets1 in totalticket)
            sr.WriteLine(tickets1.ToString());

        sr.Close();
    }

    class Tickets
    {
        public int Adult;
        public int Elder;
        public int Child;
        public Tickets(int Adult, int Child, int Elder)
        {
            this.Adult = Adult;
            this.Child = Child;
            this.Elder = Elder;
        }
        public override string ToString()
        {
            return Adult + " Adult tickets for " + Adult * 100 + "kr \n" +
                Child + " Child tickets for " + Child * 25 + "kr \n" +
                Elder + " Elder tickets for " + Elder * 75 + "kr \n# \n";
        }
        public int AdultAmount()
        {
            return Adult;
        }
        public int ChildAmount()
        {
            return Child;
        }
        public int ElderAmount()
        {
            return Elder;
        }
    }
}