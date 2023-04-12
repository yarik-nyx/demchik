using Avalonia.Media;
using Avalonia.Media.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demdemchikdem
{
    internal class MyAgent
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public int Agenttypeid { get; set; }

        public string? Agenttype 
        {
            get {
                if (Agenttypeid == 1)
                {
                    return "ЗАО";
                }
                else if (Agenttypeid == 2)
                {
                    return "МКК";
                }
                else if (Agenttypeid == 3)
                {
                    return "МФО";
                }
                else if (Agenttypeid == 4)
                {
                    return "ОАО";
                }
                else if (Agenttypeid == 5)
                {
                    return "ООО";
                }
                else if (Agenttypeid == 6)
                {
                    return "ПАО";
                }
                else
                {
                    return "xz";
                }
            } 
        }
        public string? Address { get; set; }

        public string Inn { get; set; } = null!;

        public string? Kpp { get; set; }

        public string? Directorname { get; set; }

        public string? Phone { get; set; }

        public string? Email { get; set; }

        public string? Logo { get; set; }

        public Bitmap? Img
        {
            get
            {
                if (!File.Exists($"..\\..\\..\\{Logo}"))
                {
                    return new Bitmap("..\\..\\..\\agents\\picture.png");
                }
                else
                {
                    return new Bitmap($"..\\..\\..\\{Logo}");
                }

            }
        }

        public int Priority { get; set; }

        public int Sales 
        {
            get {
                var query = Helper.database.Productsales.ToList();
                if (query.Any(x => x.Agentid == Id) == true)
                {
                    var cost = from ps in Helper.database.Productsales
                               join a in Helper.database.Agents on ps.Agentid equals a.Id
                               join p in Helper.database.Products on ps.Productid equals p.Id
                               where ps.Agentid == Id
                               select new {productcount = ps.Productcount * p.Mincostforagent };
                    return Convert.ToInt32(cost.Sum(x => x.productcount));
                               
                }
                else
                {
                    return 0;
                }
            }
        }

        public int Discount
        {
            get
            {
                if (Sales < 10000)
                {
                    return 0;
                }
                else if (Sales >= 10000 && Sales < 50000)
                {
                    return 5;
                }
                else if (Sales >= 50000 && Sales < 150000)
                {
                    return 10;
                }
                else if (Sales >= 150000 && Sales < 500000)
                {
                    return 20;
                }
                else
                {
                    return 25;
                }

            }
        }



        public string DiscountStr
        {
            get {
                return Discount + "%";
            }
        }

        public string SalesStr
        {
            get {
                return Sales + " продаж";
            }    
        }

        public string PriorityStr 
        { 
            get {
                return "Приоритетность: " + Priority;             
            } 
        }

        public Avalonia.Media.ISolidColorBrush Color
        {
            get
            {
                if (Discount > 24)
                {
                    return Avalonia.Media.Brushes.LightGreen;
                }
                else
                {
                    return Avalonia.Media.Brushes.Transparent;
                }
            }
        }
    }
}
