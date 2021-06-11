using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music_Review_Application_Services.Interfaces
{
    public interface ICreateService
    {
        string CreateSingle(string title, List<string> artistNames, List<string> genreNames, string dateDay, string dateMonth, string dateYear);

        string CreateSingle(string title, List<string> artistNames, List<string> genreNames, string dateDay, string dateMonth, string dateYear, string imgPath);
    }
}
