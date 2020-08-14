using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Profile;

namespace ProfileApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ProfileStaffMember profile = new ProfileStaffMember("Потапов.А О", "Отдел-7", "руководитель");
            profile.ChangeDepartment("Отдел-0");
            profile.ChangePosition("инженер");
            profile.ChangeDepartment("Ц0");
            profile.SalaryReceive("kl9", "директор");



        }
    }
}
