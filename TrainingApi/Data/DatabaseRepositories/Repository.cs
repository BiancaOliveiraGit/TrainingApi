using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using TrainingApi.ErrorMiddleware;
using Microsoft.Extensions.Logging;

namespace TrainingApi.Data
{
    public partial class Repository : IRepository
    {
        private ILogger _logger;
        private readonly AppDbContext _appDbContext;
        public Repository(AppDbContext appDbContext, ILogger<Repository> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public Client GetClientByObjectIdentifier(string identifier, ILogger<Client> logger)
        {
            var client = new Client();
            try
            {
                client = _appDbContext.Clients.Where(w => w.ObjectIdentifier == identifier)
                                        .Select(s => s).FirstOrDefault();
                if(client == null)
                {
                    client = new Client();
                }
                return client;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetClientByObjectIdentifier: {identifier}");
            }
            return client;
        }

        public Client GetClientById(int id, ILogger<Client> logger)
        {
            var client = new Client();
            try
            {
                client =  _appDbContext.Clients.Where(w => w.ClientId == id)
                                        .Select(s => s).FirstOrDefault();
                if (client == null)
                {
                    client = new Client();
                }
                return client;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetClientById: {id}");
            }
            return client;
        }
               

        public IEnumerable<Client> GetClients(ILogger<Client> logger)
        {
            List<Client> list = new List<Client>();
            try
            {
                list = _appDbContext.Clients.Select(s => s).ToList();
                return list;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in GetClients");
            }
            return list;
        }

        public Client PostNewClient(Client newClient, ILogger<Client> logger)
        {
            try
            {
                //check that client doesn't exist
                var exists = _appDbContext.Clients.Where(w => w.ObjectIdentifier == newClient.ObjectIdentifier)
                                                  .Select(s => s).FirstOrDefault();
                if (exists != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Client Already Exists");

                //check that unique email 
                var emailUnique = _appDbContext.Clients.Where(w => w.Email == newClient.Email)
                                                        .Select(s => s).ToList();

                if (emailUnique.Count > 0)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Must have a unique email. " + newClient.Email + " already in system");

                var item = _appDbContext.Add(newClient);
                item.State = Microsoft.EntityFrameworkCore.EntityState.Added;
                var isOk = _appDbContext.SaveChanges();

                return item.Entity;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in PostNewClient: {newClient.FirstName} - {newClient.Email}");
                throw e;
            }
        }

        public Client UpdateClient(int id, Client updateClient, ILogger<Client> logger)
        {
            try
            {
                //check that client  exists
                var existingClient = _appDbContext.Clients.Where(w => w.ObjectIdentifier == updateClient.ObjectIdentifier)
                                                  .Select(s => s).FirstOrDefault();
                if (existingClient != null)
                    throw new HttpStatusCodeException(HttpStatusCode.BadRequest, string.Format("ClientID {0},- {1} Doesn't Exist in system", updateClient.ClientId,updateClient.LastName));

                //update client
                existingClient.FirstName = updateClient.FirstName;
                existingClient.LastName = updateClient.LastName;
                existingClient.Email = updateClient.Email;
                existingClient.HomeAddress = updateClient.HomeAddress;
                existingClient.Mobile = updateClient.Mobile;

                var isOk = _appDbContext.SaveChanges();

                return existingClient;
            }
            catch (Exception e)
            {
                _logger.LogError(e, $"Error in UpdateClient: {updateClient.FirstName} - {updateClient.Email}");
            }
            return updateClient;
        }
    }
}
