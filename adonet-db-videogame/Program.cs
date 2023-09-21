namespace adonet_db_videogame
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to our Videogames management system!");

            while (true)
            {
                Console.WriteLine(@"
             - 1: Show a list of Videogames 
             - 2: Add a new Videogame
             - 3: Search a Videogame by his ID
             - 4: Search all Videogames with the name containing a certain word or letters
             - 5: Delete a Videogame
             - 6: End Program
             ");

                Console.Write("Select an option that you like: ");

                int selectedOption = int.Parse(Console.ReadLine());
                switch (selectedOption)
                {
                    case 1:
                        List<Videogame> videogames = VideogameDBManager.GetVideogames();

                        Console.WriteLine("Here you can find the list of videogames:");

                        foreach (Videogame videogame in videogames)
                        {
                            Console.WriteLine($"- {videogame}");
                        }

                        Console.WriteLine();
                        break;

                    case 2:
                        {
                            Console.WriteLine("Insert Videogame details: ");
                            Console.Write("Insert Videogame Name: ");
                            string name = Console.ReadLine();

                            Console.Write("Insert Videogame Overview: ");
                            string overview = Console.ReadLine();

                            Console.Write("Insert Videogame Release date: ");
                            DateTime releaseDate = DateTime.Parse(Console.ReadLine());

                            Console.Write("Insert Software House ID: ");
                            long softwareHouseId = long.Parse(Console.ReadLine());

                            Videogame newVideogame = new Videogame(0,name,overview, releaseDate, softwareHouseId);
                            bool inserted = VideogameDBManager.InsertVideogame(newVideogame);

                            if (inserted)
                            {
                                Console.WriteLine("Videogame added!!");
                            }
                            else
                            {
                                Console.WriteLine("OOPS something went wrong!");
                            }
                        }

                        break;

                }

            }
        }
    }
}