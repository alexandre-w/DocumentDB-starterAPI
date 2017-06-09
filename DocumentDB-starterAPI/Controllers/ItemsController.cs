using System.Collections.Generic;
using System.Web.Http;
using DocumentDB_starterAPI.BusinessLayer;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.Net;
using DocumentDB_starterAPI.Filters;
using System;
using System.Web.Http.Routing;
using System.Web;

namespace DocumentDB_starterAPI.Controllers
{
    public class ItemsController : ApiController
    {
        private BLItemRequest bl ;

        const int maxPageSize = 10 ;
        public ItemsController()
        {
            bl = new BLItemRequest();
        }

        // GET: api/Items
        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            try
            {
                List<JObject> itemList = await bl.GetAll();

                return Ok(itemList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

            
        }

        // GET: api/Items/5
        [HttpGet]
        [CheckIdFilter]
        public async Task<IHttpActionResult> Get(string id)
        {
            try
            {
                JObject item = await bl.GetOne(id);
                return Ok(item);
            }
            catch
            {
                return NotFound();
            }
            
        }

        // POST: api/Items
        [HttpPost]
        public async Task<IHttpActionResult> Post([FromBody]JObject value)
        {
            try
            {
                JObject item = await bl.Insert(value);
                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, item);
                return ResponseMessage(response);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT: api/Items/5
        [HttpPut]
        [CheckIdFilter]
        public async Task<IHttpActionResult> Put(string id, [FromBody]JObject value)
        {
            try
            {
                JObject item = await bl.Update(id, value);
                return Ok(item);
            }catch
            {
                return NotFound();
            }
        }

        // DELETE: api/Items/5
        [HttpDelete]
        [CheckIdFilter]
        public async Task<IHttpActionResult> Delete(string id)
        {
            try
            {
                var returnStatusCode = await bl.Delete(id);
                return this.StatusCode(returnStatusCode);
            }catch
            {
                return BadRequest();
            }

        }
    }
}
