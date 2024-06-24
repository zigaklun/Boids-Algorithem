using System.Diagnostics;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using BoidsAlgorith;
using System.Timers;
using System.Runtime.CompilerServices;


namespace BoidsAlgorith
{
    
    public partial class MainWindow : Window
    {

        private readonly int steviloPticev = 200;
        public Ptici[] ptici;
        private static System.Timers.Timer timer;
        private int FPS = 50;

        public MainWindow()
        {
            InitializeComponent();

            //meja kje se gibajo ptici
            RectangleGeometry meja = new RectangleGeometry();
            meja.Rect = new Rect(100, 100, 800, 400);
            Path novPath = new Path();
            novPath.Stroke = Brushes.Gray;
            novPath.StrokeThickness = 3;
            novPath.Data = meja;
            BoidsKanvas.Children.Add(novPath);


            ptici = new Ptici[steviloPticev];

            for (int i = 0; i < steviloPticev; i++)
            {
                ptici[i] = new Ptici(450, 800);
                ptici[i].Narisi(BoidsKanvas);
            }


            //15 fps => 1000ms/66
            timer = new System.Timers.Timer(1000/FPS);
            timer.Elapsed += PosodobiVse;
            timer.AutoReset = true;
            timer.Enabled = true;

        }

        private  void PosodobiVse(Object source, EventArgs e)
        {
            foreach(Ptici ptic in ptici)
            {
                ptic.Posodobi(ptici);
            }
        }
        
    }
}