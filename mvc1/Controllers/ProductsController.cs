using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

namespace mvc1.Controllers
{
    public class ProductsController : Controller
    {

        #region Actions
        public string Test()
        {
            return $"Hello from test";
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ContentResult Test1()
        {
            //ContentResult result = new ContentResult();
            //result.ContentType = "hext/html";
            //result.Content=$"Hello from test </br> bye";
            //result.StatusCode = StatusCodes.Status200OK;
            //return result;
            return Content(content: $"Hello from test </br> bye", contentType: "text/html");

        }

        public IActionResult Test2()
        {
            if (true)
            {
                return Content(content: $"Hello from test </br> bye", contentType: "text/html");
            }
            else
            {
                return NotFound();
            }

        }

        [HttpGet]
        public IActionResult GetProducts()
        {
            var products = new List<Products>()
            {
                new Products() {Id=1, Name="Mobile"},
                new Products(){Id=2,Name="laptop"}
            };
            return Json(products);

        }

        [NonAction]
        public IActionResult Test3()
        {
            return View();
        }
        [HttpGet]

        public IActionResult GetProductsById(int id)
        {
            if (id < 0)
            {
                return BadRequest();
            }
            if (id == 0)
            {
                return RedirectToAction(nameof(GetProducts));
            }
            else if (id < 10)
            {
                return NotFound();
            }
            else
            {
                return Json(new Products() { Id = 2, Name = "laptop" });
            }
        }

        public IActionResult RedirectAction()
        {
            return Redirect("https://drive.google.com/drive/folders/1pcik8IbJcgFYvvOGGVZgcBeZH4a2P2JT");
        }

        public IActionResult FileAction()
        {
            byte[] bytes = System.IO.File.ReadAllBytes("wwwroot/css");
            return File(bytes, "application.cs", "GroupPdf");
        }

        #endregion
        #region Routing
        [HttpGet("Test01")]
        public IActionResult TestRouting()
        {
            return Json("hello from routing");

        }

        [HttpGet("products/Test02")]
        public IActionResult TestRouting2()
        {
            return Json("hello from routing2");

        }
        [HttpGet("Test{id:int}")]
        public IActionResult TestRouting(int id)
        {
            return Json($"hello with id :{id}");

        }


        //baseurl/Products/TestRouting/10?name=shrouk
        public IActionResult TestRouting(int id,string name)
        {
            return Json($"hello with test routing:id={id},name={name}");

        }

        [HttpGet("productCode/{code:int}")]
        public IActionResult GetProductByCode(int code)
        {
            return Json(new Products() { Id = code, Name = "mobile"});

        }



        #endregion
        #region Model biniding

        //baseurl/Products/GetResultById/10?otherId=3
        //baseurl/Products/GetResultById?id=10&otherId=3
        public IActionResult GetResultById(int id , int otherId)
        {
            return Json(new { id, otherId });
        }

        //baseurl/Products/GetResultById?id=10&id2=3
        public IActionResult GetResultByIdFromQuery([FromQuery] int id, [FromQuery(Name = "id2")] int otherId)
        {
            return Json(new { id, otherId });
        }


        //Base64Url/Products/Search?pages=300&price=1200&type=electronics
        //Base64Url/Products/Search?filter.Price=300
        public IActionResult Search(ProductFilter filter)
        {
            return Content("search completed");
        }


        //baseurl/Products/GetByIds?ids=0&ids=1&ids=2
        //baseurl/Products/GetByIds?ids[0]=0&ids[1]=1&ids[2]=2
        public IActionResult GetByIds(int[]ids)
        {
            return Json(new {ids});
        }
        #endregion
    }
}
