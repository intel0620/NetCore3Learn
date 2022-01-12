using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NetCore3Learn.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace NetCore3Learn.Controllers
{
    public class HomeController : Controller
    {
      //  private readonly ILogger<HomeController> _logger;

        protected readonly AppSettingsModel _appSettings;
        private readonly IDbConnection _connection;
        private readonly OthierModel _otherModell;
        private readonly IConfiguration _configuration;

        public HomeController(IConfiguration configuration ,IOptions<AppSettingsModel> appSettings , IDbConnection connection , IOptions<OthierModel> otherModell)
        {
            _configuration = configuration;
            _connection = connection;
            _otherModell = otherModell.Value;
        }


        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            string A = _otherModell.AA_Second;
            string B = _otherModell.BB_Second;
            string C = _otherModell.CC_Second;

            using (_connection)
            {
                //�d�ߤ@
                string sql = $@"SELECT TOP (10) [Id],[Title],[Body],[CoverPhoto],[CreateDate],[DayOfWeek],[Tags]
                           FROM [Articles] WITH(NOLOCK)";
                var data = _connection.Query<ArticlesModel>(sql).ToList();
                //dosomething



                //�d�ߤG
                string Id = "F2061DF0-EB6F-4019-A21A-1F0FB998E1CF"; //��������~��ȤW���n����
                string sql2 = @"SELECT [Id],[Title],[Body],[CoverPhoto],[CreateDate],[DayOfWeek],[Tags]
                             FROM [Articles] WITH(NOLOCK)
                             WHERE Id= @Id";

                var dynamicParams = new DynamicParameters();//�ʺA�Ѽ�
                dynamicParams.Add("Id", Id);


                var testdata = _connection.Query<ArticlesModel>(sql2, new { Id }).ToList();
                var testdata2 = _connection.Query<ArticlesModel>(sql2, new { Id = Id }).ToList();
                var testdata3 = _connection.Query<ArticlesModel>(sql2, dynamicParams);
                //dosomething

            }



            //�d�ߤG

            //using (var connection = new SqlConnection(_configuration.GetConnectionString("DefaultConnection")))
            //{
            //    connection.Open();
            //    string Id = "F2061DF0-EB6F-4019-A21A-1F0FB998E1CF"; //��������~��ȤW���n����
            //    string sql2 = @"SELECT [Id],[Title],[Body],[CoverPhoto],[CreateDate],[DayOfWeek],[Tags]
            //                 FROM [Articles] WITH(NOLOCK)
            //                 WHERE Id= @Id";

            //    var dynamicParams = new DynamicParameters();//�ʺA�Ѽ�
            //    dynamicParams.Add("Id", Id);



            //    var testdata = connection.Query<ArticlesModel>(sql2, new { Id }).ToList();

            //    var testdata2 = connection.Query<ArticlesModel>(sql2, new { Id = Id }).ToList();

            //    var testdata3 = connection.Query<ArticlesModel>(sql2, dynamicParams).ToList();

            //    //dosomething
            //}


            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
