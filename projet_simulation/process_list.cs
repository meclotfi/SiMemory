using System;
using System.Threading;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Timers;
using System.Linq;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;

namespace ConsoleApp
{
	public class process_list 
	{
		public List<processus> en_cours;
		public List<processus> fifo;
        public List<processus> fini;
        public static int finis;
        //public EventHandler OnTimedEvent = new EventHandler();
       public int get_finis()
        {
            return finis;
        }
        public process_list()
		{
            fifo = new List<processus>();
			en_cours = new List<processus>();
            fini = new List<processus>();
        }
		public void execute_process(int id,int rep)
		{
			processus pro = new processus("",id, 0, 0);
			pro = fifo.Find(x => x.id == id);
			fifo.Remove(pro);
			pro.Set_etat(rep);
			en_cours.Add(pro);

		}
		public void ajout_process(string name,int taille, int temp)
		{
			processus pro = new processus(name,fifo.Count + en_cours.Count + 1, taille, temp);
			fifo.Add(pro);

		}
		public void afficher_encours(Canvas F,int P)
		{
            int pos=0;
			int i = -1;
            List<processus> all = new List<processus>();
            switch (P)
            {
                default:
                    all.AddRange(en_cours);
                    all.AddRange(fifo);
                    all.AddRange(fini);
                    
                    break;
                case 1:
                    all.AddRange(en_cours);
                    all.AddRange(fifo);
                    all.AddRange(fini);
                   
                    break;
                case 2:
                    all.AddRange(en_cours);
                    break;
                case 3:
                    all.AddRange(fifo);
                    break;
                case 4:
                    all.AddRange(fini);
                    break;


            }
            all.Sort();
           
			while (true)
			{
				i++;
				if ((i == all.Count)||(i==7)) break;
             
                TextBlock nom = new TextBlock();
                nom.Text = all[i].name;
                nom.FontWeight = FontWeights.UltraBold;
                nom.FontSize = 16;
                //nom.Foreground = new SolidColorBrush(Color.FromRgb(0,255,184));
                nom.Width = 40;
                nom.Height = 30;
                nom.TextAlignment = TextAlignment.Center;
                F.Children.Add(nom);
                Canvas.SetTop(nom, pos);
                Canvas.SetLeft(nom,25);
                //
                TextBlock info = new TextBlock();
                info.Text = "taille : "+ all[i].Get_taille()+"    temps : "+ (all[i].full_time-1);
                info.FontSize = 10;
                //info.FontWeight = FontWeights.UltraBold;
                info.Foreground = new SolidColorBrush(Color.FromRgb(113,113,113));
                info.Width = 205;
                info.Height =15;
                info.TextAlignment = TextAlignment.Center;
                F.Children.Add(info);
                Canvas.SetTop(info, pos + 5);
                Canvas.SetLeft(info,50);
                //the progress bar field
                Rectangle prog= new Rectangle();
                Rectangle rec= new Rectangle();
                rec.Height = 5;
                prog.Height = 1;
                prog.Width = 240;
                prog.Stroke =null;
                prog.Fill= new SolidColorBrush(Color.FromRgb(62,62,66));
                rec.Fill = new SolidColorBrush(Color.FromRgb(0,255,184));
                F.Children.Add(prog);
                F.Children.Add(rec);
                Canvas.SetTop(prog, pos +25);
                Canvas.SetLeft(prog,30);
                Canvas.SetTop(rec, pos + 23);
                Canvas.SetLeft(rec, 30);

                /*Thickness fg = new Thickness();
                fg.Bottom = 2;
                fg.Top = 2;
                prog.BorderThickness = fg;
                */
                Ellipse et = new Ellipse();
                et.Height = 10;
                et.Width = 10;
                //et.Fill = new SolidColorBrush(Colors.Green);
                F.Children.Add(et);
                Canvas.SetTop(et, pos + 10);
                Canvas.SetLeft(et,255);
                pos += 32;
                nom.Foreground = new SolidColorBrush(Color.FromRgb(all[i].clr.R, all[i].clr.G, all[i].clr.B));
                if (all[i].full_time == all[i].Get_temps())
                {
                    // prog.Value = 0;
                    rec.Width = 0;
                    et.Fill = new SolidColorBrush(Colors.Red);
                    nom.Foreground = new SolidColorBrush(Color.FromRgb(all[i].clr.R, all[i].clr.G, all[i].clr.B));
                    //prog.Foreground= new SolidColorBrush(Colors.Yellow);

                }
                else
                {
                    if (all[i].Get_temps() == 0)
                    {
                        et.Fill = new SolidColorBrush(Colors.Yellow);
                        //prog.Value = 0;
                        rec.Width = 0;
                        nom.Foreground = new SolidColorBrush(Color.FromRgb(all[i].clr.R, all[i].clr.G,all[i].clr.B));
                        //prog.Foreground = new SolidColorBrush(Colors.Black);

                        //bord.Stroke = new SolidColorBrush(Color.FromRgb(113, 113, 113));
                    }
                    else
                    {
                        DoubleAnimation db = new DoubleAnimation((all[i].full_time - all[i].Get_temps()) *240 / all[i].full_time, (all[i].full_time - all[i].Get_temps() + 1) *240/ all[i].full_time, TimeSpan.FromMilliseconds(500));
                        rec.BeginAnimation(Rectangle.WidthProperty, db);
                        
                        info.Text += "  temps restants : " + (all[i].Get_temps()-1);
                        et.Fill = new SolidColorBrush(Color.FromRgb(0,255,184));
                        if (all[i].Get_temps() == 1)
                        {
                           /* DoubleAnimation db1 = new DoubleAnimation(3, 30, TimeSpan.FromMilliseconds(1000));
                            bord.BeginAnimation(Rectangle.StrokeThicknessProperty, db1);
                            DoubleAnimation db2 = new DoubleAnimation(30, 3, TimeSpan.FromMilliseconds(1000));
                            bord.BeginAnimation(Rectangle.StrokeThicknessProperty, db2);*/
                            et.Fill = new SolidColorBrush(Colors.Yellow);
                            nom.Foreground = new SolidColorBrush(Color.FromRgb(113, 113, 113));
                        }
                    }

                }


            }
		}
		public void afficher_fifo(Canvas att)
		{
			int i=0,pos=1;
			Application.Current.Dispatcher.Invoke((Action)delegate
			{
				while (i < fifo.Count)
				{
                    TextBlock nom = new TextBlock();
                    nom.Text = fifo[i].name;
                    nom.Width = 20;
                    nom.Height = 20;
                    nom.FontWeight =FontWeights.UltraBold;
                    nom.Foreground = new SolidColorBrush(Colors.White);
                    att.Children.Add(nom);
                    Canvas.SetZIndex(nom, 1);
                    Canvas.SetTop(nom, pos+6);
                    Canvas.SetRight(nom,12);
                    Ellipse front = new Ellipse();
					front.Width = 30;
					front.Height = 30;
					front.Fill = new SolidColorBrush(Color.FromRgb(fifo[i].clr.R, fifo[i].clr.G, fifo[i].clr.B));
					att.Children.Add(front);
					Canvas.SetTop(front,pos);
                    Canvas.SetRight(front, 10);
                    pos += 31;
					i++;
				}
			});
		}
		public void supp(int id)
		{
			fifo.Remove(fifo.Find(x => x.id == id));
		}
		public void kill_procees(int id)
		{
			processus pro = new processus("",id, 0, 0);
			pro = en_cours.Find(x => x.id == id);
			en_cours.Remove(pro);
			pro.Set_etat(-1);
			fifo.Add(pro);
		}
		public void finish(int id)
		{
            fini.Add(en_cours.Find(x => x.id == id));
            en_cours.Remove(en_cours.Find(x => x.id == id));
            finis++;
    }

