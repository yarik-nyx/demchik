using Avalonia.Controls;
using demdemchikdem.db;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Linq;

namespace demdemchikdem
{
    public partial class AddEdit : Window
    {

        Agent ag = new Agent();
        public AddEdit()
        {
            
            InitializeComponent();
            Type.Items = Helper.database.Agenttypes.Select(x => x.Title);
            Title.Text = "Добавление";
            Save.Click += Save_Click;

        }

        private void Save_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ag.Title = Name.Text;
            ag.Agenttypeid = Type.SelectedIndex;
            ag.Priority = Convert.ToInt32(Priority.Text);
            ag.Logo = Logo.Text;
            ag.Address = Address.Text;
            ag.Inn = INN.Text;
            ag.Kpp = KPP.Text;
            ag.Directorname = DirName.Text;
            ag.Phone = Phone.Text;
            ag.Email = Email.Text;
            Helper.database.Agents.Add(ag);
            Helper.database.SaveChanges();
            this.Close();
        }

        public AddEdit(int id)
        {
            
            InitializeComponent();
            Save.Click += Save_Click1;  
            Type.Items = Helper.database.Agenttypes.Select(x => x.Title);
            Title.Text = "Редактирование";
            LoadData(id);

            
        }

        private void Save_Click1(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            ag.Title = Name.Text;
            ag.Agenttypeid = Type.SelectedIndex;
            ag.Priority = Convert.ToInt32(Priority.Text);
            ag.Logo = Logo.Text;
            ag.Address = Address.Text;
            ag.Inn = INN.Text;
            ag.Kpp = KPP.Text;
            ag.Directorname = DirName.Text;
            ag.Phone = Phone.Text;
            ag.Email = Email.Text;
            Helper.database.SaveChanges();
            this.Close();

        }

        public void LoadData(int id)
        {

            

            ag = Helper.database.Agents.Find(id);
            Name.Text = ag.Title;
            Type.SelectedIndex = ag.Agenttypeid;
            Priority.Text = ag.Priority.ToString();
            Logo.Text = ag.Logo;
            Address.Text = ag.Address;
            INN.Text = ag.Inn;
            KPP.Text = ag.Kpp;
            DirName.Text = ag.Directorname;
            Phone.Text = ag.Phone;
            Email.Text = ag.Email;

        }
    }
}
