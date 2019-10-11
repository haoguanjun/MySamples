using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InternalApi
{
    class Program
    {
        static void Main(string[] args)
        {
            ApiProxy proxy = new ApiProxy("https://vmw16apxcloud01.gencos.com");
            proxy.Login("admin", "advs");
            proxy.Get(@"apxlogin/api/internal/UserInfo?$d=y&$m=n&$f=u");
        }
    }
}
