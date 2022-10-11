using System;
using System.Collections.Generic;

namespace Assignment3
{
    internal class Program
    {
        public static MemberCollection libraryMembers = new MemberCollection(20);
        public static MovieCollection MoviesList = new MovieCollection();

        static void Main(string[] args)
        {

            ShowHomePage();
        }

        /**
         * ShowHomePage: Shows the Main Menu and returns selected input.
         * StaffMenu: Shows the Staff menu and returns selected input.
         * MemberMenu: Shows the Member menu and returns selected input.
         */
        static void ShowHomePage()
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine("Welcome to Community Library Movie DVD Management System");
            Console.WriteLine("===============================================================\n");

            bool running = true;
            while (running)
            {
                int userChoice = GetInputMain();
                while (userChoice == -1)
                {
                    helperFunctions.printInColor("Valid choices are 1, 2 and 0.", ConsoleColor.Red);
                    userChoice = GetInputMain();
                }

                switch (userChoice)
                {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        bool loginsuccessful = helperFunctions.AuthenticateUser("staff", "today123");
                        if (loginsuccessful == true)
                        {
                            StaffMenu();
                        }
                        break;
                    case 2:
                        IMember currentMember = memberLogin();
                        if (currentMember != null)
                        { 
                            MemberMenu(currentMember);
                        }
                        break;

                    default: break;
                }
            }

        }
        static void StaffMenu()
        {
            Console.WriteLine("\n==========================Staff Menu============================\n");
            Console.WriteLine("1. Add new DVDs of a new movie to the system");
            Console.WriteLine("2. Remove DVDs of a movie from the system");
            Console.WriteLine("3. Register a new member with the system");
            Console.WriteLine("4. Remove a registered member from the system");
            Console.WriteLine("5. Display a member's contact phone number, given the member's name");
            Console.WriteLine("6. Display all members who are currently renting a particular movie");
            Console.WriteLine("0. Return to the main menu\n");


            bool running = true;

            while (running)
            {
                int userChoice = GetInputPeople();
                while (userChoice == -1)
                {
                    helperFunctions.printInColor("Valid choices are 1, 2, 3, 4, 5, 6 and 0.", ConsoleColor.Red);
                    userChoice = GetInputPeople();
                }
                switch (userChoice)
                {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        //ADD NEW DVD
                        AddNewDVD();
                        break;
                    case 2:
                        //REMOVE DVD
                        RemoveDVD();
                        break;
                    case 3:
                        //REGISTER NEW MEMBER
                        registerMember();
                        break;
                    case 4:
                        //REMOVE MEMBER
                        removeMember();
                        break;
                    case 5:
                        //DISPLAY MEMBERS CONTACT PHONE NUMBER
                        displayContactNumber();
                        break;
                    case 6:
                        //DISPLAY ALL MEMBERS RENTING MOVIES
                        displayAllMembers();
                        break;
                    default: break;
                }

            }
        }


        static void MemberMenu(IMember currentMember) //CHNAGE TO MEMBER IF NEEDED
        {
            Console.WriteLine("==========================Member Menu============================\n");
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current borrowing movies");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to the main menu\n");

