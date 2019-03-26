using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Timers;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Windows.Media.Animation;

namespace ConsoleApp
{
	public class partition
	{
		private int taille;   //la taille de la repartition
		private int born_inf;
		private int vide;
		public Rectangle front;
		public Rectangle in_rec;
        public Rectangle br = new Rectangle();
        //public ProgressBar br=new ProgressBar();
       

        //l adresse de debut de la repartition
        // definez les getter de la classe 
        public partition(int adr_debut, int taille)
		{
			born_inf = adr_debut;
			this.taille = taille;
			this.vide = -1;
		}
		public partition(int adr_debut, int taille, int vide)
		{
			born_inf = adr_debut;
			this.taille = taille;
            this.vide = vide;
        }


		public int Get_taille()
		{
			return taille;
		}
		public int Get_adr()
		{
			return born_inf;
		}
		public int G_vide()
		{
			return vide;
		}
		// definez des setter 
		public void Set_taille(int taille)
		{
			this.taille = taille;
		}
		public void Set_adr(int adr_debut)
		{
			born_inf = adr_debut;
		}
		public void Set_vide(int vide)
		{
			this.vide = vide;
		}
		public void View(Canvas ram, int pos, byte color,process_list pro,int cap)
		{
			Application.Current.Dispatcher.Invoke((Action)delegate
			{
				front = new Rectangle();
                front.Width = 220;
                int pos2 = Get_taille() * 435 / cap;
                front.Height = Get_taille() * 435 / cap;
                TextBlock deb = new TextBlock();
                deb.Text = "@" + (Get_adr()+100).ToString();
                deb.Foreground = new SolidColorBrush(Color.FromRgb(62, 62, 66));
                deb.FontWeight = FontWeights.UltraBold;
                ram.Children.Add(deb);
                Canvas.SetTop(deb,pos-5);
                Canvas.SetLeft(deb,30);
                if (G_vide() < 0) {
                    front.Fill = new SolidColorBrush(Color.FromRgb(255, 255,255));
                    front.Stroke = new SolidColorBrush(Color.FromRgb(0, 255, 184));
                    DoubleAnimation db2 = new DoubleAnimation(5,1, TimeSpan.FromMilliseconds(1000));
                    front.BeginAnimation(Rectangle.StrokeThicknessProperty, db2);

                } 
                else
                {
                    Color clr = new Color();
                    clr = pro.Get_color_by_id(G_vide());

                    if (pro.Get_time_by_id(G_vide()) == 1)
                    {
                       
                        front.Fill = new SolidColorBrush(Color.FromRgb(color,color,color));
                        //Color clr = new Color();
                        clr = Color.FromRgb(color, 255,180);
                        // Create a ColorAnimation to animate the button's background.
                        ColorAnimation myColorAnimation = new ColorAnimation();
                        myColorAnimation.From = clr;
                        myColorAnimation.To = Colors.White;
                        myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(2000));
                        // Apply the animation to the brush's Color property.
                        front.Fill.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                         front.StrokeThickness = 5;
                        clr = pro.Get_color_by_id(G_vide());
                        //front.Stroke = new SolidColorBrush(Color.FromRgb(clr.R,clr.G,clr.B));
                        front.Stroke = new SolidColorBrush(Color.FromRgb(0,255,180));
                        // le rectangle de out 
                        Rectangle rec_out = new Rectangle();
                        rec_out.Width = 30;
                        rec_out.Height = 5;
                        ram.Children.Add(rec_out);
                        Canvas.SetTop(rec_out,pos2/2+pos);
                        Canvas.SetLeft(rec_out,5);
                        rec_out.Fill= new SolidColorBrush(Color.FromRgb(clr.R, clr.G, clr.B));
                        // creation d'un triangle 
                        Polygon myPolygon = new Polygon();
                       myPolygon.Fill = new SolidColorBrush(Color.FromRgb(clr.R, clr.G, clr.B));
                        System.Windows.Point Point1 = new System.Windows.Point(9,pos+ pos2 / 2-2);
                        System.Windows.Point Point2 = new System.Windows.Point(1, pos+ pos2 / 2+3);
                        System.Windows.Point Point3 = new System.Windows.Point(9, pos+ pos2 / 2+8);
                        PointCollection myPointCollection = new PointCollection();
                        myPointCollection.Add(Point1);
                        myPointCollection.Add(Point2);
                        myPointCollection.Add(Point3);
                        myPolygon.Points = myPointCollection;
                        ram.Children.Add(myPolygon);
                        //
                        //name of the processus
                        TextBlock nom = new TextBlock();
                        nom.Text = pro.Get_name_by_id(G_vide());
                        nom.Foreground = new SolidColorBrush(Colors.White);
                        nom.FontWeight = FontWeights.UltraBold;
                        nom.Width = 20;
                        nom.Height = 20;
                        ram.Children.Add(nom);
                        Canvas.SetTop(nom, pos + pos2 / 2 - 12);
                        Canvas.SetLeft(nom, 54);
                        Canvas.SetZIndex(nom, 1);


                        DoubleAnimation db2 = new DoubleAnimation(0,10, TimeSpan.FromMilliseconds(2000));
                        front.BeginAnimation(Rectangle.StrokeThicknessProperty, db2);
                        //
                        Ellipse fron = new Ellipse();
                        fron.Width = 30;
                        fron.Height = 30;
                        fron.Fill = new SolidColorBrush(Color.FromRgb(clr.R, clr.G, clr.B));
                        ram.Children.Add(fron);
                        Canvas.SetTop(fron, pos+ pos2 / 2-18);
                        Canvas.SetLeft(fron, 45);
                        System.Timers.Timer tim;
                        tim = new System.Timers.Timer(5);
                        tim.Start();
                        tim.Elapsed += delegate (object sender1, ElapsedEventArgs ev) { movEvent(sender1, ev, fron,nom, tim); };
                        tim.AutoReset = true;
                        tim.Enabled = true;

                    }
                    else if(pro.Get_fulltime_by_id(G_vide())-pro.Get_time_by_id(G_vide())==1)
                    {
                        front.Fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
                        //Color clr = new Color();
                       clr = Color.FromRgb(color,255,180);
                        // Create a ColorAnimation to animate the button's background.
                        ColorAnimation myColorAnimation = new ColorAnimation();
                        myColorAnimation.From = Colors.White;
                        myColorAnimation.To =clr;
                        myColorAnimation.Duration = new Duration(TimeSpan.FromMilliseconds(2000));
                        // Apply the animation to the brush's Color property.
                        front.Fill.BeginAnimation(SolidColorBrush.ColorProperty, myColorAnimation);
                        front.StrokeThickness =10;
                        clr = pro.Get_color_by_id(G_vide());
                        // front.Stroke = new SolidColorBrush(Color.FromRgb(clr.R, clr.G, clr.B));
                        front.Stroke = new SolidColorBrush(Color.FromRgb(0,255,180));
                        // le rectangle de out 
                        Rectangle rec_out = new Rectangle();
                        rec_out.Width = 30;
                        rec_out.Height = 5;
                        ram.Children.Add(rec_out);
                        Canvas.SetTop(rec_out, pos2/2+ pos);
                        Canvas.SetLeft(rec_out,300);
                        rec_out.Fill = new SolidColorBrush(Color.FromRgb(clr.R, clr.G, clr.B));
                        // creation d'un triangle 
                        Polygon myPolygon = new Polygon();
                        myPolygon.Fill = new SolidColorBrush(Color.FromRgb(clr.R, clr.G, clr.B));
                        System.Windows.Point Point1 = new System.Windows.Point(303, pos + pos2/2-2);
                        System.Windows.Point Point2 = new System.Windows.Point(295, pos + pos2 / 2 +3);
                        System.Windows.Point Point3 = new System.Windows.Point(303, pos + pos2 / 2 + 8);
                        PointCollection myPointCollection = new PointCollection();
                        myPointCollection.Add(Point1);
                        myPointCollection.Add(Point2);
                        myPointCollection.Add(Point3);
                        myPolygon.Points = myPointCollection;
                        ram.Children.Add(myPolygon);
                        //name of the processus
                        TextBlock nom = new TextBlock();
                        nom.Text = pro.Get_name_by_id(G_vide());
                        nom.Foreground = new SolidColorBrush(Colors.White);
                        nom.FontWeight = FontWeights.UltraBold;
                        nom.Width = 20;
                        nom.Height = 20;
                        ram.Children.Add(nom);
                        Canvas.SetTop(nom, pos+pos2/2-12);
                        Canvas.SetLeft(nom,340);
                        Canvas.SetZIndex(nom, 1);
                        //
                        DoubleAnimation db2 = new DoubleAnimation(front.StrokeThickness,0, TimeSpan.FromMilliseconds(2000));
                        front.BeginAnimation(Rectangle.StrokeThicknessProperty, db2);


                        //
                        Ellipse fron = new Ellipse();
                        fron.Width = 30;
                        fron.Height = 30;
                        fron.Fill = new SolidColorBrush(Color.FromRgb(clr.R, clr.G, clr.B));
                        ram.Children.Add(fron);
                        Canvas.SetTop(fron, pos+pos2/2-18);
                        Canvas.SetLeft(fron,333);
                        System.Timers.Timer time;
                        time = new System.Timers.Timer(5);
                        time.Start();
                        time.Elapsed += delegate (object sender1, ElapsedEventArgs ev) { movEvent(sender1,ev,fron,nom,time); };
                        time.AutoReset = true;
                        time.Enabled = true;
                        //


                    }
                    else {
                        front.Fill = new SolidColorBrush(Color.FromRgb(color,255,180));
                        TextBlock nom = new TextBlock();
                        nom.Text = pro.Get_name_by_id(G_vide());
                        nom.Foreground = new SolidColorBrush(Colors.White);
                        nom.FontWeight = FontWeights.UltraBold;
                        nom.Width = 20;
                        nom.Height = 20;
                        ram.Children.Add(nom);
                        Canvas.SetTop(nom, pos + pos2 / 2 - 12);
                        Canvas.SetLeft(nom,255);
                        Canvas.SetZIndex(nom,1);
                    }
                    Rectangle ou = new Rectangle();
                    ou.Width= (pro.Get_fulltime_by_id(G_vide()) - pro.Get_time_by_id(G_vide())) * 100 / pro.Get_fulltime_by_id(G_vide());
                    ou.Height = 5;
                    ou.Fill= new SolidColorBrush(Color.FromRgb(clr.R,clr.G,clr.B));
                    br.Width = 100;
                    br.Height = 5;
                    ou.RadiusY = 3;
                    br.RadiusX = 3;
                    ou.RadiusX = 3;
                    br.RadiusY =3;
                    //br.Foreground = new SolidColorBrush(Color.FromRgb(0,240,190));
                    br.Fill=new SolidColorBrush(Color.FromRgb(255, 255, 255));
                    ram.Children.Add(br);
                    ram.Children.Add(ou);
                    Canvas.SetTop(br, pos + pos2 / 2-4);
                    Canvas.SetLeft(br, 130);
                    Canvas.SetZIndex(br, 1);
                    Canvas.SetTop(ou, pos + pos2 / 2 - 4);
                    Canvas.SetLeft(ou, 130);
                    Canvas.SetZIndex(ou, 2);

                    //br.Value = (pro.Get_fulltime_by_id(G_vide()) - pro.Get_time_by_id(G_vide()) + 1) * 100 / pro.Get_fulltime_by_id(G_vide());
                    DoubleAnimation db = new DoubleAnimation((pro.Get_fulltime_by_id(G_vide())-pro.Get_time_by_id(G_vide())+1) * 100 / pro.Get_fulltime_by_id(G_vide()),TimeSpan.FromMilliseconds(500));
                    ou.BeginAnimation(Rectangle.WidthProperty,db);
                    //br.Value = pro.Get_time_by_id(G_vide()) * 100 / pro.Get_fulltime_by_id(G_vide());

                }
					ram.Children.Add(front);
				Canvas.SetTop(front, pos);
                Canvas.SetLeft(front,70);
            });
		}

        public void movEvent(Object source, ElapsedEventArgs ev,Ellipse e,TextBlock tt,System.Timers.Timer t)
        {
            Application.Current.Dispatcher.Invoke((Action)delegate
            {
                Canvas.SetLeft(e,Canvas.GetLeft(e)-1);
                Canvas.SetLeft(tt, Canvas.GetLeft(tt) - 1);

                if (Canvas.GetLeft(e) == 290)
                {
                    e.Fill = new SolidColorBrush(Colors.Cyan);
                }
                if (Canvas.GetLeft(e) == 250)
                {
                    t.Stop();
                }
                if (Canvas.GetLeft(e) ==0)
                {
                    e.Opacity = 0;
                    tt.Opacity=0;
                    t.Stop();
                }
                
            });
        }

    }
	public class partComparer : IComparer<int>
	{
		public int Compare(int x, int y)
		{
			return (x % 1000).CompareTo(y % 1000);
		}
	}
}

