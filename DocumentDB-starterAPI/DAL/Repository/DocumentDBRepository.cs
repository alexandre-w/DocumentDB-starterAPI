using DocumentDB_starterAPI.Common;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace DocumentDB_starterAPI.DAL.Repository
{

    public static class DocumentDBRepository<T> where T : class
    {
        //Name of the database
        private static readonly string databaseName = ConfigurationManager.AppSettings["database"];
        //Name of the collection into the database
        private static readonly string collectionName = ConfigurationManager.AppSettings["collection"];
        //This variable is used to configure and execute requests
        private static DocumentClient client;

        /// <summary>
        /// Function to initialize the connection to DocumentDB
        /// </summary>
        public static void Initialize()
        {
            client = new DocumentClient(new Uri(ConfigurationManager.AppSettings["endpoint"]), ConfigurationManager.AppSettings["authKey"]);
            CheckDatabaseIfExistsAsync().Wait();
            CheckCollectionIfExistsAsync().Wait();
        }

        /// <summary>
        /// Check if the database exists in your Azure account 
        /// </summary>
        /// <returns></returns>
        private static async Task CheckDatabaseIfExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
            }
            catch (DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD01, ex);
                throw;
            }
        }

        /// <summary>
        /// Check if the collection exists in your Azure account
        /// </summary>
        /// <returns></returns>
        private static async Task CheckCollectionIfExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName));
            }
            catch (DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD02, ex);
                throw;
            }
        }

        /// <summary>
        /// Search a list of Document thanks to a Linq Expression
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> GetDocumentsAsync(Expression<Func<T, bool>> predicate)
        {
            try
            {
                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName))
                    .Where(predicate)
                    .AsDocumentQuery();

                List<T> results = new List<T>();
                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                return results;
            }
            catch (DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD07, ex);
                throw;
            }
        }

        /// <summary>
        /// Get all Documents from your Collection
        /// </summary>
        /// <returns></returns>
        public static async Task<IEnumerable<T>> GetAllDocumentsAsync()
        {
            try
            {
                IDocumentQuery<T> query = client.CreateDocumentQuery<T>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName)).AsDocumentQuery();

                List<T> results = new List<T>();

                while (query.HasMoreResults)
                {
                    results.AddRange(await query.ExecuteNextAsync<T>());
                }

                return results;
            }catch(DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD08, ex);
                throw;
            }
        }

        /// <summary>
        /// Get a document from your Collection with an ID
        /// </summary>
        /// <param name="id">ID of the Document</param>
        /// <returns></returns>
        public static async Task<Document> GetOneDocumentAsync(string id)
        {
            try
            {
                ResourceResponse<Document> document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, id));
                Document result = document.Resource;
                return result;
            }
            catch (DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD03, ex);
                throw;
            }
        }

        /// <summary>
        /// Insert a new Document 
        /// </summary>
        /// <param name="jDoc">Document to insert</param>
        /// <returns></returns>
        public static async Task<Document> InsertDocument(JObject jDoc)
        {
            try
            {
                ResourceResponse<Document> query = await client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), jDoc);
                return query.Resource;
            }
            catch (DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD04, ex);
                throw;
            }
        }

        /// <summary>
        /// Update a document
        /// </summary>
        /// <param name="jDoc">Document to update</param>
        /// <returns></returns>
        public static async Task<Document> UpdateDocument(string id, JObject jDoc)
        {
            try
            {
                ResourceResponse<Document> query = await client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, id), jDoc);
                return query.Resource;
            }
            catch (DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD05, ex);
                throw;
            }
        }

        /// <summary>
        /// Delete a document with its ID
        /// </summary>
        /// <param name="id">ID of the Document</param>
        /// <returns></returns>
        public static async Task<HttpStatusCode> DeleteDocument(string id)
        {
            try
            {
                var query = await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(databaseName, collectionName, id));
                return query.StatusCode;

            }catch(DocumentClientException ex)
            {
                App.Log(client.GetType()).Error(Helpers.errorMsg.BDD06, ex);
                throw;
            }
            
        }

    }
}