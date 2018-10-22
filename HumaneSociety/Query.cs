using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace HumaneSociety
{
    public class Query
    {
        public static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        //=====================================================================================================
        private static Employee GetEmployee(Employee employee)
        {
            var employee_ = db.Employees.Where(x => x.EmployeeNumber == employee.EmployeeNumber).First();
            Console.WriteLine("Employee's name {0} {1} and Employment Number {2}", employee_.FirstName, employee_.LastName, employee_.EmployeeNumber);
            Console.ReadLine();
            return employee_;
        }

        //private static Employee GetEmployee(Employee employee)
        //{
        //    var employee_ = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).FirstOrDefault();
        //    return employee_;
        //}
        //=====================================================================================================

        private static Employee CreateEmployee(Employee employee)
        {
            //db.Employees.InsertOnSubmit(employee);
            //db.SubmitChanges();
            //return employee;
            try
            {
                try
                {
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    return employee;
                }
                catch
                {
                    return null;
                }

            }
            catch
            {
                return null;
            }


        }
        //private static Employee CreateEmployee(Employee employee)
        //{
        //    db.Employees.InsertOnSubmit(employee);
        //    db.SubmitChanges();
        //    return employee;
        //}
        //=====================================================================================================

        internal static Employee DeleteEmployee(Employee employee)
        {

            Employee employee_ = db.Employees.Where(x => x.LastName == employee.LastName && x.EmployeeNumber == employee.EmployeeNumber).Single();
            db.Employees.DeleteOnSubmit(employee_);
            db.SubmitChanges();
            return employee_;
        }

        //private static Employee DeleteEmployee(Employee employee)
        //{
        //    db.Employees.DeleteOnSubmit(employee);
        //    db.SubmitChanges();
        //    return employee;
        //}
        //=====================================================================================================

        private static Employee UpdateEmployee(Employee employee)
        {
            var employee_ = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).First();

            employee_.Email = employee.Email;
            employee_.FirstName = employee.FirstName;
            employee_.LastName = employee.LastName;
            employee_.Password = employee.Password;
            employee_.UserName = employee.UserName;
            db.SubmitChanges();
            return employee_;

        }
        //=====================================================================================================
        public static Employee RunEmployeeQueries(Employee employee, string crudOption)
        {
            //A delegate holds a reference to a method and also to the target object
            //You can pass methods as parameters to a delegate to allow the delegate
            //to point to the method
            //public delegate TResult Func<in ANYTYPE T, out TYPE TResult>(ANYTYPE T arg);
            Func<Employee, Employee> crudMethod;

            switch (crudOption)
            {
                case "read":
                    crudMethod = GetEmployee;
                    break;

                case "update":
                    crudMethod = UpdateEmployee;
                    break;

                case "create":
                    crudMethod = CreateEmployee;
                    break;

                case "delete":
                    crudMethod = DeleteEmployee;
                    break;
                default:
                    throw new Exception($"'{crudOption}' is not a valid RunEmployeeQuery option.");
            }

            return crudMethod(employee);
        }

        //=====================================================================================================

        public static void UpdateAdoption(bool v, Adoption adoption)
        {
            db.Adoptions.Where(x => x.PaymentCollected == v && x.AnimalId == adoption.AdoptionId).First();
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static IQueryable<Animal> SearchForAnimalByMultipleTraits()
        {
            var animalsFound = db.Animals.Select(x => x);
            return animalsFound;
        }
        //=====================================================================================================

        public static IQueryable<Adoption> GetPendingAdoptions()
        {
            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus != "adopted");
            //var adpotionStatus = db.Adoptions.Where(x => x);
            return pendingAdoptions;
        }
        //=====================================================================================================

        public static void UpdateShot(string v, Animal animal)
        {
            db.Animals.Where(x => x.AnimalId == animal.AnimalId).SingleOrDefault();
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static IQueryable<Shot> GetShots(Animal animal)
        {
            var shot = db.Shots.Where(x => x.AnimalShots == animal.AnimalShots);
            return shot;
        }
        //=====================================================================================================

        public static Client GetClient(string userName, string password)
        {

            var client = db.Clients.Where(x => x.UserName == userName && x.Password == password).Single();
            return client;
        }
        //=====================================================================================================

        public static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            var animal_ = db.Animals.Where(a => a.AnimalId == animal.AnimalId).First();
            foreach (KeyValuePair<int, string> update in updates)
            {
                switch (update.Key)
                {
                    case 1:
                        //select a single animal from specy where species name = current species to be updated
                        animal.Specy = db.Species.Where(s => s.Name == update.Value).Single();
                        break;
                    case 2:
                        animal_.Name = update.Value;
                        break;
                    case 3:
                        animal_.Age = Int32.Parse(update.Value);
                        break;

                    case 4:
                        animal_.Demeanor = update.Value;
                        break;
                    case 5:
                        animal_.KidFriendly = bool.Parse(update.Value);
                        break;

                    case 6:
                        animal_.PetFriendly = bool.Parse(update.Value);
                        break;

                    case 7:
                        animal_.Weight = Int32.Parse(update.Value);
                        break;

                    default:
                        break;
                }

            }
            //Save changes to database
            try
            {
                db.SubmitChanges();
                animal = animal_; //assign a reference of type animal to animal object
            }
            catch (Exception)
            {
                Console.Write("It didn't update");
            }
        }
        //=====================================================================================================

        public static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static int? GetSpecies(string userInput)
        {
            var species = db.Species.Where(s => s.Name == userInput).Single();
            return species.SpeciesId;
        }
        //=====================================================================================================

        public static int? GetDietPlan(string userInput2)
        {
            var dietPlan = db.DietPlans.Where(d => d.Name == userInput2).SingleOrDefault();
            return dietPlan.DietPlanId;
        }
        //=====================================================================================================

        public static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static Room GetRoom(int animalId)
        {
            var getRoom = db.Rooms.Where(r => r.AnimalId == animalId).Single();
            return getRoom;
        }
        //=====================================================================================================

        public static Employee EmployeeLogin(string userName, string password)
        {
            var Employee = db.Employees.Where(x => x.UserName == userName && x.Password == password).Single();
            return Employee;
        }
        //=====================================================================================================

        public static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            var Employee = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).Single();
            return Employee;
        }
        //=====================================================================================================

        public static void AddUsernameAndPassword(Employee employee)
        {
            //var employee_ = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            //employee_.UserName = employee.UserName;
            //employee_.Password = employee.Password;
            //db.SubmitChanges();
            db.Employees.InsertOnSubmit(employee);
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static bool CheckEmployeeUserNameExist(string username)
        {
            var employee = db.Employees.Where(e => e.UserName.Equals(username));
            if (employee.Equals(username))
            {
                return true;
            }
            return false;
        }
        //=====================================================================================================

        public static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
        {
            Address address = new Address();
            address.AddressLine1 = streetAddress;
            address.Zipcode = zipCode;
            address.USStateId = state;
            db.Addresses.InsertOnSubmit(address);
            db.SubmitChanges();

            Client client = new Client();
            client.FirstName = firstName;
            client.LastName = lastName;
            client.UserName = username;
            client.Password = password;
            client.Email = email;
            client.AddressId = address.AddressId;
            db.Clients.InsertOnSubmit(client);
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static List<Adoption> GetUserAdoptionStatus(Client client)
        {
            var clientAdoptingStatus = db.Adoptions.Where(x => x.ClientId == client.ClientId).ToList();
            return clientAdoptingStatus;
        }
        //=====================================================================================================

        public static IQueryable<Client> RetrieveClients()
        {
            var clients = db.Clients.Select(x => x);
            return clients;
        }
        //=====================================================================================================

        public static void UpdateClient(Client client)
        {
            var clientUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            clientUpdate.FirstName = client.FirstName;
            clientUpdate.LastName = client.LastName;
            clientUpdate.Email = client.Email;
            clientUpdate.HomeSquareFootage = client.HomeSquareFootage;
            clientUpdate.Income = client.Income;
            clientUpdate.NumberOfKids = client.NumberOfKids;
            clientUpdate.Password = client.Password;
            clientUpdate.Address = client.Address;
            clientUpdate.UserName = client.UserName;
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static void UpdateFirstName(Client client)
        {
            var firstNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            firstNameUpdate.FirstName = client.FirstName;
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static void UpdateLastName(Client client)
        {
            var lastNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            lastNameUpdate.LastName = client.LastName;
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static void UpdateEmail(Client client)
        {
            var emailUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            emailUpdate.Email = client.Email;
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static void UpdateAddress(Client client)
        {
            var addressUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            addressUpdate.Address = client.Address;
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static void UpdateUsername(Client client)
        {
            var userNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            userNameUpdate.UserName = client.UserName;
            db.SubmitChanges();
        }
        //=====================================================================================================

        public static IQueryable<USState> GetStates()
        {
            var state = db.USStates.Select(x => x);
            return state;
        }
        //=====================================================================================================

        public static Animal GetAnimalByID(int iD)
        {
            var animal = db.Animals.Where(x => x.AnimalId == iD).SingleOrDefault();
            return animal;
        }
        //=====================================================================================================

        public static void Adopt(Animal animal, Client client)
        {
            //var adoptAnimal = db.Animals.Where(a => a.AnimalId == animal && c => client.ClientId).SingleOrDefault();

        }
        //=====================================================================================================

        public static void ImportAnimalsCSV(string filePath)
        {

            string[] allLines = File.ReadAllLines(filePath);

            var query = from line in allLines
                        let data = line.Split(',')
                        select new
                        {
                            Name = data[1].Trim(),
                            SpeciesId = data[2].Trim(),
                            Weight = data[3].Trim(),
                            Age = data[4].Trim(),
                            DietPlanId = data[5].Trim(),
                            Demeanor = data[7].Trim(),
                            KidFriendly = data[8].Trim(),
                            PetFriendly = data[9].Trim(),
                            Gender = data[10].Trim(),
                            AdoptionStatus = data[11].Trim()
                        };

            var db = new HumaneSocietyDataContext();

            foreach (var a in query)
            {
                var animal = new Animal();
                //if (a.Name.Substring(0, 1) == "\"" && a.Name.Substring(a.Name.Length - 1) == "\"")
                //{
                    //animal.Name = a.Name.Substring(1, a.Name.Length - 2);
                //}

                animal.Name = a.Name;
                animal.SpeciesId = null;
                animal.Weight = int.Parse(a.Weight);
                animal.Age = int.Parse(a.Age);
                animal.DietPlanId = null;
                animal.Demeanor = a.Demeanor;
                animal.Gender = a.Gender;
                animal.AdoptionStatus = a.AdoptionStatus;

                bool? kidFriendly;
                switch (a.KidFriendly)
                {
                    case "0":
                        kidFriendly = false;
                        break;
                    case "1":
                        kidFriendly = true;
                        break;
                    default:
                        kidFriendly = null;
                        break;
                }
                animal.KidFriendly = kidFriendly;

                bool? petFriendly;
                switch (a.PetFriendly)
                {
                    case "0":
                        petFriendly = false;
                        break;
                    case "1":
                        petFriendly = true;
                        break;
                    default:
                        petFriendly = null;
                        break;
                }
                animal.PetFriendly = petFriendly;

                db.Animals.InsertOnSubmit(animal);
                db.SubmitChanges();
            }
        }
    }
}
