using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using DocumentDB_starterAPI.DAL.Repository;
using DocumentDB_starterAPI.Helpers;
using Microsoft.Azure.Documents;
using System.Net;
using System;

namespace DocumentDB_starterAPI.DAL.Request
{
    public class RepositoryRequest : IRequest
    {
        public async Task<HttpStatusCode> Delete(string id)
        {
            return await DocumentDBRepository<JObject>.DeleteDocument(id);
        }

        public async Task<List<JObject>> GetAll()
        {
            List<JObject> itemList = await DocumentDBRepository<JObject>.GetAllDocumentsAsync();
            List<JObject> cleanItems = JsonHelper.cleanJObjectList(itemList);

            return cleanItems;
        }

        public async Task<Tuple<List<JObject>, int>> GetAllSort(string sort, int page , int pageSize )
        {
            Tuple<List<JObject>, int> tupleQuery = await DocumentDBRepository<JObject>.GetAllSortedDocumentsAsync(sort, page, pageSize);
            List<JObject> cleanItems = JsonHelper.cleanJObjectList(tupleQuery.Item1);

            return tupleQuery;
        }

        public async Task<JObject> GetOne(string id)
        {
            Document doc = await DocumentDBRepository<JObject>.GetOneDocumentAsync(id);
            JObject item = JObject.FromObject(doc);
            JObject cleanItem = JsonHelper.cleanJObject(item);

            return cleanItem;
        }

        public async Task<JObject> Insert(JObject jObj)
        {
            Document doc = await DocumentDBRepository<JObject>.InsertDocument(jObj);
            JObject item = JObject.FromObject(doc);
            JObject cleanItem = JsonHelper.cleanJObject(item);

            return cleanItem;
        }

        public async Task<JObject> Update(string id, JObject jObj)
        {
            Document doc = await DocumentDBRepository<JObject>.UpdateDocument(id, jObj);
            JObject item = JObject.FromObject(doc);
            JObject cleanItem = JsonHelper.cleanJObject(item);

            return cleanItem;
        }
    }
}