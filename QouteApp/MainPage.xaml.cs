using System.Threading.Tasks;
using System.IO;
using Microsoft.Maui.Controls;

namespace QouteApp
{
    public partial class MainPage : ContentPage
    {
        // لیست جملات رندوم
        List<string> movie = new List<string>();
        Random random = new Random();

        public MainPage()
        {
            InitializeComponent();
        }

        // این متد به محض نمایش صفحه، جملات را بارگذاری می‌کند
        protected override void OnAppearing()
        {
            base.OnAppearing();
            Task.Run(async () => await LoadMauiAsset());
        }

        // متد برای کلیک کردن دکمه و انتخاب جمله رندوم
        private void Button_Clicked(object sender, EventArgs e)
        {
            // انتخاب یک جمله رندوم از لیست
            var randIndex = random.Next(movie.Count);
            lblcode.Text = movie[randIndex];  // نمایش جمله رندوم

            // تولید دو رنگ رندوم
            var color1 = GetRandomColor();
            var color2 = GetRandomColor();

            // اعمال رنگ‌ها به گرادیانت
            var gradient = new LinearGradientBrush
            {
                StartPoint = new Point(0, 0),
                EndPoint = new Point(1, 1),
                GradientStops =
                {
                    new GradientStop(color1, 0f),  // تغییر به float
                    new GradientStop(color2, 1f)   // تغییر به float
                }
            };

            // اعمال گرادیانت به پس‌زمینه
            mainLayout.Background = gradient;
        }

        // تابع برای تولید رنگ رندوم
        private Color GetRandomColor()
        {
            return new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
        }

        // بارگذاری جملات از فایل
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
