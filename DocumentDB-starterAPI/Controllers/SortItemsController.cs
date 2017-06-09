using DocumentDB_starterAPI.BusinessLayer;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;

namespace DocumentDB_starterAPI.Controllers
{
    public class SortItemsController : ApiController
    {
        private BLItemRequest bl;
        public SortItemsController()
        {
            bl = new BLItemRequest();
        }

        // GET: /api/sortitems?sort=id
        [HttpGet]
        [Route("api/sortitems", Name = "SortItemsList")]
        public async Task<IHttpActionResult> Get(string sort = "id", int page = 1, int pageSize = 5)
        {
            try
            {            
                //Get the items list and the total Count
                Tuple<List<JObject>,int> itemQuery = await bl.GetAllSort(sort, page, pageSize);

                var totalPages = (int)Math.Ceiling((double)itemQuery.Item2 / pageSize);

                var urlHelper = new UrlHelper(Request);


                //Previous page
                var prevLink = page > 1 ? urlHelper.Link("SortItemsList",
                    new
                    {
                        page = page - 1,
                        pageSize = pageSize,
                        sort = sort ,
                    }) : "";

                //Next page
                var nextLink = page < totalPages ? urlHelper.Link("SortItemsList",
                   new
                   {
                       page = page + 1,
                       pageSize = pageSize,
                       sort = sort,
                   }) : "";



                //Creation of the pagination to put in the request's header
                var paginationHeader = new
                {
                    currentPage = page,
                    pageSize = pageSize,
                    totalCount = itemQuery.Item2,
                    totalPages = totalPages,
                    prevPageLink = prevLink,
                    nextPageLink = nextLink
                };

                HttpContext.Current.Response.Headers.Add("X-Pagination", Newtonsoft.Json.JsonConvert.SerializeObject(paginationHeader));


                return Ok(itemQuery.Item1);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

    }
}
