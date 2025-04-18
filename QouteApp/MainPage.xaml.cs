using System.Threading.Tasks;
using System.IO;
using Microsoft.Maui.Controls;

namespace QouteApp
{
    public partial class MainPage : ContentPage
    {

        List<string> movie = new List<string>();
        Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () => await LoadMauiAsset());
        }

    
        private void Button_Clicked(object sender, EventArgs e)
        {
 
            var randIndex = random.Next(movie.Count);
            lblcode.Text = movie[randIndex]; 

            var color1 = GetRandomColor();
            var color2 = GetRandomColor();

            var gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops =
                {
                    new GradientStop(color1, 0f),  
                    new GradientStop(color2, 1f)   
                }
            };


            mainLayout.Background = gradient;
        }


        private Color GetRandomColor()
        {
            return new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }


        async Task LoadMauiAsset()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("textmovie.txt");
            using var reader = new StreamReader(stream);

            while (reader.Peek() != -1)
            {
                var line = reader.ReadLine();
                if (line != null)
                {
                    movie.Add(line);
                }
            }
        }
    }
}
