using System.Net.WebSockets;

namespace V._44Hemuppgift
{
    internal class Program
    {
        static void Main(string[] args)
        {
           
           

            {
                using (var db = new StudentDBContext())
                {
                    db.Database.EnsureCreated();
                }

                MenuHelper.ShowMenu();
            }


        }



    }
    
}
