using Avalonia.Controls;
using demdemchikdem.db;
using System.Collections.Generic;
using System.Linq;

namespace demdemchikdem
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitEvents();
            LoadData();
            
        }
        public void InitEvents()
        {
            
            Search.KeyUp += Search_KeyUp;

            Add.Click += Add_Click;

            Filter.SelectionChanged += Filter_SelectionChanged;
            Filter.Items = new List<string>() { 
                "Все типы",
                "Наименование А - Я", 
                "Наименование Я - А", 
                "Размер скидки 0 - 100", 
                "Размер скидки 100 - 0", 
                "Приоритет агента 0 - 100",
                "Приоритет агента 100 - 0"
            };
            Filter.SelectedIndex = 0;

            Sort.SelectionChanged += Sort_SelectionChanged;
            List<string> query = Helper.database.Agenttypes.Select(x => x.Title).ToList();
            query.Insert(0, "Все типы");
            Sort.Items = query;
            Sort.SelectedIndex = 0;
        }

        private async void Add_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            AddEdit win = new AddEdit();
            await win.ShowDialog(this);
            LoadData();
        }

        private async void Edit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int id = (int)(sender as Button).Tag;
            AddEdit win = new AddEdit(id);
            await win.ShowDialog(this);
            LoadData();

        }

        private void Delete_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            int id = (int)(sender as Button).Tag;
            Agent ag = Helper.database.Agents.FirstOrDefault(x => x.Id == id);
            Helper.database.Agents.Remove(ag);
            Helper.database.SaveChanges();
            LoadData();

        }

        private void Sort_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void Filter_SelectionChanged(object? sender, SelectionChangedEventArgs e)
        {
            LoadData();
        }

        private void Search_KeyUp(object? sender, Avalonia.Input.KeyEventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            var query = Helper.database.Agents.ToList();
            List<MyAgent> mg = new List<MyAgent>();

            if (!string.IsNullOrEmpty(Search.Text))
            {
                string text = Search.Text.Trim().ToLower();
                query = query.Where(x => x.Title is not null && x.Title
                .Trim()
                .ToLower()
                .Contains(Search.Text) || 
                x.Phone is not null && x.Phone
                .Trim()
                .ToLower()
                .Contains(text) || 
                x.Email is not null && x.Email
                .Trim()
                .ToLower()
                .Contains(text)).ToList();
            }

            mg = query.Select(x => new MyAgent() 
            { 
                Id = x.Id,
                Title = x.Title,
                Agenttypeid = x.Agenttypeid,
                Address = x.Address,
                Inn = x.Inn,
                Kpp= x.Kpp,
                Directorname= x.Directorname,
                Phone  = x.Phone,
                Email= x.Email,
                Logo = x.Logo,               
                Priority = x.Priority,
            }).ToList();


            if (Filter.SelectedIndex == 1)
            {
                mg = mg.OrderBy(x => x.Title).ToList();
            }
            else if (Filter.SelectedIndex == 2)
            {
                mg = mg.OrderByDescending(x => x.Title).ToList();
            }
            else if (Filter.SelectedIndex == 3)
            {
                mg = mg.OrderBy(x => x.Discount).ToList();
            }
            else if (Filter.SelectedIndex == 4)
            {
                mg = mg.OrderByDescending(x => x.Discount).ToList();
            }
            else if (Filter.SelectedIndex == 5)
            {
                mg = mg.OrderBy(x => x.Priority).ToList();
            }
            else if (Filter.SelectedIndex == 6)
            {
                mg = mg.OrderByDescending(x => x.Priority).ToList();
            }


            if (Sort.SelectedIndex == 1)
            {
                mg = mg.Where(x => x.Agenttypeid == 1).ToList();
            }
            else if (Sort.SelectedIndex == 2)
            {
                mg = mg.Where(x => x.Agenttypeid == 2).ToList();
            }
            else if (Sort.SelectedIndex == 3)
            {
                mg = mg.Where(x => x.Agenttypeid == 3).ToList();
            }
            else if (Sort.SelectedIndex == 4)
            {
                mg = mg.Where(x => x.Agenttypeid == 4).ToList();
            }
            else if (Sort.SelectedIndex == 5)
            {
                mg = mg.Where(x => x.Agenttypeid == 5).ToList();
            }
            else if (Sort.SelectedIndex == 6)
            {
                mg = mg.Where(x => x.Agenttypeid == 6).ToList();
            }


            Lbox.Items = mg;
        }
    }
}