            bool running = true;
            while (running)
            {
                int userChoice = GetInputPeople();
                while (userChoice == -1)
                {
                    helperFunctions.printInColor("Valid choices are 1, 2, 3, 4, 5, 6 and 0.", ConsoleColor.Red);
                    userChoice = GetInputPeople();
                }
                switch (userChoice)
                {
                    case 0:
                        running = false;
                        break;
                    case 1:
                        //BROWSE ALL THE MOVIES
                        browseMovies();
                        break;
                    case 2:
                        //DISPLAY THE MOVIE INFORMATION
                        displayMovieInformation();
                        break;
                    case 3:
                        //BORROW A MOVIE DVD
                        borrowMovie((Member)currentMember);
                        break;
                    case 4:
                        //RETURN A MOVIE DVD
                        returnMovie((Member)currentMember);
                        break;
                    case 5:
                        //LIST CURRENT BORROWING MOIVES
                        currentBorrowingMovies((Member)currentMember);
                        break;
                    case 6:
                        //DISPLAY TOP3 MOVIES
                        topThree();
                        break;
                    default: break;
                }

            }

        }

        static int GetInputMain()
        {
            Console.WriteLine("\n==========================Main Menu============================\n");
            Console.WriteLine("1.Staff Login\n2.Member Login\n0.Exit\n");
            Console.WriteLine("\nEnter your choice ==> (1/2/0)");

            int userChoice;
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out userChoice) && (userChoice == 1 || userChoice == 2 || userChoice == 0))
            {
                return userChoice;
            }
            return -1;
        }

        static int GetInputPeople()
        {
            Console.WriteLine("\nEnter your choice ==> (1/2/3/4/5/6/0)");

            int userChoice;
            string userInput = Console.ReadLine();
            if (int.TryParse(userInput, out userChoice) && (userChoice >= 0 && userChoice <= 6))
            {
                return userChoice;
            }
            return -1;
        }



        /*
         * STAFF METHODS
        */



        static void AddNewDVD()
        {
            string movieTitle = helperFunctions.GetInput("Movie Title");
            movieTitle = helperFunctions.CheckStringLength("Movie Title", movieTitle);

            if (MoviesList.Search(movieTitle) != null)
            {
                //Update total copies of the movie
                Console.WriteLine("Movie already exits so you can add new copies.");
                int totalCopies = helperFunctions.GetInteger("Number of new copies to add");
                //DON'T LET PEOPLE ADD 0 COPIES
                while (totalCopies < 0)
                {
                    helperFunctions.printInColor("Sorry, number of new copies to add can't be negative", ConsoleColor.Red);
                    totalCopies = helperFunctions.GetInteger("Number of new copies to add");
                }

                Movie aMovie = (Movie)MoviesList.Search(movieTitle);
                int previousTotalCopies = aMovie.TotalCopies; int previousAvailableCopies = aMovie.AvailableCopies;
                //Console.WriteLine($"A = {previousAvailableCopies}, T = {previousTotalCopies}");

                aMovie.TotalCopies = previousTotalCopies + totalCopies;
                aMovie.AvailableCopies = totalCopies + previousAvailableCopies;
                //Console.WriteLine($"A = {aMovie.AvailableCopies}, T = {aMovie.TotalCopies}");

                helperFunctions.printInColor($"Available copies of {movieTitle} are now {aMovie.AvailableCopies}.", ConsoleColor.Green);
            }
            else
            {
                //If movie not found add movie
                Console.WriteLine("Please select the Movie Genre from the below list:\n1) Action\n2) Comedy\n3) History\n4) Drama\n5) Western");
                int movieGenre = helperFunctions.GetOption(1, 5);
                Console.WriteLine("Please select the Movie Classification from the below list:\n1) G\n2) PG\n3) M\n4) M15Plus");
                int movieClassification = helperFunctions.GetOption(1, 4);
                int duration = helperFunctions.GetInteger("Duration");

                while(duration <= 0)
                {
                    helperFunctions.printInColor("Movie duration should be greater than 0", ConsoleColor.Red);
                    duration = helperFunctions.GetInteger("Duration");
                }
                int totalCopies = helperFunctions.GetInteger("Total Copies");
                MovieGenre g = MovieGenre.Action; MovieClassification c = MovieClassification.G;
                //DON'T LET PEOPLE ADD 0 COPIES
                while (totalCopies <= 0)
                {
                    helperFunctions.printInColor("At least 1 DVD needs to be added", ConsoleColor.Red);
                    totalCopies = helperFunctions.GetInteger("Total Copies");
                }
                
                if (movieClassification == 2) { c = MovieClassification.PG;}
                else if (movieClassification == 3) { c = MovieClassification.M;}
                else if (movieClassification == 4) { c = MovieClassification.M15Plus;}

                
                if(movieGenre == 2) { g = MovieGenre.Comedy; }
                else if(movieGenre == 3) { g = MovieGenre.History; }
                else if(movieGenre == 4) { g = MovieGenre.Drama; }
                else if(movieGenre == 5) { g = MovieGenre.Western; }
                

                Movie newMovie = new Movie(movieTitle, g, c, duration, totalCopies);
                MoviesList.Insert(newMovie);
                helperFunctions.printInColor($"DVDs of '{movieTitle}' successfully added", ConsoleColor.Green);
            }

        }

        static void RemoveDVD()
        {
            //Number of DVDs to remove
            string movieTitle = helperFunctions.GetInput("Movie Title");
            if (MoviesList.Search(movieTitle) != null)
            {
                Movie movie = (Movie)MoviesList.Search(movieTitle);
                int DVDtoRemove = helperFunctions.GetInteger("Number of DVDs to remove");
                if (DVDtoRemove == movie.AvailableCopies)
                {
                    if(movie.Borrowers.Number == 0)
                    {
                        MoviesList.Delete(movie);
                        helperFunctions.printInColor($"The movie {movie.Title} is also deleted from the system.", ConsoleColor.Green);
                    }
                    else
                    {
                        helperFunctions.printInColor("You can't delete all copies as someone is renting it currently.", ConsoleColor.Red);
                    }   
                }
                else if (DVDtoRemove < 0)
                {
                    helperFunctions.printInColor("Number of DVDs to remove must be at least 0", ConsoleColor.Red);
                }
                else if (DVDtoRemove > movie.AvailableCopies)
                {
                    helperFunctions.printInColor("Can't delete more DVDs than the total copies of the Movie", ConsoleColor.Red);
                }
                else
                {
                    movie.AvailableCopies = movie.AvailableCopies - DVDtoRemove;
                    movie.TotalCopies = movie.TotalCopies - DVDtoRemove;
                    Console.WriteLine($"Available DVDs are: {movie.AvailableCopies}.");
                }
            }
            else
            {
                helperFunctions.printInColor("Coulnd't find the movie.", ConsoleColor.Red);
            }
        }

        static void registerMember()
        {
            string firstName = helperFunctions.GetInput("First Name");
            firstName = helperFunctions.CheckStringLength("First Name",firstName);
            string lastName = helperFunctions.GetInput("Last Name");
            lastName = helperFunctions.CheckStringLength("Last Name",lastName);
            string contactNumber = helperFunctions.GetInput("Contact Number");
            while (IMember.IsValidContactNumber(contactNumber) == false)
            {
                helperFunctions.printInColor("Please enter a valid contact number.", ConsoleColor.Red);
                contactNumber = helperFunctions.GetInput("Contact Number");
            }
            string pin = helperFunctions.GetInput("PIN");
            while (IMember.IsValidPin(pin) == false)
            {
                helperFunctions.printInColor("Please enter a valid PIN. Must be 4-6 digits.", ConsoleColor.Red);
                pin = helperFunctions.GetInput("PIN");
            }

            Member testMember = new Member(firstName, lastName);
            Member memberFound = (Member)libraryMembers.Find(testMember);

            if(memberFound == null)
            {
                Member newMember = new Member(firstName, lastName, contactNumber, pin);
                libraryMembers.Add(newMember);
                helperFunctions.printInColor("Member is successfully added", ConsoleColor.Green);
            }
            else
            {
                helperFunctions.printInColor("Member already exits!", ConsoleColor.Red);
            }

            
        }

        static void removeMember()
        {
            //NEED TO TEST: A registered member cannot be removed if he/she has any movie DVD on loan currently.
            Console.WriteLine("Enter Details about the Member you want to remove:");
            string firstName = helperFunctions.GetInput("First Name");
            firstName = helperFunctions.CheckStringLength("First Name", firstName);
            string lastName = helperFunctions.GetInput("Last Name");
            lastName = helperFunctions.CheckStringLength("Last Name", lastName);

            Member testMember = new Member(firstName, lastName);
            Member memberFound = (Member)libraryMembers.Find(testMember);
            if (memberFound == null)
            {
                helperFunctions.printInColor("Sorry no member found with that name.", ConsoleColor.Red);
            }
            else
            {
                if (memberFound.Movies.Number != 0)
                {
                    helperFunctions.printInColor("This member cannot be removed because they have movie DVDs on loan currently.", ConsoleColor.Red);
                }
                else
                {
                    libraryMembers.Delete(memberFound);
                    helperFunctions.printInColor("Member removed from the system", ConsoleColor.Green);
                }
                
            }
        }

        static void displayContactNumber()
        {
            string fullName = helperFunctions.GetInput("Full Name");
            string firstName = ""; string lastName = "";
            List<string> name = helperFunctions.CheckFullNameLength(fullName);

            while (name.Count > 2 || name.Count <= 0)
            {
                helperFunctions.printInColor("Please add vaild first and last name", ConsoleColor.Red);
                fullName = helperFunctions.GetInput("Full Name");
                name = helperFunctions.CheckFullNameLength(fullName);
            }
            firstName = helperFunctions.CheckFullNameLength(fullName)[0];
            lastName = helperFunctions.CheckFullNameLength(fullName)[1];

            Member testMember = new Member(firstName, lastName);
            Member memberFound = (Member)libraryMembers.Find(testMember);
            if (memberFound != null)
            {
                Console.WriteLine($"The contact number of the member is: {memberFound.ContactNumber}");
            }
            else
            {
                helperFunctions.printInColor("Sorry member not found:(", ConsoleColor.Red);
            }

        }
        
        static void displayAllMembers()
        {
            string movieTitle = helperFunctions.GetInput("Movie Title");
            Movie movieFound = (Movie)MoviesList.Search(movieTitle);
            if (movieFound != null)
            {
                Console.WriteLine("\nHere's a list of all members currently renting this movie:");
                Console.WriteLine(movieFound.Borrowers.Number ==  0 ? "No one is currently renting this movie.":movieFound.Borrowers.ToString());
                
            }
            else
            {
                helperFunctions.printInColor($"Sorry {movieTitle} not found:(", ConsoleColor.Red);
            }

        }


        /*
         * MEMBER METHODS
         */
        static Member memberLogin()
        {
            //CHECK LOGIN AND THEN SHOW MENU
            //Console.WriteLine("\n==========================Member LogIn============================\n");
            string memberFirstNameEntered = helperFunctions.GetInput("Firstname");
            string memberLastNameEntered = helperFunctions.GetInput("Lastname");
            string memberPinEntered = helperFunctions.GetInput("Pin number");

            Member loginMember = new Member(memberFirstNameEntered, memberLastNameEntered);
            Member memberFound = (Member)libraryMembers.Find(loginMember);

            if (memberFound != null && memberFound.Pin == memberPinEntered)
            {
                helperFunctions.printInColor("Member successfully Login", ConsoleColor.Green);
                return memberFound;
            }
            helperFunctions.printInColor("Incorrect name or password.", ConsoleColor.Red);
            return null;
        }

        static void browseMovies()
        {
            Console.WriteLine("\nHere's a list of all movies:\n");
            IMovie[] array = MoviesList.ToArray();

            if (MoviesList.IsEmpty() == false)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    Console.WriteLine($"Movie Title: {array[i].Title}, Genre: {array[i].Genre}, Classification: {array[i].Classification}, Duration: {array[i].Duration}, Available DVDs: {array[i].AvailableCopies}");
                }
            }
            else
            {
                helperFunctions.printInColor("There are no movies in the system currently.", ConsoleColor.Red);
            }
        }

        static void displayMovieInformation()
        {
            string movieTitle = helperFunctions.GetInput("Search the Movie Title");
            Movie movieFound = (Movie)MoviesList.Search(movieTitle);
            if (movieFound != null)
            {
                helperFunctions.printInColor($"Genre: {movieFound.Genre}, Classification: {movieFound.Classification}, Duration: {movieFound.Duration}, Available Copy: {movieFound.AvailableCopies}", ConsoleColor.Green);
            }
            else
            {
                helperFunctions.printInColor($"Sorry {movieTitle} not found:(", ConsoleColor.Red);
            }
        }

        static void borrowMovie(Member currentMember) //CHANGED FROM IMember to Member
        {
            string movieTitle = helperFunctions.GetInput("Search the Movie Title");
            Movie borrowMovie = (Movie)MoviesList.Search(movieTitle);
            IMovieCollection movieCollection = currentMember.Movies;  //....ADDED

            if (borrowMovie == null)
            {
                helperFunctions.printInColor($"Sorry {movieTitle} is not found:(", ConsoleColor.Red);
            }
            else if (borrowMovie.AvailableCopies == 0)
            {
                helperFunctions.printInColor("Sorry there are no copies available at the moment.", ConsoleColor.Red);
            }
            else if (movieCollection.Number < 5)     
            {
                if (borrowMovie != null && borrowMovie.AddBorrower(currentMember))
                {
                    movieCollection.Insert(borrowMovie); //....ADDED
                    helperFunctions.printInColor($"{movieTitle} is sucessfully borrowed", ConsoleColor.Green);
                }
                else if (!borrowMovie.AddBorrower(currentMember))
                {
                    helperFunctions.printInColor("Sorry, you can't borrow a same movie more than once.", ConsoleColor.Red);
                }
            }
            else if(movieCollection.Number >= 5)
            {
                helperFunctions.printInColor("You can only borrow upto 5 movies at a time.", ConsoleColor.Red);
            }


        }

        static void returnMovie(Member currentMember)  //CHANGED FROM IMember to Member
        {
            string returnmovieTitle = helperFunctions.GetInput("Enter the movie you want to return");
            Movie returnMovie = (Movie)MoviesList.Search(returnmovieTitle);
            IMovieCollection movieCollection = currentMember.Movies;  //....ADDED

            if (returnMovie == null)
            {
                helperFunctions.printInColor($"Sorry {returnmovieTitle} is not found:(", ConsoleColor.Red);
            }
            else if (returnMovie != null && returnMovie.RemoveBorrower(currentMember))
            {
                movieCollection.Delete(returnMovie); //....ADDED
                helperFunctions.printInColor($"{returnmovieTitle} is sucessfully returned", ConsoleColor.Green);
            }
            else if (!returnMovie.RemoveBorrower(currentMember))
            {
                helperFunctions.printInColor("This movie is not currently borrowed by you.", ConsoleColor.Red);
            }

        }

        static void currentBorrowingMovies(Member currentMember)
        {
            
            IMovie[] borrowedMovies = currentMember.Movies.ToArray();
            if(borrowedMovies.Length == 0)
            {
                Console.WriteLine("You don't have any currently borrowed movies.");
            }
            else
            {
                Console.WriteLine("\nYou have borrowed the following Movies:");
                for (int i = 0; i < borrowedMovies.Length; i++)
                {
                    Console.WriteLine($" {i+1}. {borrowedMovies[i].Title}");
                }
            }
            
        }

        static void topThree()
        {
            
            int first = 0;
            string firstTitle = "None";
            int second = 0;
            string secondTitle = "None";
            int third = 0;
            string thirdTitle = "None";
            Console.WriteLine("\nHere's a list of top three:\n");
            IMovie[] arraytop = MoviesList.ToArray();
            
            
            for (int i = 0; i < arraytop.Length; i++)
            {
                if(arraytop[i].NoBorrowings > first)
                {
                    third = second;
                    second = first;
                    first = arraytop[i].NoBorrowings;

                    thirdTitle = secondTitle;
                    secondTitle = firstTitle;
                    firstTitle = arraytop[i].Title;

                } else if(arraytop[i].NoBorrowings > second)
                {
                    third = second;
                    second = arraytop[i].NoBorrowings;

                    thirdTitle = secondTitle;
                    secondTitle = arraytop[i].Title;  


                } else if(arraytop[i].NoBorrowings > third)
                {
                    third = arraytop[i].NoBorrowings;
                    thirdTitle = arraytop[i].Title;
                }
            }
            
            Console.WriteLine($"First: {firstTitle}, frequency: {first}\nSecond: {secondTitle}, frequency: {second}\nThird: {thirdTitle}, frequency: {third}");
            
        }
    }
}

