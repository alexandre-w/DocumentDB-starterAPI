using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace DocumentDB_starterAPI.DAL.Request
{
    public interface IRequest 
    {

        /// <summary>
        /// Get all Documents from your Collection
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<JObject>> GetAll();

        /// <summary>
        /// Get a document from your Collection with an ID
        /// </summary>
        /// <param name="id">ID of the Document</param>
        /// <returns></returns>
        Task<JObject> GetOne(string id);

        /// <summary>
        /// Insert a new Document 
        /// </summary>
        /// <param name="jDoc">Document to insert</param>
        /// <returns></returns>
        Task<JObject> Insert(JObject jObj);

        /// <summary>
        /// Update a document
        /// </summary>
        /// <param name="jDoc">Document to update</param>
        /// <returns></returns>
        Task<JObject> Update(string id, JObject jObj);

        /// <summary>
        /// Delete a document with its ID
        /// </summary>
        /// <param name="id">ID of the Document</param>
        /// <returns></returns>
        Task<HttpStatusCode> Delete(string id);

    }
}