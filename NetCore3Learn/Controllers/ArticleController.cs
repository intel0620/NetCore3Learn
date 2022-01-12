using Microsoft.AspNetCore.Mvc;
using NetCore3Learn.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCore3Learn.Controllers
{
    [Route("[controller]")]
    public class ArticleController : Controller
    {
        [Route("[Action]/{name?}")]
        [Route("~/slcopipi/{name?}", Order = 9)]// Order 預設是0 越低越先
        [Route("~/sanqq/{name?}")]
        public IActionResult Index(string name)
        {
            var bookList = GetSampleData();

            if (string.IsNullOrWhiteSpace(name))
            {
                ViewData.Model = bookList;
            }
            else
            {
                var listdata = bookList.Where(d => d.Name.Contains(name));
                //if (!string.IsNullOrWhiteSpace(author))
                //{
                //    listdata = listdata.Where(d => d.Author.Contains(author));
                //}

                ViewData.Model = listdata;
                //ViewData.Model = coachList.Where(d => d.Name == name);
            }
            return View();
        }



        private IEnumerable<BooksViewModel> GetSampleData()
        {
            return new List<BooksViewModel>
                            {
                                 new BooksViewModel
                                {
                                    Name  = "原子習慣(上集)：細微改變帶來巨大成就的實證法則"
                                  , Author="詹姆斯哥哥"
                                  , Introduction = @"每天都進步1%，一年後，你會進步37倍；</br>每天都退步1%，一年後，你會弱化到趨近於0！</br> 你的一點小改變、一個好習慣，將會產生複利效應，</br>如滾雪球般，為你帶來豐碩的人生成果！</br>"
                                  , Picture =
                                        "https://www.books.com.tw/img/001/082/25/0010822522.jpg"
                                }
                              ,
                                new BooksViewModel
                                {
                                    Name  = "原子習慣(下集)：細微改變帶來巨大成就的實證法則"
                                  , Author="詹姆斯弟弟"
                                  , Introduction = @"每天都進步1%，一年後，你會進步37倍；</br>每天都退步1%，一年後，你會弱化到趨近於0！</br> 你的一點小改變、一個好習慣，將會產生複利效應，</br>如滾雪球般，為你帶來豐碩的人生成果！</br>"
                                  , Picture =
                                        "https://www.books.com.tw/img/001/082/25/0010822522.jpg"
                                }
                              , new BooksViewModel
                                {
                                    Name  = "超速學習：我這樣做，一個月學會素描，一年學會四種語言，完成MIT四年課程"
                                  , Author="史考特‧楊"
                                  , Introduction = @"跟著學習之神，依循「超速學習」九大法則，</br>在短時間內學會任何你想學的，就算再難的技能，也不是難事！"
                                  , Picture =
                                        "https://www.books.com.tw/img/001/085/58/0010855836.jpg"
                                }
                              , new BooksViewModel
                                {
                                    Name  = "刻意練習：原創者全面解析，比天賦更關鍵的學習法"
                                  , Author="安德斯‧艾瑞克森, 羅伯特‧普爾 "
                                  , Introduction = @"找到天賦，不如找對方法！</br>天才與庸才之間的差別不在基因、不在天分，在「刻意練習」！"
                                  , Picture =
                                        "https://www.books.com.tw/img/001/075/27/0010752714.jpg"
                                }
                            };
        }


    }
}
