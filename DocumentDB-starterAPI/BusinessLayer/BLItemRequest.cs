using System.Collections.Generic;
using DocumentDB_starterAPI.DAL.Request;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using System.Net;

namespace DocumentDB_starterAPI.BusinessLayer
{
    public class BLItemRequest : IRequest
    {
        private IRequest request;

        public BLItemRequest()
        {
            request = new RepositoryRequest();
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            return await request.Delete(id);
        }

        public async Task<IEnumerable<JObject>> GetAll()
        {
            return await request.GetAll();
        }

        public async Task<JObject> GetOne(string id)
        {
            return await request.GetOne(id);
        }

        public async Task<JObject> Insert(JObject jObj)
        {
            return await request.Insert(jObj);
        }

        public async Task<JObject> Update(string id, JObject jObj)
        {
            return await request.Update(id, jObj);
        }
    }
}