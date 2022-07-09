using Business.Abstract;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class FindexManager : IFindexService {
        public IDataResult<int> GetCustomerFindexScore(string customerNationalIdentity) {
            //simulating
            var rand = new Random();
            return new SuccessDataResult<int>(rand.Next(400, 1900));
        }
    }
}
