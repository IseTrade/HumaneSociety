﻿using System;
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

        internal static object GetPendingAdoptions()
        {
            throw new NotImplementedException();
        }

        public static void UpdateShot(string v, Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.Where(x => x.AnimalId == animal.AnimalId).Select(x => x.AnimalId);
            db.SubmitChanges();
        }

        public static IQueryable<Shot> GetShots(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var shot = db.Shots.Select(x => x);
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
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.DeleteOnSubmit(animal);
            db.SubmitChanges();
        }

        public static IQueryable<Specy> GetSpecies()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var species = db.Species.Select(x => x);
            return species;
        }

        public static IQueryable<DietPlan> GetDietPlan()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var dietPlan = db.DietPlans.Select(x => x);
            return dietPlan;
        }

        public static void AddAnimal(Animal animal)
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            db.Animals.InsertOnSubmit(animal);
            db.SubmitChanges();
        }

        internal static Employee EmployeeLogin(string userName, string password)
        {
            throw new NotImplementedException();
        }

        internal static Employee RetrieveEmployeeUser(string email, int employeeNumber)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        internal static List<Adoption> GetUserAdoptionStatus(Client client)
        {
            var clientAdoptingStatus = db.Adoptions.Where(x => x.ClientId == client.ClientId).ToList();
            return clientAdoptingStatus;
        }

        public static IQueryable<Client> RetrieveClients()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
            var clients = db.Clients.Select(x => x);
            return clients;
        }

        internal static Room GetRoom(int animalId)
        {
            throw new NotImplementedException();
        }

        internal static void updateClient(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateUsername(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateEmail(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateAddress(Client client)
        {
            throw new NotImplementedException();
        }

        internal static void UpdateFirstName(Client client)
        {
            
            //var firstNameUpdate = (from Client in db.Clients where client.ClientId == client.ClientId select client).First();
            var firstNameUpdate = db.Clients.Where(x => x.ClientId == client.ClientId).SingleOrDefault();
            firstNameUpdate.FirstName = client.FirstName;
            db.SubmitChanges();
        }

        internal static void UpdateLastName(Client client)
        {
            throw new NotImplementedException();
        }

        public static IQueryable<USState> GetStates()
        {
            HumaneSocietyDataContext db = new HumaneSocietyDataContext();
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