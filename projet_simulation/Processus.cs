using System;
using System.Windows.Media;
using System.Collections.Generic;
using System.Text;
using System.Timers;


namespace ConsoleApp
{

	public class processus: IEquatable<processus>, IComparable<processus>
	{
		public int id;
		public string name;
		protected int taille;
		protected int temp_ex;
        public int full_time; //le temps d execution de la processus en seconde 
		protected int etat;// possede trois etat active(1) bloquee(-1) et en attente(0) 
		public Color clr;
       
		public processus(string name,int ID, int taille, int temps)
		{
			this.name = name;
			this.taille = taille;
			this.temp_ex = temps;
            this.full_time = temps;
			this.id = ID;
			Random r = new Random();
			clr = new Color();
			clr.R = (byte)r.Next(45,255);
			clr.G = (byte)r.Next(45,256);
			clr.B =(byte) r.Next(40,256);
		}

		// definez les getter de la classe 
		public int Get_taille()
		{
			return taille;
		}
		public int Get_temps()
		{
			return temp_ex;
		}
		public int Get_etat()
		{
			return etat;
		}
		// definez des setter 
		public void Set_taille(int taille)
		{
			this.taille = taille;
		}
		public void Set_temps(int T)
		{
			temp_ex = T;
		}
		public void Set_etat(int etat)
		{
			this.etat = etat;
		}
		public override bool Equals(Object obj)
		{
			//Check for null and compare run-time types.
			if ((obj == null) || !this.GetType().Equals(obj.GetType()))
			{
				return false;
			}
			else
			{
				processus p = (processus)obj;
				return (id == p.id);
			}
		}
		public int CompareTo(processus comparePart)
		{
			// A null value means that this object is greater.
			if (comparePart == null)
				return 1;

			else
				return this.taille.CompareTo(comparePart.taille);
		}
		public override int GetHashCode()
		{
			return id;
		}
		public bool Equals(processus other)
		{
			if (other == null) return false;
			return (this.id.Equals(other.id));
		}

	}
}
