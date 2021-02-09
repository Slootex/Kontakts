using CsvHelper;
using IronPython.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace kontakte
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public static int height = (int)SystemParameters.PrimaryScreenHeight / 2;
        public static int width = (int)SystemParameters.PrimaryScreenWidth / 2;
        public static List<TextBox> tb = new List<TextBox>();
        
        public MainWindow()
        {
            InitializeComponent();

            //Setup the Frame

            Application.Current.MainWindow.Height = height;
            Application.Current.MainWindow.Width = width;
            Application.Current.MainWindow.Top = height / 2;
            Application.Current.MainWindow.Left = width / 2;



            //read kontakt file
            var column1 = new List<string>();
            var column2 = new List<string>();
            var column3 = new List<string>();

            List<string> names = new List<string>();
            List<string> lastnames = new List<string>();
            List<string> number = new List<string>();

            using (var rd = new StreamReader(@"C:\test.csv"))
            {
                while (!rd.EndOfStream)
                {
                    var splits = rd.ReadLine().Split(';');
                    column1.Add(splits[0]);
                    column2.Add(splits[1]);
                    column3.Add(splits[2]);
                }
            }
            
           
            foreach (var element in column1)
                names.Add(element);
                

           
            foreach (var element in column2)
                lastnames.Add(element);

            
            foreach (var element in column3)
                number.Add(element);

            //create each text for the kontakt

            int q = names.Count;



            Console.WriteLine(names.Count);
            int i = 0;
            List<items> itt = new List<items>();
            while (i <= q -1)
            {

                itt.Add(new items { Name = names[i], Nachname = lastnames[i], Nummer = number[i] });
                i++;
                
            }
            kontakts.ItemsSource = itt;
            







        }

        

        class items
        {
            public string Name { get; set; }
            public string Nachname { get; set; }
            public string Nummer { get; set; }

        }

        private async void textchange1(object sender, TextChangedEventArgs e)
        {
            
            TextBox tv = sender as TextBox;
            Console.WriteLine(tv.Text.Length);
            if (tv.Text.Length >= 16)
            {
                tv.Text = tv.Text.Substring(0, (tv.Text.Length - 1));
                pu.Margin = new Thickness(50, 50, 50, 50);
                pu.Placement = PlacementMode.MousePoint;
                putxt.FontSize = 14;
                putxt.Foreground = Brushes.DarkRed;
                putxt.Background = Brushes.LightGoldenrodYellow;
                putxt.Text = "Du hast die maximale anzahl an zeichen erreicht!";
                pu.IsOpen = true;
                await Task.Delay(3000);
                pu.IsOpen = false;
                
            } 

            if(tv.Text.Length <= 16)
            {
                tv.Width = tv.Text.Length * 9.5;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            int i = 0;
            
            int size = kontakts.Items.Count - 2;
            while(i <= size)
            {

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(@"C:\test.csv", true))
                {
                    string st = kontakts.SelectedItem as string;
                    
                    Console.WriteLine(st);
                }

                i++;
            }



            



        }


    }
        
}
