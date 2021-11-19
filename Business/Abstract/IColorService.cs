using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract {
    public interface IColorService {
        List<Color> GetAll();
        Color Get(int colorId);
        void Add(Color car);
        void Update(Color car);
        void Delete(Color car);
    }
}