		// Default comparer for Procees  type.
		public void supervisor()
		{
		}
		
		public void check_fifo(RAM_var mem,ref string d,int P)
		{
            int ind;
			if (fifo.Count != 0)
			{
                int i = 0;
                string nom;
                while (true)
                {
                    switch (P)
                    {
                       
                        case 0:
                            ind = mem.firts_fit(fifo[i].Get_taille());
                            nom = "First Fit";
                            break;
                        case 1:
                            ind = mem.best_fit(fifo[i].Get_taille());
                            nom = "Best Fit";
                            break;

                        case 2:
                            ind = mem.worst_fit(fifo[i].Get_taille());
                            nom = "Worst Fit";
                            break;
                        case 3:
                            ind = mem.Next_fit(fifo[i].Get_taille());
                            nom = "Next Fit";
                            break;
                        default:
                            ind = mem.firts_fit(fifo[i].Get_taille());
                            nom = "First Fit";
                            break;


                    }
                    if (ind >= 0)
                    {
                        d+= ">>>> le processus " + fifo[i].name + "  est ajouter a l'emplacement <" + ind + "> avec l'algorithme de :  <<"+nom+">>\n\n";
                        corriger_prt(ind, mem.allocation_process(ind, fifo[i].id, fifo[i].Get_taille())); execute_process(fifo[i].id, ind);
                    }
                    else break;
                    i++;
                    if (i >= fifo.Count) break;
                }
		
			}

		}
		public void corriger_prt(int prt,int desc)
		{
			
			
			(en_cours.FindAll(x => x.Get_etat()>prt)).ForEach(x=>x.Set_etat(x.Get_etat()-desc));
			
		}
        public string Get_name_by_id(int id)
        {
           return en_cours.Find(x => x.id == id).name;
        }
        public Color Get_color_by_id(int id)
        {
            return en_cours.Find(x => x.id == id).clr;
        }
        public int Get_time_by_id(int id)
        {
            return en_cours.Find(x => x.id == id).Get_temps();
        }
        public int Get_fulltime_by_id(int id)
        {
            return en_cours.Find(x => x.id == id).full_time;
        }
        public int calcul_fifo()
        {
            if ((fifo.Count + en_cours.Count+finis) != 0)
            {
                return fifo.Count * 100 / (fifo.Count + en_cours.Count+finis);
            }
            else return 0;
        }
        public int calcul_en_cours()
        {
            if ((fifo.Count + en_cours.Count + finis) != 0)
            {
                return en_cours.Count * 100 / (fifo.Count + en_cours.Count + finis);
            }
            else return 0;
        }
        public int calcul_finis()
        {
            if ((fifo.Count + en_cours.Count + finis) != 0)
            {
                return finis * 100 / (fifo.Count + en_cours.Count + finis);
            }
            else return 0;
        }

    }
}
