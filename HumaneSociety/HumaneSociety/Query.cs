using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{

    public class Query
    {
    //Reference: https://stackoverflow.com/questions/4799758/are-nested-try-catch-blocks-a-bad-idea
    //Reference: https://stackify.com/csharp-exception-handling-best-practices/
    //Reference: https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/access-modifiers

    // Create a single static reference for our database object
        static HumaneSocietyDataContext db = new HumaneSocietyDataContext();

        //=======================================================================================================================

        internal static void UpdateAdoption(bool v, Adoption adoption)
        {
            db.Adoptions.Where(x => x.PaymentCollected == v && x.AnimalId == adoption.AdoptionId).SingleOrDefault();
            db.SubmitChanges();
        }
        //=======================================================================================================================

        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits()
        {
            var animalsFound = db.Animals.Select(x => x);
            return animalsFound;
        }

        //=======================================================================================================================

        public static IQueryable<Adoption> GetPendingAdoptions()
        {
            var pendingAdoptions = db.Adoptions.Where(a => a.ApprovalStatus != "adopted");
            //var adpotionStatus = db.Adoptions.Where(x => x);
            return pendingAdoptions;
        }

        //=======================================================================================================================
        public static void UpdateShot(string v, Animal animal)
        {
            db.Animals.Where(x => x.AnimalId == animal.AnimalId).SingleOrDefault();
            db.SubmitChanges();
        }

        //=======================================================================================================================

        public static IQueryable<AnimalShot> GetShots(Animal animal)
        {
            var shot = db.AnimalShots.Where(x => x.AnimalId == animal.AnimalId);
            return shot;
        }

        //=======================================================================================================================

        public static Client GetClient(string userName, string password)
        {
            var client = db.Clients.Where(x => x.UserName == userName && x.Password == password).FirstOrDefault();
            return client;
        }

        //=======================================================================================================================

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            var update = db.Animals.Where(x => x.AnimalId == animal.AnimalId).SingleOrDefault();
            update.AnimalId = animal.AnimalId;
            db.SubmitChanges();
        }

        //=======================================================================================================================

        public static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }

        //=======================================================================================================================

        public static int? GetSpecies(string userInput)
        {
            var species = db.Species.Where(s => s.Name == userInput).Single();
            return species.SpeciesId;
        }

        //========================================================================================================================

        public static int? GetDietPlan(string userInput2)
        {
            var dietPlan = db.DietPlans.Where(d => d.Name == userInput2).SingleOrDefault();
            return dietPlan.DietPlanId;
        }

        //=======================================================================================================================

        public static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        //=======================================================================================================================

        internal static Employee EmployeeLogin(string userName, string password)
        {
            var Employee = db.Employees.Where(x => x.UserName == userName && x.Password == password);
            return Employee.Single();
        }

        //=======================================================================================================================

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            try
            {
                var Employee = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber).Single();
                return Employee;
            }
             catch (Exception e) //Catch generic exception
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //=======================================================================================================================

        internal static void AddUsernameAndPassword(Employee employee)
        {
            var employee_ = db.Employees.Where(e => e.EmployeeId == employee.EmployeeId).FirstOrDefault();
            employee_.UserName = employee.UserName;
            employee_.Password = employee.Password;

            db.SubmitChanges();
        }

        //=======================================================================================================================

        internal static bool CheckEmployeeUserNameExist(string username)
        {
            return db.Employees.Where(e => e.UserName == username).FirstOrDefault() != null;
        }

        //=======================================================================================================================

        public static Employee RunEmployeeQueries(Employee employee, string crudOption)
        {
            Func<Employee, Employee> crudMethod;//A built-in delegate Func which takes input type and output type

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

        /// <summary>
        /// A method which reads an employee object based on employee number
        /// </summary>
        /// <param name="employee"></param>
        /// <returns>An employee object</returns>
        /// 

        //=======================================================================================================================

        internal static Employee GetEmployee(Employee employee)
        {
            Employee employee_ = db.Employees.Where(x => x.EmployeeNumber == employee.EmployeeNumber).Single();
            Console.WriteLine("Employee's name {0} {1} and Employment Number {2}", employee_.FirstName, employee_.LastName, employee_.EmployeeNumber);
            Console.ReadLine(); //need this to pause the display.
            return employee_;
        }

        //=======================================================================================================================

        internal static Employee CreateEmployee(Employee employee)
        {
            try
            {
                db.Employees.InsertOnSubmit(employee);
                try
                {
                    db.Employees.InsertOnSubmit(employee);
                    db.SubmitChanges();
                    return employee;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //=======================================================================================================================

        internal static Employee DeleteEmployee(Employee employee)
        {
            //Employee employee_ = db.Employees.Where(x => x.LastName == employee.LastName && x.EmployeeNumber == employee.EmployeeNumber).Single();
            Employee employee_ = db.Employees.Where(x => x.EmployeeNumber == employee.EmployeeNumber).Single();
            try
            {
                db.Employees.DeleteOnSubmit(employee_);
                //try harder
                try
                {
                    db.SubmitChanges();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return employee_;
        }

        //=======================================================================================================================

        private static Employee UpdateEmployee(Employee employee)
        {
            var employee_ = db.Employees.Where(e => e.EmployeeNumber == employee.EmployeeNumber).First();//Select a unique first match or single user

            employee_.Email = employee.Email;
            employee_.FirstName = employee.FirstName;
            employee_.LastName = employee.LastName;
            employee_.Password = employee.Password;
            employee_.UserName = employee.UserName;

            db.SubmitChanges();

            return employee_;
        }

        //=======================================================================================================================

        internal static void AddNewClient(string firstName, string lastName, string username, string password, string email, string streetAddress, int zipCode, int state)
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

        //=======================================================================================================================

        internal static List<Adoption> GetUserAdoptionStatus(Client client)
        {
            var clientAdoptingStatus = db.Adoptions.Where(x => x.ClientId == client.ClientId).ToList();
            return clientAdoptingStatus;
        }

        //=======================================================================================================================

        public static IQueryable<Client> RetrieveClients()
        {
            var clients = db.Clients.Select(x => x);
            return clients;
        }

        //=======================================================================================================================

        internal static Room GetRoom(int animalId)
        {
            return db.Rooms.Where(r => r.AnimalId == animalId).FirstOrDefault();
        }

        //=======================================================================================================================

        internal static void UpdateClient(Client client)
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

        //=======================================================================================================================

        internal static void UpdateUsername(Client client)
        {
            var userNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            userNameUpdate.UserName = client.UserName;
            db.SubmitChanges();
        }

        //=======================================================================================================================

        internal static void UpdateEmail(Client client)
        {
            var emailUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            emailUpdate.Email = client.Email;

            db.SubmitChanges();
        }

        //=======================================================================================================================

        internal static void UpdateAddress(Client client)
        {
            var addressUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            addressUpdate.Address = client.Address;

            db.SubmitChanges();
        }

        //=======================================================================================================================

        internal static void UpdateFirstName(Client client)
        {
            var firstNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            firstNameUpdate.FirstName = client.FirstName;

            db.SubmitChanges();
        }

        //=======================================================================================================================

        internal static void UpdateLastName(Client client)
        {
            var lastNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            lastNameUpdate.LastName = client.LastName;

            db.SubmitChanges();
        }

        //=======================================================================================================================

        public static IQueryable<USState> GetStates()
        {
            var state = db.USStates.Select(x => x);
            return state;
        }

        //=======================================================================================================================

        public static Animal GetAnimalByID(int iD)
        {
            var animal = db.Animals.Where(x => x.AnimalId == iD).SingleOrDefault();
            return animal;
        }

        //=======================================================================================================================

        public static void Adopt(Animal animal, Client client)
        {
            throw new NotImplementedException();
        }
    }
}