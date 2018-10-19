using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumaneSociety
{
    public class Query
    {
        static HumaneSocietyDataContext db = new HumaneSocietyDataContext();
        //var mikes = db.Animals.Where(async => async.Name == "Mike");

        public static void RunEmployeeQueries()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            
        }

        internal static void UpdateAdoption(bool v, Adoption adoption)
        {
            throw new NotImplementedException();
        }

        internal static IQueryable<Animal> SearchForAnimalByMultipleTraits()
        {
            var animalsFound = db.Animals.Select(x => x);
            return animalsFound;
        }

        public static IQueryable<Adoption> GetPendingAdoptions()
        {
            //var adpotionStatus = db.Adoptions.Where(x => x);
            return db.Adoptions;
        }

        public static void UpdateShot(string v, Animal animal)
        {
            db.Animals.Where(x => x.AnimalId == animal.AnimalId).SingleOrDefault();
            db.SubmitChanges();
        }

        public static IQueryable<Shot> GetShots(Animal animal)
        {
            var shot = db.Shots.Where(x => x.AnimalShots == animal.AnimalShots);
            return shot;
        }

        public static Client GetClient(string userName, string password)
        {
     
            var client = db.Clients.Where(x => x.UserName == userName && x.Password == password).SingleOrDefault();
            return client;
        }

        internal static void EnterUpdate(Animal animal, Dictionary<int, string> updates)
        {
            throw new NotImplementedException();
        }
        
        public static void RemoveAnimal(Animal animal)
        {
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }

        public static int? GetSpecies(string userInput)
        {
            var species = db.Species.Where(s => s.Name == userInput).SingleOrDefault();
            return species.SpeciesId;
        }

        public static int? GetDietPlan(string userInput2)
        {
            var dietPlan = db.DietPlans.Where(d => d.Name == userInput2).SingleOrDefault();
            return dietPlan.DietPlanId;
        }

        public static void AddAnimal(Animal animal)
        {
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            var Employee = db.Employees.Where(x => x.UserName == userName && x.Password == password);
            return Employee.SingleOrDefault();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            var Employee = db.Employees.Where(e => e.Email == email && e.EmployeeNumber == employeeNumber);
            return Employee.SingleOrDefault();
        }

        internal static void AddUsernameAndPassword(Employee employee)
        {
            throw new NotImplementedException();
        }

        internal static bool CheckEmployeeUserNameExist(string username)
        {
            throw new NotImplementedException();
        }

        internal static void RunEmployeeQueries(Employee employee, string v)
        {
            throw new NotImplementedException();
        }

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

        internal static List<Adoption> GetUserAdoptionStatus(Client client)
        {
            var clientAdoptingStatus = db.Adoptions.Where(x => x.ClientId == client.ClientId).ToList();
            return clientAdoptingStatus;
        }

        public static IQueryable<Client> RetrieveClients()
        {
            var clients = db.Clients.Select(x => x);
            return clients;
        }

        internal static Room GetRoom(int animalId)
        {
            return db.Rooms.Where(r => r.AnimalId == animalId).SingleOrDefault();
        }

        internal static void UpdateClient(Client client)
        {
            db.SubmitChanges();
        }

        internal static void UpdateUsername(Client client)
        {
            var userNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            userNameUpdate.UserName = client.UserName;
            db.SubmitChanges();
        }

        internal static void UpdateEmail(Client client)
        {
            var emailUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            emailUpdate.Email = client.Email;
            db.SubmitChanges();
        }

        internal static void UpdateAddress(Client client)
        {
            var addressUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            addressUpdate.Address = client.Address;
            db.SubmitChanges();
        }

        internal static void UpdateFirstName(Client client)
        {
            var firstNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            firstNameUpdate.FirstName = client.FirstName;
            db.SubmitChanges();
        }

        internal static void UpdateLastName(Client client)
        {
            var lastNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            lastNameUpdate.LastName = client.LastName;
            db.SubmitChanges();
        }

        public static IQueryable<USState> GetStates()
        {
            var state = db.USStates.Select(x => x);
            return state;
        }

        public static Animal GetAnimalByID(int iD)
        {
            var animal = db.Animals.Where(x => x.AnimalId == iD).SingleOrDefault();
            return animal;
        }

        public static void Adopt(Animal animal, Client client)
        {
            throw new NotImplementedException();
        }
    }
}
