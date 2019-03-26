using System;
using System.Collections;
using System.Timers;


namespace ConsoleApp
{
  class Program
	{

		private static System.Timers.Timer aTimer;

		/*public static void Main()
		{
			RAM_var mem = new RAM_var(100);
			process_list pro = new process_list();
			pro.afficher_fifo();
			int ind, i = 0;
			while (i < pro.fifo.Count)
			{
				ind = mem.firts_fit(pro.fifo[i].Get_taille());
				if (ind >= 0) {pro.corriger_prt(ind,mem.allocation_process(ind, pro.fifo[i].id, pro.fifo[i].Get_taille())); pro.execute_process(pro.fifo[i].id, ind);  }
				else {  i++; }
			}
			// Create a timer with a two second interval.
			 aTimer = new System.Timers.Timer(1000);
			// Hook up the Elapsed event for the timer.
			aTimer.Start();
			aTimer.Elapsed += delegate (object sender,ElapsedEventArgs e) { pro.OnTimedEvent(sender,e,mem); };
			aTimer.AutoReset = true;
			aTimer.Enabled= true;
			Console.ReadKey();

			/*if ((pro.fifo.Count != 0)||(pro.en_cours.Count != 0))
			{
				Console.WriteLine("Test");
				aTimer.Enabled = true;
			}
			else
			{
				aTimer.Enabled = false;
				aTimer.Stop();
				aTimer.Dispose();
				Console.WriteLine("Terminating the application...");
			}
			aTimer.AutoReset = true;
			aTimer.Stop();
			aTimer.Dispose();
        
		}*/
		

	}
}
