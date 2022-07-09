using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete {
    public class PaymentManager : IPaymentService {
        public IResult Pay(CreditCard card, int amount) {
            //simulating
            var balance = new Random().Next(500, 5000);
            if(balance < amount) {
                return new ErrorResult(Messages.BalanceInsufficent);
            }
            balance -= amount;
            return new SuccessResult(Messages.OrderConfirmed);
        }
    }
}
