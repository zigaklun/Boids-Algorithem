using System;
using System.Numerics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace BoidsAlgorith
{
    public class Ptici
    {
        public Vector hitrost;
        public Vector pozicija;
        private Polygon trikotnik;
        private static readonly Random random = new Random();
        private readonly double zasciteno_obmocje = 8;
        private readonly double faktor_odmika = 0.05;
        private readonly double faktor_ujemanja = 0.05;
        private readonly double centrirni_faktor = 0.0005;
        private readonly double faktor_obracanja = 0.2;
        private readonly double razdalja_vidlivosti = 40;

        public Ptici (double visina, double sirina)
        {
            this.pozicija = new Vector(random.NextDouble() * sirina, random.NextDouble() * visina);
            this.hitrost = new Vector((random.NextDouble() * 2) - 1, (random.NextDouble() * 2) - 1);
        }

        public void Narisi(Canvas kanvas)
        {
            
            this.trikotnik = new Polygon
            {
                Stroke = Brushes.Black, 
                Fill = Brushes.White,    
                StrokeThickness = 2     
            };

            PointCollection points = new PointCollection
            {
                new Point(pozicija.X, pozicija.Y - 7), // Zgori
                new Point(pozicija.X - 5, pozicija.Y + 5), // Spodi levo
                new Point(pozicija.X + 5, pozicija.Y + 5)  // Spodi desno
            };

            
            trikotnik.Points = points;

            
            kanvas.Children.Add(trikotnik);
        }

        public void Posodobi(Ptici[] ptici)
        {
            double blizuX = 0;
            double blizuY = 0;
            double povprecna_hitrosX = 0;
            double povprecna_hitrostY = 0;
            double povprecna_pozX = 0;
            double povprecna_pozY = 0;
            int steviloSosedov = 0;

            foreach (Ptici ptic in ptici)
            {
                if (this.Equals(ptic))
                {
                    continue;
                }

                double razdaljaMedPticema = Math.Sqrt((this.pozicija.X - ptic.pozicija.X) * (this.pozicija.X - ptic.pozicija.X) + (this.pozicija.Y - ptic.pozicija.Y) * (this.pozicija.Y - ptic.pozicija.Y));

                if (razdaljaMedPticema <= razdalja_vidlivosti)
                {
                    povprecna_hitrosX += ptic.hitrost.X;
                    povprecna_hitrostY += ptic.hitrost.Y;

                    povprecna_pozX += ptic.pozicija.X;
                    povprecna_pozY += ptic.pozicija.Y;

                    steviloSosedov++;

                    if (razdaljaMedPticema <= zasciteno_obmocje)
                    {
                        blizuX += this.pozicija.X - ptic.pozicija.X;
                        blizuY += this.pozicija.Y - ptic.pozicija.Y;
                    }
                }
            }

            if (steviloSosedov > 0)
            {
                povprecna_hitrosX /= steviloSosedov;
                povprecna_hitrostY /= steviloSosedov;

                povprecna_pozX /= steviloSosedov;
                povprecna_pozY /= steviloSosedov;

                
                this.hitrost.X += (povprecna_hitrosX - this.hitrost.X) * faktor_ujemanja
                                + (povprecna_pozX - this.pozicija.X) * centrirni_faktor;
                this.hitrost.Y += (povprecna_hitrostY - this.hitrost.Y) * faktor_ujemanja
                                + (povprecna_pozY - this.pozicija.Y) * centrirni_faktor;
            }

            this.hitrost.X += blizuX * faktor_odmika;
            this.hitrost.Y += blizuY * faktor_odmika;


            // Zavij nazaj v mejo
            if (this.pozicija.X < 100)
            {
                this.hitrost.X += faktor_obracanja;
            }
            if (this.pozicija.X > 900)
            {
                this.hitrost.X -= faktor_obracanja;
            }
            if (this.pozicija.Y < 100)
            {
                this.hitrost.Y += faktor_obracanja;
            }
            if (this.pozicija.Y > 500)
            {
                this.hitrost.Y -= faktor_obracanja;
            }

            // Speed limit
            double speed = Math.Sqrt(this.hitrost.X * this.hitrost.X + this.hitrost.Y * this.hitrost.Y);
            if (speed > 6)
            {
                this.hitrost.X = (this.hitrost.X / speed) * 6;
                this.hitrost.Y = (this.hitrost.Y / speed) * 6;
            }
            else if (speed < 3)
            {
                this.hitrost.X = (this.hitrost.X / speed) * 3;
                this.hitrost.Y = (this.hitrost.Y / speed) * 3;
            }

            
            this.pozicija.X += this.hitrost.X;
            this.pozicija.Y += this.hitrost.Y;

            double kot = Math.Atan2(hitrost.Y, hitrost.X) * (180 / Math.PI) + 90; 

            Application.Current.Dispatcher.Invoke(() =>
            {
                PointCollection tocke = new PointCollection
                {
                    new Point(pozicija.X, pozicija.Y - 7), // Zgori
                    new Point(pozicija.X - 5, pozicija.Y + 5), // Spodi levo
                    new Point(pozicija.X + 5, pozicija.Y + 5)  // Spodi desno
                };
                trikotnik.Points = tocke;

                RotateTransform rotateTransform = new RotateTransform(kot, this.pozicija.X, this.pozicija.Y);
                trikotnik.RenderTransform = rotateTransform;
            });
        }
    }
}
